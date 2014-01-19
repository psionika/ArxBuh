using System;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            readSetting();

            loadData();

            cashInOutBindingSource.Sort = "DateTime DESC";
            budgetBindingSource.Sort = "DateTime ASC";

            clearfilter();

            loadGoal();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData();
            writeSetting();

            backup();
        }



        #endregion

        #region Settings action

        private Class_Props props = new Class_Props();

        private void writeSetting()
        {
            props.Fields.Location = Location;
            props.Fields.FormSize = Size;

            props.Fields.BackupCounter = Backup.Counter;
            props.Fields.BackupDir = Backup.Dir;
            props.Fields.BackupEnable = Backup.Enable;

            props.Fields.EncryptEnable = EncryptDecrypt.Enable;

            props.Fields.UpdateEnabled = UpdateBuh.Enable;
            props.Fields.UpdatePath = UpdateBuh.Path;

            props.WriteXml();
        }

        private void readSetting()
        {
            props.ReadXml();

            Location = props.Fields.Location;
            Size = props.Fields.FormSize;

            Backup.Dir = props.Fields.BackupDir;
            Backup.Counter = props.Fields.BackupCounter;
            Backup.Enable = props.Fields.BackupEnable;

            EncryptDecrypt.Enable = props.Fields.EncryptEnable;

            UpdateBuh.Enable = props.Fields.UpdateEnabled;
            UpdateBuh.Path = props.Fields.UpdatePath;

        }

        #endregion

        #region DataSet action

        private void saveData()
        {
            if (EncryptDecrypt.Enable == false)
            {
                dataSet1.WriteXml("data.xml", XmlWriteMode.IgnoreSchema);
            }
            else
            {
                SetDataSet("data.xml", dataSet1, EncryptDecrypt.Password);
            }
        }

        private void loadData()
        {
            const string filename = "data.xml";

            dataSet1.Clear();

            if (EncryptDecrypt.Enable == false)
            {
                if (File.Exists(filename))
                {
                    dataSet1.ReadXml(filename);
                    dataGridView1.DataSource = cashInOutBindingSource;
                    dataGridView4.DataSource = goalBindingSource;
                }
            }
            else
            {
                if (File.Exists(filename))
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
                var value = row.Cells[0].Value;
                if (value != null && value.ToString() == "Доход")
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleGreen;
                        cell.Style.ForeColor = Color.Black;
                    }
                else if (value != null)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleVioletRed;
                        cell.Style.ForeColor = Color.Black;
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

        private void InOutCalc()
        {
            double i = 0;
            double y = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value == null) continue;
                switch (row.Cells[0].Value.ToString())
                {
                    case "Доход":
                        i = i + (double) row.Cells[3].Value;
                        break;
                    case "Расход":
                        y = y + (double) row.Cells[3].Value;
                        break;
                }
            }

            label2.Text = "Доход (" + i.ToString("C2") + ") - Расход (" + y.ToString("C2") + ") = " +
                          (i - y).ToString("C2");
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DGPaint();
            InOutCalc();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_element();
        }

        #endregion

        #region Element Action (Add, Edit, Delete)

        private void add_element(string inOut)
        {
            arxDs.ds = dataSet1;

            Class_element.InOut = inOut;
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            Form_AddEdit addEdit = new Form_AddEdit();
            addEdit.ShowDialog();

            if (addEdit.DialogResult == DialogResult.OK)
            {
                dataSet1.Tables["CashInOut"].Rows.Add(Class_element.InOut, Class_element.Category, Class_element.Date,
                    Class_element.Sum, Class_element.Comment);
            }

            saveData();
        }

        private void edit_element()
        {
            arxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow != null)
            {
                Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Class_element.Date = (DateTime) dataGridView1.CurrentRow.Cells[2].Value;
                Class_element.Sum = (double) dataGridView1.CurrentRow.Cells[3].Value;
                Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                Form_AddEdit addEdit = new Form_AddEdit();
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    DataRow customerRow = ((DataRowView) dataGridView1.CurrentRow.DataBoundItem).Row;

                    customerRow["InOut"] = Class_element.InOut;
                    customerRow["Category"] = Class_element.Category;
                    customerRow["DateTime"] = Class_element.Date;
                    customerRow["Sum"] = Class_element.Sum;
                    customerRow["Comment"] = Class_element.Comment;
                }
            }

            saveData();
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
                    saveData();
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
                if (col.DataType != typeof (String)) continue;

                sb.Append(col.ColumnName);
                sb.Append(" LIKE '*");
                sb.Append(toolStripComboBox1.Text);
                sb.Append("*'");
                sb.Append(" OR ");
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

            toolStripDateTimeChooser1.Value = DateTime.Now.Date;
            toolStripDateTimeChooser2.Value = DateTime.Now.Date;

            toolStripDateTimeChooser3.Value = DateTime.Now.Date;
            toolStripDateTimeChooser4.Value = DateTime.Now.Date;
                        
            DateBeginEnd.DateBegin = new DateTime(2011, 1, 1);
            DateBeginEnd.DateEnd = DateTime.Now;

            dateTimePicker1.Value = DateTime.Now.AddMonths(1);

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
            arxDs.ds = dataSet1;
            Form_Category category = new Form_Category();

            if (category.ShowDialog() == DialogResult.OK)
            {                
                dataSet1 = arxDs.ds;
            }

            saveData();
        }

        private void SettingsTSB_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            add_elementBudget("Доход");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            budgetBindingSource.RemoveCurrent();
            saveData();
        }

        private void add_elementBudget(string inOut)
        {
            arxDs.ds = dataSet1;

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
                dataSet1.Tables["Budget"].Rows.Add(Class_element.BudgetCheck, Class_element.InOut,
                    Class_element.Category, Class_element.Date, Class_element.Sum, Class_element.Comment);
            }

            saveData();
        }
        #endregion

        #region Backup action

        private void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Backup backupForm = new Form_Backup();
            backupForm.ShowDialog();

            writeSetting();
        }

        private static void backup()
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

                    if (File.Exists("category.xml"))
                    {
                        zip.AddFile(TargetDir + "category.xml", "");
                    }
                    if (File.Exists("data.xml"))
                    {
                        zip.AddFile(TargetDir + "data.xml", "");
                    }
                    if (File.Exists("settings.xml"))
                    {
                        zip.AddFile(TargetDir + "settings.xml", "");
                    }

                    zip.Save(TargetDir + "backup " + DateTime.Now.ToString().Replace(":", "-") + ".zip");
                }

                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "category.xml");
                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "data.xml");
                File.Delete(Environment.CurrentDirectory + "\\" + Backup.Dir + "\\" + "settings.xml");

                #endregion

                #region Удаляем лишнее

                int i = Directory.GetFiles(Backup.Dir, "*.*", SearchOption.AllDirectories).Length;

                while (i > Backup.Counter)
                {
                    DateTime dt = DateTime.Now;
                    var fs = Directory.GetFiles(Backup.Dir);
                    var fileToDelete = "";

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
                    i = Directory.GetFiles(Backup.Dir, "*.*", SearchOption.AllDirectories).Length;
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

            crypto.IV = Encoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                using (
                    CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateDecryptor(), CryptoStreamMode.Read)
                    )
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

            crypto.IV = Encoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            File.Delete(file);

            using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (
                    CryptoStream cryptoStream = new CryptoStream(stream, crypto.CreateEncryptor(),
                        CryptoStreamMode.Write))
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
            DataGridViewProgressColumn progressColumn = new DataGridViewProgressColumn {Width = 250, HeaderText = "Прогресс" };
            dataGridView4.Columns.Add(progressColumn);

            DataGridViewTextBoxColumn remainingColumn = new DataGridViewTextBoxColumn { Width = 100, HeaderText = "Осталось" };
            dataGridView4.Columns.Add(remainingColumn);
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (dataGridView4.CurrentRow != null)
            {
                var newGoalRow = ((DataRowView) dataGridView4.CurrentRow.DataBoundItem).Row;

                if (dataGridView4.CurrentRow != null)
                {
                    var name = newGoalRow["Name"].ToString();
                    var allSum = newGoalRow["AllSum"].ToString();
                    var comment = newGoalRow["Comment"].ToString();
                    var HistoryID = newGoalRow["HistoryID"].ToString();

                    Form_AddEditGoal faeg = new Form_AddEditGoal(name, allSum, comment, HistoryID, dataSet1);
                    faeg.ShowDialog();

                    if (faeg.DialogResult == DialogResult.OK)
                    {
                        newGoalRow["name"] = faeg.txb_GoalName.Text;
                        newGoalRow["AllSum"] = faeg.txb_GoalSum.Text;
                        newGoalRow["Comment"] = faeg.txb_GoalComment.Text;
                        newGoalRow["History"] = Goal.History.ToString();
                    }

                    saveData();
                }
            }
        }
        private void tsb_AddGoal_Click_1(object sender, EventArgs e)
        {
            Form_AddEditGoal faeg = new Form_AddEditGoal("", "", "", "-1", dataSet1);

            if (faeg.ShowDialog() == DialogResult.OK)
            {
                var newGoalRow = dataSet1.Tables["Goal"].NewRow();

                newGoalRow["name"] = faeg.txb_GoalName.Text;
                newGoalRow["AllSum"] = faeg.txb_GoalSum.Text;
                newGoalRow["Comment"] = faeg.txb_GoalComment.Text;

                dataSet1.Tables["Goal"].Rows.Add(newGoalRow);
            }

            saveData();
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
                    saveData();
                }
            }
        }

        private void GoalProgress()
        {
            double SumGoal = 0;
            double SumGoalRemaining = 0; 

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Cells[2].Value.ToString() == "") continue;

                int x = (int)(Convert.ToDouble(row.Cells[2].Value) / (Convert.ToDouble(row.Cells[1].Value) / 100));
                int y = (int)(Convert.ToDouble(row.Cells[1].Value) - (Convert.ToDouble(row.Cells[2].Value)));

                SumGoal += Convert.ToDouble(row.Cells[1].Value);
                SumGoalRemaining += Convert.ToDouble(y);

                row.Cells[5].Value = x;
                row.Cells[6].Value = y.ToString("C2");
            }

            labelGoal.Text = "Всего целей на " + SumGoal.ToString("C2") + ", осталось собрать " + SumGoalRemaining.ToString("C2");
        }

        private void dataGridView4_Paint(object sender, PaintEventArgs e)
        {
            GoalProgress();
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
            DateTime StartDate = toolStripDateTimeChooser1.Value;
            DateTime EndDate = toolStripDateTimeChooser2.Value;

            ReportDataSource reportDataSource1 = new ReportDataSource
            {
                Name = "DataSet1",
                Value = cashInOutBindingSource
            };

            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.LocalReport.ReportEmbeddedResource = "buh_02.Report.Report1.rdlc";

            if (toolStripDateTimeChooser1.Value.Date == DateTime.Now.Date &&
                toolStripDateTimeChooser2.Value.Date == DateTime.Now.Date)
            {   
                StartDate = new DateTime(2011,12,12);
                EndDate = DateTime.Now;
            }
            else
            {
                StartDate = toolStripDateTimeChooser1.Value;
                EndDate = toolStripDateTimeChooser2.Value;
            }

            ReportParameter psd = new ReportParameter("StartDate", StartDate.ToString());
            ReportParameter ped = new ReportParameter("EndDate", EndDate.ToString());

            switch (toolStripComboBox2.Text)
            {
                case "Расходы":
                {
                    ReportParameter pio = new ReportParameter("InOut", "Расход");
                    reportViewer1.LocalReport.SetParameters(new[] {psd, ped, pio});

                    reportViewer1.RefreshReport();
                }
                    break;
                case "Доходы":
                {
                    ReportParameter pio = new ReportParameter("InOut", "Доход");
                    reportViewer1.LocalReport.SetParameters(new[] {psd, ped, pio});

                    reportViewer1.RefreshReport();
                }
                    break;
            }
        }

        #region Бюджет
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Text == "Показывать выполненые")
            {
                toolStripButton1.Text = "Не показывать выполненные";
                budgetBindingSource.RemoveFilter();

                StringBuilder sb = new StringBuilder();


                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    "DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));

                budgetBindingSource.Filter = sb.ToString();
            }
            else
            {
                toolStripButton1.Text = "Показывать выполненые";
                StringBuilder sb = new StringBuilder();

                sb.Append("Check = false");

                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    " AND DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));

                budgetBindingSource.Filter = sb.ToString();
            }
        }

        private void DGBudgetPaint()
        {
            foreach (var row in dataGridView2.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[1].Value != null))
            {
                if (row.Cells[1].Value.ToString() == "Доход" && (bool) row.Cells[0].Value != true)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleGreen;
                    }
                else if (row.Cells[1].Value.ToString() == "Расход" && (bool) row.Cells[0].Value != true)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleVioletRed;
                    }
                else if ((bool)row.Cells[0].Value)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.SlateGray;
                    }
            }
        }

        private void dataGridView2_Paint_1(object sender, PaintEventArgs e)
        {
            filterBudget();
            DGBudgetPaint();
            InOutBudgetCalc();
        }

        private void InOutBudgetCalc()
        {
            double t = 0;

            double i = 0;
            double y = 0;

            if (dataSet1.Tables["CashInOut"].Rows.Count > 0)
            {
                var sumIN = dataSet1.Tables["CashInOut"].Compute("SUM(Sum)", "[InOut]='Доход'");
                var sumOUT = dataSet1.Tables["CashInOut"].Compute("SUM(Sum)", "[InOut]='Расход'");

                if (Convert.IsDBNull(sumIN) && Convert.IsDBNull(sumOUT))
                {
                    t = 0;
                }
                else if (!Convert.IsDBNull(sumIN) && Convert.IsDBNull(sumOUT))
                {
                    t = Convert.ToDouble(sumIN);
                }
                else if (Convert.IsDBNull(sumIN) && !Convert.IsDBNull(sumOUT))
                {
                    t = Convert.ToDouble(sumOUT)*(-1);
                }
                else
                {
                    t = Convert.ToDouble(sumIN) - Convert.ToDouble(sumOUT);    
                }
            }
            else
            {
                t = 0;
            }
            

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if ((bool) row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Доход")
                    {
                        i = i + (double) row.Cells[4].Value;
                    }
                    else if ((bool) row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Расход")
                    {
                        y = y + (double) row.Cells[4].Value;
                    }
                }

                labelResult.Text = "Текущее " + t.ToString("C2") + " + Доходы " + i.ToString("C2") + " - Расходы " +
                                   y.ToString("C2") + " = " + ((t + i) - y).ToString("C2");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filterBudget();
        }

        private void filterBudget()
        {
            StringBuilder sb = new StringBuilder();
            
            if (toolStripButton1.Text == "Показывать выполненые")
            {
                sb.Append("Check = false");
                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    " AND DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));

                budgetBindingSource.Filter = sb.ToString();
            }
            else
            {
                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    "DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));

                budgetBindingSource.Filter = sb.ToString();
            }
        }
        #endregion

        private void автоматическоеОбновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Update formUpdate = new Form_Update();
            formUpdate.ShowDialog();
        }

    }
}
