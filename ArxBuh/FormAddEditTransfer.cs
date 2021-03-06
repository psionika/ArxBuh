﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class FormAddEditTransfer: Form
    {
        public FormAddEditTransfer(string title)
        {
            InitializeComponent();
            Text = $@"ArxBuh: {title}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        

        private void Form_AddEditTransfer_Load(object sender, EventArgs e)
        {
            fillComboBoxes();

            comboBox1.Text = ClassElement.InOut;
            comboBox2.Text = ClassElement.Category;
            dateTimePicker1.Value = ClassElement.Date;
            textBoxSum.Text = ClassElement.Sum.ToString();
            textBoxComment.Text = ClassElement.Comment;            
        }

        private void fillComboBoxes()
        {
            var view1 = new DataView(ArxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            var view2 = new DataView(ArxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            if (view1.Cast<DataRowView>().All(rv => rv.Row.Field<string>("Account") != "Основной"))
            {
                var newRow = view1.AddNew();
                newRow["Account"] = "Основной";
                newRow["StartSum"] = 0;
                newRow.EndEdit();
            }

            comboBox1.DataSource = view1;
            comboBox1.DisplayMember = "Account";

            comboBox2.DataSource = view2;
            comboBox2.DisplayMember = "Account";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var accountsList = new FormAccountList())
            {
                accountsList.ShowDialog();
            }

            fillComboBoxes();
        }
        
        private void Form_AddEditTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && ValidateData())
            {
                ClassElement.InOut = "Перевод";
                ClassElement.Category = $"{comboBox1.Text}->{comboBox2.Text}";
                ClassElement.Date = dateTimePicker1.Value;

                ClassElement.Sum = Convert.ToDouble(parsSum(textBoxSum.Text));
                ClassElement.Comment = textBoxComment.Text;

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

        private bool ValidateData()
        {
            return validateComboBox() && validate_Sum();
        }

        bool validateComboBox()
        {
            if (comboBox1.Text == comboBox2.Text)
            {
                errorProvider1.SetError(comboBox1, "Счета должны быть разными");
                errorProvider1.SetError(comboBox2, "Счета должны быть разными");
                return false;
            }

            errorProvider1.SetError(comboBox1, "");
            errorProvider1.SetError(comboBox2, "");
            return true;
        }

        bool validate_Sum()
        {
            var result = decimal.TryParse(textBoxSum.Text, out var sum);

            errorProvider1.SetError(textBoxSum, result ? "" : "Неверные данные!");

            return result;
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

        private void textBoxSum_KeyPress(object sender, KeyPressEventArgs e)
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
