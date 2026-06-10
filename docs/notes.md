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
