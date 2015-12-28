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

namespace ArxBuh
{
    public partial class Form_Main : Form
    {
        const string datafile = "data.xml";
        const string settingsfile = "settings.xml";

        #region Form action

        public Form_Main()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            ArxBuhSettingAction.ReadXml();

            loadData();

            using (var progressColumn = new DataGridViewProgressColumn { Width = 250, HeaderText = "Прогресс" })
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

            dateTimePicker1.Value = DateTime.Now.Date.AddMonths(1);

        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData();

            ArxBuhSettings.EncryptPassword = "";
            ArxBuhSettingAction.WriteXml();

            backup();
        }

        #endregion

        #region ToolStripButtonAction

        void AboutProgramTSB_Click(object sender, EventArgs e)
        {
            using (var about = new Form_AboutBox())
            {
                about.ShowDialog();
            }
        }

        void автоматическоеОбновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formUpdate = new Form_Update())
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
            arxDs.ds = dataSet1;

            using (var category = new Form_Category())
            {
                if (category.ShowDialog() == DialogResult.OK)
                {
                    dataSet1 = arxDs.ds;
                }

                saveData();
            }
        }

        void ExitTSB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Database action

        #region SaveLoad
        void saveData()
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

        void loadData()
        {
            if (!File.Exists(datafile)) return;

            dataSet1.Clear();

            if (!ArxBuhSettings.EncryptEnable)
            {
                dataSet1.ReadXml(datafile);

                dataGridView1.DataSource = cashInOutBindingSource;
                dataGridView4.DataSource = goalBindingSource;
            }
            else
            {
                do
                {
                    PasswordRequest();
                } while (LoadCryptDataSet(datafile, ArxBuhSettings.EncryptPassword, dataSet1));

                dataGridView1.DataSource = cashInOutBindingSource;
                dataGridView4.DataSource = goalBindingSource;
            }

            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[3], ListSortDirection.Ascending);
        }
        #endregion

        #region Шифрование

        void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formEncryption = new Form_Encryption())
            {
                formEncryption.ShowDialog();

                ArxBuhSettingAction.WriteXml();
            }
        }

        static void PasswordRequest()
        {
            using (var formRP = new Form_RequestPassword())
            {
                formRP.ShowDialog();
            }
        }

        bool LoadCryptDataSet(string file, string key, DataSet ds)
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
                    catch (Exception ex)
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

        void резервноеКопированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var backupForm = new Form_Backup())
            {
                backupForm.ShowDialog();

                ArxBuhSettingAction.WriteXml();
            }
        }

        static void backup()
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

            Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            add_element();
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

        void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DGPaint();
            InOutCalc();
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_element();
        }

        #endregion

        #region Учёт доходов-расходов (Add, Edit, Delete)

        void add_element()
        {
            arxDs.ds = dataSet1;

            using (var addEdit = new Form_AddEdit("Новый элемент"))
            {
                addEdit.ShowDialog();

                if (addEdit.DialogResult == DialogResult.OK)
                {
                    dataSet1.Tables["CashInOut"].Rows.Add(Class_element.InOut, Class_element.Category, Class_element.Date,
                        Class_element.Sum, Class_element.Comment);
                }

                saveData();
            }
        }

        void edit_element()
        {
            arxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow == null) return;

            Class_element.InOut = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Class_element.Category = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
            Class_element.Comment = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            using (var addEdit = new Form_AddEdit("Редактирование элемента"))
            {
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

            saveData();
        }

        #endregion

        void toolStripButton6_Click(object sender, EventArgs e)
        {
            Class_element.InOut = "Доход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            add_element();
        }

        void toolStripButton7_Click(object sender, EventArgs e)
        {
            Class_element.InOut = "Расход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

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
                DateBeginEnd.DateBegin,
                DateBeginEnd.DateEnd));

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
                DateBeginEnd.DateBegin = DateTime.Now.AddYears(-1);                
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
            Class_element.BudgetCheck = false;
            Class_element.InOut = "Доход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

            add_elementBudget();

        }

        void toolStripButton12_Click(object sender, EventArgs e)
        {
            Class_element.BudgetCheck = false;
            Class_element.InOut = "Расход";
            Class_element.Date = DateTime.Today;
            Class_element.Category = "";
            Class_element.Sum = 0;
            Class_element.Comment = "";

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
            saveData();
        }

        void toolStripButton4_Click(object sender, EventArgs e)
        {
            remove_elementBudget();
        }

        void add_elementBudget()
        {
            arxDs.ds = dataSet1;

            using (var aeb = new Form_AddEditBudget("Новый элемент"))
            {
                aeb.ShowDialog();

                if (aeb.DialogResult == DialogResult.OK)
                {
                    dataSet1.Tables["Budget"].Rows.Add(Class_element.BudgetCheck, Class_element.InOut,
                        Class_element.Category, Class_element.Date, Class_element.Sum, Class_element.Comment);
                }

                saveData();
            }
        }

        void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) edit_elementBudget();
        }

        void edit_elementBudget()
        {
            arxDs.ds = dataSet1;

            if (dataGridView1.CurrentRow == null) return;

            Class_element.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value);
            Class_element.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            Class_element.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            Class_element.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            using (var aeb = new Form_AddEditBudget("Редактирование элемента"))
            {
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
            double t = 0, i = 0, y = 0;

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
                    t = Convert.ToDouble(sumOUT) * (-1);
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
                    if ((bool)row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Доход")
                    {
                        i = i + (double)row.Cells[4].Value;
                    }
                    else if ((bool)row.Cells[0].Value != true && row.Cells[1].Value.ToString() == "Расход")
                    {
                        y = y + (double)row.Cells[4].Value;
                    }
                }

                labelResultBudget.Text = string.Format("Текущее {0} + Планируемые Доходы {1} - Планируемые Расходы {2} = {3}",
                                                  t.ToString("C2"), i.ToString("C2"), y.ToString("C2"), ((t + i) - y).ToString("C2"));
            }
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filterBudget();
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
            Class_element.BudgetCheck = Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value);
            Class_element.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            Class_element.Date = Class_element.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            Class_element.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            Class_element.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            add_elementBudget();
        }


        private void ввестиНаОснованииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            Class_element.InOut = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            Class_element.Category = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            Class_element.Date = DateTime.ParseExact(dataGridView2.CurrentRow.Cells[3].Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));
            Class_element.Sum = Convert.ToDouble(dataGridView2.CurrentRow.Cells[4].Value);
            Class_element.Comment = dataGridView2.CurrentRow.Cells[5].Value.ToString();

            var customerRow = ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Row;

            customerRow["Check"] = true;

            saveData();

            tabControl1.SelectedTab = tabPage1;

            add_element();
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

            var newGoalRow = ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Row;

            var name = newGoalRow["Name"].ToString();
            var allSum = newGoalRow["AllSum"].ToString();
            var comment = newGoalRow["Comment"].ToString();
            var HistoryID = newGoalRow["HistoryID"].ToString();

            using (
            var faeg = new Form_AddEditGoal(name, allSum, comment, HistoryID, dataSet1))
            {
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
            using (var faeg = new Form_AddEditGoal("", "", "", "-1", dataSet1))
            {
                if (faeg.ShowDialog() == DialogResult.OK)
                {
                    var newGoalRow = dataSet1.Tables["Goal"].NewRow();

                    newGoalRow["name"] = faeg.txb_GoalName.Text;
                    newGoalRow["AllSum"] = (faeg.txb_GoalSum.Text == "") ? "0" : faeg.txb_GoalSum.Text;
                    newGoalRow["Comment"] = faeg.txb_GoalComment.Text;

                    dataSet1.Tables["Goal"].Rows.Add(newGoalRow);
                }

                saveData();
            }
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
            saveData();
        }

        void GoalProgress()
        {
            double SumGoal = 0;
            double SumGoalRemaining = 0;

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Cells[2].Value.ToString() == ""
                    || row.Cells[2].Value == null
                    || Convert.ToDouble(row.Cells[1].Value) == 0)
                    continue;

                var x = (int)(Convert.ToDouble(row.Cells[2].Value) / (Convert.ToDouble(row.Cells[1].Value) / 100));
                var y = (int)(Convert.ToDouble(row.Cells[1].Value) - (Convert.ToDouble(row.Cells[2].Value)));

                SumGoal += Convert.ToDouble(row.Cells[1].Value);
                SumGoalRemaining += Convert.ToDouble(y);

                row.Cells[5].Value = x;
                row.Cells[6].Value = y.ToString("C2");
            }

            labelResultGoal.Text = string.Format("Всего целей на {0}, осталось собрать {1}", SumGoal.ToString("C2"), SumGoalRemaining.ToString("C2"));
        }

        void dataGridView4_Paint(object sender, PaintEventArgs e)
        {
            GoalProgress();
        }

        #endregion

        #region Отчёты
        void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
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
                StartDate = new DateTime(2011, 12, 12);
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
            using (var fhh = new Form_HotkeyHelper())
            {
                fhh.ShowDialog();
            }
        }

        #endregion

        private void tsbImportCSV_Click(object sender, EventArgs e)
        {
            var csvFile = "";

            using (var saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Title = "CSV - SAVE File";
                saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "CSV File|*.CSV|" + "All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    csvFile = saveFileDialog1.FileName;

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
        }

    }
}
