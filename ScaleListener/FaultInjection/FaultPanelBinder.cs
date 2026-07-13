namespace ScaleListener.FaultInjection;

/// <summary>
/// Невизуальная привязка набора контролов одной панели сбоя к её <see cref="FaultState"/>.
/// Сами контролы живут в Designer.cs формы (каждая панель настраивается в дизайнере
/// независимо), сюда передаются готовыми ссылками — здесь только чтение/запись состояния
/// и переключение видимости параметров под текущий режим.
/// </summary>
public sealed class FaultPanelBinder
{
    private readonly FaultState _state;
    private readonly CheckBox _chkEnabled;
    private readonly ComboBox _cmbMode;
    private readonly Label _lblParam1;
    private readonly NumericUpDown _numParam1;
    private readonly Label? _lblParam2;
    private readonly NumericUpDown? _numParam2;
    private readonly NumericUpDown? _numMagnitude;
    private readonly Button _btnManual;
    private readonly Action _onManualTrigger;

    public FaultPanelBinder(
        FaultState state,
        CheckBox chkEnabled,
        ComboBox cmbMode,
        Label lblParam1,
        NumericUpDown numParam1,
        Label? lblParam2,
        NumericUpDown? numParam2,
        NumericUpDown? numMagnitude,
        Button btnManual,
        Action onManualTrigger)
    {
        _state = state;
        _chkEnabled = chkEnabled;
        _cmbMode = cmbMode;
        _lblParam1 = lblParam1;
        _numParam1 = numParam1;
        _lblParam2 = lblParam2;
        _numParam2 = numParam2;
        _numMagnitude = numMagnitude;
        _btnManual = btnManual;
        _onManualTrigger = onManualTrigger;

        _chkEnabled.Checked = state.Enabled;
        _cmbMode.SelectedIndex = (int)state.Mode;
        if (_numMagnitude is not null)
            _numMagnitude.Value = (decimal)state.Magnitude;

        _chkEnabled.CheckedChanged += (_, _) => _state.Enabled = _chkEnabled.Checked;
        _cmbMode.SelectedIndexChanged += (_, _) =>
        {
            _state.Mode = (FaultMode)_cmbMode.SelectedIndex;
            SyncParamVisibility();
        };
        _numParam1.ValueChanged += (_, _) =>
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
        };
        if (_numParam2 is not null)
            _numParam2.ValueChanged += (_, _) => _state.GapSeconds = (double)_numParam2.Value;
        if (_numMagnitude is not null)
            _numMagnitude.ValueChanged += (_, _) => _state.Magnitude = (double)_numMagnitude.Value;
        _btnManual.Click += (_, _) => _onManualTrigger();

        SyncParamVisibility();
    }

    // Discrete: интервал (Periodic) / частота в минуту, потолок 60 (Random).
    // Continuous: активность + пауза, сек (Periodic - фиксированные, Random - средние).
    private void SyncParamVisibility()
    {
        bool isDiscrete = _state.Kind == FaultKind.Discrete;
        bool isPeriodic = _cmbMode.SelectedIndex == (int)FaultMode.Periodic;
        bool isRandom = _cmbMode.SelectedIndex == (int)FaultMode.Random;
        bool isManual = _cmbMode.SelectedIndex == (int)FaultMode.Manual;

        _lblParam1.Visible = _numParam1.Visible = isPeriodic || isRandom;
        if (_lblParam2 is not null) _lblParam2.Visible = !isDiscrete && (isPeriodic || isRandom);
        if (_numParam2 is not null) _numParam2.Visible = !isDiscrete && (isPeriodic || isRandom);
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
            _numParam1.Maximum = 600m;
            _numParam1.Value = (decimal)_state.ActiveSeconds;
            if (_lblParam2 is not null) _lblParam2.Text = "Пауза, с:";
            if (_numParam2 is not null)
            {
                _numParam2.Maximum = 3600m;
                _numParam2.Value = (decimal)_state.GapSeconds;
            }
        }
    }
}
