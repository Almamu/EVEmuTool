using Editor.LogServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    class LogLineEntry
    {
        public Bitmap Image { get; init; }
        public string Message { get; init; }
        public string Source { get; init; }
        private LogLine Original { get; init; }

        public LogLineEntry(LogLine original, Device device)
        {
            this.Original = original;

            Bitmap bitmap = this.Original.LogLevel switch
            {
                (int)LogLevel.Info => Editor.Properties.Resources.Info,
                (int)LogLevel.Error => Editor.Properties.Resources.Error,
                (int)LogLevel.Counter => Editor.Properties.Resources.Counter,
                (int)LogLevel.Fatal => Editor.Properties.Resources.Fatal,
                (int)LogLevel.MethodCall => Editor.Properties.Resources.Mcall,
                (int)LogLevel.Overlap => Editor.Properties.Resources.Overlap,
                (int)LogLevel.Performance => Editor.Properties.Resources.Performance,
                (int)LogLevel.Warning => Editor.Properties.Resources.Warning,
                _ => null
            };

            this.Image = bitmap;
            this.Message = this.Original.Line.Split("\n")[0];
            this.Source = $"{device.Channels[this.Original.SourceID].Facility}::{device.Channels[this.Original.SourceID].Object}";
        }

        public LogLine GetOriginal()
        {
            return this.Original;
        }
    }
}
