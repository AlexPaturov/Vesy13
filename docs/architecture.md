# Архитектура

## Точка входа

`Program.Main` выполняет стартовую инициализацию:

1. включает community-лицензию QuestPDF;
2. инициализирует WinForms;
3. загружает или создает `settings.json`;
4. создает `SimA04ReaderStatic` и `SimA04ReaderDynamic`;
5. загружает кэш калибровки из PostgreSQL через `LocalRepository`;
6. инициализирует аудит;
7. открывает `MainForm`.

`MainForm` является навигационным хабом. Главная форма не управляет COM-портом АЦП напрямую.
Формы взвешивания открывают порт при загрузке и закрывают его при закрытии.
Сервисная форма использует ручное подключение/отключение для мониторинга и настроек.
Статика работает с watchdog `2000` мс, динамика и сервис - с `1000` мс.

## Слои

### UI: `Forms/`

- `MainForm` - главное меню.
- `StaticWeighingForm` - статическое взвешивание.
- `DynamicWeighingForm` - динамическое взвешивание.
- `ServiceForm` - настройки, мониторинг и калибровка.
- `CorrectionsForm` - перенос локальных записей и корректировка Firebird.
- `PrintForm` - поиск записей и генерация PDF-отвесной.
- `LogsForm` - просмотр и экспорт журнала аудита.
- `PasswordDialog` - ввод пароля администратора.
- `UiColors`, `UiFonts` - единая визуальная тема.

### Application: `Application/`

- `CalibrationCalculator`:
  - статический пересчет кода АЦП в тонны по активным точкам канала;
  - расчет линейной аппроксимации методом наименьших квадратов;
  - динамический пересчет по коэффициенту направления.
- `WeighingSlipDocument` - QuestPDF-документ отвесной.

### Services

- `SimA04ReaderStatic` - драйвер COM-порта для статического режима и сервисного мониторинга; таймаут живой связи задается формой: `2000` мс в статике, `1000` мс в сервисе.
- `SimA04ReaderDynamic` - драйвер непрерывного 5-байтового потока для динамического режима; таймаут живой связи `1000` мс.
- `SettingsService` - загрузка/создание/сохранение `settings.json`.
- `PasswordHasher` - PBKDF2-SHA256 для пароля администратора.
- `WeightFormatter` - округление веса по дискретности.
- `LocalRepository` - PostgreSQL `scale_db`.
- `FactoryRepository` - Firebird `GPRI`/`GRAS`.
- `AuditLogger` - запись аудита в PostgreSQL и Application Insights.

### Models

- `SimA04Frame` - один 4-байтовый статический кадр АЦП с каналами `Ch0`, `Ch1`.
- `SimA04DynamicSample` - один 5-байтовый динамический сэмпл с каналами `Ch0`, `Ch1` и `AUX`.
- `CalibPoint`, `DynamicCalib` - калибровка.
- `LocalWagon` - локальное взвешивание вагона.
- `GpriGras` - запись заводских таблиц Firebird.
- `TaraOption` - вариант тары для выбора.
- `AuditRecord` - строка журнала аудита.

## Поток данных взвешивания

```text
АЦП СИМ А04
  -> SimA04ReaderStatic -> SimA04Frame -> StaticWeighingForm
  -> SimA04ReaderDynamic -> SimA04DynamicSample -> DynamicWeighingForm
  -> CalibrationCalculator
  -> LocalRepository.SaveWagonAsync
  -> PostgreSQL.wagon_weighing
  -> CorrectionsForm
  -> FactoryRepository.InsertAsync
  -> Firebird.GPRI / Firebird.GRAS
```

## Поток данных печати

```text
PrintForm
  -> FactoryRepository.GetForPrintAsync
  -> Firebird.GPRI / Firebird.GRAS
  -> WeighingSlipDocument
  -> PDF во временной папке
```

## Поток аудита

```text
Формы / сервисы
  -> AuditLogger.Action или AuditLogger.Error
  -> PostgreSQL.audit_log
  -> Serilog Application Insights sink
```

`AuditLogger` не пробрасывает исключения наружу: ошибки записи журнала не должны
ломать основной сценарий оператора.
