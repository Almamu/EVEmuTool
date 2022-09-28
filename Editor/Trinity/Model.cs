using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Trinity
{
    /// <summary>
    /// Represents a normal 3-dimensional vector
    /// </summary>
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    /// <summary>
    /// Represents a normal 2-dimensional vector
    /// </summary>
    public struct Vector2
    {
        public float x;
        public float y;
    }

    /// <summary>
    /// Represents a 3-dimensional bounding box
    /// </summary>
    public struct Box
    {
        public Vector3 min;
        public Vector3 max;
    }

    internal static class TrinityReaderExtensions
    {
        public static Vector3 ReadVector3 (this BinaryReader reader)
        {
            return new Vector3
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle(),
            };
        }

        public static Vector2 ReadVector2 (this BinaryReader reader)
        {
            return new Vector2
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle()
            };
        }

        public static Box ReadBox (this BinaryReader reader)
        {
            return new Box
            {
                min = reader.ReadVector3(),
                max = reader.ReadVector3(),
            };
        }
    }

    /// <summary>
    /// Simple class that represents the index of vertex for the three points of a triangle
    /// </summary>
    public class Triangle
    {
        public short vertex1;
        public short vertex2;
        public short vertex3;
    }

    /// <summary>
    /// Represents a surface
    /// </summary>
    public class Surface
    {
        public int type;
        public int unknown1;
        public int unknown2;
        public int unknown3;
        public int startIndex;
        public int numTriangles;
        public Triangle[] triangles;
    }
    /// <summary>
    /// Represents a simple vertex
    /// </summary>
    public class Vertex
    {
        public Vector3 position;
        public Vector3 normal;
        public Vector2 uv;

        public virtual void Read(BinaryReader reader)
        {
            this.position = reader.ReadVector3();
            this.normal = reader.ReadVector3();
            this.uv = reader.ReadVector2();
        }
    }

    /// <summary>
    /// Represents an extended vertex
    /// </summary>
    public class VertexExtended : Vertex
    {
        public Vector3 tangent;
        public Vector3 binormal;

        public override void Read(BinaryReader reader)
        {
            // base should not be taken into account as the order is different
            this.position = reader.ReadVector3();
            this.normal = reader.ReadVector3();
            this.tangent = reader.ReadVector3();
            this.binormal = reader.ReadVector3();
            this.uv = reader.ReadVector2();
        }
    }

    /// <summary>
    /// Catch-all for some of the variable-length vertex
    /// </summary>
    public class VariableVertex : Vertex
    {
        public int Size { get; init; }
        public override void Read(BinaryReader reader)
        {
            // read the base vertex, it might not be the right representation
            // but should be enough for now
            base.Read(reader);
            // skip bytes
            reader.ReadBytes((this.Size - 32) / 4);
        }
    }

    /// <summary>
    /// Class for working on .tri files
    /// </summary>
    public class Model
    {

        /// <summary>
        /// .tri file header
        /// </summary>
        internal struct Header
        {
            public short version;
            public int unknown1;
            public int unknown2;
            public int unknown3;
            /// <summary>
            /// Byte size of each vertex
            /// </summary>
            public int vertexSize;
            public int vertexCount;
            public int surfacesCount;
            public int trianglesCount;
            public int unknwon4;
            public int unknwon5;
            public int unknwon6;
            public int unknwon7;
            public Box boundingBox;
            public int unknown8;
            public int unknown9;
            public int sizeHeader;
            public int unknown10;
        }

        private static Header ReadHeader (BinaryReader reader)
        {
            Header header = new Header
            {
                version = reader.ReadInt16(),
                unknown1 = reader.ReadInt32(),
                unknown2 = reader.ReadInt32(),
                unknown3 = reader.ReadInt32(),
                vertexSize = reader.ReadInt32(),
                vertexCount = reader.ReadInt32(),
                surfacesCount = reader.ReadInt32(),
                trianglesCount = reader.ReadInt32(),
                unknwon4 = reader.ReadInt32(),
                unknwon5 = reader.ReadInt32(),
                unknwon6 = reader.ReadInt32(),
                unknwon7 = reader.ReadInt32(),
                boundingBox = reader.ReadBox(),
                unknown8 = reader.ReadInt32(),
                unknown9 = reader.ReadInt32(),
                sizeHeader = reader.ReadInt32(),
                unknown10 = reader.ReadInt32()
            };

            if (header.version != 0x0401)
                throw new InvalidDataException($"Expected version 0x0104, but got {header.version.ToString("X4")}");

            return header;
        }

        private static Surface ReadSurface (BinaryReader reader)
        {
            Surface surface = new Surface
            {
                type = reader.ReadInt32 (),
                unknown1 = reader.ReadInt32 (),
                unknown2 = reader.ReadInt32 (),
                unknown3 = reader.ReadInt32 (),
                startIndex = reader.ReadInt32 (),
                numTriangles = reader.ReadInt32 (),
            };

            surface.triangles = new Triangle[surface.numTriangles];

            return surface;
        }

        private static Vertex ReadVertex (BinaryReader reader, Header header)
        {
            Vertex result = header.vertexSize switch
            {
                32 => new Vertex(),
                56 => new VertexExtended(),
                40 or 48 or 64 or 72 => new VariableVertex() { Size = header.vertexSize },
                _ => throw new InvalidDataException ($"Unexpected vertex bit size {header.vertexSize}")
            };

            result.Read(reader);

            return result;
        }

        private readonly Header mHeader;
        public Surface[] Surfaces { get; private set; }
        public Vertex[] Vertices { get; private set; }
        public Box BoundingBox => this.mHeader.boundingBox;

        public Model (Stream input)
        {
            BinaryReader reader = new BinaryReader (input);

            this.mHeader = ReadHeader(reader);

            this.Vertices = this.mHeader.vertexSize switch
            {
                32 => new Vertex[this.mHeader.vertexCount],
                56 => new VertexExtended[this.mHeader.vertexCount],
                40 or 48 or 64 or 72 => new VariableVertex[this.mHeader.vertexCount],
                _ => throw new InvalidDataException ($"Unexpected vertex bit size {this.mHeader.vertexSize}")
            };
            this.Surfaces = new Surface[this.mHeader.surfacesCount];

            // read all vertex
            for (int i = 0; i < this.mHeader.vertexCount; i ++)
            {
                this.Vertices[i] = ReadVertex(reader, this.mHeader);
            }

            // read all surfaces
            for (int i = 0; i < this.mHeader.surfacesCount; i ++)
            {
                this.Surfaces[i] = ReadSurface(reader);
            }

            // read all the vertices for the surfaces
            foreach (Surface surface in this.Surfaces)
            {
                short[] triangles;

                if (surface.type == 5)
                {
                    // the two extra are for the end of the last triangle
                    // part of the vertex of one triangle compose the next one
                    // seems to be some way of tightly packing the data
                    triangles = new short[surface.numTriangles + 2];
                }
                else
                {
                    triangles = new short[3 * surface.numTriangles];
                }

                // load all the triangles into the short array
                for (int i = 0; i < triangles.Length; i++)
                    triangles[i] = reader.ReadInt16();

                if (surface.type == 5)
                {
                    int count = 0;

                    for (int i = 0; i < surface.numTriangles; i ++)
                    {
                        if (triangles [i] != triangles [i + 1] && triangles [i] != triangles [i + 2] && triangles [i + 1] != triangles [i + 2])
                        {
                            if (i % 2 == 0)
                            {
                                surface.triangles[count] = new Triangle
                                {
                                    vertex1 = triangles[i],
                                    vertex2 = triangles[i + 1],
                                    vertex3 = triangles[i + 2]
                                };
                            }
                            else
                            {
                                surface.triangles[count] = new Triangle
                                {
                                    vertex1 = triangles[i + 2],
                                    vertex2 = triangles[i + 1],
                                    vertex3 = triangles[i]
                                };
                            }

                            count++;
                        }
                    }

                    // the count for the surface is not the real count
                    // ensure it's right
                    surface.numTriangles = count;
                    // resize the triangles array
                    Array.Resize(ref surface.triangles, surface.numTriangles);
                }
                else
                {
                    // copy triangle data over to the surface
                    for (int i = 0; i < surface.numTriangles; i++)
                    {
                        surface.triangles[i] = new Triangle
                        {
                            vertex1 = triangles[i * 3],
                            vertex2 = triangles[i * 3 + 1],
                            vertex3 = triangles[i * 3 + 2]
                        };
                    }
                }
            }
        }
    }
}
