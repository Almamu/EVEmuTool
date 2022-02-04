using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.UI.DataGridView
{
    class LogTextCell : DataGridViewTextBoxCell
    {
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
            string toDraw = formattedValue as string;
            // remove everything after a linefeed from it and send to the log cell texts
            toDraw = toDraw.Split('\n')[0];
            // Call the base class method to paint the default cell appearance.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                toDraw, toDraw, errorText, cellStyle,
                advancedBorderStyle, paintParts);
        }
    }
}
