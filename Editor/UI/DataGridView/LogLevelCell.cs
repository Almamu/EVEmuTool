using Editor.LogServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.UI.DataGridView
{
    public class LogLevelCell : DataGridViewImageCell
    {
        public override Type FormattedValueType { get => typeof(int); }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            int? logLevel = value as int?;

            // decide what image to draw based on the data
            Bitmap bitmap = logLevel switch
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

            // Call the base class method to paint the default cell appearance.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                bitmap, bitmap, errorText, cellStyle,
                advancedBorderStyle, paintParts);
        }
    }
}
