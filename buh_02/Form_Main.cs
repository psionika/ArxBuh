﻿using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Globalization;
using System.Security.Cryptography;

using Ionic.Zip;

using Microsoft.Reporting.WinForms; 

namespace buh_02
{
    public partial class Form_Main : Form
    {
        #region Form action
        public Form_Main()
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

            clearfilter();

            loadData("data.xml");            

            cashInOutBindingSource.Sort = "DateTime DESC";

            loadGoal();
            
            
         }
        #endregion

        #region Settings action
        Class_Props props = new Class_Props();
        //Запись настроек
        private void writeSetting()
        {
            props.Fields.Location = this.Location;
            props.Fields.FormSize = this.Size;

            props.Fields.BackupCounter = Backup.Counter;
            props.Fields.BackupDir = Backup.Dir;
            props.Fields.BackupEnable = Backup.Enable;

            props.Fields.EncryptEnable = EncryptDecrypt.Enable;

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

            EncryptDecrypt.Enable = props.Fields.EncryptEnable; 
        }
        #endregion

        #region DataSet action
        private void saveData(string filename)
        {
            if (EncryptDecrypt.Enable == false)
            {
                dataSet1.WriteXml(filename, XmlWriteMode.IgnoreSchema);
            }
            else
            {
                SetDataSet("data.xml", dataSet1, EncryptDecrypt.Password);
            }
        }

        private void loadData(string filename)
        {
            dataSet1.Clear();

            if (EncryptDecrypt.Enable == false)
            {
                if (File.Exists(filename) == true)
                {
                    dataSet1.ReadXml(filename);
                    dataGridView1.DataSource = this.cashInOutBindingSource;
                    dataGridView2.DataSource = this.budgetBindingSourceIn;
                    dataGridView3.DataSource = this.budgetBindingSourceOut;
                    dataGridView4.DataSource = this.goalBindingSource;
                }
            }
            else
            {
                if (File.Exists(filename) == true)
                {
                    do
                    {
                        PasswordRequest();
                    } while (GetDataSet(filename, EncryptDecrypt.Password, dataSet1));
                }
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

        private void DGPaintBudgetIn()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Меняем цвет ячейки
                            cell.Style.BackColor = Color.LightGreen;
                            cell.Style.ForeColor = Color.Black;

                            //Меняем шрифт ячейки
                            cell.Style.Font = new Font(this.Font, FontStyle.Regular);
                        }
            }
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

