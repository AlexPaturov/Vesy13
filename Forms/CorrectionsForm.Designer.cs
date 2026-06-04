namespace Vesy13.Forms;

partial class CorrectionsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        _pnlTop = new Panel();
        _pnlLeft = new Panel();
        _lblVidVzv = new Label();
        _rbGpri = new RadioButton();
        _rbGras = new RadioButton();
        _lblDateCap = new Label();
        _lblDt = new Label();
        _lblTimeCap = new Label();
        _lblVr = new Label();
        _lblNppCap = new Label();
        _lblNpp = new Label();
        _lblModeCap = new Label();
        _lblMode = new Label();
        _lblNvagCap = new Label();
        _txtNvag = new TextBox();
        _lblDirCap = new Label();
        _lblDir = new Label();
        _lblGruzCap = new Label();
        _txtGruz = new TextBox();
        _lblNdokCap = new Label();
        _txtNdok = new TextBox();
        _lblBruttoCap = new Label();
        _lblBrutto = new Label();
        _lblBruttoUnit = new Label();
        _lblTarCap = new Label();
        _txtTar = new TextBox();
        _lblTarUnit = new Label();
        _lblNettoCap = new Label();
        _lblNetto = new Label();
        _lblNettoUnit = new Label();
        _btnTransfer = new Button();
        _btnClear = new Button();
        _btnRefresh = new Button();
        _split = new SplitContainer();
        _gridPend = new DataGridView();
        _lblHeaderPend = new Label();
        _gridDone = new DataGridView();
        _lblHeaderDone = new Label();
        _pnlStatus = new Panel();
        _btnBack = new Button();
        _pnlTop.SuspendLayout();
        _pnlLeft.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_split).BeginInit();
        _split.Panel1.SuspendLayout();
        _split.Panel2.SuspendLayout();
        _split.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridPend).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).BeginInit();
        _pnlStatus.SuspendLayout();
        SuspendLayout();
        // 
        // _pnlTop
        // 
        _pnlTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pnlTop.BackColor = Color.FromArgb(215, 225, 248);
        _pnlTop.Controls.Add(_pnlLeft);
        _pnlTop.Controls.Add(_lblDateCap);
        _pnlTop.Controls.Add(_lblDt);
        _pnlTop.Controls.Add(_lblTimeCap);
        _pnlTop.Controls.Add(_lblVr);
        _pnlTop.Controls.Add(_lblNppCap);
        _pnlTop.Controls.Add(_lblNpp);
        _pnlTop.Controls.Add(_lblModeCap);
        _pnlTop.Controls.Add(_lblMode);
        _pnlTop.Controls.Add(_lblNvagCap);
        _pnlTop.Controls.Add(_txtNvag);
        _pnlTop.Controls.Add(_lblDirCap);
        _pnlTop.Controls.Add(_lblDir);
        _pnlTop.Controls.Add(_lblGruzCap);
        _pnlTop.Controls.Add(_txtGruz);
        _pnlTop.Controls.Add(_lblNdokCap);
        _pnlTop.Controls.Add(_txtNdok);
        _pnlTop.Controls.Add(_lblBruttoCap);
        _pnlTop.Controls.Add(_lblBrutto);
        _pnlTop.Controls.Add(_lblBruttoUnit);
        _pnlTop.Controls.Add(_lblTarCap);
        _pnlTop.Controls.Add(_txtTar);
        _pnlTop.Controls.Add(_lblTarUnit);
        _pnlTop.Controls.Add(_lblNettoCap);
        _pnlTop.Controls.Add(_lblNetto);
        _pnlTop.Controls.Add(_lblNettoUnit);
        _pnlTop.Controls.Add(_btnTransfer);
        _pnlTop.Controls.Add(_btnClear);
        _pnlTop.Controls.Add(_btnRefresh);
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1050, 168);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlLeft
        // 
        _pnlLeft.BackColor = Color.FromArgb(190, 162, 38);
        _pnlLeft.Controls.Add(_lblVidVzv);
        _pnlLeft.Controls.Add(_rbGpri);
        _pnlLeft.Controls.Add(_rbGras);
        _pnlLeft.Location = new Point(0, 0);
        _pnlLeft.Name = "_pnlLeft";
        _pnlLeft.Size = new Size(142, 168);
        _pnlLeft.TabIndex = 0;
        // 
        // _lblVidVzv
        // 
        _lblVidVzv.AutoSize = true;
        _lblVidVzv.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblVidVzv.ForeColor = Color.White;
        _lblVidVzv.Location = new Point(6, 8);
        _lblVidVzv.Name = "_lblVidVzv";
        _lblVidVzv.Size = new Size(140, 20);
        _lblVidVzv.TabIndex = 0;
        _lblVidVzv.Text = "Вид взвешивания";
        // 
        // _rbGpri
        // 
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        _rbGpri.ForeColor = Color.White;
        _rbGpri.Location = new Point(6, 36);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(96, 27);
        _rbGpri.TabIndex = 1;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        // 
        // _rbGras
        // 
        _rbGras.AutoSize = true;
        _rbGras.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        _rbGras.ForeColor = Color.White;
        _rbGras.Location = new Point(6, 68);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(88, 27);
        _rbGras.TabIndex = 2;
        _rbGras.Text = "Расход";
        // 
        // _lblDateCap
        // 
        _lblDateCap.AutoSize = true;
        _lblDateCap.Font = new Font("Segoe UI", 9F);
        _lblDateCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblDateCap.Location = new Point(148, 13);
        _lblDateCap.Name = "_lblDateCap";
        _lblDateCap.Size = new Size(44, 20);
        _lblDateCap.TabIndex = 1;
        _lblDateCap.Text = "Дата:";
        // 
        // _lblDt
        // 
        _lblDt.AutoSize = true;
        _lblDt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDt.ForeColor = Color.FromArgb(20, 20, 80);
        _lblDt.Location = new Point(191, 11);
        _lblDt.Name = "_lblDt";
        _lblDt.Size = new Size(24, 20);
        _lblDt.TabIndex = 2;
        _lblDt.Text = "—";
        // 
        // _lblTimeCap
        // 
        _lblTimeCap.AutoSize = true;
        _lblTimeCap.Font = new Font("Segoe UI", 9F);
        _lblTimeCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblTimeCap.Location = new Point(288, 13);
        _lblTimeCap.Name = "_lblTimeCap";
        _lblTimeCap.Size = new Size(57, 20);
        _lblTimeCap.TabIndex = 3;
        _lblTimeCap.Text = "Время:";
        // 
        // _lblVr
        // 
        _lblVr.AutoSize = true;
        _lblVr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblVr.ForeColor = Color.FromArgb(20, 20, 80);
        _lblVr.Location = new Point(338, 11);
        _lblVr.Name = "_lblVr";
        _lblVr.Size = new Size(24, 20);
        _lblVr.TabIndex = 4;
        _lblVr.Text = "—";
        // 
        // _lblNppCap
        // 
        _lblNppCap.AutoSize = true;
        _lblNppCap.Font = new Font("Segoe UI", 9F);
        _lblNppCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNppCap.Location = new Point(458, 13);
        _lblNppCap.Name = "_lblNppCap";
        _lblNppCap.Size = new Size(53, 20);
        _lblNppCap.TabIndex = 5;
        _lblNppCap.Text = "№п/п:";
        // 
        // _lblNpp
        // 
        _lblNpp.AutoSize = true;
        _lblNpp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblNpp.ForeColor = Color.FromArgb(20, 20, 80);
        _lblNpp.Location = new Point(500, 11);
        _lblNpp.Name = "_lblNpp";
        _lblNpp.Size = new Size(24, 20);
        _lblNpp.TabIndex = 6;
        _lblNpp.Text = "—";
        // 
        // _lblModeCap
        // 
        _lblModeCap.AutoSize = true;
        _lblModeCap.Font = new Font("Segoe UI", 9F);
        _lblModeCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblModeCap.Location = new Point(578, 13);
        _lblModeCap.Name = "_lblModeCap";
        _lblModeCap.Size = new Size(59, 20);
        _lblModeCap.TabIndex = 7;
        _lblModeCap.Text = "Режим:";
        // 
        // _lblMode
        // 
        _lblMode.AutoSize = true;
        _lblMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblMode.ForeColor = Color.FromArgb(20, 20, 80);
        _lblMode.Location = new Point(643, 13);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(24, 20);
        _lblMode.TabIndex = 8;
        _lblMode.Text = "—";
        // 
        // _lblNvagCap
        // 
        _lblNvagCap.AutoSize = true;
        _lblNvagCap.Font = new Font("Segoe UI", 9F);
        _lblNvagCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNvagCap.Location = new Point(148, 45);
        _lblNvagCap.Name = "_lblNvagCap";
        _lblNvagCap.Size = new Size(81, 20);
        _lblNvagCap.TabIndex = 9;
        _lblNvagCap.Text = "№ вагона:";
        // 
        // _txtNvag
        // 
        _txtNvag.Font = new Font("Segoe UI", 9F);
        _txtNvag.Location = new Point(235, 38);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(170, 27);
        _txtNvag.TabIndex = 10;
        // 
        // _lblDirCap
        // 
        _lblDirCap.AutoSize = true;
        _lblDirCap.Font = new Font("Segoe UI", 9F);
        _lblDirCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblDirCap.Location = new Point(625, 41);
        _lblDirCap.Name = "_lblDirCap";
        _lblDirCap.Size = new Size(107, 20);
        _lblDirCap.TabIndex = 11;
        _lblDirCap.Text = "Направление:";
        // 
        // _lblDir
        // 
        _lblDir.AutoSize = true;
        _lblDir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDir.ForeColor = Color.FromArgb(20, 20, 80);
        _lblDir.Location = new Point(753, 36);
        _lblDir.Name = "_lblDir";
        _lblDir.Size = new Size(24, 20);
        _lblDir.TabIndex = 12;
        _lblDir.Text = "—";
        // 
        // _lblGruzCap
        // 
        _lblGruzCap.AutoSize = true;
        _lblGruzCap.Font = new Font("Segoe UI", 9F);
        _lblGruzCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblGruzCap.Location = new Point(187, 69);
        _lblGruzCap.Name = "_lblGruzCap";
        _lblGruzCap.Size = new Size(42, 20);
        _lblGruzCap.TabIndex = 13;
        _lblGruzCap.Text = "Груз:";
        // 
        // _txtGruz
        // 
        _txtGruz.Font = new Font("Segoe UI", 9F);
        _txtGruz.Location = new Point(235, 65);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(270, 27);
        _txtGruz.TabIndex = 14;
        // 
        // _lblNdokCap
        // 
        _lblNdokCap.AutoSize = true;
        _lblNdokCap.Font = new Font("Segoe UI", 9F);
        _lblNdokCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNdokCap.Location = new Point(626, 69);
        _lblNdokCap.Name = "_lblNdokCap";
        _lblNdokCap.Size = new Size(106, 20);
        _lblNdokCap.TabIndex = 15;
        _lblNdokCap.Text = "№ документа:";
        // 
        // _txtNdok
        // 
        _txtNdok.Font = new Font("Segoe UI", 9F);
        _txtNdok.Location = new Point(737, 65);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 27);
        _txtNdok.TabIndex = 16;
        // 
        // _lblBruttoCap
        // 
        _lblBruttoCap.AutoSize = true;
        _lblBruttoCap.Font = new Font("Segoe UI", 9F);
        _lblBruttoCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblBruttoCap.Location = new Point(148, 101);
        _lblBruttoCap.Name = "_lblBruttoCap";
        _lblBruttoCap.Size = new Size(58, 20);
        _lblBruttoCap.TabIndex = 17;
        _lblBruttoCap.Text = "Брутто:";
        // 
        // _lblBrutto
        // 
        _lblBrutto.AutoSize = true;
        _lblBrutto.Font = new Font("Courier New", 10F, FontStyle.Bold);
        _lblBrutto.ForeColor = Color.FromArgb(20, 20, 80);
        _lblBrutto.Location = new Point(212, 97);
        _lblBrutto.Name = "_lblBrutto";
        _lblBrutto.Size = new Size(19, 20);
        _lblBrutto.TabIndex = 18;
        _lblBrutto.Text = "—";
        // 
        // _lblBruttoUnit
        // 
        _lblBruttoUnit.AutoSize = true;
        _lblBruttoUnit.Font = new Font("Segoe UI", 9F);
        _lblBruttoUnit.ForeColor = Color.FromArgb(60, 60, 80);
        _lblBruttoUnit.Location = new Point(282, 101);
        _lblBruttoUnit.Name = "_lblBruttoUnit";
        _lblBruttoUnit.Size = new Size(15, 20);
        _lblBruttoUnit.TabIndex = 19;
        _lblBruttoUnit.Text = "т";
        // 
        // _lblTarCap
        // 
        _lblTarCap.AutoSize = true;
        _lblTarCap.Font = new Font("Segoe UI", 9F);
        _lblTarCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblTarCap.Location = new Point(306, 101);
        _lblTarCap.Name = "_lblTarCap";
        _lblTarCap.Size = new Size(45, 20);
        _lblTarCap.TabIndex = 20;
        _lblTarCap.Text = "Тара:";
        // 
        // _txtTar
        // 
        _txtTar.Font = new Font("Segoe UI", 9F);
        _txtTar.Location = new Point(357, 94);
        _txtTar.Name = "_txtTar";
        _txtTar.Size = new Size(90, 27);
        _txtTar.TabIndex = 21;
        _txtTar.TextChanged += TxtTar_TextChanged;
        // 
        // _lblTarUnit
        // 
        _lblTarUnit.AutoSize = true;
        _lblTarUnit.Font = new Font("Segoe UI", 9F);
        _lblTarUnit.ForeColor = Color.FromArgb(60, 60, 80);
        _lblTarUnit.Location = new Point(448, 101);
        _lblTarUnit.Name = "_lblTarUnit";
        _lblTarUnit.Size = new Size(15, 20);
        _lblTarUnit.TabIndex = 22;
        _lblTarUnit.Text = "т";
        // 
        // _lblNettoCap
        // 
        _lblNettoCap.AutoSize = true;
        _lblNettoCap.Font = new Font("Segoe UI", 9F);
        _lblNettoCap.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNettoCap.Location = new Point(480, 105);
        _lblNettoCap.Name = "_lblNettoCap";
        _lblNettoCap.Size = new Size(52, 20);
        _lblNettoCap.TabIndex = 23;
        _lblNettoCap.Text = "Нетто:";
        // 
        // _lblNetto
        // 
        _lblNetto.AutoSize = true;
        _lblNetto.Font = new Font("Courier New", 10F, FontStyle.Bold);
        _lblNetto.ForeColor = Color.FromArgb(0, 110, 30);
        _lblNetto.Location = new Point(538, 97);
        _lblNetto.Name = "_lblNetto";
        _lblNetto.Size = new Size(19, 20);
        _lblNetto.TabIndex = 24;
        _lblNetto.Text = "—";
        // 
        // _lblNettoUnit
        // 
        _lblNettoUnit.AutoSize = true;
        _lblNettoUnit.Font = new Font("Segoe UI", 9F);
        _lblNettoUnit.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNettoUnit.Location = new Point(588, 101);
        _lblNettoUnit.Name = "_lblNettoUnit";
        _lblNettoUnit.Size = new Size(15, 20);
        _lblNettoUnit.TabIndex = 25;
        _lblNettoUnit.Text = "т";
        // 
        // _btnTransfer
        // 
        _btnTransfer.BackColor = Color.FromArgb(0, 120, 40);
        _btnTransfer.Enabled = false;
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.FlatStyle = FlatStyle.Flat;
        _btnTransfer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        _btnTransfer.ForeColor = Color.White;
        _btnTransfer.Location = new Point(148, 128);
        _btnTransfer.Name = "_btnTransfer";
        _btnTransfer.Size = new Size(240, 32);
        _btnTransfer.TabIndex = 26;
        _btnTransfer.Text = "Перенести";
        _btnTransfer.UseVisualStyleBackColor = false;
        _btnTransfer.Click += BtnTransfer_Click;
        // 
        // _btnClear
        // 
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Font = new Font("Segoe UI", 9F);
        _btnClear.Location = new Point(402, 128);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(110, 32);
        _btnClear.TabIndex = 27;
        _btnClear.Text = "Очистить";
        _btnClear.Click += BtnClear_Click;
        // 
        // _btnRefresh
        // 
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Font = new Font("Segoe UI", 9F);
        _btnRefresh.Location = new Point(526, 128);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(110, 32);
        _btnRefresh.TabIndex = 28;
        _btnRefresh.Text = "Обновить";
        _btnRefresh.Click += BtnRefresh_Click;
        // 
        // _split
        // 
        _split.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _split.BackColor = Color.FromArgb(18, 32, 65);
        _split.Location = new Point(0, 168);
        _split.Name = "_split";
        // 
        // _split.Panel1
        // 
        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel1MinSize = 579;
        _split.FixedPanel    = FixedPanel.Panel1;
        //
        // _split.Panel2
        //
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);
        _split.Panel2MinSize = 280;
        _split.Size = new Size(1050, 458);
        _split.SplitterDistance = 579;
        _split.TabIndex = 1;
        // 
        // _gridPend
        // 
        _gridPend.AllowUserToAddRows = false;
        _gridPend.AllowUserToDeleteRows = false;
        _gridPend.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(242, 246, 255);
        _gridPend.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _gridPend.BackgroundColor = Color.White;
        _gridPend.BorderStyle = BorderStyle.None;
        _gridPend.ColumnHeadersHeight = 24;
        _gridPend.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridPend.Dock = DockStyle.Fill;
        _gridPend.Font = new Font("Segoe UI", 9F);
        _gridPend.Location = new Point(0, 24);
        _gridPend.MultiSelect = false;
        _gridPend.Name = "_gridPend";
        _gridPend.ReadOnly = true;
        _gridPend.RowHeadersVisible = false;
        _gridPend.RowHeadersWidth = 51;
        dataGridViewCellStyle2.BackColor = Color.White;
        _gridPend.RowsDefaultCellStyle = dataGridViewCellStyle2;
        _gridPend.RowTemplate.Height = 22;
        _gridPend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridPend.Size = new Size(524, 434);
        _gridPend.TabIndex = 0;
        _gridPend.SelectionChanged += GridPend_SelectionChanged;
        // 
        // _lblHeaderPend
        // 
        _lblHeaderPend.BackColor = Color.FromArgb(25, 45, 90);
        _lblHeaderPend.Dock = DockStyle.Top;
        _lblHeaderPend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblHeaderPend.ForeColor = Color.White;
        _lblHeaderPend.Location = new Point(0, 0);
        _lblHeaderPend.Name = "_lblHeaderPend";
        _lblHeaderPend.Size = new Size(524, 24);
        _lblHeaderPend.TabIndex = 1;
        _lblHeaderPend.Text = "  Не перенесённые";
        _lblHeaderPend.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _gridDone
        // 
        _gridDone.AllowUserToAddRows = false;
        _gridDone.AllowUserToDeleteRows = false;
        _gridDone.AllowUserToResizeRows = false;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(242, 246, 255);
        _gridDone.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
        _gridDone.BackgroundColor = Color.White;
        _gridDone.BorderStyle = BorderStyle.None;
        _gridDone.ColumnHeadersHeight = 24;
        _gridDone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridDone.Dock = DockStyle.Fill;
        _gridDone.Font = new Font("Segoe UI", 9F);
        _gridDone.Location = new Point(0, 24);
        _gridDone.MultiSelect = false;
        _gridDone.Name = "_gridDone";
        _gridDone.ReadOnly = true;
        _gridDone.RowHeadersVisible = false;
        _gridDone.RowHeadersWidth = 51;
        dataGridViewCellStyle4.BackColor = Color.White;
        _gridDone.RowsDefaultCellStyle = dataGridViewCellStyle4;
        _gridDone.RowTemplate.Height = 22;
        _gridDone.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridDone.Size = new Size(522, 434);
        _gridDone.TabIndex = 0;
        // 
        // _lblHeaderDone
        // 
        _lblHeaderDone.BackColor = Color.FromArgb(25, 45, 90);
        _lblHeaderDone.Dock = DockStyle.Top;
        _lblHeaderDone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblHeaderDone.ForeColor = Color.White;
        _lblHeaderDone.Location = new Point(0, 0);
        _lblHeaderDone.Name = "_lblHeaderDone";
        _lblHeaderDone.Size = new Size(522, 24);
        _lblHeaderDone.TabIndex = 1;
        _lblHeaderDone.Text = "  Перенесённые";
        _lblHeaderDone.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _pnlStatus
        // 
        _pnlStatus.BackColor = Color.FromArgb(18, 32, 65);
        _pnlStatus.Controls.Add(_btnBack);
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 626);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1050, 34);
        _pnlStatus.TabIndex = 2;
        // 
        // _btnBack
        // 
        _btnBack.BackColor = Color.FromArgb(40, 70, 130);
        _btnBack.FlatAppearance.BorderSize = 0;
        _btnBack.FlatStyle = FlatStyle.Flat;
        _btnBack.Font = new Font("Segoe UI", 8F);
        _btnBack.ForeColor = Color.White;
        _btnBack.Location = new Point(8, 6);
        _btnBack.Name = "_btnBack";
        _btnBack.Size = new Size(80, 22);
        _btnBack.TabIndex = 0;
        _btnBack.Text = "← Назад";
        _btnBack.UseVisualStyleBackColor = false;
        _btnBack.Click += BtnBack_Click;
        // 
        // CorrectionsForm
        // 
        BackColor = Color.FromArgb(240, 242, 245);
        ClientSize = new Size(1050, 660);
        Controls.Add(_pnlTop);
        Controls.Add(_split);
        Controls.Add(_pnlStatus);
        MinimumSize = new Size(860, 560);
        Name = "CorrectionsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Корректировки — перенос в учётную систему";
        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        _pnlLeft.ResumeLayout(false);
        _pnlLeft.PerformLayout();
        _split.Panel1.ResumeLayout(false);
        _split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_split).EndInit();
        _split.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridPend).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).EndInit();
        _pnlStatus.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel          _pnlTop;
    private Panel          _pnlLeft;
    private Panel          _pnlStatus;
    private Label          _lblVidVzv;
    private RadioButton    _rbGpri;
    private RadioButton    _rbGras;
    private Label          _lblDateCap;
    private Label          _lblDt;
    private Label          _lblTimeCap;
    private Label          _lblVr;
    private Label          _lblNppCap;
    private Label          _lblNpp;
    private Label          _lblModeCap;
    private Label          _lblMode;
    private Label          _lblNvagCap;
    private TextBox        _txtNvag;
    private Label          _lblDirCap;
    private Label          _lblDir;
    private Label          _lblGruzCap;
    private TextBox        _txtGruz;
    private Label          _lblNdokCap;
    private TextBox        _txtNdok;
    private Label          _lblBruttoCap;
    private Label          _lblBrutto;
    private Label          _lblBruttoUnit;
    private Label          _lblTarCap;
    private TextBox        _txtTar;
    private Label          _lblTarUnit;
    private Label          _lblNettoCap;
    private Label          _lblNetto;
    private Label          _lblNettoUnit;
    private Button         _btnTransfer;
    private Button         _btnClear;
    private Button         _btnRefresh;
    private SplitContainer _split;
    private Label          _lblHeaderPend;
    private DataGridView   _gridPend;
    private Label          _lblHeaderDone;
    private DataGridView   _gridDone;
    private Button         _btnBack;
}
