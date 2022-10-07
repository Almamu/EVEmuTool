using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects.Parameters
{
    public class TriTransformParameter : Tr2EffectParameter
    {
        public TriTransformParameter(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
        }
    }
}
