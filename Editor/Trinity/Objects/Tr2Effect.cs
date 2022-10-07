using EVEmuTool.EmbedFS;
using EVEmuTool.Trinity.Objects.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class Tr2Effect : RedObject
    {
        public Tr2Effect(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            // name is optional for Tr2Effect
            if (root.Children.ContainsKey("name") == true)
                this.Name = (string)root["name"];

            this.EffectFilePath = (string)root["effectFilePath"];

            // go through the list of parameters
            YamlSequenceNode parameters = (YamlSequenceNode)root["parameters"];
            // load Tr2MeshArea's
            this.Parameters = new Tr2EffectParameter[parameters.Children.Count];

            // TODO: DETERMINE IT BASED OFF THE INDEX VALUE IN THE LIST?
            int index = 0;

            foreach (YamlMappingNode node in parameters.Children)
            {
                this.Parameters[index++] = Red.ParseExpectObject<Tr2EffectParameter>(node, source);
            }
        }

        public string Name { get; init; }
        public string EffectFilePath { get; init; }
        public Tr2EffectParameter[] Parameters { get; init; }
        public Tr2EffectParameter[] Resources { get; init; }
    }
}
