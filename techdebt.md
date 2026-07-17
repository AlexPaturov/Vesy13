# Tech Debt

## РЕШЕНИЕ 2026-07-17: аудит при недоступной БД — закрыто, делать не будем

Долг снят с очереди решением владельца проекта. **Код не менялся**: очередь аудита
(`QueueCapacity = 30000`), вытеснение старых записей, ретраи воркера и показ состояния
очереди в статусной строке форм взвешивания работают как раньше. Раздел оставлен, чтобы
вопрос не заводили заново.

Исходное требование: при недоступной локальной PostgreSQL аудит не ведётся вообще — не
накапливается, не ретраится, не влияет на UI.

Причина отказа: разбор вариантов реализации показал, что цена выше пользы. Существенное:

- Событие `CalibrationFallback` (`1008`) доставляется в `audit_log` **только** благодаря
  ретраям. Оно пишется в `Program.cs`, когда БД недоступна на старте и калибровка поднята
  из кэша `settings.json`, а сам `audit_log` лежит в той же `scale_db` — записать его в
  момент возникновения нельзя. Отказ от очереди уничтожает единственный след того, что
  смена работала на кэшированной калибровке; отдельного UI-состояния для этого нет.
- Очередь в здоровом режиме работает сериализатором записи. Фильтры пишут аудит на каждое
  срабатывание без троттлинга (`FilterClamp/Delta/Stuck/Checksum`), динамика — поток ~30 Гц.
- Отличить «БД недоступна» от одной транзиентной ошибки (`DbTimeoutSeconds = 3`, перезапуск
  postgres, дедлок) можно только эвристикой; ошибка эвристики стоит потери принятых записей
  при живой БД.
- Проверка живости коннектом не равна «INSERT пройдёт» (нет таблицы, права, диск, схема) —
  даёт флаппинг с фоновой долбёжкой коннектами вместо тишины, которой требовало требование.

Приток в очередь при этом уже ограничен, и это остаётся в силе: статика не пишет в аудит
поток АЦП (`eb73246`), обе формы пишут только события. Взвешивание падение БД переживает
самостоятельно — `SaveAsync` ловит ошибку, не блокирует оператора и показывает
«БД недоступна, запись не сохранена».

Известное следствие, принятое осознанно: `!audit.IsDatabaseAvailable` в `hasError` остаётся
единственным ранним признаком мёртвой БД на экране оператора — флаг
`_weighingStorageAvailable` ставится только постфактум, при неудачном сохранении вагона.

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
