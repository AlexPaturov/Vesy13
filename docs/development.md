# Разработка и сопровождение

## Структура проекта

```text
Vesy13.sln
Vesy13.csproj
Program.cs
MainForm.cs
Forms/
Application/
Models/
Services/
  Configuration/
  Hardware/
  Repositories/
install/
ScaleListener/
docs/
```

`Vesy13.csproj` исключает `ScaleListener/**` из компиляции основного проекта.
`ScaleListener` собирается отдельным проектом.

## Зависимости основного проекта

- `Dapper` - легкий доступ к SQL.
- `Npgsql` - PostgreSQL.
- `FirebirdSql.Data.FirebirdClient` - Firebird.
- `QuestPDF` - генерация PDF.
- `Serilog` и `Serilog.Sinks.ApplicationInsights` - телеметрия и аудит.
- `System.IO.Ports` - COM-порт.

## Установка базы

Штатное развёртывание локальной базы выполняется скриптом `install/database/install.ps1` от имени администратора:

```powershell
powershell.exe -ExecutionPolicy Bypass -NoProfile -File install/database/install.ps1
```

Скрипт устанавливает и настраивает PostgreSQL, создаёт роль `scale_user`, базу `scale_db`, правила локального доступа и задание очистки, а также применяет идемпотентные обновления схемы. Файл `install/database/scale_db.sql` описывает исходную схему новой базы; его прямой запуск подходит только для подготовленного тестового окружения и не заменяет штатный установщик.

Таблицы создаются пустыми. Признаком заданной поправочных коэффициентов направления служит наличие активной строки в `direction_correction_profiles`, поэтому на новой станции таблица остаётся пустой до первой калибровки на эталонных грузах.

Историю изменений схемы смотреть командой `git log -- install/database/`.

## Ошибки и аудит

Большинство пользовательских сценариев ловит исключения, пишет событие через
`AuditLogger.Error` и показывает оператору короткое сообщение.

`AuditLogger` намеренно не бросает исключения из операций логирования. Это
снижает риск остановки весового процесса из-за недоступного журнала или
телеметрии.

## Текущие технические риски

- Строки подключения и телеметрия находятся в исходном коде. Учётные данные из них
  содержат только Firebird и Application Insights; локальная PostgreSQL работает через
  `trust` на петле и пароля в коде не имеет.
- Локальная PostgreSQL-строка подключения продублирована в `LocalRepository` и
  `AuditLogger`.
- Нет автоматической проверки и применения миграций при запуске.
- Статическая калибровка использует кусочно-постоянный выбор коэффициента без
  интерполяции.
- Операции сохранения взвешивания вызываются из `async void`; ошибка
  показывается оператору, но вызывающий поток не может дождаться результата.
- `ScaleListener` жестко использует `COM4`.
- Проект рассчитан на Windows и не является переносимым на Linux/macOS без
  изменения UI, COM и P/Invoke-частей.

## Рекомендуемые улучшения

- Вынести строки подключения Firebird и Application Insights в конфигурацию.
- Проверять установщик и обновление существующей базы на чистой тестовой Windows-ВМ.
- Добавить мигратор или проверку версии схемы при старте.
- Добавить интеграционные тесты для репозиториев на тестовых БД.
- Добавить модульные тесты для:
  - `CalibrationCalculator`;
  - `WeightFormatter`;
  - `SimA04Frame.Parse`;
  - сопоставления `LocalWagon` -> `GpriGras`.
- Убрать дублирование параметров подключения.
- Сделать порт `ScaleListener` настраиваемым.

## Где искать код по задачам

| Задача | Файлы |
| --- | --- |
| COM-порт и статический протокол АЦП | `Services/Hardware/SimA04ReaderStatic.cs`, `Models/SimA04Frame.cs` |
| COM-порт и динамический протокол АЦП | `Services/Hardware/SimA04ReaderDynamic.cs`, `Models/SimA04DynamicSample.cs`, `docs/protocol.md` |
| Статическое взвешивание | `Forms/StaticWeighingForm.cs` |
| Динамическое взвешивание | `Forms/DynamicWeighingForm.cs` |
| Калибровка и сервис | `Forms/ServiceForm.cs`, `Application/CalibrationCalculator.cs` |
| Локальная БД | `Services/Repositories/LocalRepository.cs`, `install/database/scale_db.sql` |
| Firebird | `Services/Repositories/FactoryRepository.cs` |
| Перенос и корректировка | `Forms/CorrectionsForm.cs` |
| Печать PDF | `Forms/PrintForm.cs`, `Application/WeighingSlipDocument.cs` |
| Аудит | `Services/Repositories/AuditLogger.cs`, `Forms/LogsForm.cs` |
| Настройки | `Services/Configuration/SettingsService.cs`, `AppSettings.cs` |
