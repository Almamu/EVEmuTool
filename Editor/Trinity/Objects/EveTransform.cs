using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveTransform : EveSpaceObject
    {
        public EveTransform(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.Name = (string)root["name"];

            if (root.Children.ContainsKey("mesh") == true)
                this.Mesh = Red.ParseExpectObject<Tr2Mesh>((YamlMappingNode)root["mesh"], source);

            YamlSequenceNode scaling = (YamlSequenceNode)root["scaling"];
            YamlSequenceNode rotation = (YamlSequenceNode)root["rotation"];
            YamlSequenceNode translation = (YamlSequenceNode)root["translation"];

            this.Scaling = new Vector3
            {
                x = float.Parse((string)scaling[0], CultureInfo.InvariantCulture),
                y = float.Parse((string)scaling[1], CultureInfo.InvariantCulture),
                z = float.Parse((string)scaling[2], CultureInfo.InvariantCulture),
            };

            this.Rotation = new Vector4
            {
                x = float.Parse((string)rotation[0], CultureInfo.InvariantCulture),
                y = float.Parse((string)rotation[1], CultureInfo.InvariantCulture),
                z = float.Parse((string)rotation[2], CultureInfo.InvariantCulture),
                w = float.Parse((string)rotation[3], CultureInfo.InvariantCulture),
            };

            this.Translation = new Vector3
            {
                x = float.Parse((string)translation[0], CultureInfo.InvariantCulture),
                y = float.Parse((string)translation[1], CultureInfo.InvariantCulture),
                z = float.Parse((string)translation[2], CultureInfo.InvariantCulture),
            };
        }

        public string Name { get; init; }
        public Tr2Mesh Mesh { get; init; }
        public Vector3 Scaling { get; init; }
        public Vector4 Rotation { get; init; }
        public Vector3 Translation { get; init; }
        public object[] Observers { get; init; }
        public object[] ParticleSystems { get; init; }
        public object[] ParticleEmitters { get; init; }
        public object[] Children { get; init; }
        public object[] CurveSets { get; init; }
    }
}
