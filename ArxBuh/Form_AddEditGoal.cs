﻿using System;
using System.Data;
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
            this.goalHistoryBindingSource.DataSource = ds;
            dataGridView1.DataSource = this.goalHistoryBindingSource;
        }

        void filter(int HistoryID)
        {
            goalHistoryBindingSource.Filter = "(HistoryID = " + HistoryID.ToString() + ")";
        }


        void tsb_AddGoalElement_Click(object sender, EventArgs e)
        {
            var faeg = new Form_AddGoalElement();
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

        void tsb_DeleteGoalElement_Click(object sender, EventArgs e)
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
    }
}