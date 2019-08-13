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

using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

namespace ArxBuh
{
    public partial class FormMain : Form
    {
        const string datafile = "data.xml";
        const string settingsfile = "settings.xml";

        #region Form action

        public FormMain()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            ArxBuhSettingAction.ReadXml();
            timer1.Start();
            LoadData();

            using (var progressColumn = new DataGridViewProgressColumn { Width = 250, Name = "Progress", HeaderText = "Прогресс" })
            {
                dataGridView4.Columns.Add(progressColumn);
            }

            using (var remainingColumn = new DataGridViewTextBoxColumn { Width = 100, HeaderText = "Осталось" })
            {
                dataGridView4.Columns.Add(remainingColumn);
            }

            if(dataSet1.Tables["CashInOut"].Rows.Count > 0)
            {
                toolStripDateTimeChooser1.Value = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().Select(k => Convert.ToDateTime(k["DateTime"])).Min();
                toolStripDateTimeChooser3.Value = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().Select(k => Convert.ToDateTime(k["DateTime"])).Min();
            }
            else
            {
                toolStripDateTimeChooser1.Value = DateTime.Now.AddYears(-1);
                toolStripDateTimeChooser3.Value = DateTime.Now.AddYears(-1);
            }

            toolStripDateTimeChooser2.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            toolStripDateTimeChooser4.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            if (ArxBuhSettings.BudgetDate != new DateTime(1,1,1,0,0,0,0))
            {
                dateTimePicker1.Value = ArxBuhSettings.BudgetDate;
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now.Date.AddMonths(3);
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();

            ArxBuhSettings.EncryptPassword = "";
            ArxBuhSettingAction.WriteXml();

            Backup();
        }

        #endregion

        #region ToolStripButtonAction

        void AboutProgramTSB_Click(object sender, EventArgs e)
        {
            using (var about = new FormAboutBox())
            {
                about.ShowDialog();
            }
        }

