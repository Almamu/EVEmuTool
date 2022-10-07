using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveSpriteSetItem : RedObject
    {
        public EveSpriteSetItem(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.Position = Red.ParseExpectObject<TriVector>((YamlMappingNode)root["position"], source);

            if (root.Children.ContainsKey("blinkRate") == true)
                this.BlinkRate = float.Parse((string)root["blinkRate"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("minScale") == true)
                this.MinScale = float.Parse((string)root["minScale"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("maxScale") == true)
                this.MaxScale = float.Parse((string)root["maxScale"], CultureInfo.InvariantCulture);
            if (root.Children.ContainsKey("falloff") == true)
                this.Falloff = float.Parse((string)root["falloff"], CultureInfo.InvariantCulture);

            this.Color = Red.ParseExpectObject<TriColor>((YamlMappingNode)root["color"], source);
        }

        public TriVector Position { get; init; }
        public float BlinkRate { get; init; }
        public float MinScale { get; init; }
        public float MaxScale { get; init; }
        public float Falloff { get; init; }
        public TriColor Color { get; init; }

        /*-   type: EveSpriteSet
    sprites:
    -   type: EveSpriteSetItem
        position:
            type: TriVector
            y: 12.56795
            z: 21.78672
        blinkRate: 0.3111644
        minScale: 1.5
        maxScale: 4.5
        falloff: 0
        color:
            type: TriColor
            r: 0.7755069
            g: 0.3612274
            b: 0.1975127
            a: 1
*/
    }
}
