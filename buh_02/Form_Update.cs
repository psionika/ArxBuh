using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace buh_02
{
    public partial class Form_Update : Form
    {
        public Form_Update()
        {
            InitializeComponent();

            checkBox1.Checked = UpdateBuh.Enable;
            textBox1.Text = UpdateBuh.Path;

            EnabledComponents();
        }

        private void EnabledComponents()
        {
            switch (checkBox1.Checked)
            {
                case true:
                    textBox1.Enabled = true;
                    break;
                case false:
                    textBox1.Enabled = false;
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnabledComponents();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateBuh.Enable = checkBox1.Checked;
            UpdateBuh.Path = textBox1.Text;
        }

    }
}
