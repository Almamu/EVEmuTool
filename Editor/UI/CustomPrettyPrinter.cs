using Editor.CustomMarshal.CustomTypes;
using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.UI
{
    public class CustomPrettyPrinter : PrettyPrinter
    {
        private CustomPrettyPrinter() : base()
        {
        }

        /// <summary>
        /// Utility method, creates a new pretty printer instance and dumps the given <paramref name="obj" />
        /// </summary>
        /// <param name="obj">The Python type to dump</param>
        /// <returns></returns>
        public static string FromDataType(PyDataType obj)
        {
            CustomPrettyPrinter printer = new CustomPrettyPrinter();

            printer.Process(obj);

            return printer.GetResult();
        }

        protected override void ProcessPythonType(PyDataType obj)
        {
            if (obj is PyInsightSubStream)
            {
                this.ProcessInsightSubStream(obj as PyInsightSubStream);
                return;
            }

            base.ProcessPythonType(obj);
        }

        protected void ProcessInsightSubStream(PyInsightSubStream stream)
        {
            this.mStringBuilder.AppendFormat("[PySubStream]");
            this.mStringBuilder.AppendLine();
            this.mIndentation++;

            this.Process(stream.Stream);

            this.mIndentation--;
        }
    }
}
