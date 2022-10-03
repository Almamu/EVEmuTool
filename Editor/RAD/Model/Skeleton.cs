using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using LSLib.Granny.GR2;

namespace LSLib.Granny.Model
{
    public class DivinityBoneExtendedData
    {
        public String UserDefinedProperties;
        public Int32 IsRigid;
    }

    public class Bone
    {
        public string Name;
        public int ParentIndex;
        public Transform Transform;
        [Serialization(ArraySize = 16)]
        public float[] InverseWorldTransform;
        public float LODError;
        [Serialization(Type = MemberType.VariantReference)]
        public DivinityBoneExtendedData ExtendedData;

        [Serialization(Kind = SerializationKind.None)]
        public string TransformSID;
        [Serialization(Kind = SerializationKind.None)]
        public Matrix4 OriginalTransform;
        [Serialization(Kind = SerializationKind.None)]
        public Matrix4 WorldTransform;

        public bool IsRoot { get { return ParentIndex == -1; } }
        
        public void UpdateWorldTransforms(List<Bone> bones)
        {
            var localTransform = Transform.ToMatrix4Composite();
            if (IsRoot)
            {
                WorldTransform = localTransform;
            }
            else
            {
                var parentBone = bones[ParentIndex];
                WorldTransform = localTransform * parentBone.WorldTransform;
            }

            var iwt = WorldTransform.Inverted();
            InverseWorldTransform = new float[] {
                iwt[0, 0], iwt[0, 1], iwt[0, 2], iwt[0, 3],
                iwt[1, 0], iwt[1, 1], iwt[1, 2], iwt[1, 3],
                iwt[2, 0], iwt[2, 1], iwt[2, 2], iwt[2, 3],
                iwt[3, 0], iwt[3, 1], iwt[3, 2], iwt[3, 3]
            };
        }
    }

    public class Skeleton
    {
        public string Name;
        public List<Bone> Bones;
        public int LODType;
        [Serialization(Type = MemberType.VariantReference, MinVersion = 0x80000027)]
        public object ExtendedData;

        [Serialization(Kind = SerializationKind.None)]
        public Dictionary<string, Bone> BonesBySID;

        [Serialization(Kind = SerializationKind.None)]
        public Dictionary<string, Bone> BonesByID;

        [Serialization(Kind = SerializationKind.None)]
        public bool IsDummy = false;

        public Bone GetBoneByName(string name)
        {
            return Bones.FirstOrDefault(b => b.Name == name);
        }

        public void TransformRoots(Matrix4 transform)
        {
            foreach (var bone in Bones)
            {
                if (bone.IsRoot)
                {
                    var boneTransform = bone.Transform.ToMatrix4() * transform;
                    bone.Transform = GR2.Transform.FromMatrix4(boneTransform);
                }
            }

            UpdateWorldTransforms();
        }

        public void Flip()
        {
            foreach (var bone in Bones)
            {
                if (bone.IsRoot)
                {
                    bone.Transform.Flags |= (uint)Transform.TransformFlags.HasScaleShear;
                    bone.Transform.ScaleShear = new Matrix3(
                        -1.0f, 0.0f, 0.0f,
                        0.0f, 1.0f, 0.0f,
                        0.0f, 0.0f, 1.0f
                    );
                }
            }

            UpdateWorldTransforms();
        }

        public void UpdateWorldTransforms()
        {
            foreach (var bone in Bones)
            {
                bone.UpdateWorldTransforms(Bones);
            }
        }
    }
}
