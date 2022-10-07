using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class Tr2Mesh : RedObject
    {
        public Tr2Mesh(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.GeometryResPath = (string)root["geometryResPath"];
            this.DecalAreas = new object[0];
            this.DepthAreas = new object[0];
            this.AdditiveAreas = new object[0];

            YamlSequenceNode opaqueAreas = (YamlSequenceNode)root["opaqueAreas"];
            // load Tr2MeshArea's
            this.OpaqueAreas = new Tr2MeshArea[opaqueAreas.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            int index = 0;

            foreach (YamlMappingNode node in opaqueAreas.Children)
            {
                this.OpaqueAreas[index++] = Red.ParseExpectObject<Tr2MeshArea>(node, source);
            }

            YamlSequenceNode transparentAreas = (YamlSequenceNode)root["transparentAreas"];
            // load Tr2MeshArea's
            this.TransparentAreas = new Tr2MeshArea[transparentAreas.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            index = 0;

            foreach (YamlMappingNode node in transparentAreas.Children)
            {
                this.TransparentAreas[index++] = Red.ParseExpectObject<Tr2MeshArea>(node, source);
            }
        }

        public string GeometryResPath { get; init; }
        public Tr2MeshArea[] OpaqueAreas { get; init; }
        public object[] DecalAreas { get; init; }
        public object[] DepthAreas { get; init; }
        public Tr2MeshArea[] TransparentAreas { get; init; }
        public object[] AdditiveAreas { get; init; }
    }
}
