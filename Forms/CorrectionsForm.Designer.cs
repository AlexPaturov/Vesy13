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
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
        _pnlTop = new Panel();
        label2 = new Label();
        lblpotr = new Label();
        _tbPlat = new TextBox();
        _tbPotr = new TextBox();
        _pnlLeft = new Panel();
        _lblVidVzv = new Label();
        _rbGpri = new RadioButton();
        _rbGras = new RadioButton();
        _pnlMode = new Panel();
        _lblVidMode = new Label();
        _rbBrutto = new RadioButton();
        _rbTara = new RadioButton();
        _btnSave = new Button();
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
        _cmbTar = new ComboBox();
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
        tbCex = new TextBox();
        label3 = new Label();
        _pnlTop.SuspendLayout();
        _pnlLeft.SuspendLayout();
        _pnlMode.SuspendLayout();
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
        _pnlTop.Controls.Add(label3);
        _pnlTop.Controls.Add(tbCex);
        _pnlTop.Controls.Add(label2);
        _pnlTop.Controls.Add(lblpotr);
        _pnlTop.Controls.Add(_tbPlat);
        _pnlTop.Controls.Add(_tbPotr);
        _pnlTop.Controls.Add(_pnlLeft);
        _pnlTop.Controls.Add(_pnlMode);
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
        _pnlTop.Controls.Add(_cmbTar);
        _pnlTop.Controls.Add(_lblNettoCap);
        _pnlTop.Controls.Add(_lblNetto);
        _pnlTop.Controls.Add(_lblNettoUnit);
        _pnlTop.Controls.Add(_btnSave);
        _pnlTop.Controls.Add(_btnTransfer);
        _pnlTop.Controls.Add(_btnClear);
        _pnlTop.Controls.Add(_btnRefresh);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1264, 168);
        _pnlTop.TabIndex = 0;
        //
        // label2
        //
        label2.AutoSize = true;
        label2.Location = new Point(851, 38);
        label2.Name = "label2";
        label2.Size = new Size(94, 20);
        label2.TabIndex = 32;
        label2.Text = "Плательщик";
        //
        // lblpotr
        //
        lblpotr.AutoSize = true;
        lblpotr.Location = new Point(845, 11);
        lblpotr.Name = "lblpotr";
        lblpotr.Size = new Size(100, 20);
        lblpotr.TabIndex = 31;
        lblpotr.Text = "Потребитель";
        //
        // _tbPlat
        //
        _tbPlat.Location = new Point(954, 33);
        _tbPlat.Name = "_tbPlat";
        _tbPlat.Size = new Size(233, 27);
        _tbPlat.TabIndex = 30;
        //
        // _tbPotr
        //
        _tbPotr.Location = new Point(954, 5);
        _tbPotr.Name = "_tbPotr";
        _tbPotr.Size = new Size(232, 27);
        _tbPotr.TabIndex = 29;
        //
        // _pnlLeft
        //
        _pnlLeft.Controls.Add(_lblVidVzv);
        _pnlLeft.Controls.Add(_rbGpri);
        _pnlLeft.Controls.Add(_rbGras);
        _pnlLeft.Location = new Point(0, 0);
        _pnlLeft.Name = "_pnlLeft";
        _pnlLeft.Size = new Size(142, 98);
        _pnlLeft.TabIndex = 0;
        //
        // _lblVidVzv
        //
        _lblVidVzv.AutoSize = true;
        _lblVidVzv.Location = new Point(2, 5);
        _lblVidVzv.Name = "_lblVidVzv";
        _lblVidVzv.Size = new Size(140, 20);
        _lblVidVzv.TabIndex = 0;
        _lblVidVzv.Text = "Вид взвешивания";
        //
        // _rbGpri
        //
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Location = new Point(6, 29);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(96, 27);
        _rbGpri.TabIndex = 1;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        //
        // _rbGras
        //
        _rbGras.AutoSize = true;
        _rbGras.Location = new Point(6, 59);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(88, 27);
        _rbGras.TabIndex = 2;
        _rbGras.Text = "Расход";
        //
        // _pnlMode
        //
        _pnlMode.Controls.Add(_lblVidMode);
        _pnlMode.Controls.Add(_rbBrutto);
        _pnlMode.Controls.Add(_rbTara);
        _pnlMode.Location = new Point(0, 100);
        _pnlMode.Name = "_pnlMode";
        _pnlMode.Size = new Size(142, 66);
        _pnlMode.TabIndex = 35;
        //
        // _lblVidMode
        //
        _lblVidMode.AutoSize = true;
        _lblVidMode.Location = new Point(2, 3);
        _lblVidMode.Name = "_lblVidMode";
        _lblVidMode.Size = new Size(100, 20);
        _lblVidMode.TabIndex = 0;
        _lblVidMode.Text = "Тип операции";
        //
        // _rbBrutto
        //
        _rbBrutto.AutoSize = true;
        _rbBrutto.Checked = true;
        _rbBrutto.Location = new Point(6, 22);
        _rbBrutto.Name = "_rbBrutto";
        _rbBrutto.Size = new Size(90, 27);
        _rbBrutto.TabIndex = 1;
        _rbBrutto.TabStop = true;
        _rbBrutto.Text = "Брутто";
        //
        // _rbTara
        //
        _rbTara.AutoSize = true;
        _rbTara.Location = new Point(6, 44);
        _rbTara.Name = "_rbTara";
        _rbTara.Size = new Size(72, 27);
        _rbTara.TabIndex = 2;
        _rbTara.Text = "Тара";
        _rbTara.CheckedChanged += RbTara_CheckedChanged;
        //
        // _btnSave
        //
        _btnSave.Enabled = false;
        _btnSave.FlatAppearance.BorderSize = 0;
        _btnSave.FlatStyle = FlatStyle.Flat;
        _btnSave.Location = new Point(148, 128);
        _btnSave.Name = "_btnSave";
        _btnSave.Size = new Size(240, 32);
        _btnSave.TabIndex = 36;
        _btnSave.Text = "Сохранить";
        _btnSave.UseVisualStyleBackColor = false;
        _btnSave.Visible = false;
        _btnSave.Click += BtnSave_Click;
        //
        // _lblDateCap
        //
        _lblDateCap.AutoSize = true;
        _lblDateCap.Location = new Point(148, 11);
        _lblDateCap.Name = "_lblDateCap";
        _lblDateCap.Size = new Size(44, 20);
        _lblDateCap.TabIndex = 1;
        _lblDateCap.Text = "Дата:";
        //
        // _lblDt
        //
        _lblDt.AutoSize = true;
        _lblDt.Location = new Point(198, 11);
        _lblDt.Name = "_lblDt";
        _lblDt.Size = new Size(24, 20);
        _lblDt.TabIndex = 2;
        _lblDt.Text = "—";
        //
        // _lblTimeCap
        //
        _lblTimeCap.AutoSize = true;
        _lblTimeCap.Location = new Point(321, 11);
        _lblTimeCap.Name = "_lblTimeCap";
        _lblTimeCap.Size = new Size(57, 20);
        _lblTimeCap.TabIndex = 3;
        _lblTimeCap.Text = "Время:";
        //
        // _lblVr
        //
        _lblVr.AutoSize = true;
        _lblVr.Location = new Point(384, 11);
        _lblVr.Name = "_lblVr";
        _lblVr.Size = new Size(24, 20);
        _lblVr.TabIndex = 4;
        _lblVr.Text = "—";
        //
        // _lblNppCap
        //
        _lblNppCap.AutoSize = true;
        _lblNppCap.Location = new Point(504, 11);
        _lblNppCap.Name = "_lblNppCap";
        _lblNppCap.Size = new Size(53, 20);
        _lblNppCap.TabIndex = 5;
        _lblNppCap.Text = "№п/п:";
        //
        // _lblNpp
        //
        _lblNpp.AutoSize = true;
        _lblNpp.Location = new Point(563, 11);
        _lblNpp.Name = "_lblNpp";
        _lblNpp.Size = new Size(24, 20);
        _lblNpp.TabIndex = 6;
        _lblNpp.Text = "—";
        //
        // _lblModeCap
        //
        _lblModeCap.AutoSize = true;
        _lblModeCap.Location = new Point(672, 11);
        _lblModeCap.Name = "_lblModeCap";
        _lblModeCap.Size = new Size(59, 20);
        _lblModeCap.TabIndex = 7;
        _lblModeCap.Text = "Режим:";
        //
        // _lblMode
        //
        _lblMode.AutoSize = true;
        _lblMode.Location = new Point(737, 11);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(24, 20);
        _lblMode.TabIndex = 8;
        _lblMode.Text = "—";
        //
        // _lblNvagCap
        //
        _lblNvagCap.AutoSize = true;
        _lblNvagCap.Location = new Point(151, 42);
        _lblNvagCap.Name = "_lblNvagCap";
        _lblNvagCap.Size = new Size(81, 20);
        _lblNvagCap.TabIndex = 9;
        _lblNvagCap.Text = "№ вагона:";
        //
        // _txtNvag
        //
        _txtNvag.Location = new Point(235, 38);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(170, 27);
        _txtNvag.TabIndex = 10;
        _txtNvag.Leave += TxtNvag_Leave;
        //
        // _lblDirCap
        //
        _lblDirCap.AutoSize = true;
        _lblDirCap.Location = new Point(625, 41);
        _lblDirCap.Name = "_lblDirCap";
        _lblDirCap.Size = new Size(107, 20);
        _lblDirCap.TabIndex = 11;
        _lblDirCap.Text = "Направление:";
        //
        // _lblDir
        //
        _lblDir.AutoSize = true;
        _lblDir.Location = new Point(738, 41);
        _lblDir.Name = "_lblDir";
        _lblDir.Size = new Size(24, 20);
        _lblDir.TabIndex = 12;
        _lblDir.Text = "—";
        //
        // _lblGruzCap
        //
        _lblGruzCap.AutoSize = true;
        _lblGruzCap.Location = new Point(190, 68);
        _lblGruzCap.Name = "_lblGruzCap";
        _lblGruzCap.Size = new Size(42, 20);
        _lblGruzCap.TabIndex = 13;
        _lblGruzCap.Text = "Груз:";
        //
        // _txtGruz
        //
        _txtGruz.Location = new Point(235, 65);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(270, 27);
        _txtGruz.TabIndex = 14;
        //
        // _lblNdokCap
        //
        _lblNdokCap.AutoSize = true;
        _lblNdokCap.Location = new Point(626, 69);
        _lblNdokCap.Name = "_lblNdokCap";
        _lblNdokCap.Size = new Size(106, 20);
        _lblNdokCap.TabIndex = 15;
        _lblNdokCap.Text = "№ документа:";
        //
        // _txtNdok
        //
        _txtNdok.Location = new Point(737, 65);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 27);
        _txtNdok.TabIndex = 16;
        //
        // _lblBruttoCap
        //
        _lblBruttoCap.AutoSize = true;
        _lblBruttoCap.Location = new Point(148, 96);
        _lblBruttoCap.Name = "_lblBruttoCap";
        _lblBruttoCap.Size = new Size(58, 20);
        _lblBruttoCap.TabIndex = 17;
        _lblBruttoCap.Text = "Брутто:";
        //
        // _lblBrutto
        //
        _lblBrutto.AutoSize = true;
        _lblBrutto.Location = new Point(212, 96);
        _lblBrutto.Name = "_lblBrutto";
        _lblBrutto.Size = new Size(19, 20);
        _lblBrutto.TabIndex = 18;
        _lblBrutto.Text = "—";
        //
        // _lblBruttoUnit
        //
        _lblBruttoUnit.AutoSize = true;
        _lblBruttoUnit.Location = new Point(282, 96);
        _lblBruttoUnit.Name = "_lblBruttoUnit";
        _lblBruttoUnit.Size = new Size(15, 20);
        _lblBruttoUnit.TabIndex = 19;
        _lblBruttoUnit.Text = "т";
        //
        // _lblTarCap
        //
        _lblTarCap.AutoSize = true;
        _lblTarCap.Location = new Point(306, 96);
        _lblTarCap.Name = "_lblTarCap";
        _lblTarCap.Size = new Size(45, 20);
        _lblTarCap.TabIndex = 20;
        _lblTarCap.Text = "Тара:";
        //
        // _cmbTar
        //
        _cmbTar.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbTar.Location = new Point(352, 94);
        _cmbTar.Name = "_cmbTar";
        _cmbTar.Size = new Size(284, 28);
        _cmbTar.TabIndex = 21;
        _cmbTar.SelectedIndexChanged += CmbTar_SelectedIndexChanged;
        //
        // _lblNettoCap
        //
        _lblNettoCap.AutoSize = true;
        _lblNettoCap.Location = new Point(646, 97);
        _lblNettoCap.Name = "_lblNettoCap";
        _lblNettoCap.Size = new Size(52, 20);
        _lblNettoCap.TabIndex = 23;
        _lblNettoCap.Text = "Нетто:";
        //
        // _lblNetto
        //
        _lblNetto.AutoSize = true;
        _lblNetto.Location = new Point(710, 97);
        _lblNetto.Name = "_lblNetto";
        _lblNetto.Size = new Size(19, 20);
        _lblNetto.TabIndex = 24;
        _lblNetto.Text = "—";
        //
        // _lblNettoUnit
        //
        _lblNettoUnit.AutoSize = true;
        _lblNettoUnit.Location = new Point(759, 97);
        _lblNettoUnit.Name = "_lblNettoUnit";
        _lblNettoUnit.Size = new Size(15, 20);
        _lblNettoUnit.TabIndex = 25;
        _lblNettoUnit.Text = "т";
        //
        // _btnTransfer
        //
        _btnTransfer.Enabled = false;
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.FlatStyle = FlatStyle.Flat;
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
        _btnClear.Location = new Point(402, 128);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(110, 32);
        _btnClear.TabIndex = 27;
        _btnClear.Text = "Очистить";
        _btnClear.UseVisualStyleBackColor = false;
        _btnClear.Click += BtnClear_Click;
        //
        // _btnRefresh
        //
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Location = new Point(526, 128);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(110, 32);
        _btnRefresh.TabIndex = 28;
        _btnRefresh.Text = "Обновить";
        _btnRefresh.UseVisualStyleBackColor = false;
        _btnRefresh.Click += BtnRefresh_Click;
        //
        // _split
        //
        _split.Dock = DockStyle.Fill;
        _split.FixedPanel = FixedPanel.Panel1;
        _split.Location = new Point(0, 168);
        _split.Name = "_split";
        //
        // _split.Panel1
        //
        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel1MinSize = 563;
        //
        // _split.Panel2
        //
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);
        _split.Panel2MinSize = 280;
        _split.Size = new Size(1264, 458);
        _split.SplitterDistance = 563;
        _split.TabIndex = 1;
        //
        // _gridPend
        //
        _gridPend.AllowUserToAddRows = false;
        _gridPend.AllowUserToDeleteRows = false;
        _gridPend.AllowUserToResizeRows = false;
        _gridPend.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
        _gridPend.BackgroundColor = UiColors.Surface;
        _gridPend.BorderStyle = BorderStyle.None;
        _gridPend.ColumnHeadersHeight = 32;
        _gridPend.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridPend.EnableHeadersVisualStyles = false;
        _gridPend.GridColor = UiColors.GridLine;
        _gridPend.Dock = DockStyle.Fill;
        _gridPend.Location = new Point(0, 24);
        _gridPend.MultiSelect = false;
        _gridPend.Name = "_gridPend";
        _gridPend.ReadOnly = true;
        _gridPend.RowHeadersVisible = false;
        _gridPend.RowHeadersWidth = 51;
        _gridPend.RowsDefaultCellStyle = dataGridViewCellStyle10;
        _gridPend.RowTemplate.Height = 28;
        _gridPend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridPend.Size = new Size(563, 434);
        _gridPend.TabIndex = 0;
        _gridPend.SelectionChanged += GridPend_SelectionChanged;
        //
        // _lblHeaderPend
        //
        _lblHeaderPend.Dock = DockStyle.Top;
        _lblHeaderPend.Location = new Point(0, 0);
        _lblHeaderPend.Name = "_lblHeaderPend";
        _lblHeaderPend.Size = new Size(563, 24);
        _lblHeaderPend.TabIndex = 1;
        _lblHeaderPend.Text = "  Не перенесённые";
        _lblHeaderPend.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _gridDone
        //
        _gridDone.AllowUserToAddRows = false;
        _gridDone.AllowUserToDeleteRows = false;
        _gridDone.AllowUserToResizeRows = false;
        _gridDone.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
        _gridDone.BackgroundColor = UiColors.Surface;
        _gridDone.BorderStyle = BorderStyle.None;
        _gridDone.ColumnHeadersHeight = 32;
        _gridDone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridDone.EnableHeadersVisualStyles = false;
        _gridDone.GridColor = UiColors.GridLine;
        _gridDone.Dock = DockStyle.Fill;
        _gridDone.Location = new Point(0, 24);
        _gridDone.MultiSelect = false;
        _gridDone.Name = "_gridDone";
        _gridDone.ReadOnly = true;
        _gridDone.RowHeadersVisible = false;
        _gridDone.RowHeadersWidth = 51;
        _gridDone.RowsDefaultCellStyle = dataGridViewCellStyle12;
        _gridDone.RowTemplate.Height = 28;
        _gridDone.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridDone.Size = new Size(697, 434);
        _gridDone.TabIndex = 0;
        _gridDone.SelectionChanged += GridDone_SelectionChanged;
        //
        // _lblHeaderDone
        //
        _lblHeaderDone.Dock = DockStyle.Top;
        _lblHeaderDone.Location = new Point(0, 0);
        _lblHeaderDone.Name = "_lblHeaderDone";
        _lblHeaderDone.Size = new Size(697, 24);
        _lblHeaderDone.TabIndex = 1;
        _lblHeaderDone.Text = "  Перенесённые";
        _lblHeaderDone.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _pnlStatus
        //
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 626);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1264, 4);
        _pnlStatus.TabIndex = 2;
        //
        //
        //
        // tbCex
        //
        tbCex.Location = new Point(954, 61);
        tbCex.MaxLength = 3;
        tbCex.Name = "tbCex";
        tbCex.Size = new Size(233, 27);
        tbCex.TabIndex = 33;
        //
        // label3
        //
        label3.AutoSize = true;
        label3.Location = new Point(910, 65);
        label3.Name = "label3";
        label3.Size = new Size(35, 20);
        label3.TabIndex = 34;
        label3.Text = "Цех";
        //
        // CorrectionsForm
        //
        ClientSize = new Size(1264, 660);
        Controls.Add(_split);
        Controls.Add(_pnlTop);
        Controls.Add(_pnlStatus);
        MinimumSize = new Size(860, 560);
        Name = "CorrectionsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Корректировки — перенос в учётную систему";
        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        _pnlLeft.ResumeLayout(false);
        _pnlLeft.PerformLayout();
        _pnlMode.ResumeLayout(false);
        _pnlMode.PerformLayout();
        _split.Panel1.ResumeLayout(false);
        _split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_split).EndInit();
        _split.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridPend).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).EndInit();
        _pnlStatus.ResumeLayout(false);
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
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
    private ComboBox       _cmbTar;
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
    private Label          label2;
    private Label          lblpotr;
    private TextBox        _tbPlat;
    private TextBox        _tbPotr;
    private Label          label3;
    private TextBox        tbCex;
    private Panel          _pnlMode;
    private Label          _lblVidMode;
    private RadioButton    _rbBrutto;
    private RadioButton    _rbTara;
    private Button         _btnSave;
}
