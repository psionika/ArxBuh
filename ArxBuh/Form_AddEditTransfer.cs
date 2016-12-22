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
    public partial class Form_AddEditTransfer: Form
    {
        public Form_AddEditTransfer(string title)
        {
            InitializeComponent();
            Text = title;
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

            comboBox1.Text = Class_element.InOut;
            comboBox2.Text = Class_element.Category;
            dateTimePicker1.Value = Class_element.Date;
            textBoxSum.Text = Class_element.Sum.ToString();
            textBoxComment.Text = Class_element.Comment;            
        }

        private void fillComboBoxes()
        {
            var view1 = new DataView(arxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            var view2 = new DataView(arxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            if (!view1.Cast<DataRowView>()
                .Any(rv => rv.Row.Field<string>("Account") == "Основной"))
            {
                DataRowView newRow = view1.AddNew();
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
            using (var accountsList = new Form_AccountList())
            {
                accountsList.ShowDialog();
            }

            fillComboBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var accountsList = new Form_AccountList())
            {
                accountsList.ShowDialog();
            }

            fillComboBoxes();
        }

        private void Form_AddEditTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                Class_element.InOut = "Перевод";
                Class_element.Category = $"{comboBox1.Text}->{comboBox2.Text}";
                Class_element.Date = dateTimePicker1.Value;

                Class_element.Sum = Convert.ToDouble(parsSum(textBoxSum.Text));
                Class_element.Comment = textBoxComment.Text;

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
