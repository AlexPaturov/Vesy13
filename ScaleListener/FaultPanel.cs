using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Секция управления одним сбоем (Enable/Mode/параметры/«Сработать сейчас»), привязывается
/// к конкретному <see cref="FaultState"/> через <see cref="Bind"/> - статическая разметка
/// живёт в FaultPanel.Designer.cs и редактируется дизайнером, привязка к данным - в рантайме.
/// </summary>
public partial class FaultPanel : UserControl
{
    private FaultState _state = null!;
    private Action _onManualTrigger = null!;

    public FaultPanel()
    {
        InitializeComponent();
    }

    public void Bind(FaultState state, string title, string magnitudeLabel, Action onManualTrigger)
    {
        _state = state;
        _onManualTrigger = onManualTrigger;

        _group.Text = title;
        _chkEnabled.Checked = state.Enabled;
        _cmbMode.SelectedIndex = (int)state.Mode;

        bool hasMagnitude = magnitudeLabel != "—";
        _lblMagnitude.Text = magnitudeLabel;
        _lblMagnitude.Visible = hasMagnitude;
        _numMagnitude.Visible = hasMagnitude;
        _numMagnitude.Value = (decimal)state.Magnitude;

        SyncParamVisibility();
    }

    private void ChkEnabled_CheckedChanged(object? sender, EventArgs e) => _state.Enabled = _chkEnabled.Checked;

    private void CmbMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        _state.Mode = (FaultMode)_cmbMode.SelectedIndex;
        SyncParamVisibility();
    }

    private void NumParam1_ValueChanged(object? sender, EventArgs e)
    {
        if (_state.Kind == FaultKind.Discrete)
        {
            if (_state.Mode == FaultMode.Periodic) _state.IntervalSeconds = (double)_numParam1.Value;
            else _state.RatePerMinute = (double)_numParam1.Value;
        }
        else
        {
            _state.ActiveSeconds = (double)_numParam1.Value;
        }
    }

    private void NumParam2_ValueChanged(object? sender, EventArgs e) => _state.GapSeconds = (double)_numParam2.Value;

    private void NumMagnitude_ValueChanged(object? sender, EventArgs e) => _state.Magnitude = (double)_numMagnitude.Value;

    private void BtnManual_Click(object? sender, EventArgs e) => _onManualTrigger();

    // Discrete: интервал (Periodic) / частота в минуту, потолок 60 (Random).
    // Continuous: активность+пауза, сек (Periodic - фиксированные, Random - средние).
    private void SyncParamVisibility()
    {
        bool isDiscrete = _state.Kind == FaultKind.Discrete;
        bool isPeriodic = _cmbMode.SelectedIndex == (int)FaultMode.Periodic;
        bool isRandom = _cmbMode.SelectedIndex == (int)FaultMode.Random;
        bool isManual = _cmbMode.SelectedIndex == (int)FaultMode.Manual;

        _lblParam1.Visible = _numParam1.Visible = isPeriodic || isRandom;
        _lblParam2.Visible = _numParam2.Visible = !isDiscrete && (isPeriodic || isRandom);
        _btnManual.Visible = isManual;

        if (isDiscrete)
        {
            _lblParam1.Text = isPeriodic ? "Интервал, с:" : "Частота, /мин:";
            _numParam1.Maximum = isPeriodic ? 3600m : 60m; // потолок частоты - не даёт настроить сбой "почти на каждый сэмпл"
            _numParam1.Value = (decimal)(isPeriodic ? _state.IntervalSeconds : _state.RatePerMinute);
        }
        else
        {
            _lblParam1.Text = "Актив., с:";
            _lblParam2.Text = "Пауза, с:";
            _numParam1.Maximum = 600m;
            _numParam2.Maximum = 3600m;
            _numParam1.Value = (decimal)_state.ActiveSeconds;
            _numParam2.Value = (decimal)_state.GapSeconds;
        }
    }
}
