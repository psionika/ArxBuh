using System;
using System.Windows.Forms;

namespace buh_02
{
    public partial class Form_DateFilter : Form
    {
        public Form_DateFilter()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Месяц")
            {
                dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
                dateTimePicker2.Value = DateTime.Now;
            }
            else if(comboBox1.Text == "Два месяца")
            {
                dateTimePicker1.Value = DateTime.Now.AddMonths(-2);
                dateTimePicker2.Value = DateTime.Now;
            }
            else if(comboBox1.Text == "Три месяца")
            {
                dateTimePicker1.Value = DateTime.Now.AddMonths(-3);
                dateTimePicker2.Value = DateTime.Now;
            }            
            else if(comboBox1.Text == "Полгода")
            {
                dateTimePicker1.Value = DateTime.Now.AddMonths(-6);
                dateTimePicker2.Value = DateTime.Now;
            }
            else if(comboBox1.Text == "Год")
            {
                dateTimePicker1.Value = DateTime.Now.AddMonths(-12);
                dateTimePicker2.Value = DateTime.Now;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateBeginEnd.DateBegin = dateTimePicker1.Value.Date;
            DateBeginEnd.DateEnd = dateTimePicker2.Value.Date;

            Close();
        }
    }
}
