using System;
using System.Windows.Forms;

namespace buh_02
{
    public partial class AddEdit : Form
    {
        public AddEdit()
        {
            InitializeComponent();

            comboBox1.Text = element.InOut;
            comboBox2.Text = element.Category;
            dateTimePicker1.Value = element.Date;
            textBox2.Text = element.Sum.ToString();
            textBox1.Text = element.Comment;

            validate();
        }

        private void AddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && validate())
            {
                element.InOut = comboBox1.Text;
                element.Category = comboBox2.Text;
                element.Date = dateTimePicker1.Value;

                element.Sum = Convert.ToDouble(textBox2.Text);
                element.Comment = textBox1.Text;

                e.Cancel = false;
            }
            else if (this.DialogResult == DialogResult.Cancel)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Доход")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("Зарплата");
                comboBox2.Items.Add("Колым");
                comboBox2.Items.Add("Подарок");
                comboBox2.Items.Add("Корректировка");
            }

            if (comboBox1.Text == "Расход")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("Развлечения");
                comboBox2.Items.Add("Кредиты");
                comboBox2.Items.Add("Ремонт");
                comboBox2.Items.Add("Дом");
                comboBox2.Items.Add("Другое");
                comboBox2.Items.Add("Корректировка");
            }

            validate();
        }

        private bool validate()
        {

            if (validate_InOut() && validate_Sum())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool validate_InOut()
        {

            if (comboBox1.Text == string.Empty)
            {
                errorProvider1.SetError(comboBox1, "Выберите хотя бы один вариант");
                return false;
            }
            else
            {
                errorProvider1.SetError(comboBox1, "");
                return true;
            }
        }

        private bool validate_Sum()
        {
            if (textBox2.Text == string.Empty || textBox2.Text == "0")
            {
                errorProvider1.SetError(textBox2, "Укажите сумму");
                return false;
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
                return true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            validate();

            textBox2.Text = textBox2.Text.Replace(".", ",");
            textBox2.SelectionStart = textBox2.TextLength;
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                !(Char.IsDigit(e.KeyChar)) &&
                !(
                  (e.KeyChar == ',' || e.KeyChar == '.') &&
                  (textBox2.Text.IndexOf(",") == -1 && textBox2.Text.IndexOf(".") == -1) &&
                  (textBox2.Text.Length != 0)
                 )
               )
            {
                if (e.KeyChar != (char)Keys.Back)
                {

                    e.Handled = true;
                }
            }
        }


    }
}
