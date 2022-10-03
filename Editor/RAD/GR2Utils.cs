using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LSLib.Granny.GR2;
using LSLib.Granny.Model;

namespace LSLib.Granny
{
    public class GR2Utils
    {
        public delegate void ConversionErrorDelegate(string inputPath, string outputPath, Exception exc);

        public delegate void ProgressUpdateDelegate(string status, long numerator, long denominator);

        public ConversionErrorDelegate ConversionError = delegate { };
        public ProgressUpdateDelegate ProgressUpdate = delegate { };

        public static Root LoadModel(string inputPath)
        {
            using (var fs = File.Open(inputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return LoadModel(fs);
            }
        }

        public static Root LoadModel(Stream input)
        {
            var root = new Root();
            var gr2 = new GR2Reader(input);
            gr2.Read(root);
            root.PostLoad(gr2.Tag);
            return root;
        }
    }
}
