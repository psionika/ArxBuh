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

            loadData("category.xml", "InOutCategories");

            InOutfilter();
        }

        private void Category_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData("category.xml");
        }
        #endregion

        #region Dataset Save-Load
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
            }
        }
        #endregion

        #region Удаление строки
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
          
            if (tabControl1.SelectedTab.Text == "Доходы")
            {
                if (dataGridView1.CurrentRow != null)
                {
                    inOutCategoriesBindingSource.RemoveCurrent();
                    saveData("category.xml");
                }
            }
            else if (tabControl1.SelectedTab.Text == "Расходы")
            {
                if (dataGridView2.CurrentRow != null)
                {
                    inOutCategoriesBindingSource.RemoveCurrent();
                    saveData("category.xml");
                }
            }
        }
        #endregion

        #region Добавление новой категории
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != "")
            {
                dataSet1.Tables["InOutCategories"].Rows.Add(tabControl1.SelectedTab.Text, toolStripTextBox1.Text);
                toolStripTextBox1.Text = "";
            }
        }
        #endregion

        #region Фильтрация
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            InOutfilter();
        }

        private void InOutfilter()
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Доходы":
                    inOutCategoriesBindingSource.Filter = "convert(InOut,'System.String') LIKE '*" + "Доход" + "*'";
                    break;
                case "Расходы":
                    inOutCategoriesBindingSource.Filter = "convert(InOut,'System.String') LIKE '*" + "Расход" + "*'";
                    dataGridViewTextBoxColumn1.Visible = false;
                    break;
            }
        }

        #endregion

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dataSet1.Tables["InOutCategories"].Rows.Add(tabControl1.SelectedTab.Text, toolStripTextBox1.Text);
                toolStripTextBox1.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
