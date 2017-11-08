using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace ArxBuh
{
    public partial class FormCategory : Form
    {
        #region Form Action
        public FormCategory()
        {
            InitializeComponent();

            dataSet1 = ArxDs.ds;

            categoriesBindingSource.DataSource = ArxDs.ds;

            dataGridView1.DataSource = categoriesBindingSource;
        }
        #endregion

        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex !=
                dataGridView1.Columns["Удалить"].Index ||
                categoriesBindingSource.Current == null)
                return;

            categoriesBindingSource.RemoveCurrent();

        }

        void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != "")
            {
                var newGoalRow = dataSet1.Tables["Categories"].NewRow();
                newGoalRow["Category"] = toolStripTextBox1.Text;
                newGoalRow["In"] = true;
                newGoalRow["Out"] = true;
                dataSet1.Tables["Categories"].Rows.Add(newGoalRow);
            }
        }

        void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && toolStripTextBox1.Text != "")
            {
                var newGoalRow = dataSet1.Tables["Categories"].NewRow();
                newGoalRow["Category"] = toolStripTextBox1.Text;
                newGoalRow["In"] = true;
                newGoalRow["Out"] = true;
                dataSet1.Tables["Categories"].Rows.Add(newGoalRow);
            }
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

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoriesBindingSource.RemoveCurrent();
        }

        private void переместитьВверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            if (dataGridView1.RowCount > 0)
            {
                var idx = dataGridView1.SelectedRows[0].Index;

                var dt = dataSet1.Tables["Categories"];

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

                var dt = dataSet1.Tables["Categories"];

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

        private void Form_Category_FormClosing(object sender, FormClosingEventArgs e)
        {
            ArxDs.ds = dataSet1;
        }
    }
}