        private void DGPaintBudgetOut()
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Меняем цвет ячейки
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.Black;
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
            if(e.RowIndex != -1) edit_element();
        }


        private void dataGridView3_Paint(object sender, PaintEventArgs e)
        {
            budgetBindingSourceOut.Filter = "convert(InOut,'System.String') LIKE 'Расход'";
            DGPaintBudgetOut();
            InOutCalcBudget();
        }

        private void InOutCalcBudget()
        {
            double xIn = 0;
            double xOut = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    xIn = xIn + (double)row.Cells[3].Value;
                }

                labelIn.Text = xIn.ToString();
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    xOut = xOut + (double)row.Cells[3].Value;
                }

                labelOut.Text = xOut.ToString();
            }

            labelResult.Text = (xIn - xOut).ToString();
        }

        private void dataGridView2_Paint(object sender, PaintEventArgs e)
        {
            budgetBindingSourceIn.Filter = "convert(InOut,'System.String') LIKE 'Доход'";
            DGPaintBudgetIn();
            InOutCalcBudget();
        }
        #endregion

        #region Element Action (Add, Edit, Delete)
        private void add_element(string inOut)
        {
            Class_element.InOut = inOut;
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            Form_AddEdit addEdit = new Form_AddEdit();
            addEdit.ShowDialog();

            if (addEdit.DialogResult == DialogResult.OK)
            {
                dataSet1.Tables["CashInOut"].Rows.Add(Class_element.InOut, Class_element.Category, Class_element.Date, Class_element.Sum, Class_element.Comment);
            }

            saveData("data.xml");
        }

        private void edit_element()
        {
            if (dataGridView1.CurrentRow != null)
            {
                Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Class_element.Date = (DateTime)dataGridView1.CurrentRow.Cells[2].Value;
                Class_element.Sum = (double)dataGridView1.CurrentRow.Cells[3].Value;
                Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                Form_AddEdit addEdit = new Form_AddEdit();
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    DataRow customerRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                    customerRow["InOut"] = Class_element.InOut;
                    customerRow["Category"] = Class_element.Category;
                    customerRow["DateTime"] = Class_element.Date;
                    customerRow["Sum"] = Class_element.Sum;
                    customerRow["Comment"] = Class_element.Comment;
                }
            }

            saveData("data.xml");
        }

        private void delete_element()
        {
            if (dataGridView1.CurrentRow != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                    "Удаление элемента",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cashInOutBindingSource.RemoveCurrent();
                    saveData("data.xml");
                }
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


                sb.Append(string.Format(CultureInfo.InvariantCulture,
                  " AND DateTime >= #{0:MM/dd/yyyy}# AND DateTime <= #{1:MM/dd/yyyy}# ",
                  DateBeginEnd.DateBegin,
                  DateBeginEnd.DateEnd));
            


            cashInOutBindingSource.Filter = sb.ToString();
        }

        private void clearfilter()
        {
            toolStripComboBox1.Text = "";

            toolStripDateTimeChooser1.Value = DateTime.Now;
            toolStripDateTimeChooser2.Value = DateTime.Now;

            toolStripDateTimeChooser3.Value = DateTime.Now;
            toolStripDateTimeChooser4.Value = DateTime.Now;

            DateBeginEnd.DateBegin = new DateTime(1970, 1, 1);
            DateBeginEnd.DateEnd = new DateTime(2032, 1, 1);

            cashInOutBindingSource.RemoveFilter();
        }

        #endregion

        #region ToolStripButtonAction

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            add_element("Доход");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            add_element("Расход");
        }

        private void DeleteTSB_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void FilterClearTSB_Click(object sender, EventArgs e)
        {
            clearfilter();
        }

        private void AboutProgramTSB_Click(object sender, EventArgs e)
        {
            Form_AboutBox1 about = new Form_AboutBox1();
            about.ShowDialog();
        }

        private void ExitTSB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void категорииДоходовИРасходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Category category = new Form_Category();
            category.ShowDialog();
        }

        private void SettingsTSB_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            add_elementBudget("Доход");
        }
        private void add_elementBudget(string inOut)
        {
            Class_element.BudgetCheck = false;
            Class_element.InOut = inOut;
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            Form_AddEditBudget aeb = new Form_AddEditBudget();
            aeb.ShowDialog();

            if (aeb.DialogResult == DialogResult.OK)
            {
                dataSet1.Tables["Budget"].Rows.Add(Class_element.BudgetCheck, Class_element.InOut, Class_element.Category, Class_element.Date, Class_element.Sum, Class_element.Comment);
            }

            saveData("data.xml");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                budgetBindingSourceIn.RemoveCurrent();
                saveData("data.xml");
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
            {
                budgetBindingSourceOut.RemoveCurrent();
                saveData("data.xml");
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form_AboutBox1 about = new Form_AboutBox1();
            about.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form_Category category = new Form_Category();
            category.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form_Backup backupForm = new Form_Backup();
            backupForm.ShowDialog();

            writeSetting();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            add_elementBudget("Расход");
        }

        #endregion

        #region Backup action
        private void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Backup backupForm = new Form_Backup();
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

                    if (File.Exists("category.xml") == true) { zip.AddFile(TargetDir + "category.xml", ""); }
                    if (File.Exists("data.xml") == true) { zip.AddFile(TargetDir + "data.xml", ""); }
                    if (File.Exists("settings.xml") == true) { zip.AddFile(TargetDir + "settings.xml", ""); }

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
        
        #region Контекстное меню
        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_element();            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_element();
        }


        #endregion

        #region Шифрование
        private void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Encryption formEncryption = new Form_Encryption();
            formEncryption.ShowDialog();

            writeSetting();
        }

        private void PasswordRequest()
        {
            Form_RequestPassword formRP = new Form_RequestPassword();
            formRP.ShowDialog();
        }

        public bool GetDataSet(string file, string key, DataSet ds)
        {
            Rijndael crypto = Rijndael.Create();

            crypto.IV = ASCIIEncoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = ASCIIEncoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    try
                    {
                        ds.ReadXml(cryptoStream);
                    }
                    catch
                    {
                        MessageBox.Show("Пароль не верен!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                    cryptoStream.Close();
                    stream.Close();                    
                }
            }
            return false;
        }

        public void SetDataSet(string file, DataSet ds, string key)
        {
            Rijndael crypto = Rijndael.Create();

            crypto.IV = ASCIIEncoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = ASCIIEncoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            File.Delete(file);

            using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    ds.WriteXml(cryptoStream);
                    cryptoStream.Flush();
                    stream.Flush();
                    cryptoStream.Close();
                    stream.Close();
                }
            }
        }
        #endregion

        #region Цели
        private void loadGoal()
        {
            DataGridViewProgressColumn column = new DataGridViewProgressColumn();
            column.Width = 250;
            dataGridView4.Columns.Add(column);
        }

        private void tsb_AddGoal_Click(object sender, EventArgs e)
        {
            Form_AddEditGoal faeg = new Form_AddEditGoal("", "", "", "-1", dataSet1);
            faeg.ShowDialog();

            if (faeg.DialogResult == DialogResult.OK)
            {
                DataRow newGoalRow = dataSet1.Tables["Goal"].NewRow();

                newGoalRow["name"] = faeg.txb_GoalName.Text;
                newGoalRow["AllSum"] = faeg.txb_GoalSum.Text;
                newGoalRow["Comment"] = faeg.txb_GoalComment.Text;

                dataSet1.Tables["Goal"].Rows.Add(newGoalRow);                                
            }

            saveData("data.xml");            
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow newGoalRow = ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Row;

                if (dataGridView4.CurrentRow != null)
                {
                    string name = newGoalRow["Name"].ToString();
                    string allSum = newGoalRow["AllSum"].ToString();
                    string comment = newGoalRow["Comment"].ToString();
                    string HistoryID = newGoalRow["HistoryID"].ToString();

                    Form_AddEditGoal faeg = new Form_AddEditGoal(name, allSum, comment, HistoryID, dataSet1);
                    faeg.ShowDialog();

                    if (faeg.DialogResult == DialogResult.OK)
                    {
                        newGoalRow["name"] = faeg.txb_GoalName.Text;
                        newGoalRow["AllSum"] = faeg.txb_GoalSum.Text;
                        newGoalRow["Comment"] = faeg.txb_GoalComment.Text;
                        newGoalRow["History"] = Goal.History.ToString();
                    }

                    saveData("data.xml");
                }
            }
        }

        private void tsb_GoalDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                    "Удаление элемента",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    goalBindingSource.RemoveCurrent();
                    saveData("data.xml");
                }
            }
        }

        private void ProgressGoal()
        {
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                int x = (int)(Convert.ToDouble(row.Cells[2].Value) / (Convert.ToDouble(row.Cells[1].Value) / 100));
                row.Cells[5].Value = x;
            }
        }

        private void dataGridView4_Paint(object sender, PaintEventArgs e)
        {
            ProgressGoal();
        }        
        #endregion

        private void toolStripDateTimeChooser3_ValueChanged(object sender, EventArgs e)
        {
            DateBeginEnd.DateBegin = toolStripDateTimeChooser3.Value.Date;            
            filter();
        }

        private void toolStripDateTimeChooser4_ValueChanged(object sender, EventArgs e)
        {
            DateBeginEnd.DateEnd = toolStripDateTimeChooser4.Value.Date;
            filter();
        }

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            RefreshReport();
        }

        private void RefreshReport()
        {
            DateTime StartDate = new DateTime(2012, 01, 1);
            DateTime EndDate = new DateTime(2033, 12, 31);

            ReportDataSource reportDataSource1 = new ReportDataSource();

            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = cashInOutBindingSource;

            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.LocalReport.ReportEmbeddedResource = "buh_02.Report.Report1.rdlc";

            if (toolStripDateTimeChooser1.Value.Date == DateTime.Now.Date && toolStripDateTimeChooser2.Value.Date == DateTime.Now.Date)
            {
                StartDate = new DateTime(2012, 01, 1);
                EndDate = new DateTime(2033, 12, 31);
            }
            else
            {
                StartDate = toolStripDateTimeChooser1.Value;
                EndDate = toolStripDateTimeChooser2.Value;
            }

            ReportParameter psd = new ReportParameter("StartDate", StartDate.ToString());
            ReportParameter ped = new ReportParameter("EndDate", EndDate.ToString());

            if (toolStripComboBox2.Text == "Расходы")
            {
                ReportParameter pio = new ReportParameter("InOut", "Расход");
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { psd, ped, pio });

                reportViewer1.RefreshReport();
            }
            else if (toolStripComboBox2.Text == "Доходы")
            {
                ReportParameter pio = new ReportParameter("InOut", "Доход");
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { psd, ped, pio });

                reportViewer1.RefreshReport();
            }

        }
    }
}
