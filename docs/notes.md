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
- Keep avoiding extra database storage for session history and avoid reintroducing manual row insertion into the right grid.
