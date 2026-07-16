# Tech Debt

## Аудит не должен вестись при недоступной БД

- Priority: **0** (высший).
- Требование: при недоступной локальной PostgreSQL аудит и любое другое логирование **не ведутся вообще** — они не должны ни блокировать взвешивание, ни накапливаться, ни влиять на UI.
- Сейчас в этом режиме программа продолжает писать аудит через `AuditLogger`: очередь (`QueueCapacity = 30000`) копится, при переполнении вытесняется самая старая запись (`RemoveFirst()`, счётчик `_droppedCount`), воркер циклически ретраит запись в БД (`RetryDelayMs`). Обе формы взвешивания показывают оператору состояние этой очереди в статусной строке («БД недоступна; очередь N/30000; потеряно M») — то есть аудит влияет на UI.
- Scope: `Services/Repositories/AuditLogger.cs` и точки его вызова; статусная строка форм взвешивания.
- Сделано ранее и сюда не входит: само **взвешивание падение БД переживает** — `SaveAsync` ловит ошибку записи, не блокирует оператора и показывает «БД недоступна, запись не сохранена»; калибровка на старте поднимается из кэша `settings.json` (`RestoreLastKnownCalibration`, аудит `1008`).
- Сделано ранее и сюда не входит: приток в очередь сокращён — статика перестала писать в аудит поток АЦП (`eb73246`), теперь обе формы пишут только события.

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
