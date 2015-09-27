using System;
using System.IO;
using System.Windows.Forms;

namespace ArxBuh
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

        void btnOK_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.BackupDir = textBox1.Text;
            ArxBuhSettings.BackupCounter = numericUpDown1.Value;
            ArxBuhSettings.BackupEnable = checkBox1.Checked;
        }

        void EnabledComponents()
        {
            switch (checkBox1.Checked)
            {
                case true:
                    textBox1.Enabled = true;
                    numericUpDown1.Enabled = true;
                    btnChangeBackupDirectory.Enabled = true;
                    break;
                case false:
                    textBox1.Enabled = false;
                    numericUpDown1.Enabled = false;
                    btnChangeBackupDirectory.Enabled = false;
                    break;
            }
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnabledComponents();
        }

        void btnChangeBackupDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.SelectedPath = string.Empty;

                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;

                if (folderBrowserDialog1.SelectedPath == "")
                {
                    return;
                }

                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
            catch
            {
            }
        }
    }
}
