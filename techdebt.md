# Tech Debt

## Взвешивание должно переживать падение локальной БД

- Priority: **0** (высший; браться сразу после текущей задачи о фильтрах входного потока).
- Требование: при недоступной локальной PostgreSQL программа обязана продолжать **взвешивать**. Это единственная функция, которая должна работать в этом режиме.
- Аудит и любое другое логирование при недоступной БД **не ведутся вообще** — они не должны ни блокировать взвешивание, ни накапливаться, ни влиять на UI.
- Сейчас в этом режиме программа продолжает писать аудит через `AuditLogger`: очередь (`QueueCapacity = 30000`) копится, при переполнении вытесняется самая старая запись (`RemoveFirst()`, счётчик `_droppedCount`), воркер циклически ретраит запись в БД (`RetryDelayMs`).
- Scope: `Services/Repositories/AuditLogger.cs` и точки его вызова; поведение форм взвешивания при `_databaseAvailable = 0`.
- Не смешивать с задачей о фильтрах: коды аудита фильтров (2020–2024) вводятся в рамках текущей задачи и переосмысливаются здесь вместе со всем остальным аудитом.

## Line endings policy for generated WinForms files

- Files: `*.Designer.cs`, potentially other Windows-oriented project files.
- Problem: the project currently has mixed or CRLF line endings in designer files, and `git diff --check` can report CRLF-added lines as trailing whitespace.
- Why it matters: routine one-line designer changes may produce noisy validation output or accidental line-ending churn.
- Recommended fix:
  - add a root `.gitattributes` with explicit `eol` rules
  - keep `*.Designer.cs` and other WinForms/project files on `crlf`
  - keep Markdown and other text docs on `lf`
  - run `git add --renormalize .` once in a separate technical commit
- Priority: low
- Scope: repository-wide metadata and line-ending normalization; do not mix with feature fixes.
