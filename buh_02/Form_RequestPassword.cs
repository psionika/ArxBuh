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
            EncryptDecrypt.Password = maskedTextBox1.Text;
            Close();
        }
    }
}
