using EVESharp.PythonTypes.Types.Collections;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.UI
{
    internal class NativeToPython
    {
        public static PyDataType Convert(object data)
        {
            return data switch
            {
                null => null,
                int => new PyInteger((int)data),
                long => new PyInteger((long)data),
                short => new PyInteger((short)data),
                byte => new PyInteger((byte)data),
                ushort => new PyInteger((ushort)data),
                uint => new PyInteger((uint)data),
                ulong => new PyInteger((long)data),
                sbyte => new PyInteger((sbyte)data),
                float => new PyDecimal((float)data),
                double => new PyDecimal((double)data),
                decimal => new PyDecimal((double)(decimal)data),
                byte[] => new PyBuffer((byte[])data),
                object[] => ConvertTuple((object[])data),
                string => new PyString((string)data),
                DateTime => new PyInteger(((DateTime)data).ToFileTimeUtc()),
                TimeSpan => new PyInteger(((TimeSpan)data).Ticks),
                ArrayList => ConvertArrayList((ArrayList)data),
                Hashtable => ConvertHashtable((Hashtable)data),
                IDictionary<string, object> => ConvertObject((IDictionary<string, object>)data),
                _ => throw new InvalidDataException ($"Unsupported data type {data.GetType()}")
            };
        }

        private static PyTuple ConvertTuple(object[] data)
        {
            PyTuple result = new PyTuple(data.Length);
            int i = 0;

            foreach (object item in data)
            {
                result[i++] = Convert(item);
            }

            return result;
        }

        private static PyDataType ConvertArrayList(ArrayList data)
        {
            PyList result = new PyList(data.Count);
            int i = 0;

            foreach (object item in data)
            {
                result[i++] = Convert(item);
            }

            return result;
        }

        private static PyDictionary ConvertHashtable(Hashtable data)
        {
            PyDictionary result = new PyDictionary();

            foreach (object key in data.Keys)
            {
                result[Convert(key)] = Convert(data[key]);
            }

            return result;
        }

        private static PyObjectData ConvertObject(IDictionary<string, object> data)
        {
            PyDictionary members = new PyDictionary();

            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Key == "class")
                    continue;

                members[item.Key] = Convert(item.Value);
            }

            return new PyObjectData((string)data["class"], members);
        }
    }
}
