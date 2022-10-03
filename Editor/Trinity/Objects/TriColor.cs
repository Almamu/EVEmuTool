using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class TriColor : RedObject
    {
        public TriColor(YamlMappingNode root) : base(root)
        {
            if (root.Children.ContainsKey("r") == true)
                this.R = float.Parse((string)root.Children["r"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("g") == true)
                this.G = float.Parse((string)root.Children["g"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("b") == true)
                this.B = float.Parse((string)root.Children["b"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("a") == true)
                this.A = float.Parse((string)root.Children["a"], CultureInfo.InvariantCulture);
        }

        public float R { get; init; } = 1.0f;
        public float G { get; init; } = 1.0f;
        public float B { get; init; } = 1.0f;
        public float A { get; init; } = 1.0f;
    }
}
