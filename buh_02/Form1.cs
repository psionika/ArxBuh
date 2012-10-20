using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Globalization;

using Ionic.Zip;

namespace buh_02
{
    public partial class Form1 : Form
    {
        #region Form action
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData("data.xml");
            writeSetting();

            backup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readSetting();

            loadData("data.xml", "CashInOut");

            cashInOutBindingSource.Sort = "DateTime DESC";
         }
        #endregion

        #region Settings action
        Props props = new Props();
        //Запись настроек
        private void writeSetting()
        {
            props.Fields.Location = this.Location;
            props.Fields.FormSize = this.Size;

            props.Fields.BackupCounter = Backup.Counter;
            props.Fields.BackupDir = Backup.Dir;
            props.Fields.BackupEnable = Backup.Enable;

            props.WriteXml();
        }

        //Чтение настроек
        private void readSetting()
        {
            props.ReadXml();

            this.Location = props.Fields.Location;
            this.Size = props.Fields.FormSize;
            Backup.Dir = props.Fields.BackupDir;
            Backup.Counter = props.Fields.BackupCounter;
            Backup.Enable = props.Fields.BackupEnable;
        }
        #endregion

        #region DataSet action
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
        #endregion

        #region DataGridView action
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
        #endregion

        #region Element Action (Add, Edit, Delete)
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

        private void DeleteTSB_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                cashInOutBindingSource.RemoveCurrent();
                saveData("data.xml");
            }
        }
        #endregion

        #region Filter Action
        private void filter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");

            foreach (DataColumn col in dataSet1.Tables["CashInOut"].Columns)
            {
                if (col.DataType == typeof(System.String))
                {
                    sb.Append(col.ColumnName);
                    sb.Append(" LIKE '*");
                    sb.Append(toolStripComboBox1.Text);
                    sb.Append("*'");
                    sb.Append(" OR ");
                }
            }

            sb.Remove(sb.Length - 3, 2);
            sb.Append(")");

            if (TSBTime.Text != "Фильтр по дате")
            {
                sb.Append(string.Format(CultureInfo.InvariantCulture,
                  " AND DateTime >= #{0:MM/dd/yyyy}# AND DateTime <= #{1:MM/dd/yyyy}# ",
                  DateBeginEnd.DateBegin,
                  DateBeginEnd.DateEnd));
            }


            cashInOutBindingSource.Filter = sb.ToString();
        }

        private void clearfilter()
        {
            toolStripComboBox1.Text = "";
            TSBTime.Text = "Фильтр по дате";
            DateBeginEnd.DateBegin = new DateTime(1970, 1, 1);
            DateBeginEnd.DateEnd = new DateTime(2032, 1, 1);

            cashInOutBindingSource.RemoveFilter();
        }

        #endregion

        #region ToolStripButtonAction
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

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void EditTSB_Click(object sender, EventArgs e)
        {
            edit_element();
        }

        private void FilterClearTSB_Click(object sender, EventArgs e)
        {
            clearfilter();
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

        private void категорииДоходовИРасходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.ShowDialog();
        }

        private void TSBTime_Click(object sender, EventArgs e)
        {
            DateFilter df = new DateFilter();
            df.ShowDialog();

            if (df.DialogResult == DialogResult.OK)
            {
                TSBTime.Text = "Фильтр по дате: " + DateBeginEnd.DateBegin.ToShortDateString() + " - " + DateBeginEnd.DateEnd.ToShortDateString();
                filter();
            }
        }

        private void SettingsTSB_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }
        #endregion

        #region Backup action
        private void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupForm backupForm = new BackupForm();
            backupForm.ShowDialog();

            writeSetting();
        }

        private void backup()
        {
            if (Backup.Enable)
            {
                if (!Directory.Exists(Backup.Dir))
                {
                    Directory.CreateDirectory(Backup.Dir);
                }

                #region Copy to dir
                string sourceDir = Environment.CurrentDirectory;
                string backupDir = Environment.CurrentDirectory + "\\" + Backup.Dir;

                try
                {
                    string[] xmlList = Directory.GetFiles(sourceDir, "*.xml");

                    // Copy picture files.
                    foreach (string f in xmlList)
                    {
                        // Remove path from the file name.
                        string fName = f.Substring(sourceDir.Length + 1);

                        // Use the Path.Combine method to safely append the file name to the path.
                        // Will overwrite if the destination file already exists.
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                    }
                }
                catch (DirectoryNotFoundException dirNotFound)
                {
                    MessageBox.Show(dirNotFound.Message);
                }
                #endregion

                #region Archive
                string TargetDir = Environment.CurrentDirectory + "\\" + Backup.Dir + "\\";

                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                    zip.AddFile(TargetDir + "category.xml", "");
                    zip.AddFile(TargetDir + "data.xml", "");
                    zip.AddFile(TargetDir + "settings.xml", "");

                    zip.Save(TargetDir + "backup " + DateTime.Now.ToString().Replace(":", "-") + ".zip");
                }

                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "category.xml");
                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "data.xml");
                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "settings.xml");

                #endregion

                #region Удаляем лишнее
                int i = System.IO.Directory.GetFiles(Backup.Dir, "*.*", SearchOption.AllDirectories).Length;

                while (i > Backup.Counter)
                {
                    DateTime dt = DateTime.Now;
                    string[] fs = Directory.GetFiles(Backup.Dir);
                    string fileToDelete = "";

                    foreach (string file in fs)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.CreationTime < dt)
                        {
                            fileToDelete = file;
                            dt = fi.CreationTime;
                        }
                    }

                    if (File.Exists(fileToDelete))
                    {
                        File.Delete(fileToDelete);
                    }
                    i = System.IO.Directory.GetFiles(Backup.Dir, "*.*", SearchOption.AllDirectories).Length;
                }
                #endregion
            }
        }


        #endregion
    }
}