        void автоматическоеОбновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formUpdate = new FormUpdate())
            {
                formUpdate.ShowDialog();
            }
        }

        void SettingsTSB_ButtonClick(object sender, EventArgs e)
        {
            SettingsTSB.ShowDropDown();
        }

        void CalculatorTSB_Click(object sender, EventArgs e)
        {
            var calcPath = Environment.SystemDirectory + Path.DirectorySeparatorChar + "calc.exe";

            if (!File.Exists(calcPath))
            {
                MessageBox.Show("Калькулятор в системной папке не найден!");
                return;
            }

            using (var Proc = new Process())
            {
                Proc.StartInfo.FileName = calcPath;
                Proc.Start();
            }
        }

        void категорииДоходовИРасходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArxDs.ds = dataSet1;

            using (var category = new FormCategory())
            {
                if (category.ShowDialog() == DialogResult.OK)
                {
                    dataSet1 = ArxDs.ds;
                }

                SaveData();
            }
        }

        void ExitTSB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Database action

        #region SaveLoad
        void SaveData()
        {
            switch (ArxBuhSettings.EncryptEnable)
            {
                case false:
                    dataSet1.WriteXml(datafile, XmlWriteMode.IgnoreSchema);
                    break;
                default:
                    SaveCryptDataSet(datafile, dataSet1, ArxBuhSettings.EncryptPassword);
                    break;
            }
        }

        private void LoadData()
        {
            dataGridView1.DataSource = cashInOutBindingSource;
            dataGridView4.DataSource = goalBindingSource;
            
            if (!File.Exists(datafile)) return;

            dataSet1.Clear();

            if (!ArxBuhSettings.EncryptEnable)
            {
                dataSet1.ReadXml(datafile);
            }
            else
            {
                do
                {
                    PasswordRequest();
                } while (LoadCryptDataSet(datafile, ArxBuhSettings.EncryptPassword, dataSet1));               
            }

            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[3], ListSortDirection.Ascending);
        }

        #endregion

        #region Шифрование

        void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formEncryption = new FormEncryption())
            {
                formEncryption.ShowDialog();

                ArxBuhSettingAction.WriteXml();
            }
        }

        private static void PasswordRequest()
        {
            using (var formRp = new FormRequestPassword())
            {
                formRp.ShowDialog();
            }
        }

        private bool LoadCryptDataSet(string file, string key, DataSet ds)
        {
            var crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            using (var stream = new FileStream(file, FileMode.Open))
            {
                using (
                    var cryptoStream = new CryptoStream(stream, crypto.CreateDecryptor(), CryptoStreamMode.Read)
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

        void SaveCryptDataSet(string file, DataSet ds, string key)
        {
            var crypto = Rijndael.Create();

            crypto.IV = Encoding.ASCII.GetBytes("qwert".PadRight(16, 'x'));
            crypto.Key = Encoding.ASCII.GetBytes(key.PadRight(16, 'x'));
            crypto.Padding = PaddingMode.Zeros;

            File.Delete(file);

            using (var stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (
                    var cryptoStream = new CryptoStream(stream, crypto.CreateEncryptor(),
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

        void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var backupForm = new FormBackup())
            {
                backupForm.ShowDialog();

                ArxBuhSettingAction.WriteXml();
            }
        }

        static void Backup()
        {
            if (!ArxBuhSettings.BackupEnable) return;

            var TargetDir = "";

            if (!ArxBuhSettings.BackupDir.Contains(Path.DirectorySeparatorChar))
            {
                TargetDir = Environment.CurrentDirectory + Path.DirectorySeparatorChar + ArxBuhSettings.BackupDir + Path.DirectorySeparatorChar;
            }
            else
            {
                TargetDir = ArxBuhSettings.BackupDir + Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(ArxBuhSettings.BackupDir))
            {
                Directory.CreateDirectory(ArxBuhSettings.BackupDir);
            }

            #region Copy to Backup Dir

            File.Copy(Environment.CurrentDirectory + Path.DirectorySeparatorChar + datafile,
                TargetDir + Path.DirectorySeparatorChar + "backup data " +
                         DateTime.Now.ToString(CultureInfo.InvariantCulture)
                             .Replace(@"/", "-")
                             .Replace(":", "-") + ".xml");

            File.Copy(Environment.CurrentDirectory + Path.DirectorySeparatorChar + settingsfile,
                TargetDir + Path.DirectorySeparatorChar + "backup settings " +
                        DateTime.Now.ToString(CultureInfo.InvariantCulture)
                            .Replace(@"/", "-")
                            .Replace(":", "-") + ".xml");

            #endregion

            #region Удаляем лишнее

            var i = Directory.GetFiles(TargetDir, "*.*", SearchOption.AllDirectories).Length;

            while (i > ArxBuhSettings.BackupCounter * 2)
            {
                var dt = DateTime.Now;
                var fs = Directory.GetFiles(TargetDir);
                var fileToDelete = "";

                foreach (var file in fs)
                {
                    var fi = new FileInfo(file);

                    if (fi.CreationTime >= dt) continue;

                    fileToDelete = file;
                    dt = fi.CreationTime;
                }

                File.Delete(fileToDelete);

                i = Directory.GetFiles(TargetDir, "*.*", SearchOption.AllDirectories).Length;
            }
            #endregion
        }

        #endregion

        #endregion

        #region Учёт доходов и расходов
        #region Контекстное меню

        void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_element();
        }

        void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "Доход" ||
                dataGridView1.CurrentRow.Cells[0].Value.ToString() == "Расход")
            {
                ClassElement.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ClassElement.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                ClassElement.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
                ClassElement.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
                ClassElement.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                add_element();
            }
            else if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "Перевод")
            {
                var array = dataGridView1.CurrentRow.Cells[1].Value.ToString().Split(
                         new string[] { "->" }, StringSplitOptions.None);

                var inOut = array[0];
                var category = array[1];

                ClassElement.InOut = inOut;
                ClassElement.Category = category;

                ClassElement.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
                ClassElement.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
                ClassElement.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                add_transfer();
            }
                        
        }

        void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        #endregion

        #region DataGridView action

        void DGPaint()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                switch (row.Cells[0].Value.ToString())
                {
                    case "Доход":
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.PaleGreen;
                        }
                        break;
                    case "Расход":
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.PaleVioletRed;
                        }
                        break;
                    case "Перевод":
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.PaleTurquoise;
                        }
                        break;
                    default:
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.LightGoldenrodYellow;
                        }
                        break;
                }
            }
        }

        void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

        void InOutCalc()
        {
            decimal xIn = 0, xOut = 0, xTransfer = 0;

            foreach (var row in dataGridView1.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[0].Value != null))
            {
                switch (row.Cells[0].Value.ToString())
                {
                    case "Доход":
                        xIn = xIn + (decimal)row.Cells[3].Value;
                        break;
                    case "Расход":
                        xOut = xOut + (decimal)row.Cells[3].Value;
                        break;
                    case "Перевод":
                        {
                            var array = row.Cells[1].Value.ToString().Split(new string[] { "->" }, StringSplitOptions.None);

                            var transferOut = array[0];
                            var transferIn = array[1];

                            if(transferOut == "Основной")
                            {
                                xTransfer = xTransfer - (decimal)row.Cells[3].Value;
                            }
                            else
                            {
                                xTransfer = xTransfer + (decimal)row.Cells[3].Value;
                            }
                        }
                        break;
                }
            }

            labelResultInOut.Text = $"Доход ({xIn.ToString("C2")}) - Расход ({xOut.ToString("C2")}) + Перевод ({xTransfer.ToString("C2")}) = {(xIn - xOut + xTransfer).ToString("C2")}";
        }

        void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DGPaint();
            InOutCalc();
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var type = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                if (type == "Доход" || type == "Расход")
                {
                    edit_element();
                }
                else if (type == "Перевод")
                {
                    edit_transfer();
                }                
            }
        }

        #endregion

        #region Учёт доходов-расходов (Add, Edit, Delete)

        bool add_element()
        {
            ArxDs.ds = dataSet1;

            using (var addEdit = new FormAddEdit("Новый элемент"))
            {
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    dataSet1.Tables["CashInOut"].Rows.Add(ClassElement.InOut, ClassElement.Category, ClassElement.Date,
                        ClassElement.Sum, ClassElement.Comment);

                    SaveData();

                    return true;
                }
                return false;
            }
        }

        void edit_element()
        {
            ArxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow == null) return;

            ClassElement.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ClassElement.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ClassElement.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            ClassElement.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            ClassElement.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            using (var addEdit = new FormAddEdit("Редактирование элемента"))
            {
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    var editRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                    editRow["InOut"] = ClassElement.InOut;
                    editRow["Category"] = ClassElement.Category;
                    editRow["DateTime"] = ClassElement.Date;
                    editRow["Sum"] = ClassElement.Sum;
                    editRow["Comment"] = ClassElement.Comment;
                }

                SaveData();
            }
        }

        void delete_element()
        {
            if (dataGridView1.CurrentRow == null) return;

            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            cashInOutBindingSource.RemoveCurrent();

            SaveData();
        }

        #endregion

        void toolStripButton6_Click(object sender, EventArgs e)
        {
            ClassElement.InOut = "Доход";
            ClassElement.Date = DateTime.Today;
            ClassElement.Category = "";
            ClassElement.Sum = 0;
            ClassElement.Comment = "";

            add_element();
        }

        void toolStripButton7_Click(object sender, EventArgs e)
        {
            ClassElement.InOut = "Расход";
            ClassElement.Date = DateTime.Today;
            ClassElement.Category = "";
            ClassElement.Sum = 0;
            ClassElement.Comment = "";

            add_element();
        }

        void DeleteTSB_Click(object sender, EventArgs e)
        {
            delete_element();
        }

        #region Filter Action

        void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void FilterClearTSB_Click(object sender, EventArgs e)
        {
            arxClearFilter();
        }

        void toolStripDateTimeChooser3_ValueChanged(object sender, EventArgs e)
        {
            DateBeginEnd.DateBegin = toolStripDateTimeChooser3.Value.Date;
            filter();
        }

        void toolStripDateTimeChooser4_ValueChanged(object sender, EventArgs e)
        {
            DateBeginEnd.DateEnd = toolStripDateTimeChooser4.Value.Date;
            filter();
        }

        void filter()
        {
            var sb = new StringBuilder();
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
                                    DateBeginEnd.DateBegin, DateBeginEnd.DateEnd));

            cashInOutBindingSource.Filter = sb.ToString();
        }

        void arxClearFilter()
        {
            if (dataSet1.Tables["CashInOut"].Rows.Count > 0)
            {
                DateBeginEnd.DateBegin = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().
                    Select(k => Convert.ToDateTime(k["DateTime"])).Min();
            }
            else
            {
            }


            DateBeginEnd.DateEnd = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            toolStripDateTimeChooser3.Value = DateBeginEnd.DateBegin;
            toolStripDateTimeChooser4.Value = DateBeginEnd.DateEnd;

            toolStripComboBox1.Text = "";

            cashInOutBindingSource.RemoveFilter();
        }
        #endregion

        #endregion

        #region Бюджет
        void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            ClassElement.BudgetCheck = false;
            ClassElement.InOut = "Доход";
            ClassElement.Date = DateTime.Today;
            ClassElement.Category = "";
            ClassElement.Sum = 0;
            ClassElement.Comment = "";

            add_elementBudget();

        }

        void toolStripButton12_Click(object sender, EventArgs e)
        {
            ClassElement.BudgetCheck = false;
            ClassElement.InOut = "Расход";
            ClassElement.Date = DateTime.Today;
            ClassElement.Category = "";
            ClassElement.Sum = 0;
            ClassElement.Comment = "";

            add_elementBudget();
        }

        void remove_elementBudget()
        {
            if (dataGridView2.CurrentRow == null) return;

            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            budgetBindingSource.RemoveCurrent();
            SaveData();
        }

        void toolStripButton4_Click(object sender, EventArgs e)
        {
            remove_elementBudget();
        }

        void add_elementBudget()
        {
            ArxDs.ds = dataSet1;

            using (var aeb = new FormAddEditBudget("Новый элемент"))
            {
                aeb.ShowDialog();

                if (aeb.DialogResult == DialogResult.OK)
                {
                    dataSet1.Tables["Budget"].Rows.Add(ClassElement.BudgetCheck, ClassElement.InOut,
                        ClassElement.Category, ClassElement.Date, ClassElement.Sum, ClassElement.Comment);
                }

                SaveData();
            }
        }

        void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_elementBudget();
        }

        void edit_elementBudget()
        {
            ArxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow == null) return;

            ClassElement.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value);
            ClassElement.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            ClassElement.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            ClassElement.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            ClassElement.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            ClassElement.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            using (var aeb = new FormAddEditBudget("Редактирование элемента"))
            {
                aeb.ShowDialog();

                if (aeb.DialogResult == DialogResult.OK)
                {
                    var customerRow = ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Row;

                    customerRow["Check"] = ClassElement.BudgetCheck;
                    customerRow["InOut"] = ClassElement.InOut;
                    customerRow["Category"] = ClassElement.Category;
                    customerRow["DateTime"] = ClassElement.Date;
                    customerRow["Sum"] = ClassElement.Sum;
                    customerRow["Comment"] = ClassElement.Comment;
                }

                SaveData();
            }
        }
        void toolStripButton1_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            if (toolStripButton1.Text == "Показывать выполненые")
            {
                toolStripButton1.Text = "Не показывать выполненные";
                budgetBindingSource.RemoveFilter();

                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    "DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));
            }
            else
            {
                toolStripButton1.Text = "Показывать выполненые";

                sb.Append("Check = false");

                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    " AND DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));
            }

            budgetBindingSource.Filter = sb.ToString();
        }

        void DGBudgetPaint()
        {
            foreach (var row in dataGridView2.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[1].Value != null))
            {
                if (row.Cells[1].Value.ToString() == "Доход" && !(bool)row.Cells[0].Value)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //Меняем цвет ячейки
                        cell.Style.BackColor = Color.PaleGreen;
                    }
                else if (row.Cells[1].Value.ToString() == "Расход" && !(bool)row.Cells[0].Value)
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

        void dataGridView2_Paint_1(object sender, PaintEventArgs e)
        {
            filterBudget();
            DGBudgetPaint();
            InOutBudgetCalc();
        }

        void InOutBudgetCalc()
        {
            decimal xTransfer = 0, t = 0, i = 0, y = 0;

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
                    t = Convert.ToDecimal(sumIN);
                }
                else if (Convert.IsDBNull(sumIN) && !Convert.IsDBNull(sumOUT))
                {
                    t = Convert.ToDecimal(sumOUT) * (-1);
                }
                else
                {
                    t = Convert.ToDecimal(sumIN) - Convert.ToDecimal(sumOUT);
                }
            }

            foreach (DataRow dr in dataSet1.Tables["CashInOut"].Rows)
            {
                if (dr["InOut"].ToString() == "Перевод")
                {
                    var array = dr["Category"].ToString().Split(new string[] { "->" }, StringSplitOptions.None);

                    var transferOut = array[0];
                    var transferIn = array[1];

                    if (transferOut == "Основной")
                    {
                        xTransfer = xTransfer - (decimal)dr["Sum"];
                    }
                    else
                    {
                        xTransfer = xTransfer + (decimal)dr["Sum"];
                    }
                }                  
                
            }

            t = t + xTransfer;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if ((bool)row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Доход")
                    {
                        i = i + (decimal)row.Cells[4].Value;
                    }
                    else if ((bool)row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Расход")
                    {
                        y = y + (decimal)row.Cells[4].Value;
                    }
                }                
            }
            labelResultBudget.Text = $"Текущее {t.ToString("C2")} + Планируемые Доходы {i.ToString("C2")} - "
                    + $"Планируемые Расходы {y.ToString("C2")} = {((t + i) - y).ToString("C2")}";
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filterBudget();

            ArxBuhSettings.BudgetDate = dateTimePicker1.Value;
            ArxBuhSettingAction.WriteXml();
        }

        void filterBudget()
        {
            var sb = new StringBuilder();

            if (toolStripButton1.Text == "Показывать выполненые")
            {
                sb.Append("Check = false");
                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    " AND DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));
            }
            else
            {
                sb.Append(string.Format(CultureInfo.InvariantCulture,
                    "DateTime <= #{0:MM/dd/yyyy}# ", dateTimePicker1.Value));
            }

            budgetBindingSource.Filter = sb.ToString();
        }

        void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var pt = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            pt.X += e.Location.X;
            pt.Y += e.Location.Y;

            contextMenuStrip2.Show(dataGridView2, pt);
        }

        void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DGBudgetPaint();
            InOutBudgetCalc();
            filterBudget();
            dataGridView2.Refresh();
        }

        void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            edit_elementBudget();
        }

        void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            remove_elementBudget();
        }

        void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            ClassElement.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value);
            ClassElement.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            ClassElement.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            ClassElement.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            ClassElement.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            ClassElement.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            add_elementBudget();
        }

        private void ввестиНаОснованииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            var customerRow = ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Row;

            ClassElement.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            ClassElement.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            ClassElement.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            ClassElement.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            ClassElement.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            tabControl1.SelectedTab = tabPage1;

            if (add_element())
            {
                var result = MessageBox.Show("Вы хотите отметить выполненным элемент на основании которого была создана операция?",
                "Выполнено",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
                customerRow["Check"] = true;

                SaveData();
            }
        }
        #endregion

        #region Цели

        void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_elementGoal();
        }

        void edit_elementGoal()
        {
            if (dataGridView4.CurrentRow == null) return;

            var editGoalRow = ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Row;

            var name = editGoalRow["Name"].ToString();
            var allSum = editGoalRow["AllSum"].ToString();
            var comment = editGoalRow["Comment"].ToString();
            var historyId = editGoalRow["HistoryID"].ToString();

            using (var faeg = new FormAddEditGoal(name, allSum, comment, historyId, dataSet1))
            {
                faeg.ShowDialog();

                if (faeg.DialogResult == DialogResult.OK)
                {
                    editGoalRow["name"] = faeg.txb_GoalName.Text;
                    editGoalRow["AllSum"] = faeg.txb_GoalSum.Text;
                    editGoalRow["Comment"] = faeg.txb_GoalComment.Text;
                    editGoalRow["History"] = Goal.History.ToString(CultureInfo.InvariantCulture);
                }

                SaveData();
            }
        }

        void tsb_GoalDelete_Click(object sender, EventArgs e)
        {
            remove_elementGoal();
        }

        void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var pt = dataGridView4.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            pt.X += e.Location.X;
            pt.Y += e.Location.Y;

            contextMenuStrip3.Show(dataGridView4, pt);
        }

        void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            edit_elementGoal();
        }

        void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            remove_elementGoal();
        }

        void tsb_AddGoal_Click_1(object sender, EventArgs e)
        {
            var newGoalRow = dataSet1.Tables["Goal"].NewRow();

            newGoalRow["name"] = "";
            newGoalRow["AllSum"] = "0";
            newGoalRow["Comment"] = "";

            dataSet1.Tables["Goal"].Rows.Add(newGoalRow);

            using (var faeg = new FormAddEditGoal("", "", "", newGoalRow["HistoryID"].ToString(), dataSet1))
            {
                if (faeg.ShowDialog() != DialogResult.OK)
                {
                    dataSet1.Tables["Goal"].Rows.Remove(newGoalRow);
                }
            }

            SaveData();
        }

        void remove_elementGoal()
        {
            if (dataGridView4.CurrentRow == null) return;

            var result = MessageBox.Show("Вы действительно хотите удалить текущий элемент?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
            goalBindingSource.RemoveCurrent();
            SaveData();
        }

        void GoalProgress()
        {
            decimal SumGoal = 0;
            decimal SumGoalRemaining = 0;

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Cells[2].Value == null
                    || row.Cells[2].Value.ToString() == ""
                    || Convert.ToDecimal(row.Cells[1].Value) == 0)
                {
                    row.Cells[2].Value = 0;
                }

                decimal completeProcent = 0;
                decimal remaining = 0;

                var sum = Convert.ToDecimal(row.Cells[1].Value);
                var complete = Convert.ToDecimal(row.Cells[2].Value);

                if (sum != 0)
                {
                    completeProcent = complete / (sum / 100);
                }
                
                remaining = sum - complete;

                //Требуется Int для корректного отображения progress Column в датагриде
                row.Cells[5].Value = Convert.ToInt32(completeProcent);

                row.Cells[6].Value = remaining.ToString("C2");

                if ((toolStripButton13.Text == "Показывать выполненные" && (int)(row.Cells[5].Value) < 100) ||
                     toolStripButton13.Text != "Показывать выполненные")
                {
                    SumGoal += sum;
                    SumGoalRemaining += Convert.ToDecimal(remaining);
                }
            }

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (toolStripButton13.Text != "Показывать выполненные")
                {
                    row.Visible = true;
                }
                else
                {
                    if ((int)(row.Cells[5].Value) >= 100)
                    {
                        if (dataGridView4.CurrentCell.RowIndex == row.Index)
                        {
                            foreach (DataGridViewRow rowV in dataGridView4.Rows)
                            {
                                if (rowV.Visible && row.Index != rowV.Index)
                                {
                                    dataGridView4.CurrentCell = dataGridView4.Rows[rowV.Index].Cells[0];
                                    dataGridView4.Rows[rowV.Index].Selected = true;
                                    break;
                                }
                            }
                        }

                        row.Visible = false;
                    }
                }
            }
            if (SumGoal > 0)
            {
                string procent = (1 - SumGoalRemaining / SumGoal).ToString("p1");
                labelResultGoal.Text = string.Format($"Всего целей на {SumGoal.ToString("C2")}, осталось собрать {SumGoalRemaining.ToString("C2")} ({procent} собрано)");

            }
        }

        void dataGridView4_Paint(object sender, PaintEventArgs e)
        {
            GoalProgress();
        }

        #endregion

        #region Отчёты
        void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            arxClearFilter();
            RefreshReport();
        }

        void RefreshReport()
        {
            DateTime StartDate;
            DateTime EndDate;

            var reportDataSource1 = new ReportDataSource
            {
                Name = "DataSet1",
                Value = cashInOutBindingSource
            };

            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.LocalReport.ReportEmbeddedResource = "ArxBuh.Report.Report1.rdlc";

            if (toolStripDateTimeChooser1.Value.Date == DateTime.Now.Date &&
                toolStripDateTimeChooser2.Value.Date == DateTime.Now.Date)
            {
                StartDate = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().
        Select(k => Convert.ToDateTime(k["DateTime"])).Min(); 
                EndDate = DateTime.Now;
            }
            else
            {
                StartDate = toolStripDateTimeChooser1.Value;
                EndDate = toolStripDateTimeChooser2.Value;
            }

            var psd = new ReportParameter("StartDate", StartDate.ToString());
            var ped = new ReportParameter("EndDate", EndDate.ToString());

            switch (toolStripComboBox2.Text)
            {
                case "Расходы":
                    {
                        var pio = new ReportParameter("InOut", "Расход");
                        reportViewer1.LocalReport.SetParameters(new[] { psd, ped, pio });

                        reportViewer1.RefreshReport();
                    }
                    break;
                case "Доходы":
                    {
                        var pio = new ReportParameter("InOut", "Доход");
                        reportViewer1.LocalReport.SetParameters(new[] { psd, ped, pio });

                        reportViewer1.RefreshReport();
                    }
                    break;
            }
        }
        #endregion

        #region Горячие клавиши
        void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt) return;

            var key = e.KeyCode;
            switch (key)
            {
                case Keys.Q:
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        toolStripButton6.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        toolStripButton3.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage3)
                    {
                        tsb_AddGoal.PerformClick();
                    }
                    break;
                case Keys.W:
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        toolStripButton7.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        toolStripButton12.PerformClick();
                    }
                    break;
                case Keys.D:
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        DeleteTSB.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        toolStripButton4.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage3)
                    {
                        tsb_GoalDelete.PerformClick();
                    }
                    break;
                case Keys.F:
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        toolStripComboBox1.Focus();
                    }
                    break;
                case Keys.S:
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        SettingsTSB.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        SettingsTSB.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage3)
                    {
                        SettingsTSB.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage4)
                    {
                        SettingsTSB.PerformClick();
                    }
                    break;
                case Keys.D1:
                    tabControl1.SelectedTab = tabPage1;
                    break;
                case Keys.D2:
                    tabControl1.SelectedTab = tabPage2;
                    break;
                case Keys.D3:
                    tabControl1.SelectedTab = tabPage3;
                    break;
                case Keys.D4:
                    tabControl1.SelectedTab = tabPage4;
                    break;
                case Keys.X:
                    ExitTSB.PerformClick();
                    break;
            }
        }

        void справкаПоГорячимКлавишамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fhh = new FormHotkeyHelper())
            {
                fhh.ShowDialog();
            }
        }

        #endregion

        private void tsbImportCSV_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Title = "CSV - SAVE File";
                saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "CSV File|*.CSV|" + "All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;

                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

                var csvFile = saveFileDialog1.FileName;

                var sb = new StringBuilder();

                var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
                sb.AppendLine(string.Join(";", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.AppendLine(string.Join(";", cells.Select(cell => "\"" + cell.Value.ToString().Replace('\r', ' ').Replace('\n', ' ').Replace(';', ',') + "\"").ToArray()));
                }

                using (TextWriter tw = new StreamWriter(csvFile, false, Encoding.Default))
                {
                    tw.WriteLine(sb);

                    tw.Close();
                }
            }
        }

        #region Date time filter с по
        private void сНачалаНеделиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            while (dtBegin.DayOfWeek != DayOfWeek.Monday)
            {
                dtBegin = dtBegin.AddDays(-1);
            }

            dateTimePeriod(dtBegin, DateTime.Now);
        }

        private void сНачалаМесяцаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year, dtBegin.Month, 1);

            dateTimePeriod(dtBegin, DateTime.Now);
        }

        private void сНачалаГодаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year, 1, 1);

            dateTimePeriod(dtBegin, DateTime.Now);
        }

        private void предыдущаяНеделяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtEnd = DateTime.Now.Date;

            if (dtEnd.DayOfWeek == DayOfWeek.Sunday) dtEnd = dtEnd.AddDays(-1);

            while (dtEnd.DayOfWeek != DayOfWeek.Sunday)
            {
                dtEnd = dtEnd.AddDays(-1);
            }

            dtEnd = dtEnd.AddDays(1).AddSeconds(-1);

            var dtBegin = dtEnd;

            while (dtBegin.DayOfWeek != DayOfWeek.Monday)
            {
                dtBegin = dtBegin.AddDays(-1);
            }

            dateTimePeriod(dtBegin, dtEnd);
        }

        private void предыдущийМесяцToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;
            var dtEnd = DateTime.Now.Date;

            if(dtBegin.Month != 1)
            {
                dtBegin = new DateTime(dtBegin.Year, dtBegin.Month - 1, 1);
                dtEnd = new DateTime(dtEnd.Year, dtEnd.Month - 1, DateTime.DaysInMonth(dtEnd.Year, dtEnd.Month - 1), 23, 59, 59);
            }
            else
            {
                dtBegin = new DateTime(dtBegin.Year - 1, 12, 1);
                dtEnd = new DateTime(dtEnd.Year - 1, 12, 31, 23, 59, 59);
            }

            dateTimePeriod(dtBegin, dtEnd);
        }

        private void предыдущийГодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;
            var dtEnd = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year - 1, 1, 1);
            dtEnd = new DateTime(dtEnd.Year - 1, 12, 31, 23, 59, 59);

            dateTimePeriod(dtBegin, dtEnd);
        }

        private void заВсёВремяToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().
                    Select(k => Convert.ToDateTime(k["DateTime"])).Min();

            var dtEnd = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            dateTimePeriod(dtBegin, dtEnd);
        }

        void dateTimePeriod(DateTime dtBegin, DateTime dtEnd)
        {
            DateBeginEnd.DateBegin = dtBegin;
            DateBeginEnd.DateEnd = dtEnd;

            toolStripDateTimeChooser3.Value = DateBeginEnd.DateBegin;
            toolStripDateTimeChooser4.Value = DateBeginEnd.DateEnd;

            filter();
        }

        #endregion

        private void СписокСчетовtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArxDs.ds = dataSet1;

            using (var fListAcc = new FormAccountList())
            {
                
                if (fListAcc.ShowDialog() == DialogResult.OK)
                {
                    dataSet1 = ArxDs.ds;
                }

                SaveData();
            }
        }

        private void toolsbTransfer_Click(object sender, EventArgs e)
        {
            ClassElement.InOut = "Основной";
            ClassElement.Category = "Основной";
            ClassElement.Date = DateTime.Now.Date;
            ClassElement.Sum = 0;
            ClassElement.Comment = "";

            add_transfer();
        }

        private bool add_transfer()
        {
            ArxDs.ds = dataSet1;

            using (var fTransfer = new FormAddEditTransfer("Новый перевод"))
            {
                fTransfer.ShowDialog();

                if (fTransfer.DialogResult == DialogResult.OK)
                {
                    dataSet1.Tables["CashInOut"].Rows.Add(ClassElement.InOut, ClassElement.Category, ClassElement.Date,
                        ClassElement.Sum, ClassElement.Comment);
                    SaveData();

                    return true;
                }

                return false;
            }
        }

        private void edit_transfer()
        {
            ArxDs.ds = dataSet1;

            using (var fTransfer = new FormAddEditTransfer("Редактирование перевода"))
            {
                var array = dataGridView1.CurrentRow.Cells[1].Value.ToString().Split(
                         new string[] { "->" }, StringSplitOptions.None);

                var inOut = array[0];
                var category = array[1];               

                ClassElement.InOut = inOut;
                ClassElement.Category = category;

                ClassElement.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
                ClassElement.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
                ClassElement.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                fTransfer.ShowDialog();

                if (fTransfer.DialogResult == DialogResult.OK)
                {
                    var editRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                    editRow["InOut"] = ClassElement.InOut;
                    editRow["Category"] = ClassElement.Category;
                    editRow["DateTime"] = ClassElement.Date;
                    editRow["Sum"] = ClassElement.Sum;
                    editRow["Comment"] = ClassElement.Comment;
                }

                SaveData();
            }
        }

        private void сНачалаНеделиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            while (dtBegin.DayOfWeek != DayOfWeek.Monday)
            {
                dtBegin = dtBegin.AddDays(-1);
            }

            dateTimePeriodReport(dtBegin, DateTime.Now);
        }

        private void сНачалаМесяцаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year, dtBegin.Month, 1);

            dateTimePeriodReport(dtBegin, DateTime.Now);
        }

        private void сНачалаГодаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year, 1, 1);

            dateTimePeriodReport(dtBegin, DateTime.Now);
        }

        private void предыдущаяНеделяToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtEnd = DateTime.Now.Date;

            if (dtEnd.DayOfWeek == DayOfWeek.Sunday) dtEnd = dtEnd.AddDays(-1);

            while (dtEnd.DayOfWeek != DayOfWeek.Sunday)
            {
                dtEnd = dtEnd.AddDays(-1);
            }

            dtEnd = dtEnd.AddDays(1).AddSeconds(-1);

            var dtBegin = dtEnd;

            while (dtBegin.DayOfWeek != DayOfWeek.Monday)
            {
                dtBegin = dtBegin.AddDays(-1);
            }

            dateTimePeriodReport(dtBegin, dtEnd);
        }

        private void предыдущийМесяцToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;
            var dtEnd = DateTime.Now.Date;

            if (dtBegin.Month != 1)
            {
                dtBegin = new DateTime(dtBegin.Year, dtBegin.Month - 1, 1);
                dtEnd = new DateTime(dtEnd.Year, dtEnd.Month - 1, DateTime.DaysInMonth(dtEnd.Year, dtEnd.Month - 1), 23, 59, 59);
            }
            else
            {
                dtBegin = new DateTime(dtBegin.Year - 1, 12, 1);
                dtEnd = new DateTime(dtEnd.Year - 1, 12, 31, 23, 59, 59);
            }

            dateTimePeriodReport(dtBegin, dtEnd);
        }

        private void предыдущийГодToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dtBegin = DateTime.Now.Date;
            var dtEnd = DateTime.Now.Date;

            dtBegin = new DateTime(dtBegin.Year - 1, 1, 1);
            dtEnd = new DateTime(dtEnd.Year - 1, 12, 31, 23, 59, 59);

            dateTimePeriodReport(dtBegin, dtEnd);
        }

        private void заВсёВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dtBegin = dataSet1.Tables["CashInOut"].Rows.OfType<DataRow>().
        Select(k => Convert.ToDateTime(k["DateTime"])).Min();

            var dtEnd = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            dateTimePeriodReport(dtBegin, dtEnd);
        }

        void dateTimePeriodReport(DateTime dtBegin, DateTime dtEnd)
        {
            toolStripDateTimeChooser1.Value = dtBegin;
            toolStripDateTimeChooser2.Value = dtEnd;

            RefreshReport();
        }

        private void toolStripSplitButton4_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButton4.ShowDropDown();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            toolStripDropDownButton1.ShowDropDown();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            if (toolStripButton13.Text == "Показывать выполненные")
            {
                toolStripButton13.Text = "Не показывать выполненные";
                GoalProgress();
            }
            else
            {
                toolStripButton13.Text = "Показывать выполненные";
                GoalProgress();
            }
        }

        private void преобразоватьВПереводToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            
            ClassElement.InOut = "Основной";
            ClassElement.Category = "";
            ClassElement.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            ClassElement.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            ClassElement.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            if(add_transfer())
            {
                var result = MessageBox.Show("Вы хотите удалить элемент на основании которого был создан перевод?",
                "Удаление элемента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
                cashInOutBindingSource.RemoveCurrent();

                SaveData();
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var convertToTransferButtonVisible = (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "Доход")
                                                 || (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "Расход");
            преобразоватьВПереводToolStripMenuItem.Available = convertToTransferButtonVisible;
            toolStripMenuItem6.Available = convertToTransferButtonVisible;
        }


        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public static uint GetIdleTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            GetLastInputInfo(ref LastUserAction);
            return ((uint)Environment.TickCount - LastUserAction.dwTime);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (GetIdleTime() > 5000)
              //  Application.Exit();//For Example
        }

        private void автоматическоеЗакрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formAutoClose = new FormAutoCloseApp())
            {
                formAutoClose.ShowDialog();

                ArxBuhSettingAction.WriteXml();
            }
        }
    }
}
