using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.EmbedFS
{
    public class StuffEntry
    {
        public EmbedFSFile Origin { get; init; }
        public string FileName { get; init; }
        public long Offset { get; set; } = 0;
        public int Length { get; init; }
    };
}
