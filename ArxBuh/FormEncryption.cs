using System;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class FormEncryption : Form
    {
        public FormEncryption()
        {
            InitializeComponent();
            checkBox1.Checked = ArxBuhSettings.EncryptEnable;
            if (ArxBuhSettings.EncryptPassword != null)
            {
                maskedTextBox1.Text = ArxBuhSettings.EncryptPassword;
                maskedTextBox2.Text = ArxBuhSettings.EncryptPassword;
            }
            EnabledComponents();
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnabledComponents();
        }

        void EnabledComponents()
        {
            switch (checkBox1.Checked)
            {
                case true:
                    maskedTextBox1.Enabled = true;
                    maskedTextBox2.Enabled = true;
                    break;
                case false:
                    maskedTextBox1.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    break;
            }
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked
                && maskedTextBox1.Text == maskedTextBox2.Text
                && maskedTextBox1.Text != "")
            {
                ArxBuhSettings.EncryptEnable = checkBox1.Checked;
                ArxBuhSettings.EncryptPassword = maskedTextBox1.Text;

                Close();
            }
            else if (checkBox1.Checked && maskedTextBox1.Text != maskedTextBox2.Text)
            {
                MessageBox.Show("Введённые пароли не одинаковы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkBox1.Checked && maskedTextBox1.Text == "")
            {
                MessageBox.Show("Пароль не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (checkBox1.Checked) return;
            ArxBuhSettings.EncryptEnable = false;
            ArxBuhSettings.EncryptPassword = "";
            Close();
        }
    }
}
