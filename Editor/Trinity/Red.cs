using EVEmuTool.EmbedFS;
using EVEmuTool.Trinity.Objects;
using EVEmuTool.Trinity.Objects.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EVEmuTool.Trinity
{
    public class Red
    {
        public static RedObject Parse(string content, IEmbedFS container)
        {
            YamlStream yaml = new YamlStream();
            yaml.Load(new StringReader(content));

            YamlDocument document = yaml.Documents.First();
            YamlNode root = document.RootNode;

            return ParseObject((YamlMappingNode) root, container);
        }
        public static RedObject ParseObject(YamlMappingNode node, IEmbedFS container)
        {
            string type = (string) node ["type"];

            return type switch
            {
                "EveShip2" => new EveShip2(node, container),
                "EveStation2" => new EveStation2(node, container),
                "BlueObjectProxy" => ParseObject((YamlMappingNode) node["object"], container),
                "Tr2Mesh" => new Tr2Mesh(node, container),
                "Tr2MeshArea" => new Tr2MeshArea(node, container),
                "Tr2Effect" => new Tr2Effect(node, container),
                "Tr2FloatParameter" => new Tr2FloatParameter (node, container),
                "TriFloatParameter" => new TriFloatParameter(node, container),
                "Tr2Vector4Parameter" => new Tr2Vector4Parameter(node, container),
                "TriVector4Parameter" => new TriVector4Parameter(node, container),
                "TriVariableParameter" => new TriVariableParameter(node, container),
                "TriTransformParameter" => new TriTransformParameter(node, container),
                "TriVector" => new TriVector(node, container),
                "TriColor" => new TriColor(node, container),
                "EveSpriteSet" => new EveSpriteSet(node, container),
                "EveSpriteSetItem" => new EveSpriteSetItem(node, container),
                "EveTransform" => new EveTransform(node, container),
                _ => throw new InvalidDataException($"Unknown red object type {type}")
            };
        }

        public static T ParseExpectObject<T> (YamlMappingNode node, IEmbedFS container) where T : RedObject
        {
            RedObject redObject = ParseObject(node, container);

            return redObject as T;
        }
    }
}
