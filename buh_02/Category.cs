using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace buh_02
{
    public partial class Category : Form
    {
        #region Form Action
        public Category()
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
            if (tabControl1.SelectedTab.Text == "Доходы")
            {
                filter("Доход");
            }
            else if (tabControl1.SelectedTab.Text == "Расходы")
            {
                filter("Расход");
                dataGridViewTextBoxColumn1.Visible = false;
            }
        }
        
        private void filter(string str)
        {
            inOutCategoriesBindingSource.Filter = "convert(InOut,'System.String') LIKE '*" + str + "*'";
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


    }
}
