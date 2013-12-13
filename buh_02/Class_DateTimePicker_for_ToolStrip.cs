﻿using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace buh_02
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class ToolStripDateTimeChooser : ToolStripControlHost
    {
        public event EventHandler ValueChanged;

        private DateTimePicker dtPicker = new DateTimePicker();
        private FlowLayoutPanel flPanel;

        public ToolStripDateTimeChooser()
            : base(new FlowLayoutPanel())
        {

            this.flPanel = (FlowLayoutPanel)base.Control;

            flPanel.BackColor = System.Drawing.Color.Transparent;
            dtPicker.Format = DateTimePickerFormat.Short;
            flPanel.Controls.Add(dtPicker);
            dtPicker.Width = 120;

            dtPicker.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
        }

        void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
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
                dtPicker.Value = value;
            }
        }
    }
}
