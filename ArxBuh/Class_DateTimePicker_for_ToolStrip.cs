using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ArxBuh
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class ToolStripDateTimeChooser : ToolStripControlHost
    {
        public event EventHandler ValueChanged;

        DateTimePicker dtPicker = new DateTimePicker();
        FlowLayoutPanel flPanel;

        public ToolStripDateTimeChooser()
            : base(new FlowLayoutPanel())
        {

            this.flPanel = (FlowLayoutPanel)base.Control;

            flPanel.BackColor = System.Drawing.Color.Transparent;
            dtPicker.Format = DateTimePickerFormat.Short;
            flPanel.Controls.Add(dtPicker);
            dtPicker.Width = 90;

            dtPicker.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
        }

        void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                var handler = ValueChanged;
                if (handler != null)
                    handler(this, e);
            }
        }

        public DateTime Value
        {
            get
            {
                return dtPicker.Value;
            }
            set
            {
                if (value <= DateTime.MinValue)
                {
                    value = DateTime.MinValue;
                }
                else  dtPicker.Value = value;
            }
        }
    }
}
