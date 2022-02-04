using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.CustomMarshal
{
    class UnmarshallException : Exception
    {
        public PyDataType CurrentObject { get; init; }

        public UnmarshallException(string message, PyDataType currentObject) : base(message)
        {
            this.CurrentObject = currentObject;
        }
    }
}
