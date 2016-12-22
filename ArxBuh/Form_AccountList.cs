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
    public partial class Form_AccountList : Form
    {
        public Form_AccountList()
        {
            InitializeComponent();

            dataSet1 = arxDs.ds;

            accountsBindingSource.DataSource = arxDs.ds;

            dataGridView1.DataSource = accountsBindingSource;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_AccountList_FormClosing(object sender, FormClosingEventArgs e)
        {
            arxDs.ds = dataSet1;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != "")
            {
                var newAccountRow = dataSet1.Tables["Accounts"].NewRow();
                newAccountRow["Account"] = toolStripTextBox1.Text;
                newAccountRow["StartSum"] = 0;

                dataSet1.Tables["Accounts"].Rows.Add(newAccountRow);
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (dataSet1.Tables["Accounts"].Rows.Count > 0)
            {
                var sumAccounts = Convert.ToDecimal(dataSet1.Tables["Accounts"].Compute("Sum(StartSum)", ""));

                decimal xIn = 0, xOut = 0;

                xIn = Convert.ToDecimal(dataSet1.Tables["CashInOut"].Compute("Sum(Sum)", "InOut = 'Доход'"));
                xOut = Convert.ToDecimal(dataSet1.Tables["CashInOut"].Compute("Sum(Sum)", "InOut = 'Расход'"));


                labelResult.Text = $"Вклады: ({sumAccounts.ToString("C2")}) + текущее ({(xIn - xOut).ToString("C2")}) = Всего {(sumAccounts + (xIn - xOut)).ToString("C2")}";
            }            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accountsBindingSource.RemoveCurrent();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var pt = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                pt.X += e.Location.X;
                pt.Y += e.Location.Y;
                contextMenuStrip1.Show(dataGridView1, pt);
            }
        }

        private void переместитьВверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            if (dataGridView1.RowCount > 0)
            {
                var idx = dataGridView1.SelectedRows[0].Index;

                var dt = dataSet1.Tables["Accounts"];

                var array = dt.Rows[idx].ItemArray;

                var row = dt.Rows[idx];

                if (idx != 0)
                {
                    dt.Rows.Remove(row);

                    row = dt.NewRow();
                    row.ItemArray = array;

                    dt.Rows.InsertAt(row, idx - 1);

                    dataGridView1.CurrentCell = dataGridView1.Rows[idx - 1].Cells[1];
                }
            }
        }

        private void переместитьВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            if (dataGridView1.RowCount > 0)
            {
                var idx = dataGridView1.SelectedRows[0].Index;

                var dt = dataSet1.Tables["Accounts"];

                var array = dt.Rows[idx].ItemArray;

                var row = dt.Rows[idx];

                if (idx != dataGridView1.Rows.Count - 1)
                {
                    dt.Rows.Remove(row);

                    row = dt.NewRow();
                    row.ItemArray = array;

                    dt.Rows.InsertAt(row, idx + 1);

                    dataGridView1.CurrentCell = dataGridView1.Rows[idx + 1].Cells[1];
                }
            }
        }
    }
}
