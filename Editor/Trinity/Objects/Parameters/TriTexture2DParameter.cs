using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects.Parameters
{
    public class TriTexture2DParameter : Tr2EffectParameter
    {
        public TriTexture2DParameter(YamlMappingNode root) : base(root)
        {
            this.ResourcePath = (string)root["resourcePath"];
        }

        public string ResourcePath { get; init; }
    }
}
