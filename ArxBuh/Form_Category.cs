using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace ArxBuh
{
    public partial class Form_Category : Form
    {
        #region Form Action
        public Form_Category()
        {
            InitializeComponent();

            dataSet1 = arxDs.ds;

            categoriesBindingSource.DataSource = arxDs.ds;

            dataGridView1.DataSource = categoriesBindingSource;
        }
        #endregion

        void btnCancel_Click(object sender, EventArgs e)
        {
            arxDs.ds = dataSet1;
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
    }
}
