﻿using System;
using System.Windows.Forms;

namespace buh_02
{
    public partial class Form_Backup : Form
    {
        public Form_Backup()
        {
            InitializeComponent();

            checkBox1.Checked = ArxBuhSettings.BackupEnable;
            textBox1.Text = ArxBuhSettings.BackupDir;
            numericUpDown1.Value = ArxBuhSettings.BackupCounter;
            
            EnabledComponents();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.BackupDir = textBox1.Text;
            ArxBuhSettings.BackupCounter = numericUpDown1.Value;
            ArxBuhSettings.BackupEnable = checkBox1.Checked;
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
