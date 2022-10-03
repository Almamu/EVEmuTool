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
    public class Tr2Vector4Parameter : Tr2EffectParameter
    {
        public Tr2Vector4Parameter(YamlMappingNode root) : base(root)
        {
            if (root.Children.ContainsKey("value") == false)
                return;

            YamlSequenceNode sequence = (YamlSequenceNode) root["value"];

            this.Value = new Vector4 {
                x = float.Parse((string)sequence[0], CultureInfo.InvariantCulture),
                y = float.Parse((string)sequence[1], CultureInfo.InvariantCulture),
                z = float.Parse((string)sequence[2], CultureInfo.InvariantCulture),
                w = float.Parse((string)sequence[3], CultureInfo.InvariantCulture),
            };
        }

        public Vector4 Value { get; init; } = new Vector4 { x = 1.0f, y = 1.0f, z = 1.0f, w = 1.0f };
    }
}
