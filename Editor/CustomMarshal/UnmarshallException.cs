using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.CustomMarshal
{
    class UnmarshallException : Exception
    {
        public PyDataType CurrentObject { get; init; }
        public InsightUnmarshal Unmarshal { get; init; }

        public UnmarshallException(string message, PyDataType currentObject, InsightUnmarshal unmarshal) : base(message)
        {
            this.CurrentObject = currentObject;
            this.Unmarshal = unmarshal;
        }
    }
}
