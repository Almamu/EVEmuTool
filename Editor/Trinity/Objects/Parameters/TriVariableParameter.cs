using EVEmuTool.EmbedFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EVEmuTool.Trinity.Objects.Parameters
{
    public class TriVariableParameter : Tr2EffectParameter
    {
        public TriVariableParameter(YamlMappingNode root, IEmbedFS source) : base(root, source)
        {
            this.VariableName = (string)root["variableName"];
        }

        public string VariableName { get; init; }
    }
}
