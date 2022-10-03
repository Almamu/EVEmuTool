using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveShip2 : EveEntity
    {
        public EveShip2(YamlMappingNode root) : base(root)
        {
            this.Name = (string) root["name"];
            this.Turrets = new object[0];
        }

        public string Name { get; init; }
        public object[] Turrets { get; init; }
    }
}
