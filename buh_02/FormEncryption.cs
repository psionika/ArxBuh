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
    public partial class FormEncryption : Form
    {
        public FormEncryption()
        {
            InitializeComponent();
            checkBox1.Checked = EncryptDecrypt.Enable;
            if (EncryptDecrypt.Password != null)
            {
                maskedTextBox1.Text = EncryptDecrypt.Password;
                maskedTextBox2.Text = EncryptDecrypt.Password;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {   
            if(checkBox1.Checked == true 
                && maskedTextBox1.Text == maskedTextBox2.Text 
                && maskedTextBox1.Text != "")
            {
                EncryptDecrypt.Enable = checkBox1.Checked;
                EncryptDecrypt.Password = maskedTextBox1.Text;
                
                Close();
            }
            else if (checkBox1.Checked == true && maskedTextBox1.Text != maskedTextBox2.Text)
            {
                MessageBox.Show("Введённые пароли не одинаковы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            else if (checkBox1.Checked == true && maskedTextBox1.Text == "")
            {
                MessageBox.Show("Пароль не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (checkBox1.Checked == false)
            {
                EncryptDecrypt.Enable = false;
                EncryptDecrypt.Password = "";
                Close();
            }
        }
    }
}
