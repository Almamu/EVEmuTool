using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects.Parameters
{
    public class TriVector4Parameter : Tr2EffectParameter
    {
        public TriVector4Parameter(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.Value = new Vector4 {
                x = float.Parse((string)(root.Children.ContainsKey("v1") ? root["v1"] : "0"), CultureInfo.InvariantCulture),
                y = float.Parse((string)(root.Children.ContainsKey("v2") ? root["v2"] : "0"), CultureInfo.InvariantCulture),
                z = float.Parse((string)(root.Children.ContainsKey("v3") ? root["v3"] : "0"), CultureInfo.InvariantCulture),
                w = float.Parse((string)(root.Children.ContainsKey("v4") ? root["v4"] : "0"), CultureInfo.InvariantCulture),
            };
        }

        public Vector4 Value { get; init; } = new Vector4 { x = 1.0f, y = 1.0f, z = 1.0f, w = 1.0f };
    }
}
