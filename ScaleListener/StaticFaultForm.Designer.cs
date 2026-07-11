using ScaleListener.FaultInjection;

namespace ScaleListener;

partial class StaticFaultForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _top = new FlowLayoutPanel();
        _btnCycle = new Button();
        _btnClearHistory = new Button();
        _panels = new FlowLayoutPanel();
        _panelSilence = new FaultPanel();
        _panelSpike = new FaultPanel();
        _panelDrift = new FaultPanel();
        _panelCorrupt = new FaultPanel();
        _panelStuck = new FaultPanel();
        _history = new FaultHistoryListBox();
        _top.SuspendLayout();
        _panels.SuspendLayout();
        SuspendLayout();
        //
        // _top
        //
        _top.Controls.Add(_btnCycle);
        _top.Controls.Add(_btnClearHistory);
        _top.Dock = DockStyle.Top;
        _top.Height = 40;
        _top.Location = new Point(0, 0);
        _top.Name = "_top";
        _top.Padding = new Padding(8);
        _top.Size = new Size(560, 40);
        _top.TabIndex = 0;
        //
        // _btnCycle
        //
        _btnCycle.FlatStyle = FlatStyle.Flat;
        _btnCycle.Location = new Point(11, 11);
        _btnCycle.Name = "_btnCycle";
        _btnCycle.Size = new Size(150, 26);
        _btnCycle.TabIndex = 0;
        _btnCycle.Click += BtnCycle_Click;
        //
        // _btnClearHistory
        //
        _btnClearHistory.FlatStyle = FlatStyle.Flat;
        _btnClearHistory.Location = new Point(167, 11);
        _btnClearHistory.Name = "_btnClearHistory";
        _btnClearHistory.Size = new Size(140, 26);
        _btnClearHistory.TabIndex = 1;
        _btnClearHistory.Text = "Очистить историю";
        _btnClearHistory.Click += BtnClearHistory_Click;
        //
        // _panels
        //
        _panels.Controls.Add(_panelSilence);
        _panels.Controls.Add(_panelSpike);
        _panels.Controls.Add(_panelDrift);
        _panels.Controls.Add(_panelCorrupt);
        _panels.Controls.Add(_panelStuck);
        _panels.Dock = DockStyle.Top;
        _panels.FlowDirection = FlowDirection.TopDown;
        _panels.Location = new Point(0, 40);
        _panels.Name = "_panels";
        _panels.Padding = new Padding(4);
        _panels.Size = new Size(560, 520);
        _panels.TabIndex = 1;
        _panels.WrapContents = false;
        //
        // _panelSilence
        //
        _panelSilence.Location = new Point(7, 7);
        _panelSilence.Name = "_panelSilence";
        _panelSilence.Size = new Size(536, 96);
        _panelSilence.TabIndex = 0;
        //
        // _panelSpike
        //
        _panelSpike.Location = new Point(7, 109);
        _panelSpike.Name = "_panelSpike";
        _panelSpike.Size = new Size(536, 96);
        _panelSpike.TabIndex = 1;
        //
        // _panelDrift
        //
        _panelDrift.Location = new Point(7, 211);
        _panelDrift.Name = "_panelDrift";
        _panelDrift.Size = new Size(536, 96);
        _panelDrift.TabIndex = 2;
        //
        // _panelCorrupt
        //
        _panelCorrupt.Location = new Point(7, 313);
        _panelCorrupt.Name = "_panelCorrupt";
        _panelCorrupt.Size = new Size(536, 96);
        _panelCorrupt.TabIndex = 3;
        //
        // _panelStuck
        //
        _panelStuck.Location = new Point(7, 415);
        _panelStuck.Name = "_panelStuck";
        _panelStuck.Size = new Size(536, 96);
        _panelStuck.TabIndex = 4;
        //
        // _history
        //
        _history.Dock = DockStyle.Fill;
        _history.FormattingEnabled = true;
        _history.Location = new Point(0, 560);
        _history.Name = "_history";
        _history.Size = new Size(560, 220);
        _history.TabIndex = 2;
        //
        // StaticFaultForm
        //
        BackColor = Color.FromArgb(255, 250, 240);
        ClientSize = new Size(560, 780);
        Controls.Add(_history);
        Controls.Add(_panels);
        Controls.Add(_top);
        MinimumSize = new Size(560, 700);
        Name = "StaticFaultForm";
        Text = "Сбои — Статика";
        _top.ResumeLayout(false);
        _panels.ResumeLayout(false);
        ResumeLayout(false);
    }

    private FlowLayoutPanel _top;
    private Button _btnCycle;
    private Button _btnClearHistory;
    private FlowLayoutPanel _panels;
    private FaultPanel _panelSilence;
    private FaultPanel _panelSpike;
    private FaultPanel _panelDrift;
    private FaultPanel _panelCorrupt;
    private FaultPanel _panelStuck;
    private FaultHistoryListBox _history;
}
