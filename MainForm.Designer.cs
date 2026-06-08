namespace Vesy13;

partial class MainForm
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
        _pnlHeader    = new Panel();
        _lblTitle     = new Label();
        _pnlMenu      = new TableLayoutPanel();
        _btnStatic     = new Button();
        _btnDynamic    = new Button();
        _btnService    = new Button();
        _btnCorrections = new Button();
        _btnPrint      = new Button();
        _btnLogs       = new Button();
        _pnlStatus    = new Panel();
        _dotConn      = new Panel();
        _lblConn      = new Label();
        _btnConn      = new Button();

        _pnlHeader.SuspendLayout();
        _pnlMenu.SuspendLayout();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // ── Header ────────────────────────────────────────────────────────────
        _lblTitle.Text      = "ВЕСЫ №13";
        _lblTitle.Font      = UiFonts.Title;
        _lblTitle.ForeColor = Forms.UiColors.TextOnDark;
        _lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        _lblTitle.Dock      = DockStyle.Fill;

        _pnlHeader.Dock      = DockStyle.Top;
        _pnlHeader.Height    = 100;
        _pnlHeader.BackColor = Forms.UiColors.HeaderBar;
        _pnlHeader.Controls.Add(_lblTitle);

        // ── Menu buttons ──────────────────────────────────────────────────────
        _btnStatic.Text      = "Взвешивание — Статика";
        _btnStatic.Dock      = DockStyle.Fill;
        _btnStatic.Font      = UiFonts.NavButton;
        _btnStatic.FlatStyle = FlatStyle.Flat;
        _btnStatic.Margin    = new Padding(0, 5, 0, 5);
        _btnStatic.BackColor = Forms.UiColors.HeaderBar;
        _btnStatic.ForeColor = Forms.UiColors.TextOnDark;
        _btnStatic.UseVisualStyleBackColor = false;
        _btnStatic.Cursor    = Cursors.Hand;
        _btnStatic.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnStatic.Click    += BtnStatic_Click;

        _btnDynamic.Text      = "Взвешивание — Динамика";
        _btnDynamic.Dock      = DockStyle.Fill;
        _btnDynamic.Font      = UiFonts.NavButton;
        _btnDynamic.FlatStyle = FlatStyle.Flat;
        _btnDynamic.Margin    = new Padding(0, 5, 0, 5);
        _btnDynamic.BackColor = Forms.UiColors.HeaderBar;
        _btnDynamic.ForeColor = Forms.UiColors.TextOnDark;
        _btnDynamic.UseVisualStyleBackColor = false;
        _btnDynamic.Cursor    = Cursors.Hand;
        _btnDynamic.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnDynamic.Click    += BtnDynamic_Click;

        _btnService.Text      = "Сервис";
        _btnService.Dock      = DockStyle.Fill;
        _btnService.Font      = UiFonts.NavButton;
        _btnService.FlatStyle = FlatStyle.Flat;
        _btnService.Margin    = new Padding(0, 5, 0, 5);
        _btnService.BackColor = Forms.UiColors.HeaderBar;
        _btnService.ForeColor = Forms.UiColors.TextOnDark;
        _btnService.UseVisualStyleBackColor = false;
        _btnService.Cursor    = Cursors.Hand;
        _btnService.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnService.Click    += BtnService_Click;

        _btnCorrections.Text      = "Корректировки";
        _btnCorrections.Dock      = DockStyle.Fill;
        _btnCorrections.Font      = UiFonts.NavButton;
        _btnCorrections.FlatStyle = FlatStyle.Flat;
        _btnCorrections.Margin    = new Padding(0, 5, 0, 5);
        _btnCorrections.BackColor = Forms.UiColors.HeaderBar;
        _btnCorrections.ForeColor = Forms.UiColors.TextOnDark;
        _btnCorrections.UseVisualStyleBackColor = false;
        _btnCorrections.Cursor    = Cursors.Hand;
        _btnCorrections.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnCorrections.Click    += BtnCorrections_Click;

        _btnPrint.Text      = "Печать отвесной";
        _btnPrint.Dock      = DockStyle.Fill;
        _btnPrint.Font      = UiFonts.NavButton;
        _btnPrint.FlatStyle = FlatStyle.Flat;
        _btnPrint.Margin    = new Padding(0, 5, 0, 5);
        _btnPrint.BackColor = Forms.UiColors.HeaderBar;
        _btnPrint.ForeColor = Forms.UiColors.TextOnDark;
        _btnPrint.UseVisualStyleBackColor = false;
        _btnPrint.Cursor    = Cursors.Hand;
        _btnPrint.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnPrint.Click    += BtnPrint_Click;

        _btnLogs.Text      = "Просмотр логов";
        _btnLogs.Dock      = DockStyle.Fill;
        _btnLogs.Font      = UiFonts.NavButton;
        _btnLogs.FlatStyle = FlatStyle.Flat;
        _btnLogs.Margin    = new Padding(0, 5, 0, 5);
        _btnLogs.BackColor = Forms.UiColors.HeaderBar;
        _btnLogs.ForeColor = Forms.UiColors.TextOnDark;
        _btnLogs.UseVisualStyleBackColor = false;
        _btnLogs.Cursor    = Cursors.Hand;
        _btnLogs.FlatAppearance.BorderColor = Forms.UiColors.ButtonBorder;
        _btnLogs.Click    += BtnLogs_Click;

        _pnlMenu.Dock        = DockStyle.Fill;
        _pnlMenu.ColumnCount = 1;
        _pnlMenu.RowCount    = 6;
        _pnlMenu.Padding     = new Padding(30, 16, 30, 16);
        _pnlMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));
        _pnlMenu.Controls.Add(_btnStatic,      0, 0);
        _pnlMenu.Controls.Add(_btnDynamic,     0, 1);
        _pnlMenu.Controls.Add(_btnService,     0, 2);
        _pnlMenu.Controls.Add(_btnCorrections, 0, 3);
        _pnlMenu.Controls.Add(_btnPrint,       0, 4);
        _pnlMenu.Controls.Add(_btnLogs,        0, 5);

        // ── Status bar ────────────────────────────────────────────────────────
        _dotConn.Size      = new Size(10, 10);
        _dotConn.Location  = new Point(10, 13);
        _dotConn.BackColor = Forms.UiColors.Disconnected;

        _lblConn.Text      = "АЦП: отключён";
        _lblConn.Font      = UiFonts.Body;
        _lblConn.ForeColor = Forms.UiColors.TextOnDarkMuted;
        _lblConn.Location  = new Point(26, 9);
        _lblConn.AutoSize  = true;

        _btnConn.Text      = "Подключить";
        _btnConn.Size      = new Size(90, 22);
        _btnConn.Anchor    = AnchorStyles.Right | AnchorStyles.Top;
        _btnConn.Location  = new Point(272, 7);
        _btnConn.Font      = UiFonts.Small;
        _btnConn.FlatStyle = FlatStyle.Flat;
        _btnConn.BackColor = Forms.UiColors.NavigationAction;
        _btnConn.ForeColor = Forms.UiColors.TextOnDark;
        _btnConn.UseVisualStyleBackColor = false;
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.Click    += BtnConn_Click;

        _pnlStatus.Dock      = DockStyle.Bottom;
        _pnlStatus.Height    = 36;
        _pnlStatus.BackColor = Forms.UiColors.StatusBar;
        _pnlStatus.Padding   = new Padding(8, 0, 8, 0);
        _pnlStatus.Controls.Add(_dotConn);
        _pnlStatus.Controls.Add(_lblConn);
        _pnlStatus.Controls.Add(_btnConn);

        // ── Form ──────────────────────────────────────────────────────────────
        Text            = "Весы №13";
        ClientSize      = new Size(380, 500);
        MinimumSize     = new Size(320, 440);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = Forms.UiColors.AppBackground;
        Controls.Add(_pnlMenu);
        Controls.Add(_pnlHeader);
        Controls.Add(_pnlStatus);

        _pnlHeader.ResumeLayout(false);
        _pnlMenu.ResumeLayout(false);
        _pnlStatus.ResumeLayout(false);
        _pnlStatus.PerformLayout();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ResumeLayout(false);
    }

    private Panel           _pnlHeader;
    private Label           _lblTitle;
    private TableLayoutPanel _pnlMenu;
    private Button          _btnStatic;
    private Button          _btnDynamic;
    private Button          _btnService;
    private Button          _btnCorrections;
    private Button          _btnPrint;
    private Button          _btnLogs;
    private Panel           _pnlStatus;
    private Panel           _dotConn;
    private Label           _lblConn;
    private Button          _btnConn;
}
