using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ArxBuh
{
    public partial class Form_AddEditGoal : Form
    {
        Int32 _HistoryID;

        public Form_AddEditGoal(string name, string sum, string comment, string HistoryID, DataSet ds)
        {
            InitializeComponent();

            txb_GoalName.Text = name;
            txb_GoalSum.Text = sum;
            txb_GoalComment.Text = comment;

            if (HistoryID != "")
            {
                _HistoryID = Convert.ToInt32(HistoryID);

                filter(_HistoryID);
            }

            dataSet1 = (DataSet1)ds;
            goalHistoryBindingSource.DataSource = ds;
            dataGridView1.DataSource = goalHistoryBindingSource;
        }

        void filter(int HistoryID)
        {
            goalHistoryBindingSource.Filter = "(HistoryID = " + HistoryID.ToString() + ")";
        }


        void tsb_AddGoalElement_Click(object sender, EventArgs e)
        {
            add_element();
        }

        void tsb_DeleteGoalElement_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        void add_element()
        {
            using (var faeg = new Form_AddEditGoalElement("Новая выплата по цели"))
            {
                faeg.ShowDialog();

                if (faeg.DialogResult == DialogResult.OK)
                {
                    var newGoalRow = dataSet1.Tables["GoalHistory"].NewRow();

                    newGoalRow["HistoryID"] = Convert.ToInt32(_HistoryID);
                    newGoalRow["DateTime"] = faeg.dtp_DateTimeGoalElement.Value;
                    newGoalRow["Sum"] = (faeg.txb_GoalElementAllSum.Text == "") ? "0" : faeg.txb_GoalElementAllSum.Text;
                    newGoalRow["Comment"] = faeg.txb_GoalElementComment.Text;

                    dataSet1.Tables["GoalHistory"].Rows.Add(newGoalRow);
                }
            }
        }

        void delete_element()
        {
            if (dataGridView1.CurrentRow != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                    "Удаление элемента",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    goalHistoryBindingSource.RemoveCurrent();
                }
            }
        }

        void edit_element()
        {
            if (dataGridView1.CurrentRow == null) return;

            using (var addEdit = new Form_AddEditGoalElement("Редактирование выплаты по цели"))
            {
                addEdit.dtp_DateTimeGoalElement.Value = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
                addEdit.txb_GoalElementAllSum.Text = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value).ToString();
                addEdit.txb_GoalElementComment.Text = Class_element.Comment = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    var editRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                    editRow["HistoryID"] = Convert.ToInt32(_HistoryID);
                    editRow["DateTime"] = addEdit.dtp_DateTimeGoalElement.Value;
                    editRow["Sum"] = (addEdit.txb_GoalElementAllSum.Text == "") ? "0" : addEdit.txb_GoalElementAllSum.Text;
                    editRow["Comment"] = addEdit.txb_GoalElementComment.Text;
                }
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            var i = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    i += Convert.ToInt32(row.Cells[2].Value);
                }
            }

            Goal.History = i;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Point pt = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                pt.X += e.Location.X;
                pt.Y += e.Location.Y;
                contextMenuStrip1.Show(dataGridView1, pt);
            }
        }

        private void новаяВыплатаПоЦелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_element();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_element();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_element();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            using (var addEdit = new Form_AddEditGoalElement("Редактирование выплаты по цели"))
            {
                addEdit.dtp_DateTimeGoalElement.Value = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
                addEdit.txb_GoalElementAllSum.Text = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value).ToString();
                addEdit.txb_GoalElementComment.Text = Class_element.Comment = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    var newRow = dataSet1.Tables["GoalHistory"].NewRow();

                    newRow["HistoryID"] = Convert.ToInt32(_HistoryID);
                    newRow["DateTime"] = addEdit.dtp_DateTimeGoalElement.Value;
                    newRow["Sum"] = (addEdit.txb_GoalElementAllSum.Text == "") ? "0" : addEdit.txb_GoalElementAllSum.Text;
                    newRow["Comment"] = addEdit.txb_GoalElementComment.Text;

                    dataSet1.Tables["GoalHistory"].Rows.Add(newRow);
                }
            }
        }
    }
}
