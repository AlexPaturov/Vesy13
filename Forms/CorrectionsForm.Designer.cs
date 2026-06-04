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
        _pnlTop        = new Panel();
        _pnlLeft       = new Panel();
        _lblVidVzv     = new Label();
        _rbGpri        = new RadioButton();
        _rbGras        = new RadioButton();
        _lblDateCap    = new Label();
        _lblDt         = new Label();
        _lblTimeCap    = new Label();
        _lblVr         = new Label();
        _lblNppCap     = new Label();
        _lblNpp        = new Label();
        _lblModeCap    = new Label();
        _lblMode       = new Label();
        _lblNvagCap    = new Label();
        _txtNvag       = new TextBox();
        _lblDirCap     = new Label();
        _lblDir        = new Label();
        _lblGruzCap    = new Label();
        _txtGruz       = new TextBox();
        _lblNdokCap    = new Label();
        _txtNdok       = new TextBox();
        _lblBruttoCap  = new Label();
        _lblBrutto     = new Label();
        _lblBruttoUnit = new Label();
        _lblTarCap     = new Label();
        _txtTar        = new TextBox();
        _lblTarUnit    = new Label();
        _lblNettoCap   = new Label();
        _lblNetto      = new Label();
        _lblNettoUnit  = new Label();
        _btnTransfer   = new Button();
        _btnClear      = new Button();
        _btnRefresh    = new Button();
        _split         = new SplitContainer();
        _lblHeaderPend = new Label();
        _gridPend      = new DataGridView();
        _lblHeaderDone = new Label();
        _gridDone      = new DataGridView();
        _pnlStatus     = new Panel();
        _btnBack       = new Button();

        _pnlTop.SuspendLayout();
        _pnlLeft.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridPend).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).BeginInit();
        _split.Panel1.SuspendLayout();
        _split.Panel2.SuspendLayout();
        _split.SuspendLayout();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // ── _pnlLeft ──────────────────────────────────────────────────────────
        _pnlLeft.Location  = new Point(0, 0);
        _pnlLeft.Size      = new Size(138, 168);
        _pnlLeft.BackColor = Color.FromArgb(190, 162, 38);
        _pnlLeft.Controls.Add(_lblVidVzv);
        _pnlLeft.Controls.Add(_rbGpri);
        _pnlLeft.Controls.Add(_rbGras);

        _lblVidVzv.Text      = "Вид взвешивания";
        _lblVidVzv.Location  = new Point(6, 8);
        _lblVidVzv.AutoSize  = true;
        _lblVidVzv.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblVidVzv.ForeColor = Color.White;

        _rbGpri.Text      = "Приход (GPRI)";
        _rbGpri.Location  = new Point(6, 36);
        _rbGpri.Checked   = true;
        _rbGpri.AutoSize  = true;
        _rbGpri.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
        _rbGpri.ForeColor = Color.White;

        _rbGras.Text      = "Расход (GRAS)";
        _rbGras.Location  = new Point(6, 68);
        _rbGras.AutoSize  = true;
        _rbGras.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
        _rbGras.ForeColor = Color.White;

        // ── Row 1: дата / время / №п/п / режим ───────────────────────────────
        _lblDateCap.Text      = "Дата:";
        _lblDateCap.Location  = new Point(148, 13);
        _lblDateCap.AutoSize  = true;
        _lblDateCap.Font      = new Font("Segoe UI", 9F);
        _lblDateCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblDt.Text      = "—";
        _lblDt.Location  = new Point(191, 11);
        _lblDt.AutoSize  = true;
        _lblDt.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDt.ForeColor = Color.FromArgb(20, 20, 80);

        _lblTimeCap.Text      = "Время:";
        _lblTimeCap.Location  = new Point(288, 13);
        _lblTimeCap.AutoSize  = true;
        _lblTimeCap.Font      = new Font("Segoe UI", 9F);
        _lblTimeCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblVr.Text      = "—";
        _lblVr.Location  = new Point(338, 11);
        _lblVr.AutoSize  = true;
        _lblVr.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblVr.ForeColor = Color.FromArgb(20, 20, 80);

        _lblNppCap.Text      = "№п/п:";
        _lblNppCap.Location  = new Point(458, 13);
        _lblNppCap.AutoSize  = true;
        _lblNppCap.Font      = new Font("Segoe UI", 9F);
        _lblNppCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblNpp.Text      = "—";
        _lblNpp.Location  = new Point(500, 11);
        _lblNpp.AutoSize  = true;
        _lblNpp.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblNpp.ForeColor = Color.FromArgb(20, 20, 80);

        _lblModeCap.Text      = "Режим:";
        _lblModeCap.Location  = new Point(578, 13);
        _lblModeCap.AutoSize  = true;
        _lblModeCap.Font      = new Font("Segoe UI", 9F);
        _lblModeCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblMode.Text      = "—";
        _lblMode.Location  = new Point(626, 11);
        _lblMode.AutoSize  = true;
        _lblMode.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblMode.ForeColor = Color.FromArgb(20, 20, 80);

        // ── Row 2: № вагона / направление ────────────────────────────────────
        _lblNvagCap.Text      = "№ вагона:";
        _lblNvagCap.Location  = new Point(148, 45);
        _lblNvagCap.AutoSize  = true;
        _lblNvagCap.Font      = new Font("Segoe UI", 9F);
        _lblNvagCap.ForeColor = Color.FromArgb(60, 60, 80);

        _txtNvag.Location = new Point(228, 38);
        _txtNvag.Size     = new Size(170, 24);
        _txtNvag.Font     = new Font("Segoe UI", 9F);

        _lblDirCap.Text      = "Направление:";
        _lblDirCap.Location  = new Point(424, 45);
        _lblDirCap.AutoSize  = true;
        _lblDirCap.Font      = new Font("Segoe UI", 9F);
        _lblDirCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblDir.Text      = "—";
        _lblDir.Location  = new Point(522, 43);
        _lblDir.AutoSize  = true;
        _lblDir.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDir.ForeColor = Color.FromArgb(20, 20, 80);

        // ── Row 3: груз / № документа ─────────────────────────────────────────
        _lblGruzCap.Text      = "Груз:";
        _lblGruzCap.Location  = new Point(148, 73);
        _lblGruzCap.AutoSize  = true;
        _lblGruzCap.Font      = new Font("Segoe UI", 9F);
        _lblGruzCap.ForeColor = Color.FromArgb(60, 60, 80);

        _txtGruz.Location = new Point(196, 66);
        _txtGruz.Size     = new Size(270, 24);
        _txtGruz.Font     = new Font("Segoe UI", 9F);

        _lblNdokCap.Text      = "№ документа:";
        _lblNdokCap.Location  = new Point(488, 73);
        _lblNdokCap.AutoSize  = true;
        _lblNdokCap.Font      = new Font("Segoe UI", 9F);
        _lblNdokCap.ForeColor = Color.FromArgb(60, 60, 80);

        _txtNdok.Location = new Point(590, 66);
        _txtNdok.Size     = new Size(130, 24);
        _txtNdok.Font     = new Font("Segoe UI", 9F);

        // ── Row 4: брутто / тара / нетто ──────────────────────────────────────
        _lblBruttoCap.Text      = "Брутто:";
        _lblBruttoCap.Location  = new Point(148, 101);
        _lblBruttoCap.AutoSize  = true;
        _lblBruttoCap.Font      = new Font("Segoe UI", 9F);
        _lblBruttoCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblBrutto.Text      = "—";
        _lblBrutto.Location  = new Point(206, 97);
        _lblBrutto.AutoSize  = true;
        _lblBrutto.Font      = new Font("Courier New", 10F, FontStyle.Bold);
        _lblBrutto.ForeColor = Color.FromArgb(20, 20, 80);

        _lblBruttoUnit.Text      = "т";
        _lblBruttoUnit.Location  = new Point(282, 101);
        _lblBruttoUnit.AutoSize  = true;
        _lblBruttoUnit.Font      = new Font("Segoe UI", 9F);
        _lblBruttoUnit.ForeColor = Color.FromArgb(60, 60, 80);

        _lblTarCap.Text      = "Тара:";
        _lblTarCap.Location  = new Point(306, 101);
        _lblTarCap.AutoSize  = true;
        _lblTarCap.Font      = new Font("Segoe UI", 9F);
        _lblTarCap.ForeColor = Color.FromArgb(60, 60, 80);

        _txtTar.Location     = new Point(350, 94);
        _txtTar.Size         = new Size(90, 24);
        _txtTar.Font         = new Font("Segoe UI", 9F);
        _txtTar.TextChanged += TxtTar_TextChanged;

        _lblTarUnit.Text      = "т";
        _lblTarUnit.Location  = new Point(448, 101);
        _lblTarUnit.AutoSize  = true;
        _lblTarUnit.Font      = new Font("Segoe UI", 9F);
        _lblTarUnit.ForeColor = Color.FromArgb(60, 60, 80);

        _lblNettoCap.Text      = "Нетто:";
        _lblNettoCap.Location  = new Point(468, 101);
        _lblNettoCap.AutoSize  = true;
        _lblNettoCap.Font      = new Font("Segoe UI", 9F);
        _lblNettoCap.ForeColor = Color.FromArgb(60, 60, 80);

        _lblNetto.Text      = "—";
        _lblNetto.Location  = new Point(520, 97);
        _lblNetto.AutoSize  = true;
        _lblNetto.Font      = new Font("Courier New", 10F, FontStyle.Bold);
        _lblNetto.ForeColor = Color.FromArgb(0, 110, 30);

        _lblNettoUnit.Text      = "т";
        _lblNettoUnit.Location  = new Point(588, 101);
        _lblNettoUnit.AutoSize  = true;
        _lblNettoUnit.Font      = new Font("Segoe UI", 9F);
        _lblNettoUnit.ForeColor = Color.FromArgb(60, 60, 80);

        // ── Row 5: кнопки ──────────────────────────────────────────────────────
        _btnTransfer.Text      = "Перенести в Firebird  ▶";
        _btnTransfer.Location  = new Point(148, 128);
        _btnTransfer.Size      = new Size(240, 32);
        _btnTransfer.FlatStyle = FlatStyle.Flat;
        _btnTransfer.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
        _btnTransfer.BackColor = Color.FromArgb(0, 120, 40);
        _btnTransfer.ForeColor = Color.White;
        _btnTransfer.Enabled   = false;
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.Click    += BtnTransfer_Click;

        _btnClear.Text      = "Очистить";
        _btnClear.Location  = new Point(402, 128);
        _btnClear.Size      = new Size(110, 32);
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Font      = new Font("Segoe UI", 9F);
        _btnClear.Click    += BtnClear_Click;

        _btnRefresh.Text      = "Обновить";
        _btnRefresh.Location  = new Point(526, 128);
        _btnRefresh.Size      = new Size(110, 32);
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Font      = new Font("Segoe UI", 9F);
        _btnRefresh.Click    += BtnRefresh_Click;

        // ── _pnlTop: собираем ─────────────────────────────────────────────────
        _pnlTop.Location  = new Point(0, 0);
        _pnlTop.Size      = new Size(1050, 168);
        _pnlTop.Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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

        // ── Grids ─────────────────────────────────────────────────────────────
        _gridPend.Dock                                       = DockStyle.Fill;
        _gridPend.ReadOnly                                   = true;
        _gridPend.AllowUserToAddRows                         = false;
        _gridPend.AllowUserToDeleteRows                      = false;
        _gridPend.AllowUserToResizeRows                      = false;
        _gridPend.RowHeadersVisible                          = false;
        _gridPend.MultiSelect                                = false;
        _gridPend.SelectionMode                              = DataGridViewSelectionMode.FullRowSelect;
        _gridPend.BackgroundColor                            = Color.White;
        _gridPend.BorderStyle                                = BorderStyle.None;
        _gridPend.Font                                       = new Font("Segoe UI", 9F);
        _gridPend.ColumnHeadersHeightSizeMode                = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridPend.ColumnHeadersHeight                        = 24;
        _gridPend.RowTemplate.Height                         = 22;
        _gridPend.RowsDefaultCellStyle.BackColor             = Color.White;
        _gridPend.AlternatingRowsDefaultCellStyle.BackColor  = Color.FromArgb(242, 246, 255);
        _gridPend.SelectionChanged                          += GridPend_SelectionChanged;

        _gridDone.Dock                                       = DockStyle.Fill;
        _gridDone.ReadOnly                                   = true;
        _gridDone.AllowUserToAddRows                         = false;
        _gridDone.AllowUserToDeleteRows                      = false;
        _gridDone.AllowUserToResizeRows                      = false;
        _gridDone.RowHeadersVisible                          = false;
        _gridDone.MultiSelect                                = false;
        _gridDone.SelectionMode                              = DataGridViewSelectionMode.FullRowSelect;
        _gridDone.BackgroundColor                            = Color.White;
        _gridDone.BorderStyle                                = BorderStyle.None;
        _gridDone.Font                                       = new Font("Segoe UI", 9F);
        _gridDone.ColumnHeadersHeightSizeMode                = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridDone.ColumnHeadersHeight                        = 24;
        _gridDone.RowTemplate.Height                         = 22;
        _gridDone.RowsDefaultCellStyle.BackColor             = Color.White;
        _gridDone.AlternatingRowsDefaultCellStyle.BackColor  = Color.FromArgb(242, 246, 255);

        // ── SplitContainer ────────────────────────────────────────────────────
        _lblHeaderPend.Text      = "  Не перенесённые";
        _lblHeaderPend.Dock      = DockStyle.Top;
        _lblHeaderPend.Height    = 24;
        _lblHeaderPend.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblHeaderPend.BackColor = Color.FromArgb(25, 45, 90);
        _lblHeaderPend.ForeColor = Color.White;
        _lblHeaderPend.TextAlign = ContentAlignment.MiddleLeft;

        _lblHeaderDone.Text      = "  Перенесённые";
        _lblHeaderDone.Dock      = DockStyle.Top;
        _lblHeaderDone.Height    = 24;
        _lblHeaderDone.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblHeaderDone.BackColor = Color.FromArgb(25, 45, 90);
        _lblHeaderDone.ForeColor = Color.White;
        _lblHeaderDone.TextAlign = ContentAlignment.MiddleLeft;

        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);

        _split.Location         = new Point(0, 168);
        _split.Size             = new Size(1050, 458);
        _split.Anchor           = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _split.Orientation      = Orientation.Vertical;
        _split.SplitterDistance = 524;
        _split.Panel1MinSize    = 280;
        _split.Panel2MinSize    = 280;
        _split.BackColor        = Color.FromArgb(18, 32, 65);
        _split.SplitterWidth    = 4;

        // ── Status panel ──────────────────────────────────────────────────────
        _btnBack.Text      = "← Назад";
        _btnBack.Location  = new Point(8, 6);
        _btnBack.Size      = new Size(80, 22);
        _btnBack.FlatStyle = FlatStyle.Flat;
        _btnBack.Font      = new Font("Segoe UI", 8F);
        _btnBack.BackColor = Color.FromArgb(40, 70, 130);
        _btnBack.ForeColor = Color.White;
        _btnBack.FlatAppearance.BorderSize = 0;
        _btnBack.Click    += BtnBack_Click;

        _pnlStatus.Dock      = DockStyle.Bottom;
        _pnlStatus.Height    = 34;
        _pnlStatus.BackColor = Color.FromArgb(18, 32, 65);
        _pnlStatus.Controls.Add(_btnBack);

        // ── Form ──────────────────────────────────────────────────────────────
        Text          = "Корректировки — перенос в учётную систему";
        ClientSize    = new Size(1050, 660);
        MinimumSize   = new Size(860, 560);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(240, 242, 245);
        Controls.Add(_pnlTop);
        Controls.Add(_split);
        Controls.Add(_pnlStatus);

        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        _pnlLeft.ResumeLayout(false);
        _pnlLeft.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_gridPend).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).EndInit();
        _split.Panel1.ResumeLayout(false);
        _split.Panel2.ResumeLayout(false);
        _split.ResumeLayout(false);
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
