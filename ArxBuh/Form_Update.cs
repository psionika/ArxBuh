using System;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class Form_Update : Form
    {
        public Form_Update()
        {
            InitializeComponent();

            checkBox1.Checked = ArxBuhSettings.UpdateEnabled;
            textBox1.Text = ArxBuhSettings.UpdatePath;

            EnabledComponents();
        }

        void EnabledComponents()
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

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnabledComponents();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.UpdateEnabled = checkBox1.Checked;
            ArxBuhSettings.UpdatePath = textBox1.Text;
        }

    }
}
