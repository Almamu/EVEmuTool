using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveStation2 : EveEntity
    {
        public EveStation2(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
        }
    }
}
