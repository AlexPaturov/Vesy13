namespace ScaleListener.FaultInjection;

/// <summary>
/// Строит одну секцию управления сбоем (Enable/Mode/параметры/кнопка ручного срабатывания),
/// привязанную напрямую к переданному <see cref="FaultState"/> - контролы читают/пишут его
/// поля по месту, наружу ничего кроме готового GroupBox не отдаётся.
/// </summary>
public static class FaultPanelBuilder
{
    public static GroupBox BuildPanel(FaultState state, string title, string magnitudeLabel, Action onManualTrigger)
    {
        var group = new GroupBox
        {
            Text = title,
            Dock = DockStyle.Top,
            Height = 96,
            Padding = new Padding(8, 4, 8, 4),
            BackColor = Color.FromArgb(255, 243, 224), // тёплый жёлто-оранжевый фон панели сбоев
        };

        var chkEnabled = new CheckBox
        {
            Text = "Вкл",
            Location = new Point(8, 20),
            Size = new Size(60, 22),
            Checked = state.Enabled,
        };
        chkEnabled.CheckedChanged += (_, _) => state.Enabled = chkEnabled.Checked;

        var lblMode = new Label { Text = "Режим:", Location = new Point(76, 22), Size = new Size(48, 20) };
        var cmbMode = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(126, 19),
            Size = new Size(110, 22),
        };
        cmbMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        cmbMode.SelectedIndex = (int)state.Mode;

        // Discrete: интервал (Periodic) / частота в минуту (Random).
        // Continuous: активность+пауза, сек (Periodic - фиксированные, Random - средние).
        var lblParam1 = new Label { Location = new Point(8, 48), Size = new Size(100, 20) };
        var numParam1 = new NumericUpDown
        {
            Location = new Point(112, 46), Size = new Size(72, 22),
            DecimalPlaces = 1, Minimum = 0.1m, Increment = 0.5m,
        };

        var lblParam2 = new Label { Location = new Point(192, 48), Size = new Size(64, 20) };
        var numParam2 = new NumericUpDown
        {
            Location = new Point(256, 46), Size = new Size(72, 22),
            DecimalPlaces = 1, Minimum = 0.1m, Increment = 0.5m,
        };

        bool hasMagnitude = magnitudeLabel != "—";
        var lblMagnitude = new Label { Text = magnitudeLabel, Location = new Point(336, 48), Size = new Size(90, 20), Visible = hasMagnitude };
        var numMagnitude = new NumericUpDown
        {
            Location = new Point(430, 46), Size = new Size(72, 22),
            DecimalPlaces = 2, Minimum = 0, Maximum = 200, Increment = 0.5m,
            Value = (decimal)state.Magnitude,
            Visible = hasMagnitude,
        };
        numMagnitude.ValueChanged += (_, _) => state.Magnitude = (double)numMagnitude.Value;

        var btnManual = new Button
        {
            Text = "Сработать сейчас",
            Location = new Point(8, 72),
            Size = new Size(150, 22),
            FlatStyle = FlatStyle.Flat,
        };
        btnManual.Click += (_, _) => onManualTrigger();

        void SyncParamVisibility()
        {
            bool isDiscrete = state.Kind == FaultKind.Discrete;
            bool isPeriodic = cmbMode.SelectedIndex == (int)FaultMode.Periodic;
            bool isRandom = cmbMode.SelectedIndex == (int)FaultMode.Random;
            bool isManual = cmbMode.SelectedIndex == (int)FaultMode.Manual;

            lblParam1.Visible = numParam1.Visible = isPeriodic || isRandom;
            lblParam2.Visible = numParam2.Visible = !isDiscrete && (isPeriodic || isRandom);
            btnManual.Visible = isManual;

            if (isDiscrete)
            {
                lblParam1.Text = isPeriodic ? "Интервал, с:" : "Частота, /мин:";
                numParam1.Maximum = isPeriodic ? 3600m : 60m; // потолок частоты - не даёт настроить сбой "почти на каждый сэмпл"
                numParam1.Value = (decimal)(isPeriodic ? state.IntervalSeconds : state.RatePerMinute);
            }
            else
            {
                lblParam1.Text = "Актив., с:";
                lblParam2.Text = "Пауза, с:";
                numParam1.Maximum = 600m;
                numParam2.Maximum = 3600m;
                numParam1.Value = (decimal)state.ActiveSeconds;
                numParam2.Value = (decimal)state.GapSeconds;
            }
        }

        cmbMode.SelectedIndexChanged += (_, _) =>
        {
            state.Mode = (FaultMode)cmbMode.SelectedIndex;
            SyncParamVisibility();
        };

        numParam1.ValueChanged += (_, _) =>
        {
            if (state.Kind == FaultKind.Discrete)
            {
                if (state.Mode == FaultMode.Periodic) state.IntervalSeconds = (double)numParam1.Value;
                else state.RatePerMinute = (double)numParam1.Value;
            }
            else
            {
                state.ActiveSeconds = (double)numParam1.Value;
            }
        };
        numParam2.ValueChanged += (_, _) => state.GapSeconds = (double)numParam2.Value;

        SyncParamVisibility();

        group.Controls.AddRange(new Control[]
        {
            chkEnabled, lblMode, cmbMode, lblParam1, numParam1, lblParam2, numParam2,
            lblMagnitude, numMagnitude, btnManual,
        });
        return group;
    }
}
