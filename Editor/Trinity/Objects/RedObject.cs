using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class RedObject
    {
        public IEmbedFS Source { get; init; }

        public RedObject(YamlMappingNode root, IEmbedFS source)
        {
            this.Source = source;
        }
    }
}
