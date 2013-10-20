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
    public partial class Form_Backup : Form
    {
        public Form_Backup()
        {
            InitializeComponent();

            textBox1.Text = Backup.Dir;
            numericUpDown1.Value = Backup.Counter;
            checkBox1.Checked = Backup.Enable;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Backup.Dir = textBox1.Text;
            Backup.Counter = numericUpDown1.Value;
            Backup.Enable = checkBox1.Checked;
        }
    }
}
