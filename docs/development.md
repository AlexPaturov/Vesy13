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
migrations/
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

## Типовые команды

Сборка всего решения:

```bash
dotnet build Vesy13.sln
```

Запуск основного приложения:

```bash
dotnet run --project Vesy13.csproj
```

Запуск эмулятора:

```bash
dotnet run --project ScaleListener/ScaleListener.csproj
```

## Работа с миграциями

Миграции лежат в `migrations/` и применяются вручную. В проекте нет
автоматического мигратора, поэтому при развертывании нужно явно выполнять SQL в
правильном порядке.

Текущий порядок:

1. `migration_v2.sql`
2. `002_calibration_points.sql`
3. `003_calibration_points_deleted_at.sql`

## Ошибки и аудит

Большинство пользовательских сценариев ловит исключения, пишет событие через
`AuditLogger.Error` и показывает оператору короткое сообщение.

`AuditLogger` намеренно не бросает исключения из операций логирования. Это
снижает риск остановки весового процесса из-за недоступного журнала или
телеметрии.

## План тестирования динамического протокола

До доработки основного алгоритма динамического взвешивания сначала проверить
формат потока на других усреднениях из таблицы динамики в `docs/protocol.md`.
Цель проверки - подтвердить или опровергнуть, что схема `5 байт = 1 сэмпл`
работает для всех нужных режимов, а не только для `Set=126`, `Req=254`.

Порядок проверки:

1. Для каждого выбранного усреднения снять короткий DEC-дамп через `DynamicDumpDec`.
2. Проверить, что после warmup поток стабильно раскладывается как `CH0_LO CH0_HI CH1_LO CH1_HI AUX`.
3. Проверить диапазоны `CH0` и `CH1` на статической нагрузке.
4. Проверить правило `AUX = B0 + B2 + 28` и счетчик `SKIPPED_BYTES`.
5. Для `Set=126`, `Req=254` правило подтверждено контрольным запуском: `RAW_BYTES=1205`, `SAMPLES=240`, `SKIPPED_BYTES=5`, `CH0=3585/3606.35/3626`, `CH1=3586/3605.94/3626`.
6. Проверка других усреднений выполнена: `Avg64` и `Avg128` подтверждены, `Avg32` требует просмотра строк, `Avg1/2/4/8/16` не использовать.
7. Для переноса в основную программу брать `Avg128` как основной режим и `Avg64` как резервный.
8. Только после этого переносить разбор динамического потока в основную программу.

## Текущие технические риски

- Строки подключения и телеметрия находятся в исходном коде.
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

- Вынести строки подключения в конфигурацию.
- Добавить явную документацию/скрипт развертывания PostgreSQL-пользователя.
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
| COM-порт и протокол АЦП | `Services/Hardware/SimA04Reader.cs`, `Models/SimA04Frame.cs` |
| Статическое взвешивание | `Forms/StaticWeighingForm.cs` |
| Динамическое взвешивание | `Forms/DynamicWeighingForm.cs` |
| Калибровка и сервис | `Forms/ServiceForm.cs`, `Application/CalibrationCalculator.cs` |
| Локальная БД | `Services/Repositories/LocalRepository.cs`, `migrations/` |
| Firebird | `Services/Repositories/FactoryRepository.cs` |
| Перенос и корректировка | `Forms/CorrectionsForm.cs` |
| Печать PDF | `Forms/PrintForm.cs`, `Application/WeighingSlipDocument.cs` |
| Аудит | `Services/Repositories/AuditLogger.cs`, `Forms/LogsForm.cs` |
| Настройки | `Services/Configuration/SettingsService.cs`, `AppSettings.cs` |
