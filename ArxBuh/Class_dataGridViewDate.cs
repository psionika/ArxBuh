using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ArxBuh
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell.
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            var progressVal = 0;

            if (value != null)
                progressVal = (int)value;


            var percentage = ((float)progressVal / 100.0f);

            using (var backColorBrush = new SolidBrush(cellStyle.BackColor))
            {
                using (var foreColorBrush = new SolidBrush(cellStyle.ForeColor))
                {
                    // Draws the cell grid
                    base.Paint(g, clipBounds, cellBounds,
                     rowIndex, cellState, value, formattedValue, errorText,
                     cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                    if (percentage > 0.0)
                    {
                        // Draw the progress bar and the text
                        using (var solidBrush = new SolidBrush(Color.FromArgb(163, 189, 242)))
                        {
                            // Draw the progress bar and the text
                            g.FillRectangle(solidBrush, cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                        }
                        g.DrawString(progressVal.ToString() + " %", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                    }
                    else
                    {
                        // draw the text
                        if (DataGridView.CurrentRow.Index == rowIndex)
                            g.DrawString(progressVal.ToString() + " %", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
                        else
                            g.DrawString(progressVal.ToString() + " %", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                    }
                }
            }
        }
    }
}

