﻿using System;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class Form_RequestPassword : Form
    {
        public Form_RequestPassword()
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
    }
}