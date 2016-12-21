using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace ArxBuh
{
    partial class Form_Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.DeleteTSB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsbTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDateTimeChooser3 = new ArxBuh.ToolStripDateTimeChooser();
            this.toolStripSplitButton4 = new System.Windows.Forms.ToolStripSplitButton();
            this.сНачалаНеделиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сНачалаМесяцаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сНачалаГодаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripSeparator();
            this.предыдущаяНеделяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предыдущийМесяцToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предыдущийГодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.заВсёВремяToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDateTimeChooser4 = new ArxBuh.ToolStripDateTimeChooser();
            this.FilterClearTSB = new System.Windows.Forms.ToolStripButton();
            this.ExitTSB = new System.Windows.Forms.ToolStripButton();
            this.SettingsTSB = new System.Windows.Forms.ToolStripSplitButton();
            this.AboutProgramTSB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.категорииДоходовИРасходовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.СписокСчетовtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.резервноеКопированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шифрованиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.автоматическоеОбновлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripSeparator();
            this.справкаПоГорячимКлавишамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CalculatorTSB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.labelResultInOut = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.inOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.labelResultBudget = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelIn = new System.Windows.Forms.Label();
            this.labelOut = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.checkDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.inOutDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.budgetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new ArxBuh.DataSet1();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.tsb_AddGoal = new System.Windows.Forms.ToolStripButton();
            this.tsb_GoalDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton3 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelResultGoal = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDateTimeChooser1 = new ArxBuh.ToolStripDateTimeChooser();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.сНачалаНеделиToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сНачалаМесяцаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сНачалаГодаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.предыдущаяНеделяToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.предыдущийМесяцToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.предыдущийГодToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.заВсёВремяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDateTimeChooser2 = new ArxBuh.ToolStripDateTimeChooser();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.cashInOutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripSeparator();
            this.ввестиНаОснованииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.cashInOutBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalBindingSource)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashInOutBindingSource)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashInOutBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripButton7,
            this.DeleteTSB,
            this.toolStripSeparator11,
            this.toolsbTransfer,
            this.toolStripSeparator2,
            this.toolStripComboBox1,
            this.toolStripLabel5,
            this.toolStripDateTimeChooser3,
            this.toolStripSplitButton4,
            this.toolStripDateTimeChooser4,
            this.FilterClearTSB,
            this.ExitTSB,
            this.SettingsTSB,
            this.CalculatorTSB,
            this.toolStripSeparator8});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "Добавить";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton6.Text = "Доход";
            this.toolStripButton6.ToolTipText = "Добавить новый доход (Hotkey - Alt+Q)";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::ArxBuh.Properties.Resources.remove_32x32;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton7.Text = "Расход";
            this.toolStripButton7.ToolTipText = "Добавить новый расход (Hotkey - Alt+W)";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // DeleteTSB
            // 
            this.DeleteTSB.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.DeleteTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteTSB.Name = "DeleteTSB";
            this.DeleteTSB.Size = new System.Drawing.Size(71, 22);
            this.DeleteTSB.Text = "Удалить";
            this.DeleteTSB.ToolTipText = "Удалить выделенный в данный момент элемент (Hotkey - Alt+D)";
            this.DeleteTSB.Click += new System.EventHandler(this.DeleteTSB_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // toolsbTransfer
            // 
            this.toolsbTransfer.Image = global::ArxBuh.Properties.Resources.expand_32x32;
            this.toolsbTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsbTransfer.Name = "toolsbTransfer";
            this.toolsbTransfer.Size = new System.Drawing.Size(74, 22);
            this.toolsbTransfer.Text = "Перевод";
            this.toolsbTransfer.Click += new System.EventHandler(this.toolsbTransfer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoSize = false;
            this.toolStripComboBox1.DropDownWidth = 210;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Доход",
            "Расход",
            "Перевод"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(210, 23);
            this.toolStripComboBox1.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel5.Text = "C";
            // 
            // toolStripDateTimeChooser3
            // 
            this.toolStripDateTimeChooser3.AutoSize = false;
            this.toolStripDateTimeChooser3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDateTimeChooser3.Name = "toolStripDateTimeChooser3";
            this.toolStripDateTimeChooser3.Size = new System.Drawing.Size(96, 29);
            this.toolStripDateTimeChooser3.Text = "toolStripDateTimeChooser3";
            this.toolStripDateTimeChooser3.Value = new System.DateTime(2014, 2, 15, 21, 22, 26, 559);
            this.toolStripDateTimeChooser3.ValueChanged += new System.EventHandler(this.toolStripDateTimeChooser3_ValueChanged);
            // 
            // toolStripSplitButton4
            // 
            this.toolStripSplitButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сНачалаНеделиToolStripMenuItem,
            this.сНачалаМесяцаToolStripMenuItem,
            this.сНачалаГодаToolStripMenuItem,
            this.toolStripMenuItem22,
            this.предыдущаяНеделяToolStripMenuItem,
            this.предыдущийМесяцToolStripMenuItem,
            this.предыдущийГодToolStripMenuItem,
            this.toolStripMenuItem5,
            this.заВсёВремяToolStripMenuItem1});
            this.toolStripSplitButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton4.Image")));
            this.toolStripSplitButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton4.Name = "toolStripSplitButton4";
            this.toolStripSplitButton4.Size = new System.Drawing.Size(37, 22);
            this.toolStripSplitButton4.Text = "по";
            this.toolStripSplitButton4.ButtonClick += new System.EventHandler(this.toolStripSplitButton4_ButtonClick);
            // 
            // сНачалаНеделиToolStripMenuItem
            // 
            this.сНачалаНеделиToolStripMenuItem.Name = "сНачалаНеделиToolStripMenuItem";
            this.сНачалаНеделиToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.сНачалаНеделиToolStripMenuItem.Text = "С начала недели";
            this.сНачалаНеделиToolStripMenuItem.Click += new System.EventHandler(this.сНачалаНеделиToolStripMenuItem_Click);
            // 
            // сНачалаМесяцаToolStripMenuItem
            // 
            this.сНачалаМесяцаToolStripMenuItem.Name = "сНачалаМесяцаToolStripMenuItem";
            this.сНачалаМесяцаToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.сНачалаМесяцаToolStripMenuItem.Text = "С начала месяца";
            this.сНачалаМесяцаToolStripMenuItem.Click += new System.EventHandler(this.сНачалаМесяцаToolStripMenuItem_Click);
            // 
            // сНачалаГодаToolStripMenuItem
            // 
            this.сНачалаГодаToolStripMenuItem.Name = "сНачалаГодаToolStripMenuItem";
            this.сНачалаГодаToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.сНачалаГодаToolStripMenuItem.Text = "С начала года";
            this.сНачалаГодаToolStripMenuItem.Click += new System.EventHandler(this.сНачалаГодаToolStripMenuItem_Click);
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Size = new System.Drawing.Size(184, 6);
            // 
            // предыдущаяНеделяToolStripMenuItem
            // 
            this.предыдущаяНеделяToolStripMenuItem.Name = "предыдущаяНеделяToolStripMenuItem";
            this.предыдущаяНеделяToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.предыдущаяНеделяToolStripMenuItem.Text = "Предыдущая неделя";
            this.предыдущаяНеделяToolStripMenuItem.Click += new System.EventHandler(this.предыдущаяНеделяToolStripMenuItem_Click);
            // 
            // предыдущийМесяцToolStripMenuItem
            // 
            this.предыдущийМесяцToolStripMenuItem.Name = "предыдущийМесяцToolStripMenuItem";
            this.предыдущийМесяцToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.предыдущийМесяцToolStripMenuItem.Text = "Предыдущий месяц";
            this.предыдущийМесяцToolStripMenuItem.Click += new System.EventHandler(this.предыдущийМесяцToolStripMenuItem_Click);
            // 
            // предыдущийГодToolStripMenuItem
            // 
            this.предыдущийГодToolStripMenuItem.Name = "предыдущийГодToolStripMenuItem";
            this.предыдущийГодToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.предыдущийГодToolStripMenuItem.Text = "Предыдущий год";
            this.предыдущийГодToolStripMenuItem.Click += new System.EventHandler(this.предыдущийГодToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(184, 6);
            // 
            // заВсёВремяToolStripMenuItem1
            // 
            this.заВсёВремяToolStripMenuItem1.Name = "заВсёВремяToolStripMenuItem1";
            this.заВсёВремяToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.заВсёВремяToolStripMenuItem1.Text = "За всё время";
            this.заВсёВремяToolStripMenuItem1.Click += new System.EventHandler(this.заВсёВремяToolStripMenuItem1_Click);
            // 
            // toolStripDateTimeChooser4
            // 
            this.toolStripDateTimeChooser4.AutoSize = false;
            this.toolStripDateTimeChooser4.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDateTimeChooser4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripDateTimeChooser4.Name = "toolStripDateTimeChooser4";
            this.toolStripDateTimeChooser4.Size = new System.Drawing.Size(96, 29);
            this.toolStripDateTimeChooser4.Text = "toolStripDateTimeChooser4";
            this.toolStripDateTimeChooser4.Value = new System.DateTime(2014, 2, 15, 21, 22, 26, 565);
            this.toolStripDateTimeChooser4.ValueChanged += new System.EventHandler(this.toolStripDateTimeChooser4_ValueChanged);
            // 
            // FilterClearTSB
            // 
            this.FilterClearTSB.Image = global::ArxBuh.Properties.Resources.expand_32x32;
            this.FilterClearTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FilterClearTSB.Name = "FilterClearTSB";
            this.FilterClearTSB.Size = new System.Drawing.Size(123, 22);
            this.FilterClearTSB.Text = "Очистить фильтр";
            this.FilterClearTSB.Click += new System.EventHandler(this.FilterClearTSB_Click);
            // 
            // ExitTSB
            // 
            this.ExitTSB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ExitTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExitTSB.Image = global::ArxBuh.Properties.Resources.door_32x32;
            this.ExitTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExitTSB.Name = "ExitTSB";
            this.ExitTSB.Size = new System.Drawing.Size(23, 22);
            this.ExitTSB.Text = "Выход";
            this.ExitTSB.Click += new System.EventHandler(this.ExitTSB_Click);
            // 
            // SettingsTSB
            // 
            this.SettingsTSB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SettingsTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SettingsTSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutProgramTSB,
            this.toolStripMenuItem1,
            this.категорииДоходовИРасходовToolStripMenuItem,
            this.СписокСчетовtoolStripMenuItem,
            this.toolStripMenuItem2,
            this.tsbImportCSV,
            this.резервноеКопированиеToolStripMenuItem,
            this.шифрованиеToolStripMenuItem,
            this.автоматическоеОбновлениеToolStripMenuItem,
            this.toolStripMenuItem21,
            this.справкаПоГорячимКлавишамToolStripMenuItem});
            this.SettingsTSB.Image = global::ArxBuh.Properties.Resources.gear_wheel_32x32;
            this.SettingsTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsTSB.Name = "SettingsTSB";
            this.SettingsTSB.Size = new System.Drawing.Size(32, 22);
            this.SettingsTSB.Text = "Дополнительно";
            this.SettingsTSB.Click += new System.EventHandler(this.SettingsTSB_ButtonClick);
            // 
            // AboutProgramTSB
            // 
            this.AboutProgramTSB.Image = global::ArxBuh.Properties.Resources.help_32x32;
            this.AboutProgramTSB.Name = "AboutProgramTSB";
            this.AboutProgramTSB.Size = new System.Drawing.Size(249, 22);
            this.AboutProgramTSB.Text = "О Программе";
            this.AboutProgramTSB.Click += new System.EventHandler(this.AboutProgramTSB_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(246, 6);
            // 
            // категорииДоходовИРасходовToolStripMenuItem
            // 
            this.категорииДоходовИРасходовToolStripMenuItem.Name = "категорииДоходовИРасходовToolStripMenuItem";
            this.категорииДоходовИРасходовToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.категорииДоходовИРасходовToolStripMenuItem.Text = "Категории доходов и расходов";
            this.категорииДоходовИРасходовToolStripMenuItem.Click += new System.EventHandler(this.категорииДоходовИРасходовToolStripMenuItem_Click);
            // 
            // СписокСчетовtoolStripMenuItem
            // 
            this.СписокСчетовtoolStripMenuItem.Name = "СписокСчетовtoolStripMenuItem";
            this.СписокСчетовtoolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.СписокСчетовtoolStripMenuItem.Text = "Список счетов";
            this.СписокСчетовtoolStripMenuItem.Click += new System.EventHandler(this.СписокСчетовtoolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(246, 6);
            // 
            // tsbImportCSV
            // 
            this.tsbImportCSV.Name = "tsbImportCSV";
            this.tsbImportCSV.Size = new System.Drawing.Size(249, 22);
            this.tsbImportCSV.Text = "Выгрузить в CSV файл";
            this.tsbImportCSV.Click += new System.EventHandler(this.tsbImportCSV_Click);
            // 
            // резервноеКопированиеToolStripMenuItem
            // 
            this.резервноеКопированиеToolStripMenuItem.Name = "резервноеКопированиеToolStripMenuItem";
            this.резервноеКопированиеToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.резервноеКопированиеToolStripMenuItem.Text = "Резервное копирование";
            this.резервноеКопированиеToolStripMenuItem.Click += new System.EventHandler(this.резервноеКопированиеToolStripMenuItem_Click);
            // 
            // шифрованиеToolStripMenuItem
            // 
            this.шифрованиеToolStripMenuItem.Name = "шифрованиеToolStripMenuItem";
            this.шифрованиеToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.шифрованиеToolStripMenuItem.Text = "Шифрование";
            this.шифрованиеToolStripMenuItem.Click += new System.EventHandler(this.шифрованиеToolStripMenuItem_Click);
            // 
            // автоматическоеОбновлениеToolStripMenuItem
            // 
            this.автоматическоеОбновлениеToolStripMenuItem.Name = "автоматическоеОбновлениеToolStripMenuItem";
            this.автоматическоеОбновлениеToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.автоматическоеОбновлениеToolStripMenuItem.Text = "Автоматическое обновление";
            this.автоматическоеОбновлениеToolStripMenuItem.Click += new System.EventHandler(this.автоматическоеОбновлениеToolStripMenuItem_Click);
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(246, 6);
            // 
            // справкаПоГорячимКлавишамToolStripMenuItem
            // 
            this.справкаПоГорячимКлавишамToolStripMenuItem.Name = "справкаПоГорячимКлавишамToolStripMenuItem";
            this.справкаПоГорячимКлавишамToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.справкаПоГорячимКлавишамToolStripMenuItem.Text = "Справка по горячим клавишам";
            this.справкаПоГорячимКлавишамToolStripMenuItem.Click += new System.EventHandler(this.справкаПоГорячимКлавишамToolStripMenuItem_Click);
            // 
            // CalculatorTSB
            // 
            this.CalculatorTSB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CalculatorTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CalculatorTSB.Image = global::ArxBuh.Properties.Resources.calculator_32x32;
            this.CalculatorTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CalculatorTSB.Name = "CalculatorTSB";
            this.CalculatorTSB.Size = new System.Drawing.Size(23, 22);
            this.CalculatorTSB.Text = "Калькулятор";
            this.CalculatorTSB.Click += new System.EventHandler(this.CalculatorTSB_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // labelResultInOut
            // 
            this.labelResultInOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResultInOut.AutoSize = true;
            this.labelResultInOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResultInOut.Location = new System.Drawing.Point(20, 414);
            this.labelResultInOut.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.labelResultInOut.Name = "labelResultInOut";
            this.labelResultInOut.Size = new System.Drawing.Size(18, 20);
            this.labelResultInOut.TabIndex = 3;
            this.labelResultInOut.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1058, 512);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(1052, 506);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1044, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Доходы-Расходы";
            this.tabPage1.ToolTipText = "Доходы-Расходы (HotKey - Alt+1)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labelResultInOut, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 28);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1038, 449);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.inOutDataGridViewTextBoxColumn,
            this.categoryDataGridViewTextBoxColumn,
            this.dateTimeDataGridViewTextBoxColumn,
            this.sumDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1032, 393);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // inOutDataGridViewTextBoxColumn
            // 
            this.inOutDataGridViewTextBoxColumn.DataPropertyName = "InOut";
            this.inOutDataGridViewTextBoxColumn.FillWeight = 80F;
            this.inOutDataGridViewTextBoxColumn.HeaderText = "*";
            this.inOutDataGridViewTextBoxColumn.Name = "inOutDataGridViewTextBoxColumn";
            this.inOutDataGridViewTextBoxColumn.ReadOnly = true;
            this.inOutDataGridViewTextBoxColumn.Width = 80;
            // 
            // categoryDataGridViewTextBoxColumn
            // 
            this.categoryDataGridViewTextBoxColumn.DataPropertyName = "Category";
            this.categoryDataGridViewTextBoxColumn.HeaderText = "Категория";
            this.categoryDataGridViewTextBoxColumn.Name = "categoryDataGridViewTextBoxColumn";
            this.categoryDataGridViewTextBoxColumn.ReadOnly = true;
            this.categoryDataGridViewTextBoxColumn.Width = 180;
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            this.dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.dateTimeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateTimeDataGridViewTextBoxColumn.FillWeight = 80F;
            this.dateTimeDataGridViewTextBoxColumn.HeaderText = "Дата";
            this.dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            this.dateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateTimeDataGridViewTextBoxColumn.Width = 70;
            // 
            // sumDataGridViewTextBoxColumn
            // 
            this.sumDataGridViewTextBoxColumn.DataPropertyName = "Sum";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.sumDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sumDataGridViewTextBoxColumn.FillWeight = 80F;
            this.sumDataGridViewTextBoxColumn.HeaderText = "Сумма";
            this.sumDataGridViewTextBoxColumn.Name = "sumDataGridViewTextBoxColumn";
            this.sumDataGridViewTextBoxColumn.ReadOnly = true;
            this.sumDataGridViewTextBoxColumn.Width = 85;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Комментарий";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1044, 480);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Бюджет";
            this.tabPage2.ToolTipText = "Бюджет (HotKey - Alt+2)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.toolStrip3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.dataGridView2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1038, 474);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.tableLayoutPanel3.SetColumnSpan(this.toolStrip3, 2);
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton12,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSplitButton1,
            this.toolStripSeparator7,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip3.TabIndex = 9;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton3.Text = "&Доход";
            this.toolStripButton3.ToolTipText = "Добавить новый доход в Бюджет  (Hotkey - Alt+Q)";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = global::ArxBuh.Properties.Resources.remove_32x32;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton12.Text = "&Расход";
            this.toolStripButton12.ToolTipText = "Добавить новый расход в Бюджет  (Hotkey - Alt+W)\r\n";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton4.Text = "Удалить";
            this.toolStripButton4.ToolTipText = "Удалить выделенный в данный момент элемент  (Hotkey - Alt+D)";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::ArxBuh.Properties.Resources.door_32x32;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Выход";
            this.toolStripButton5.Click += new System.EventHandler(this.ExitTSB_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = global::ArxBuh.Properties.Resources.gear_wheel_32x32;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "Дополнительно";
            this.toolStripSplitButton1.Click += new System.EventHandler(this.SettingsTSB_ButtonClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::ArxBuh.Properties.Resources.filter_32x32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(166, 22);
            this.toolStripButton1.Text = "Показывать выполненые";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::ArxBuh.Properties.Resources.calculator_32x32;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Калькулятор";
            this.toolStripButton2.Click += new System.EventHandler(this.CalculatorTSB_Click);
            // 
            // panel1
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.labelResultBudget);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelIn);
            this.panel1.Controls.Add(this.labelOut);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 427);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 44);
            this.panel1.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(65, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(130, 20);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.Value = new System.DateTime(2014, 3, 8, 23, 14, 17, 497);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "На дату";
            // 
            // labelResultBudget
            // 
            this.labelResultBudget.AutoSize = true;
            this.labelResultBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResultBudget.Location = new System.Drawing.Point(201, 11);
            this.labelResultBudget.Name = "labelResultBudget";
            this.labelResultBudget.Size = new System.Drawing.Size(18, 20);
            this.labelResultBudget.TabIndex = 1;
            this.labelResultBudget.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(484, -170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сальдо";
            // 
            // labelIn
            // 
            this.labelIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelIn.AutoSize = true;
            this.labelIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIn.Location = new System.Drawing.Point(246, -166);
            this.labelIn.Name = "labelIn";
            this.labelIn.Size = new System.Drawing.Size(18, 20);
            this.labelIn.TabIndex = 4;
            this.labelIn.Text = "0";
            // 
            // labelOut
            // 
            this.labelOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOut.AutoSize = true;
            this.labelOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOut.Location = new System.Drawing.Point(386, -166);
            this.labelOut.Name = "labelOut";
            this.labelOut.Size = new System.Drawing.Size(18, 20);
            this.labelOut.TabIndex = 5;
            this.labelOut.Text = "0";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkDataGridViewCheckBoxColumn,
            this.inOutDataGridViewTextBoxColumn1,
            this.categoryDataGridViewTextBoxColumn1,
            this.dateTimeDataGridViewTextBoxColumn1,
            this.sumDataGridViewTextBoxColumn1,
            this.commentDataGridViewTextBoxColumn1});
            this.tableLayoutPanel3.SetColumnSpan(this.dataGridView2, 2);
            this.dataGridView2.DataSource = this.budgetBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 28);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1032, 393);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            this.dataGridView2.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView2_Paint_1);
            // 
            // checkDataGridViewCheckBoxColumn
            // 
            this.checkDataGridViewCheckBoxColumn.DataPropertyName = "Check";
            this.checkDataGridViewCheckBoxColumn.HeaderText = "Выполнено";
            this.checkDataGridViewCheckBoxColumn.Name = "checkDataGridViewCheckBoxColumn";
            this.checkDataGridViewCheckBoxColumn.Width = 80;
            // 
            // inOutDataGridViewTextBoxColumn1
            // 
            this.inOutDataGridViewTextBoxColumn1.DataPropertyName = "InOut";
            this.inOutDataGridViewTextBoxColumn1.HeaderText = "Доход-Расход";
            this.inOutDataGridViewTextBoxColumn1.Name = "inOutDataGridViewTextBoxColumn1";
            this.inOutDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // categoryDataGridViewTextBoxColumn1
            // 
            this.categoryDataGridViewTextBoxColumn1.DataPropertyName = "Category";
            this.categoryDataGridViewTextBoxColumn1.HeaderText = "Категория";
            this.categoryDataGridViewTextBoxColumn1.Name = "categoryDataGridViewTextBoxColumn1";
            this.categoryDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dateTimeDataGridViewTextBoxColumn1
            // 
            this.dateTimeDataGridViewTextBoxColumn1.DataPropertyName = "DateTime";
            this.dateTimeDataGridViewTextBoxColumn1.HeaderText = "Дата";
            this.dateTimeDataGridViewTextBoxColumn1.Name = "dateTimeDataGridViewTextBoxColumn1";
            this.dateTimeDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dateTimeDataGridViewTextBoxColumn1.Width = 80;
            // 
            // sumDataGridViewTextBoxColumn1
            // 
            this.sumDataGridViewTextBoxColumn1.DataPropertyName = "Sum";
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.sumDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.sumDataGridViewTextBoxColumn1.HeaderText = "Сумма";
            this.sumDataGridViewTextBoxColumn1.Name = "sumDataGridViewTextBoxColumn1";
            this.sumDataGridViewTextBoxColumn1.ReadOnly = true;
            this.sumDataGridViewTextBoxColumn1.Width = 80;
            // 
            // commentDataGridViewTextBoxColumn1
            // 
            this.commentDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn1.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn1.HeaderText = "Комментарий";
            this.commentDataGridViewTextBoxColumn1.Name = "commentDataGridViewTextBoxColumn1";
            this.commentDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // budgetBindingSource
            // 
            this.budgetBindingSource.DataMember = "Budget";
            this.budgetBindingSource.DataSource = this.dataSet1;
            this.budgetBindingSource.Filter = "Check = false";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1044, 480);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Цели";
            this.tabPage3.ToolTipText = "Цели (HotKey - Alt+3)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.toolStrip4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.dataGridView4, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelResultGoal, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1038, 474);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // toolStrip4
            // 
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AddGoal,
            this.tsb_GoalDelete,
            this.toolStripButton11,
            this.toolStripSplitButton3,
            this.toolStripButton9,
            this.toolStripSeparator1,
            this.toolStripButton13});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip4.TabIndex = 2;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // tsb_AddGoal
            // 
            this.tsb_AddGoal.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.tsb_AddGoal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AddGoal.Name = "tsb_AddGoal";
            this.tsb_AddGoal.Size = new System.Drawing.Size(79, 22);
            this.tsb_AddGoal.Text = "Добавить";
            this.tsb_AddGoal.ToolTipText = "Добавить новую цель  (Hotkey - Alt+Q)";
            this.tsb_AddGoal.Click += new System.EventHandler(this.tsb_AddGoal_Click_1);
            // 
            // tsb_GoalDelete
            // 
            this.tsb_GoalDelete.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.tsb_GoalDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GoalDelete.Name = "tsb_GoalDelete";
            this.tsb_GoalDelete.Size = new System.Drawing.Size(71, 22);
            this.tsb_GoalDelete.Text = "Удалить";
            this.tsb_GoalDelete.ToolTipText = "Удалить выделенный в данный момент элемент  (Hotkey - Alt+D)";
            this.tsb_GoalDelete.Click += new System.EventHandler(this.tsb_GoalDelete_Click);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = global::ArxBuh.Properties.Resources.door_32x32;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "toolStripButton11";
            this.toolStripButton11.ToolTipText = "Выход";
            this.toolStripButton11.Click += new System.EventHandler(this.ExitTSB_Click);
            // 
            // toolStripSplitButton3
            // 
            this.toolStripSplitButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton3.Image = global::ArxBuh.Properties.Resources.gear_wheel_32x32;
            this.toolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton3.Name = "toolStripSplitButton3";
            this.toolStripSplitButton3.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton3.Text = "Дополнительно";
            this.toolStripSplitButton3.Click += new System.EventHandler(this.SettingsTSB_ButtonClick);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::ArxBuh.Properties.Resources.calculator_32x32;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "Калькулятор";
            this.toolStripButton9.Click += new System.EventHandler(this.CalculatorTSB_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.Image = global::ArxBuh.Properties.Resources.filter_32x32;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(173, 22);
            this.toolStripButton13.Text = "Показывать выполненные";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.allSumDataGridViewTextBoxColumn,
            this.historyDataGridViewTextBoxColumn,
            this.historyIDDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn3});
            this.dataGridView4.DataSource = this.goalBindingSource;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(3, 28);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dataGridView4.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(1032, 393);
            this.dataGridView4.TabIndex = 1;
            this.dataGridView4.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellDoubleClick);
            this.dataGridView4.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView4_CellMouseClick);
            this.dataGridView4.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView4_Paint);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Цель";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 250;
            // 
            // allSumDataGridViewTextBoxColumn
            // 
            this.allSumDataGridViewTextBoxColumn.DataPropertyName = "AllSum";
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.allSumDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.allSumDataGridViewTextBoxColumn.HeaderText = "Сумма";
            this.allSumDataGridViewTextBoxColumn.Name = "allSumDataGridViewTextBoxColumn";
            this.allSumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // historyDataGridViewTextBoxColumn
            // 
            this.historyDataGridViewTextBoxColumn.DataPropertyName = "History";
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.historyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.historyDataGridViewTextBoxColumn.HeaderText = "Внесено";
            this.historyDataGridViewTextBoxColumn.Name = "historyDataGridViewTextBoxColumn";
            this.historyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // historyIDDataGridViewTextBoxColumn
            // 
            this.historyIDDataGridViewTextBoxColumn.DataPropertyName = "HistoryID";
            this.historyIDDataGridViewTextBoxColumn.HeaderText = "HistoryID";
            this.historyIDDataGridViewTextBoxColumn.Name = "historyIDDataGridViewTextBoxColumn";
            this.historyIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.historyIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // commentDataGridViewTextBoxColumn3
            // 
            this.commentDataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn3.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn3.HeaderText = "Комментарий";
            this.commentDataGridViewTextBoxColumn3.Name = "commentDataGridViewTextBoxColumn3";
            this.commentDataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // goalBindingSource
            // 
            this.goalBindingSource.DataMember = "Goal";
            this.goalBindingSource.DataSource = this.dataSet1;
            // 
            // labelResultGoal
            // 
            this.labelResultGoal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResultGoal.AutoSize = true;
            this.labelResultGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResultGoal.Location = new System.Drawing.Point(20, 439);
            this.labelResultGoal.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.labelResultGoal.Name = "labelResultGoal";
            this.labelResultGoal.Size = new System.Drawing.Size(18, 20);
            this.labelResultGoal.TabIndex = 3;
            this.labelResultGoal.Text = "0";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.reportViewer1);
            this.tabPage4.Controls.Add(this.toolStrip5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1044, 480);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Отчёты";
            this.tabPage4.ToolTipText = "Отчёты (HotKey - Alt+4)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(3, 28);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1038, 449);
            this.reportViewer1.TabIndex = 1;
            // 
            // toolStrip5
            // 
            this.toolStrip5.AutoSize = false;
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBox2,
            this.toolStripSeparator6,
            this.toolStripLabel2,
            this.toolStripDateTimeChooser1,
            this.toolStripDropDownButton1,
            this.toolStripDateTimeChooser2,
            this.toolStripButton8,
            this.toolStripSplitButton2,
            this.toolStripButton10});
            this.toolStrip5.Location = new System.Drawing.Point(3, 3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel1.Text = "Показать отчёт";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "Расходы",
            "Доходы"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(200, 25);
            this.toolStripComboBox2.TextChanged += new System.EventHandler(this.toolStripComboBox2_TextChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel2.Text = "С";
            // 
            // toolStripDateTimeChooser1
            // 
            this.toolStripDateTimeChooser1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDateTimeChooser1.Margin = new System.Windows.Forms.Padding(0, -1, 0, 2);
            this.toolStripDateTimeChooser1.Name = "toolStripDateTimeChooser1";
            this.toolStripDateTimeChooser1.Size = new System.Drawing.Size(96, 24);
            this.toolStripDateTimeChooser1.Text = "toolStripDateTimeChooser1";
            this.toolStripDateTimeChooser1.Value = new System.DateTime(2014, 2, 15, 21, 22, 26, 959);
            this.toolStripDateTimeChooser1.ValueChanged += new System.EventHandler(this.toolStripComboBox2_TextChanged);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сНачалаНеделиToolStripMenuItem1,
            this.сНачалаМесяцаToolStripMenuItem1,
            this.сНачалаГодаToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.предыдущаяНеделяToolStripMenuItem1,
            this.предыдущийМесяцToolStripMenuItem1,
            this.предыдущийГодToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.заВсёВремяToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 22);
            this.toolStripDropDownButton1.Text = "по";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // сНачалаНеделиToolStripMenuItem1
            // 
            this.сНачалаНеделиToolStripMenuItem1.Name = "сНачалаНеделиToolStripMenuItem1";
            this.сНачалаНеделиToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.сНачалаНеделиToolStripMenuItem1.Text = "С начала недели";
            this.сНачалаНеделиToolStripMenuItem1.Click += new System.EventHandler(this.сНачалаНеделиToolStripMenuItem1_Click);
            // 
            // сНачалаМесяцаToolStripMenuItem1
            // 
            this.сНачалаМесяцаToolStripMenuItem1.Name = "сНачалаМесяцаToolStripMenuItem1";
            this.сНачалаМесяцаToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.сНачалаМесяцаToolStripMenuItem1.Text = "С начала месяца";
            this.сНачалаМесяцаToolStripMenuItem1.Click += new System.EventHandler(this.сНачалаМесяцаToolStripMenuItem1_Click);
            // 
            // сНачалаГодаToolStripMenuItem1
            // 
            this.сНачалаГодаToolStripMenuItem1.Name = "сНачалаГодаToolStripMenuItem1";
            this.сНачалаГодаToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.сНачалаГодаToolStripMenuItem1.Text = "С начала года";
            this.сНачалаГодаToolStripMenuItem1.Click += new System.EventHandler(this.сНачалаГодаToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 6);
            // 
            // предыдущаяНеделяToolStripMenuItem1
            // 
            this.предыдущаяНеделяToolStripMenuItem1.Name = "предыдущаяНеделяToolStripMenuItem1";
            this.предыдущаяНеделяToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.предыдущаяНеделяToolStripMenuItem1.Text = "Предыдущая неделя";
            this.предыдущаяНеделяToolStripMenuItem1.Click += new System.EventHandler(this.предыдущаяНеделяToolStripMenuItem1_Click);
            // 
            // предыдущийМесяцToolStripMenuItem1
            // 
            this.предыдущийМесяцToolStripMenuItem1.Name = "предыдущийМесяцToolStripMenuItem1";
            this.предыдущийМесяцToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.предыдущийМесяцToolStripMenuItem1.Text = "Предыдущий месяц";
            this.предыдущийМесяцToolStripMenuItem1.Click += new System.EventHandler(this.предыдущийМесяцToolStripMenuItem1_Click);
            // 
            // предыдущийГодToolStripMenuItem1
            // 
            this.предыдущийГодToolStripMenuItem1.Name = "предыдущийГодToolStripMenuItem1";
            this.предыдущийГодToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.предыдущийГодToolStripMenuItem1.Text = "Предыдущий год";
            this.предыдущийГодToolStripMenuItem1.Click += new System.EventHandler(this.предыдущийГодToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(184, 6);
            // 
            // заВсёВремяToolStripMenuItem
            // 
            this.заВсёВремяToolStripMenuItem.Name = "заВсёВремяToolStripMenuItem";
            this.заВсёВремяToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.заВсёВремяToolStripMenuItem.Text = "За всё время";
            this.заВсёВремяToolStripMenuItem.Click += new System.EventHandler(this.заВсёВремяToolStripMenuItem_Click);
            // 
            // toolStripDateTimeChooser2
            // 
            this.toolStripDateTimeChooser2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDateTimeChooser2.Margin = new System.Windows.Forms.Padding(0, -1, 0, 2);
            this.toolStripDateTimeChooser2.Name = "toolStripDateTimeChooser2";
            this.toolStripDateTimeChooser2.Size = new System.Drawing.Size(96, 24);
            this.toolStripDateTimeChooser2.Text = "toolStripDateTimeChooser2";
            this.toolStripDateTimeChooser2.Value = new System.DateTime(2014, 2, 15, 21, 22, 26, 963);
            this.toolStripDateTimeChooser2.ValueChanged += new System.EventHandler(this.toolStripComboBox2_TextChanged);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::ArxBuh.Properties.Resources.door_32x32;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "Выход";
            this.toolStripButton8.Click += new System.EventHandler(this.ExitTSB_Click);
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton2.Image = global::ArxBuh.Properties.Resources.gear_wheel_32x32;
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
            this.toolStripSplitButton2.ToolTipText = "Дополнительно";
            this.toolStripSplitButton2.Click += new System.EventHandler(this.SettingsTSB_ButtonClick);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::ArxBuh.Properties.Resources.calculator_32x32;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "Калькулятор";
            this.toolStripButton10.ToolTipText = "Калькулятор";
            this.toolStripButton10.Click += new System.EventHandler(this.CalculatorTSB_Click);
            // 
            // cashInOutBindingSource
            // 
            this.cashInOutBindingSource.DataMember = "CashInOut";
            this.cashInOutBindingSource.DataSource = this.dataSet1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem19,
            this.toolStripSeparator9,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.повторитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 120);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem11.Text = "Новый доход";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.Image = global::ArxBuh.Properties.Resources.remove_32x32;
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem19.Text = "Новый расход";
            this.toolStripMenuItem19.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(151, 6);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Image = global::ArxBuh.Properties.Resources.edit_32x32;
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // повторитьToolStripMenuItem
            // 
            this.повторитьToolStripMenuItem.Image = global::ArxBuh.Properties.Resources.redo_32x32;
            this.повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            this.повторитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.повторитьToolStripMenuItem.Text = "Повторить";
            this.повторитьToolStripMenuItem.Click += new System.EventHandler(this.повторитьToolStripMenuItem_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Sum";
            dataGridViewCellStyle8.NullValue = "0";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.FillWeight = 80F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 15;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn2.HeaderText = "Комментарий";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DateTime";
            this.dataGridViewTextBoxColumn3.FillWeight = 80F;
            this.dataGridViewTextBoxColumn3.HeaderText = "DateTime";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Comment";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.FillWeight = 80F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Goal";
            this.dataGridViewTextBoxColumn5.HeaderText = "Goal";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "InOut";
            this.dataGridViewTextBoxColumn6.HeaderText = "InOut";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Category";
            this.dataGridViewTextBoxColumn7.HeaderText = "Category";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 75;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "DateTime";
            this.dataGridViewTextBoxColumn8.HeaderText = "DateTime";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 50;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Sum";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn9.HeaderText = "Sum";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 50;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn10.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "InOut";
            this.dataGridViewTextBoxColumn11.HeaderText = "InOut";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Category";
            this.dataGridViewTextBoxColumn12.HeaderText = "Category";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 75;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "DateTime";
            this.dataGridViewTextBoxColumn13.HeaderText = "DateTime";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 50;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Sum";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn14.HeaderText = "Sum";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 50;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn15.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Visible = false;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn16.HeaderText = "Name";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 250;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "AllSum";
            this.dataGridViewTextBoxColumn17.HeaderText = "AllSum";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "History";
            this.dataGridViewTextBoxColumn18.HeaderText = "History";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "HistoryID";
            this.dataGridViewTextBoxColumn19.HeaderText = "HistoryID";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Visible = false;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn20.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem17,
            this.toolStripMenuItem20,
            this.toolStripSeparator10,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem23,
            this.ввестиНаОснованииToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(234, 148);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem17.Text = "Новый планируемый доход";
            this.toolStripMenuItem17.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.Image = global::ArxBuh.Properties.Resources.remove_32x32;
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem20.Text = "Новый планируемый расход";
            this.toolStripMenuItem20.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(230, 6);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Image = global::ArxBuh.Properties.Resources.edit_32x32;
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem12.Text = "Редактировать";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem13.Text = "Удалить";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Image = global::ArxBuh.Properties.Resources.redo_32x32;
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem14.Text = "Повторить";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.toolStripMenuItem14_Click);
            // 
            // toolStripMenuItem23
            // 
            this.toolStripMenuItem23.Name = "toolStripMenuItem23";
            this.toolStripMenuItem23.Size = new System.Drawing.Size(230, 6);
            // 
            // ввестиНаОснованииToolStripMenuItem
            // 
            this.ввестиНаОснованииToolStripMenuItem.Image = global::ArxBuh.Properties.Resources.flash_32x32;
            this.ввестиНаОснованииToolStripMenuItem.Name = "ввестиНаОснованииToolStripMenuItem";
            this.ввестиНаОснованииToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.ввестиНаОснованииToolStripMenuItem.Text = "Ввести на основании";
            this.ввестиНаОснованииToolStripMenuItem.Click += new System.EventHandler(this.ввестиНаОснованииToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem18,
            this.toolStripMenuItem15,
            this.toolStripMenuItem16});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(155, 70);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Image = global::ArxBuh.Properties.Resources.add_32x32;
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem18.Text = "Добавить";
            this.toolStripMenuItem18.Click += new System.EventHandler(this.tsb_AddGoal_Click_1);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Image = global::ArxBuh.Properties.Resources.edit_32x32;
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem15.Text = "Редактировать";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.toolStripMenuItem15_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Image = global::ArxBuh.Properties.Resources.delete_32x32;
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem16.Text = "Удалить";
            this.toolStripMenuItem16.Click += new System.EventHandler(this.toolStripMenuItem16_Click);
            // 
            // cashInOutBindingSource1
            // 
            this.cashInOutBindingSource1.DataMember = "CashInOut";
            this.cashInOutBindingSource1.DataSource = this.dataSet1;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 460);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "ArxBuh";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalBindingSource)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashInOutBindingSource)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cashInOutBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton DeleteTSB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label labelResultInOut;
        private System.Windows.Forms.ToolStripButton FilterClearTSB;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton ExitTSB;
        private System.Windows.Forms.ToolStripSplitButton SettingsTSB;
        private System.Windows.Forms.ToolStripMenuItem AboutProgramTSB;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource cashInOutBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem категорииДоходовИРасходовToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem резервноеКопированиеToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.Label labelIn;
        private System.Windows.Forms.Label labelOut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelResultBudget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шифрованиеToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.BindingSource goalBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private TabPage tabPage4;
        private ToolStrip toolStrip5;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox toolStripComboBox2;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripLabel toolStripLabel2;
        private ToolStripDateTimeChooser toolStripDateTimeChooser1;
        private ToolStripDateTimeChooser toolStripDateTimeChooser2;
        private ToolStripDateTimeChooser toolStripDateTimeChooser3;
        private ToolStripDateTimeChooser toolStripDateTimeChooser4;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripButton8;
        private ToolStripSplitButton toolStripSplitButton2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DateTimePicker dateTimePicker1;
        private Label label5;
        private DataGridView dataGridView2;
        private BindingSource budgetBindingSource;
        private ToolStrip toolStrip3;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton toolStripButton1;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn allSumDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn historyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn historyIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn3;
        public DataSet1 dataSet1;
        private TableLayoutPanel tableLayoutPanel4;
        private ToolStrip toolStrip4;
        private ToolStripButton tsb_AddGoal;
        private ToolStripButton tsb_GoalDelete;
        private ToolStripButton toolStripButton11;
        private ToolStripSplitButton toolStripSplitButton3;
        private Label labelResultGoal;
        private ToolStripMenuItem автоматическоеОбновлениеToolStripMenuItem;
        private ToolStripMenuItem повторитьToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripMenuItem toolStripMenuItem14;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem toolStripMenuItem15;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripButton CalculatorTSB;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton9;
        private ToolStripButton toolStripButton10;
        private BindingSource cashInOutBindingSource1;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toolStripMenuItem17;
        private ToolStripMenuItem toolStripMenuItem18;
        private ToolStripMenuItem toolStripMenuItem19;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton toolStripButton12;
        private ToolStripMenuItem toolStripMenuItem20;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripSeparator toolStripMenuItem21;
        private ToolStripMenuItem справкаПоГорячимКлавишамToolStripMenuItem;
        private ToolStripMenuItem tsbImportCSV;
        private SaveFileDialog saveFileDialog1;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton toolsbTransfer;
        private ToolStripSplitButton toolStripSplitButton4;
        private ToolStripMenuItem сНачалаНеделиToolStripMenuItem;
        private ToolStripMenuItem сНачалаМесяцаToolStripMenuItem;
        private ToolStripMenuItem сНачалаГодаToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem22;
        private ToolStripMenuItem предыдущаяНеделяToolStripMenuItem;
        private ToolStripMenuItem предыдущийМесяцToolStripMenuItem;
        private ToolStripMenuItem предыдущийГодToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem23;
        private ToolStripMenuItem ввестиНаОснованииToolStripMenuItem;
        private DataGridViewCheckBoxColumn checkDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn inOutDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sumDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn1;
        private ToolStripMenuItem СписокСчетовtoolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem сНачалаНеделиToolStripMenuItem1;
        private ToolStripMenuItem сНачалаМесяцаToolStripMenuItem1;
        private ToolStripMenuItem сНачалаГодаToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem предыдущаяНеделяToolStripMenuItem1;
        private ToolStripMenuItem предыдущийМесяцToolStripMenuItem1;
        private ToolStripMenuItem предыдущийГодToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem заВсёВремяToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem заВсёВремяToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton13;
        private DataGridViewTextBoxColumn inOutDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sumDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}

