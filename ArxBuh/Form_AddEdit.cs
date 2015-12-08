using System;
using System.Data;
using System.Windows.Forms;

using System.Globalization;

namespace ArxBuh
{
    public partial class Form_AddEdit : Form
    {
        public Form_AddEdit(string title)
        {
            InitializeComponent();
            Text = title;
        }

        void Form_AddEdit_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Class_element.InOut;
            comboBox2.Text = Class_element.Category;
            dateTimePicker1.Value = Class_element.Date;
            txbSum.Text = Class_element.Sum.ToString();
            textBox1.Text = Class_element.Comment;

            validate();
        }

        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();

            validate();
        }

        void categoryEdit_Click(object sender, EventArgs e)
        {
            using (var category = new Form_Category())
            {
                category.ShowDialog();
            }
        }

        void filter()
        {
            if (comboBox1.Text == "Доход")
            {
                var townsView = new DataView(arxDs.ds.Tables["Categories"], "[In] = true", "CategoryID",
                    DataViewRowState.CurrentRows);

                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
            }

            if (comboBox1.Text == "Расход")
            {
                var townsView = new DataView(arxDs.ds.Tables["Categories"], "[Out] = true", "CategoryID",
                    DataViewRowState.CurrentRows);
                comboBox2.DataSource = townsView;
                comboBox2.DisplayMember = "Category";
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
            try
            {
                var i = Convert.ToDouble(parsSum(txbSum.Text));
                errorProvider1.SetError(txbSum, "");
                return true;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txbSum, "Неверные данные!");
                return false;
            }
        }
        #endregion

        void AddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && validate())
            {
                Class_element.InOut = comboBox1.Text;
                Class_element.Category = comboBox2.Text;
                Class_element.Date = dateTimePicker1.Value;

                Class_element.Sum = Convert.ToDouble(parsSum(txbSum.Text));
                Class_element.Comment = textBox1.Text;

                e.Cancel = false;
            }
            else if (DialogResult == DialogResult.Cancel)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

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
    }
}
