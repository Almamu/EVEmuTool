using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects
{
    public class EveSpriteSet : RedObject
    {
        public EveSpriteSet(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.Effect = Red.ParseExpectObject<Tr2Effect>((YamlMappingNode) root["effect"], source);
            // go through the list of sprites
            YamlSequenceNode sprites = (YamlSequenceNode)root["sprites"];
            // load Tr2MeshArea's
            this.Sprites = new EveSpriteSetItem[sprites.Children.Count];

            int index = 0;

            foreach (YamlMappingNode node in sprites.Children)
            {
                this.Sprites[index++] = Red.ParseExpectObject<EveSpriteSetItem>(node, source);
            }
        }

        public EveSpriteSetItem[] Sprites { get; init; }
        public Tr2Effect Effect { get; init; }
    }
}
