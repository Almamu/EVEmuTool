using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveEntity : EveSpaceObject
    {
        public EveEntity(YamlMappingNode root) : base(root)
        {
            if (root.Children.ContainsKey("highDetailMesh") == true)
                this.HighDetailMesh = Red.ParseExpectObject<Tr2Mesh>((YamlMappingNode)root["highDetailMesh"]);
            if (root.Children.ContainsKey("mediumDetailMesh") == true)
                this.MediumDetailMesh = Red.ParseExpectObject<Tr2Mesh>((YamlMappingNode)root["mediumDetailMesh"]);
            if (root.Children.ContainsKey("lowDetailMesh") == true)
                this.LowDetailMesh = Red.ParseExpectObject<Tr2Mesh>((YamlMappingNode)root["lowDetailMesh"]);

            this.ShadowEffect = Red.ParseExpectObject<Tr2Effect>((YamlMappingNode)root["shadowEffect"]);
            YamlSequenceNode spriteSets = (YamlSequenceNode)root["spriteSets"];
            // load Tr2MeshArea's
            this.SpriteSets = new EveSpriteSet[spriteSets.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            int index = 0;

            foreach (YamlMappingNode node in spriteSets.Children)
            {
                this.SpriteSets[index++] = Red.ParseExpectObject<EveSpriteSet>(node);
            }

            YamlSequenceNode damageLocators = (YamlSequenceNode)root["damageLocators"];
            // load Tr2MeshArea's
            this.DamageLocators = new TriVector[damageLocators.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            index = 0;

            foreach (YamlMappingNode node in damageLocators.Children)
            {
                this.DamageLocators[index++] = Red.ParseExpectObject<TriVector>(node);
            }

            YamlSequenceNode children = (YamlSequenceNode)root["children"];
            this.Children = new EveSpaceObject[children.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            index = 0;

            foreach (YamlMappingNode node in children.Children)
            {
                this.Children[index++] = Red.ParseExpectObject<EveSpaceObject>(node);
            }
        }
        public Tr2Mesh HighDetailMesh { get; init; }
        public Tr2Mesh MediumDetailMesh { get; init; }
        public Tr2Mesh LowDetailMesh { get; init; }
        public Tr2Effect ShadowEffect { get; init; }
        public TriVector[] DamageLocators { get; init; }
        public EveSpriteSet[] SpriteSets { get; init; }
        public object[] Locators { get; init; }
        public object[] Observers { get; init; }
        public object[] ParticleSystems { get; init; }
        public object[] ParticleEmitters { get; init; }
        public Vector3 BoundingSphereCenter { get; init; }
        public float BoundingSphereRadius { get; init; }
        public EveSpaceObject[] Children { get; init; }
        public object[] CurveSets { get; init; }
    }
}
