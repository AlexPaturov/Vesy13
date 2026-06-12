# Working Notes

Branch: `serviceform-designer-refactor`

## Workflow
- User makes the designer change in Visual Studio.
- I review the current diff and layout.
- We agree on the change.
- Then I commit and push.

## Active focus
- `CorrectionsForm` is the active form.
- Keep the layout work scoped to the grids and their surrounding container logic.
- Treat the current grid layout as the baseline unless the user asks for a wider refactor.

## CorrectionsForm rules
- Grid columns should use explicit `Width` plus `MinimumWidth`.
- Do not switch grid columns back to `Fill` unless the user explicitly asks for that behavior.
- Keep `AllowUserToResizeColumns = true` for both grids.
- Preserve operator control over the column widths; do not block manual resizing.
- Use DPI only for initial sizing if needed, not as a hard override of operator-resizable widths.
- Keep date/time/weight columns compact, but readable.

## Style rules
- Keep fonts and colors centralized in `UiFonts` / `UiColors`.
- Do not reintroduce local layout/style duplication in form logic.
- Avoid touching unrelated user changes.
- Prefer targeted layout fixes over broad rewrites.

## Files to leave alone unless explicitly requested
- `ScaleListener/MainForm.cs`
- `docs/rules.md`
- `Vesy13.sln`

## Conventional Commits
Use conventional commit prefixes.

- `feat` - new functionality
- `fix` - bug fix
- `chore` - maintenance
- `docs` - documentation
- `refactor` - refactor
- `test` - tests
- `build` - build, Docker, CI
- `ci` - GitHub Actions
- `style` - formatting
- `perf` - performance
- `breaking` - breaking change when needed

## Session Context
- Active page: `Forms/CorrectionsForm.cs`.
- Current business flow is stable: refresh loads both grids, transfer writes to Firebird then marks PostgreSQL transferred, save updates an existing Firebird row.
- Right-grid processed-row highlight is in-memory only for the current window session and should stay limited to the single most recent processed record.
- Left-grid transferred rows remain visible and are tinted `#FFDAB9`; processed right-grid rows are tinted `#98FB98`.
- After transfer and refresh, the left grid restores focus, scrolls to the just-transferred local row, and keeps that row selected for operator visibility.
- Selecting a transferred left-grid row should not reopen transfer actions; the detail panel is cleared while the row selection stays visible.
- Keep avoiding extra database storage for session history and avoid reintroducing manual row insertion into the right grid.

## ADC COM port settings
- Use local `settings.json` next to the application executable as the persistent source for workstation-level application settings.
- If `settings.json` is missing, create it automatically with default values.
- Default values: ADC COM port `COM1`, max capacity (NPV) `140 t`, weight discretization `0.05 t`, operator zero limit `2%` of NPV, admin-configurable zero limit `100%` of NPV.
- `settings.json` stores configurable values: ADC COM port, max capacity (NPV), weight discretization, zero settings, and administrator password hash data.
- `settings.json` contains default values and is read by application modules that need configuration.
- Users with the administrator password can configure and save the working ADC COM port, NPV, discretization, and zero settings from the settings UI.
- Saving a new working ADC COM port does not forcibly reconnect an already-open ADC session; the new value is applied on the next automatic connection.
- `MainForm` automatic ADC connection reads the working port from `settings.json`, not from a hard-coded constant.
- `ServiceForm` -> `Мониторинг`: existing COM-port behavior stays unchanged. This tab is a manual diagnostic/temporary connection tool for the shared `SimA04Reader`; it is not the persistent source of the working ADC port.
- Keep the monitor combo/button logic separate from saved settings unless explicitly requested later.
- Dynamic weighing collection window is an internal hard-coded algorithm parameter, not a user setting and not a `settings.json` value.
- Weight discretization stays as a user setting in `settings.json`; apply it after weight calculation for displayed and saved mass values.
- Administrator password is stored in `settings.json` only as PBKDF2 hash + salt; never store plaintext or reversible encrypted password. The initial default password is `vesy13fuck` and is written only as hash + salt.
