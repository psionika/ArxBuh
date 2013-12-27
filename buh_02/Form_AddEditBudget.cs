using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace buh_02
{
    public partial class Form_AddEditBudget : Form
    {
        public Form_AddEditBudget()
        {
            InitializeComponent();

            loadData("category.xml", "InOutCategories");

            comboBox1.Text = Class_element.InOut;
            comboBox2.Text = Class_element.Category;
            dateTimePicker1.Value = Class_element.Date;
            calculatorTextBox1.TextBoxText = Class_element.Sum.ToString();
            textBox1.Text = Class_element.Comment;

            validate();
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {

        }

        private void AddEditBudget_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && validate())
            {
                Class_element.BudgetCheck = checkBox1.Checked;
                Class_element.InOut = comboBox1.Text;
                Class_element.Category = comboBox2.Text;
                Class_element.Date = dateTimePicker1.Value;

                Class_element.Sum = Convert.ToDouble(parsSum(calculatorTextBox1.TextBoxText));
                Class_element.Comment = textBox1.Text;

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

        #region Validate
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
            try
            {
                double i = Convert.ToDouble(parsSum(calculatorTextBox1.TextBoxText));
                errorProvider1.SetError(calculatorTextBox1, "");
                return true;
            }
            catch
            {
                errorProvider1.SetError(calculatorTextBox1, "Неверные данные!");
                return false;
            }

        }
        #endregion


        private string parsSum(string e)
        {
            CultureInfo c = CultureInfo.CurrentCulture;
            string cs = c.NumberFormat.CurrencySymbol;
            string ns = c.NumberFormat.NegativeSign;
            string parsed = e;
            if (parsed.Contains(cs))
                parsed = parsed.Replace(cs, "");
            if (parsed.StartsWith("(") && parsed.EndsWith(")"))
                parsed = ns + parsed.Replace("(", "").Replace(")", "");

            parsed = parsed.Replace(".", ",");

            return parsed;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Доход")
            {
                filter("Доход");
            }

            if (comboBox1.Text == "Расход")
            {
                filter("Расход");
            }

            validate();
        }

        private void filter(string str)
        {
            inOutCategoriesBindingSource.Filter = "convert(InOut,'System.String') LIKE '*" + str + "*'";
        }

        #region Dataset Load
        private void loadData(string filename, string tablename)
        {
            dataSet1.Clear();

            if (File.Exists(filename) == true)
            {
                dataSet1.ReadXml(filename);
            }

            comboBox2.DataSource = inOutCategoriesBindingSource;
            comboBox2.DisplayMember = "Category";
        }
        #endregion

        private void categoryEdit_Click(object sender, EventArgs e)
        {

        }

    }
}
