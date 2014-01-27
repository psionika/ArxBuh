using System;
using System.Windows.Forms;

namespace buh_02
{
    public partial class Form_RequestPassword : Form
    {
        public Form_RequestPassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.EncryptPassword = maskedTextBox1.Text;
            Close();
        }
    }
}
