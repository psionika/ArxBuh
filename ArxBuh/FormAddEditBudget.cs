using System;
using System.Data;

using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class FormAddEditBudget : Form
    {
        public FormAddEditBudget(string title)
        {
            InitializeComponent();
            Text = $"ArxBuh: {title}";
        }

        private void Form_AddEditBudget_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = ClassElement.BudgetCheck;
            comboBox1.Text = ClassElement.InOut;
            comboBox2.Text = ClassElement.Category;
            dateTimePicker1.Value = ClassElement.Date;
            txbSum.Text = ClassElement.Sum.ToString();
            textBox1.Text = ClassElement.Comment;

            validate();
        }

        void AddEditBudget_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && validate())
            {
                ClassElement.BudgetCheck = checkBox1.Checked;
                ClassElement.InOut = comboBox1.Text;
                ClassElement.Category = comboBox2.Text;
                ClassElement.Date = dateTimePicker1.Value;

                ClassElement.Sum = Convert.ToDouble(parsSum(txbSum.Text));
                ClassElement.Comment = textBox1.Text;

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
        bool validate()
        {
            return validate_InOut() && validate_Sum();
        }

        bool validate_InOut()
        {
            if (comboBox1.Text == string.Empty)
            {
                errorProvider1.SetError(comboBox1, "Выберите хотя бы один вариант");
                return false;
            }

            errorProvider1.SetError(comboBox1, "");
            return true;
        }

        bool validate_Sum()
        {
            bool result = Decimal.TryParse(txbSum.Text, out decimal sum);

            if (result)
            {
                errorProvider1.SetError(txbSum, "");
            }
            else
            {
                errorProvider1.SetError(txbSum, "Неверные данные!");
            }

            return result;
        }
        #endregion


        string parsSum(string e)
        {
            var c = CultureInfo.CurrentCulture;
            var cs = c.NumberFormat.CurrencySymbol;
            var ns = c.NumberFormat.NegativeSign;
            var parsed = e;
            if (parsed.Contains(cs))
                parsed = parsed.Replace(cs, "");
            if (parsed.StartsWith("(") && parsed.EndsWith(")"))
                parsed = ns + parsed.Replace("(", "").Replace(")", "");

            parsed = parsed.Replace(".", ",");

            return parsed;
        }

        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
            validate();
        }

        void filter()
        {
            if (comboBox1.Text == "Доход")
            {
                var townsView = new DataView(ArxDs.ds.Tables["Categories"], "[In] = true", "",
                    DataViewRowState.CurrentRows);
                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
            }

            if (comboBox1.Text == "Расход")
            {
                var townsView = new DataView(ArxDs.ds.Tables["Categories"], "[Out] = true", "",
                    DataViewRowState.CurrentRows);
                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
            }
        }

        void categoryEdit_Click(object sender, EventArgs e)
        {
            using (var category = new FormCategory())
            {
                category.ShowDialog();

                filter();
            }
        }

        private void txbSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == ','
                && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
