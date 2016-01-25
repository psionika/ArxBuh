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
        }
    }
}
