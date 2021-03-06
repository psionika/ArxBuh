﻿using System;
using System.Data;
using System.Windows.Forms;

using System.Globalization;

namespace ArxBuh
{
    public partial class FormAddEdit : Form
    {
        public FormAddEdit(string title)
        {
            InitializeComponent();
            Text = $"ArxBuh: {title}";
        }

        void Form_AddEdit_Load(object sender, EventArgs e)
        {
            comboBox1.Text = ClassElement.InOut;
            comboBox2.Text = ClassElement.Category;
            dateTimePicker1.Value = ClassElement.Date;
            txbSum.Text = ClassElement.Sum.ToString();
            textBox1.Text = ClassElement.Comment;

            validate();
        }

        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();

            validate();
        }

        void categoryEdit_Click(object sender, EventArgs e)
        {
            using (var category = new FormCategory())
            {
                category.ShowDialog();                
            }

            filter();
        }

        void filter()
        {
            if (comboBox1.Text == "Доход")
            {
                var view = new DataView(ArxDs.ds.Tables["Categories"], "[In] = true", "",
                    DataViewRowState.CurrentRows );

                comboBox2.DataSource = view;
                comboBox2.DisplayMember = "Category";
            }

            if (comboBox1.Text == "Расход")
            {
                var view = new DataView(ArxDs.ds.Tables["Categories"], "[Out] = true", "",
                    DataViewRowState.CurrentRows);
                comboBox2.DataSource = view;
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

        void AddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && validate())
            {
                ClassElement.InOut = comboBox1.Text;
                ClassElement.Category = comboBox2.Text;
                ClassElement.Date = dateTimePicker1.Value;

                ClassElement.Sum = Convert.ToDouble(parsSum(txbSum.Text));
                ClassElement.Comment = textBox1.Text;

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
