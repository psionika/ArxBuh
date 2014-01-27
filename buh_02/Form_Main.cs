using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.ComponentModel;

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
            ArxBuhSettingAction.ReadXml(); 
            
            loadData();

            clearfilter();

            loadGoal();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData();

            ArxBuhSettingAction.WriteXml();
            backup();
        }

        private void AboutProgramTSB_Click(object sender, EventArgs e)
        {
            Form_AboutBox1 about = new Form_AboutBox1();
            about.ShowDialog();
        }
        #endregion

        #region ToolStripButtonAction

        private void автоматическоеОбновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Update formUpdate = new Form_Update();
            formUpdate.ShowDialog();
        }

        private void SettingsTSB_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }

        private void CalculatorTSB_Click(object sender, EventArgs e)
        {
            Process Proc = new Process
            {
                StartInfo = { FileName = "calc.exe", WorkingDirectory = Environment.SystemDirectory }
            };
            Proc.Start();
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

        private void ExitTSB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Database action

        #region SaveLoad
        private void saveData()
        {
            switch (ArxBuhSettings.EncryptEnable)
            {
                case false:
                    dataSet1.WriteXml("data.xml", XmlWriteMode.IgnoreSchema);
                    break;
                default:
                    SetDataSet("data.xml", dataSet1, ArxBuhSettings.EncryptPassword);
                    break;
            }
        }

        private void loadData()
        {
            const string filename = "data.xml";
            if (!File.Exists(filename)) return;

            dataSet1.Clear();

            if (ArxBuhSettings.EncryptEnable == false)
            {
                dataSet1.ReadXml(filename);
                
                dataGridView1.DataSource = cashInOutBindingSource; 
                dataGridView4.DataSource = goalBindingSource;
            }
            else
            {
                do
                {
                    PasswordRequest();
                } while (GetDataSet(filename, ArxBuhSettings.EncryptPassword, dataSet1));
            }
            
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[3], ListSortDirection.Ascending);
        }
        #endregion

        #region Шифрование

        private void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Encryption formEncryption = new Form_Encryption();
            formEncryption.ShowDialog();

            ArxBuhSettingAction.WriteXml();
        }

        private void PasswordRequest()
        {
            Form_RequestPassword formRP = new Form_RequestPassword();
            formRP.ShowDialog();
        }

        public bool GetDataSet(string file, string key, DataSet ds)
        {
            var crypto = Rijndael.Create();

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
            var crypto = Rijndael.Create();

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

        #region Backup action

        private void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Backup backupForm = new Form_Backup();
            backupForm.ShowDialog();

            ArxBuhSettingAction.WriteXml();
        }

        private static void backup()
        {
            if (!ArxBuhSettings.BackupEnable) return;

            if (!Directory.Exists(ArxBuhSettings.BackupDir))
            {
                Directory.CreateDirectory(ArxBuhSettings.BackupDir);
            }

            #region Archive

            var TargetDir = Environment.CurrentDirectory + Path.DirectorySeparatorChar + ArxBuhSettings.BackupDir + Path.DirectorySeparatorChar;

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                if (File.Exists("data.xml"))
                {
                    zip.AddFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "data.xml", "");
                }
                if (File.Exists("settings.xml"))
                {
                    zip.AddFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "settings.xml", "");
                }

                zip.Save(TargetDir + "backup " + DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(@"/", "-").Replace(":", "-") + ".zip");
            }

            #endregion

            #region Удаляем лишнее

            var i = Directory.GetFiles(ArxBuhSettings.BackupDir, "*.*", SearchOption.AllDirectories).Length;

            while (i > ArxBuhSettings.BackupCounter)
            {
                var dt = DateTime.Now;
                var fs = Directory.GetFiles(ArxBuhSettings.BackupDir);
                var fileToDelete = "";

                foreach (var file in fs)
                {
                    FileInfo fi = new FileInfo(file);

                    if (fi.CreationTime >= dt) continue;

                    fileToDelete = file;
                    dt = fi.CreationTime;
                }

                File.Delete(fileToDelete);

                i = Directory.GetFiles(ArxBuhSettings.BackupDir, "*.*", SearchOption.AllDirectories).Length;
            }
            #endregion
        }

        #endregion

        #endregion

        #region Учёт доходов и расходов
        #region Контекстное меню

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_element();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = DateTime.Now.Date;
            Class_element.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            add_element();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        #endregion

        #region DataGridView action

        private void DGPaint()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == "Доход")
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleGreen;
                        cell.Style.ForeColor = Color.Black;
                    }
                else if (row.Cells[0].Value != null)
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
                var pt = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                pt.X += e.Location.X;
                pt.Y += e.Location.Y;

                contextMenuStrip1.Show(dataGridView1, pt);
            }
        }

        private void InOutCalc()
        {
            double xIn = 0, xOut = 0;

            foreach (var row in dataGridView1.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[0].Value != null))
            {
                switch (row.Cells[0].Value.ToString())
                {
                    case "Доход":
                        xIn = xIn + (double)row.Cells[3].Value;
                        break;
                    case "Расход":
                        xOut = xOut + (double)row.Cells[3].Value;
                        break;
                }
            }

            labelResultInOut.Text = string.Format("Доход ({0}) - Расход ({1}) = {2}", xIn.ToString("C2"), xOut.ToString("C2"), (xIn - xOut).ToString("C2"));
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

        #region Учёт доходов-расходов (Add, Edit, Delete)

        private void add_element()
        {
            arxDs.ds = dataSet1;

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

            if (dataGridView1.CurrentRow == null) return;

            Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            Form_AddEdit addEdit = new Form_AddEdit();
            addEdit.ShowDialog();

            if (addEdit.DialogResult == DialogResult.OK)
            {
                var customerRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                customerRow["InOut"] = Class_element.InOut;
                customerRow["Category"] = Class_element.Category;
                customerRow["DateTime"] = Class_element.Date;
                customerRow["Sum"] = Class_element.Sum;
                customerRow["Comment"] = Class_element.Comment;
            }

            saveData();
        }

        private void delete_element()
        {
            if (dataGridView1.CurrentRow == null) return;
            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            cashInOutBindingSource.RemoveCurrent();

            saveData();
        }

        #endregion

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Class_element.InOut = "Доход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            add_element();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Class_element.InOut = "Расход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            add_element();
        }

        private void DeleteTSB_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        #region Filter Action

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void FilterClearTSB_Click(object sender, EventArgs e)
        {
            clearfilter();
        }

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

        private void filter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");

            foreach (var col in dataSet1.Tables["CashInOut"].Columns.Cast<DataColumn>().Where(col => col.DataType == typeof(String)))
            {
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

        #endregion

        #region Бюджет
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            Class_element.BudgetCheck = false;
            Class_element.InOut = "Доход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            add_elementBudget();
        }

        private void remove_elementBudget()
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            remove_elementBudget();
        }

        private void add_elementBudget()
        {
            arxDs.ds = dataSet1;

            Form_AddEditBudget aeb = new Form_AddEditBudget();
            aeb.ShowDialog();

            if (aeb.DialogResult == DialogResult.OK)
            {
                dataSet1.Tables["Budget"].Rows.Add(Class_element.BudgetCheck, Class_element.InOut,
                    Class_element.Category, Class_element.Date, Class_element.Sum, Class_element.Comment);
            }

            saveData();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_elementBudget();
        }

        private void edit_elementBudget()
        {
            arxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow == null) return;

            Class_element.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value);
            Class_element.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            Class_element.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            Class_element.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            Form_AddEditBudget aeb = new Form_AddEditBudget();
            aeb.ShowDialog();

            if (aeb.DialogResult == DialogResult.OK)
            {
                var customerRow = ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Row;

                customerRow["Check"] = Class_element.BudgetCheck;
                customerRow["InOut"] = Class_element.InOut;
                customerRow["Category"] = Class_element.Category;
                customerRow["DateTime"] = Class_element.Date;
                customerRow["Sum"] = Class_element.Sum;
                customerRow["Comment"] = Class_element.Comment;
            }

            saveData();
        }
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
            double t = 0, i = 0 , y = 0;

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
                
                labelResultBudget.Text = string.Format("Текущее {0} + Доходы {1} - Расходы {2} = {3}", 
                                                  t.ToString("C2"), i.ToString("C2"), y.ToString("C2"), ((t + i) - y).ToString("C2"));
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

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var pt = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            pt.X += e.Location.X;
            pt.Y += e.Location.Y;

            contextMenuStrip2.Show(dataGridView2, pt);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            edit_elementBudget();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            remove_elementBudget();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Class_element.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value); ;
            Class_element.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = DateTime.Today;
            Class_element.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();;
            Class_element.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            Class_element.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            add_elementBudget();
        }
        #endregion

        #region Цели

        private void loadGoal()
        {
            DataGridViewProgressColumn progressColumn = new DataGridViewProgressColumn { Width = 250, HeaderText = "Прогресс" };
            dataGridView4.Columns.Add(progressColumn);

            DataGridViewTextBoxColumn remainingColumn = new DataGridViewTextBoxColumn { Width = 100, HeaderText = "Осталось" };
            dataGridView4.Columns.Add(remainingColumn);
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_elementGoal();
        }

        private void edit_elementGoal()
        {
            if (dataGridView4.CurrentRow == null) return;

            var newGoalRow = ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Row;

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
                newGoalRow["History"] = Goal.History.ToString(CultureInfo.InvariantCulture);
            }

            saveData();
        }

        private void tsb_GoalDelete_Click(object sender, EventArgs e)
        {
            remove_elementGoal();
        }

        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var pt = dataGridView4.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            pt.X += e.Location.X;
            pt.Y += e.Location.Y;

            contextMenuStrip3.Show(dataGridView4, pt);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            edit_elementGoal();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {

            remove_elementGoal();
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

        private void remove_elementGoal()
        {
            if (dataGridView4.CurrentRow == null) return;
            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            goalBindingSource.RemoveCurrent();
            saveData();
        }

        private void GoalProgress()
        {
            double SumGoal = 0;
            double SumGoalRemaining = 0;

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Cells[2].Value.ToString() == "" || row.Cells[2].Value == null) continue;

                int x = (int)(Convert.ToDouble(row.Cells[2].Value) / (Convert.ToDouble(row.Cells[1].Value) / 100));
                int y = (int)(Convert.ToDouble(row.Cells[1].Value) - (Convert.ToDouble(row.Cells[2].Value)));

                SumGoal += Convert.ToDouble(row.Cells[1].Value);
                SumGoalRemaining += Convert.ToDouble(y);

                row.Cells[5].Value = x;
                row.Cells[6].Value = y.ToString("C2");
            }

            labelResultGoal.Text = string.Format("Всего целей на {0}, осталось собрать {1}", SumGoal.ToString("C2"), SumGoalRemaining.ToString("C2"));
        }

        private void dataGridView4_Paint(object sender, PaintEventArgs e)
        {
            GoalProgress();
        }

        #endregion

        #region Отчёты
        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            RefreshReport();
        }

        private void RefreshReport()
        {
            DateTime StartDate;
            DateTime EndDate;

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
                StartDate = new DateTime(2011, 12, 12);
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
                        reportViewer1.LocalReport.SetParameters(new[] { psd, ped, pio });

                        reportViewer1.RefreshReport();
                    }
                    break;
                case "Доходы":
                    {
                        ReportParameter pio = new ReportParameter("InOut", "Доход");
                        reportViewer1.LocalReport.SetParameters(new[] { psd, ped, pio });

                        reportViewer1.RefreshReport();
                    }
                    break;
            }
        }
        #endregion

    }
}
