using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            comboBox1.Text = Class_element.InOut;
            comboBox2.Text = Class_element.Category;
            dateTimePicker1.Value = Class_element.Date;
            textBoxSum.Text = Class_element.Sum.ToString();
            textBoxComment.Text = Class_element.Comment;

            fillComboBoxes();
        }

        private void fillComboBoxes()
        {
            var view1 = new DataView(arxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            var view2 = new DataView(arxDs.ds.Tables["Accounts"], "", "",
                     DataViewRowState.CurrentRows);

            if (!view1.Cast<DataRowView>()
                .Any(rv => rv.Row.Field<string>("Account") == "Общий"))
            {
                DataRowView newRow1 = view1.AddNew();
                newRow1["Account"] = "Общий";
                newRow1.EndEdit();

                DataRowView newRow2 = view1.AddNew();
                newRow2["Account"] = "Общий";
                newRow2.EndEdit();
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
    }
}
