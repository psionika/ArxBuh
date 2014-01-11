using System;
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
            EnabledComponents();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Backup.Dir = textBox1.Text;
            Backup.Counter = numericUpDown1.Value;
            Backup.Enable = checkBox1.Checked;
        }

        private void EnabledComponents()
        {
            switch (checkBox1.Checked)
            {
                case true:
                    textBox1.Enabled = true;
                    numericUpDown1.Enabled = true;
                    break;
                case false:
                    textBox1.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnabledComponents();
        }
    }
}
