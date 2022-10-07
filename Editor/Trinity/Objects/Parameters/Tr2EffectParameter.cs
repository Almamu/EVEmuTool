using EVEmuTool.EmbedFS;
using EVEmuTool.LogServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects.Parameters
{
    public abstract class Tr2EffectParameter : RedObject
    {
        public Tr2EffectParameter(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.Name = (string)root["name"];
        }

        public string Name { get; init; }
    }
}
