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
        public static void Parse(string content)
        {
            YamlStream yaml = new YamlStream();
            yaml.Load(new StringReader(content));

            YamlDocument document = yaml.Documents.First();
            YamlNode root = document.RootNode;

            RedObject redObject = ParseObject((YamlMappingNode) root);
        }
        public static RedObject ParseObject(YamlMappingNode node)
        {
            string type = (string) node ["type"];

            return type switch
            {
                "EveShip2" => new EveShip2(node),
                "EveStation2" => new EveStation2(node),
                "BlueObjectProxy" => ParseObject((YamlMappingNode) node["object"]),
                "Tr2Mesh" => new Tr2Mesh(node),
                "Tr2MeshArea" => new Tr2MeshArea(node),
                "Tr2Effect" => new Tr2Effect(node),
                "Tr2FloatParameter" => new Tr2FloatParameter (node),
                "Tr2Vector4Parameter" => new Tr2Vector4Parameter(node),
                "TriVariableParameter" => new TriVariableParameter(node),
                "TriTransformParameter" => new TriTransformParameter(node),
                "TriVector" => new TriVector(node),
                "TriColor" => new TriColor(node),
                "EveSpriteSet" => new EveSpriteSet(node),
                "EveSpriteSetItem" => new EveSpriteSetItem(node),
                "EveTransform" => new EveTransform(node),
                _ => throw new InvalidDataException($"Unknown red object type {type}")
            };
        }

        public static T ParseExpectObject<T> (YamlMappingNode node) where T : RedObject
        {
            RedObject redObject = ParseObject(node);

            return redObject as T;
        }
    }
}
