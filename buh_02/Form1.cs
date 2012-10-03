using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace buh_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            loadData("data.xml", "CashInOut");

            cashInOutBindingSource.Sort = "DateTime DESC";
        }

        private void saveData(string filename)
        {
            dataSet1.WriteXml(filename, XmlWriteMode.IgnoreSchema);
        }

        private void loadData(string filename, string tablename)
        {
            dataSet1.Clear();

            if (File.Exists(filename) == true)
            {
                dataSet1.ReadXml(filename);
                dataGridView1.DataSource = this.cashInOutBindingSource;
            }
        }

        private void DGPaint()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString() == "Доход")
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Меняем цвет ячейки
                            cell.Style.BackColor = Color.LightGreen;
                            cell.Style.ForeColor = Color.Black;

                            //Меняем шрифт ячейки
                            cell.Style.Font = new Font(this.Font, FontStyle.Regular);
                        }
                    else if (row.Cells[0].Value.ToString() == "Расход")
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Меняем цвет ячейки
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.Black;

                            //Меняем шрифт ячейки
                            cell.Style.Font = new Font(this.Font, FontStyle.Bold);
                        }
                    else
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Меняем цвет ячейки
                            cell.Style.BackColor = Color.Gold;
                            cell.Style.ForeColor = Color.Black;

                            //Меняем шрифт ячейки
                            cell.Style.Font = new Font(this.Font, FontStyle.Regular);
                        }

                }
            }
        }

        private void edit_element()
        {
            if (dataGridView1.CurrentRow != null)
            {
                element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                element.Date = (DateTime)dataGridView1.CurrentRow.Cells[2].Value;
                element.Sum = (double)dataGridView1.CurrentRow.Cells[3].Value;
                element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                AddEdit addEdit = new AddEdit();
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    DataRow customerRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                    customerRow["InOut"] = element.InOut;
                    customerRow["Category"] = element.Category;
                    customerRow["DateTime"] = element.Date;
                    customerRow["Sum"] = element.Sum;
                    customerRow["Comment"] = element.Comment;
                }
            }

            saveData("data.xml");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData("data.xml");
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            InOutCalc();
        }

        private void InOutCalc()
        {
            double i = 0;
            double y = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString() == "Доход")
                    {
                        i = i + (double)row.Cells[3].Value;
                    }
                    else if (row.Cells[0].Value.ToString() == "Расход")
                    {
                        y = y + (double)row.Cells[3].Value;
                    }
                }

                label2.Text = "Доход (" + i.ToString() + ") - Расход (" + y.ToString() + ") = " + (i - y).ToString();
            }
        }

        private void filter()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in dataSet1.Tables["CashInOut"].Columns)
            {
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" OR ");
                    }

                    if (col.DataType != typeof(System.DateTime))
                    {
                        sb.Append("convert(");
                        sb.Append(col.ColumnName);
                        sb.Append(",'System.String')");
                    }
                    else
                    {
                        sb.Append("convert(");
                        sb.Append("SUBSTRING((CONVERT(DateTime, System.String)), 1, 11), System.String)");
                    }

                    sb.Append(" LIKE '*");
                    sb.Append(toolStripComboBox1.Text);
                    sb.Append("*'");
                }
            }

            cashInOutBindingSource.Filter = sb.ToString();
        }

        private void removeFilter()
        {
            toolStripComboBox1.Text = "";

            cashInOutBindingSource.RemoveFilter();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            add_element("Доход");
        }

        private void доходToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            add_element("Доход");
        }

        private void расходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_element("Расход");
        }

        private void add_element(string inOut)
        {
            element.InOut = inOut;
            element.Date = DateTime.Today;
            element.Category = "";
            element.Sum = 0;
            element.Comment = "";

            AddEdit addEdit = new AddEdit();
            addEdit.ShowDialog();

            if (addEdit.DialogResult == DialogResult.OK)
            {
                dataSet1.Tables["CashInOut"].Rows.Add(element.InOut, element.Category, element.Date, element.Sum, element.Comment);
            }

            saveData("data.xml");
        }


        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DGPaint();
            InOutCalc();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //проверяем что не заголовок
            if (e.RowIndex != -1) edit_element();   
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void EditTSB_Click(object sender, EventArgs e)
        {
            edit_element();
        }

        private void FilterTSB_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void FilterClearTSB_Click(object sender, EventArgs e)
        {
            removeFilter();
        }

        private void DeleteTSB_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                cashInOutBindingSource.RemoveCurrent();
                saveData("data.xml");
            }
        }

        private void AboutProgramTSB_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void ExitTSB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
