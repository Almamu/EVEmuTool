using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveSpaceObject : RedObject
    {
        public EveSpaceObject(YamlMappingNode root) : base(root)
        {
        }
    }
}
