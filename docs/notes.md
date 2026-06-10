# Working Notes

Branch: `serviceform-designer-refactor`

## Current workflow
- User edits forms in Visual Studio designer.
- I review the diff and current layout.
- We agree the change.
- Then I commit and push.

## Active focus
- `CorrectionsForm` is the active form being adjusted.
- Top area layout has been split into smaller panels.
- `StaticWeighingForm` still has an uncommitted designer tweak.

## Style rules
- Keep fonts and colors centralized in `UiFonts` / `UiColors`.
- Do not reintroduce local layout/style duplication in form logic.
- Avoid touching unrelated user changes.

## Files to leave alone unless explicitly requested
- `ScaleListener/MainForm.cs`
- `docs/rules.md`
- `Vesy13.sln`

### Conventional Commits

Используем стандарт + добавили **`breaking:`** как тип верхнего уровня.
Типы:

```
feat       — новая функциональность
fix        — исправление бага
breaking   — ломающее изменение (видно в git log сразу!)
chore      — обслуживание
docs       — документация
refactor   — рефакторинг
test       — тесты
build      — сборка, Docker, CI
ci         — GitHub Actions
style      — форматирование
perf       — производительность
```