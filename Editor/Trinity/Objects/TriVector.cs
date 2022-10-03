using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class TriVector : RedObject
    {
        public TriVector(YamlMappingNode root) : base(root)
        {
            if (root.Children.ContainsKey("x") == true)
                this.X = float.Parse((string)root.Children["x"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("y") == true)
                this.Y = float.Parse((string)root.Children["y"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("z") == true)
                this.Z = float.Parse((string)root.Children["z"], CultureInfo.InvariantCulture);
        }

        public float X { get; init; } = 1.0f;
        public float Y { get; init; } = 1.0f;
        public float Z { get; init; } = 1.0f;
    }
}
