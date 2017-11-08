using System;
using System.Threading;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class FormRequestPassword : Form
    {
        public FormRequestPassword()
        {
            InitializeComponent();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.EncryptPassword = maskedTextBox1.Text;
            Close();
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelLanguage.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName.ToUpper();
        }
    }
}
