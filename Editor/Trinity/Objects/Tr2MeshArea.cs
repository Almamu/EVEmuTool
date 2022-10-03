using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class Tr2MeshArea : RedObject
    {
        public Tr2MeshArea(YamlMappingNode root) : base(root)
        {
            this.Name = (string)root["name"];
            this.Effect = Red.ParseExpectObject<Tr2Effect>((YamlMappingNode) root["effect"]);
        }

        public string Name { get; init; }
        public Tr2Effect Effect { get; init; }
    }
}
