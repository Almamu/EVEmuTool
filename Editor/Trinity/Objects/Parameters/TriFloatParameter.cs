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
    public class TriFloatParameter : Tr2EffectParameter
    {
        public TriFloatParameter(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            if (root.Children.ContainsKey ("value") == true)
                this.Value = float.Parse((string)root["value"], CultureInfo.InvariantCulture);
        }

        public float Value { get; init; } = 1.0f;
    }
}
