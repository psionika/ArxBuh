using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace buh_02
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            arxDs.ds = dataSet1;
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.RowIndex < 0 || 
                e.ColumnIndex !=
                dataGridView1.Columns["Удалить"].Index ||
                categoriesBindingSource.Current == null) return;

             categoriesBindingSource.RemoveCurrent();
            
        }


    }
}
