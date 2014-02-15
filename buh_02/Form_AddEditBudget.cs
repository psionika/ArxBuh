using System;
using System.Data;

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
            
            comboBox1.Text = Class_element.InOut;
            filter();
            comboBox2.Text = Class_element.Category;
            dateTimePicker1.Value = Class_element.Date;
            txbSum.Text = Class_element.Sum.ToString();
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

                Class_element.Sum = Convert.ToDouble(parsSum(txbSum.Text));
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
                double i = Convert.ToDouble(parsSum(txbSum.Text));
                errorProvider1.SetError(txbSum, "");
                return true;
            }
            catch
            {
                errorProvider1.SetError(txbSum, "Неверные данные!");
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
            filter();
            validate();
        }

        private void filter()
        {
            if (comboBox1.Text == "Доход")
            {
                DataView townsView = new DataView(arxDs.ds.Tables["Categories"], "[In] = true", "CategoryID", DataViewRowState.CurrentRows);
                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
            }

            if (comboBox1.Text == "Расход")
            {
                DataView townsView = new DataView(arxDs.ds.Tables["Categories"], "[Out] = true", "CategoryID", DataViewRowState.CurrentRows);
                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
            }
        }

        private void categoryEdit_Click(object sender, EventArgs e)
        {
            Form_Category category = new Form_Category();
            category.ShowDialog();
        }

    }
}
