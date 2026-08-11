# Текущие потоки данных: сервисная динамика и калибровка динамики

Дата фиксации: 2026-07-01. Актуализировано 2026-07-28 (см. правки «Точки входа» и «Путь raw-log»).

Документ описывает текущее фактическое устройство `ServiceForm` после выполнения групп 1, 2 и 3: обработчики данных, workflow подключения и диагностические счётчики разделены по вкладкам.

## Точки входа

Внутри `ServiceForm` динамические процессы используют разные экземпляры reader-а:

```text
ServiceForm._dynamicServiceSim : SimA04ReaderDynamic
ServiceForm._directionCorrectionSim   : SimA04ReaderDynamic
```

Оба reader-а создаются внутри самой `ServiceForm` (`new SimA04ReaderDynamic`) — после рефакторинга развязки reader-ов (`docs/status_2026-08-07.md`) ни один не приходит извне; раньше `_dynamicServiceSim` был общим экземпляром с `MainForm._dynamicSim`. `_dynamicServiceSim` обслуживает вкладку `Сервисный режим Динамика`, `_directionCorrectionSim` — только вкладку `Коэффициенты направлений`.

Каждый `SimA04ReaderDynamic` читает свой открытый COM-порт, собирает 5-байтовый динамический сэмпл и публикует события:

```text
COM port bytes
  -> SimA04ReaderDynamic.ProcessByte
  -> RawSampleReceived(raw bytes)
  -> SampleReceived(parsed SimA04DynamicSample)
```

## Подписки на данные

Подписки на raw/sample поток переключаются по активной вкладке:

```text
Tabs_SelectedIndexChanged
  -> UpdateDynamicDataSubscriptions()
  -> SetDynamicServiceDataSubscription(active tab == _tabDynamicService)
  -> SetDirectionCorrectionProfileDataSubscription(active tab == _tabDirectionCorrections)
```

Для `Сервисный режим Динамика`:

```text
_dynamicServiceSim.RawSampleReceived += OnDynamicServiceRawSample
_dynamicServiceSim.SampleReceived    += OnDynamicServiceSample
```

Для `Коэффициенты направлений`:

```text
_directionCorrectionSim.SampleReceived += OnDirectionCorrectionProfileSample
```

Raw-подписки для калибровки динамики нет.

## Вкладка "Сервисный режим Динамика"

Назначение вкладки: смотреть поток динамического АЦП, CH0/CH1 и raw-log.

Путь подключения:

```text
_btnDynamicConn.Click
  -> BtnDynamicConn_Click
  -> ToggleDynamicServiceConnection(_cmbDynamicPort.SelectedItem, ...)
  -> CloseStaticConnection()
  -> CloseDirectionCorrectionProfileConnection()
  -> _dynamicServiceSim.Open(port) / CloseDynamicServiceConnection()
  -> UpdateDynamicServiceMonitorConn(...)
```

Путь parsed sample:

```text
_dynamicServiceSim.SampleReceived
  -> OnDynamicServiceSample(sample)
  -> BeginInvoke(...), если событие пришло не на UI-потоке
  -> _lblDynamicCh0.Text
  -> _lblDynamicCh1.Text
```

Путь raw-log:

```text
_dynamicServiceSim.RawSampleReceived
  -> OnDynamicServiceRawSample(raw)   // приходит на потоке reader-а, не на UI
  -> if (!_chkDynamicLog.Checked) return
  -> SimA04DynamicSample.Parse(raw)
  -> FormatDynamicServiceLogLine(raw, sample)
  -> Enqueue в _dynamicServiceLogQueue (лимит 500, лишнее вытесняется)
```

Очередь дренируется на UI-потоке по таймеру:

```text
таймер -> FlushDynamicServiceLogQueue()
  -> перелить очередь в _dynamicServiceLogBatch под локом
  -> _lstDynamicLog.BeginUpdate()
  -> AddDynamicLogLine(...) для каждой строки  // Insert(0), лимит DynamicServiceLogLineLimit = 300
  -> _lstDynamicLog.TopIndex = 0
  -> _lstDynamicLog.EndUpdate()
```

Лог сервисной динамики — owner-drawn `ListBox` `_lstDynamicLog` (элементы `DynamicServiceLogLine`), физически на вкладке `_tabDynamicService`. Цвет хранится на самом элементе, `RichTextBox` (`_rtbDynamicLog`) больше не используется. Гейтинг по активной вкладке выполняется на уровне подписки (`SetDynamicServiceDataSubscription`), а не внутри обработчика.

## Вкладка "Коэффициенты направлений"

Назначение вкладки: калибровка коэффициентов динамики, просмотр текущего кода АЦП и расчёт live-веса.

Путь подключения:

```text
_btnDirectionCorrectionProfileConn.Click
  -> BtnDirectionCorrectionProfileConn_Click
  -> ToggleDirectionCorrectionProfileConnection(_cmbDirectionCorrectionProfilePort.SelectedItem, ...)
  -> CloseStaticConnection()
  -> CloseDynamicServiceConnection()
  -> _directionCorrectionSim.Open(port) / CloseDirectionCorrectionProfileConnection()
  -> UpdateDirectionCorrectionProfileMonitorConn(...)
```

Путь parsed sample для калибровки:

```text
_directionCorrectionSim.SampleReceived
  -> OnDirectionCorrectionProfileSample(sample)
  -> сохранить sample как latest
  -> RefreshDynamicSampleDisplay() по timer 100 ms
  -> _lastDynCh0 / _lastDynCh1
  -> UpdateLiveDirectionCorrectionLabels()
  -> _lblLiveAdcD.Text
  -> _lblLiveWeightD.Text
```

Расчёт live-веса на вкладке калибровки:

```text
CurrentDynamicAdcCode()
  -> выбранный канал из _directionCorrectionSim.Channel
  -> code * right_direction_correction_factor / code * left_direction_correction_factor
  -> FormatServiceDynamicWeight(...)
  -> _lblLiveWeightD
```

Raw-log калибровка динамики не получает и не пишет.

## Переключение вкладок

При смене вкладки выполняются разделённые действия:

```text
Tabs_SelectedIndexChanged
  -> UpdateDynamicDataSubscriptions()
  -> если вкладка _tabMonitor или _tabCalibS: CloseDynamicConnections()
  -> если вкладка _tabDynamicService: CloseStaticConnection(); CloseDirectionCorrectionProfileConnection()
  -> если вкладка _tabDirectionCorrections: CloseStaticConnection(); CloseDynamicServiceConnection()
  -> иначе CloseStaticConnection(); CloseDynamicConnections()
```

Переход между `_tabDynamicService` и `_tabDirectionCorrections` больше не удерживает общий stream.

## Текущая блок-схема

```text
  _tabDynamicService                                      _tabDirectionCorrections
          |                                                     |
          v                                                     v
  _dynamicServiceSim                                   _directionCorrectionSim
          |                                                     |
          | RawSampleReceived + SampleReceived                  | SampleReceived only
          |                                                     |
          v                                                     v
  OnDynamicServiceRawSample                          OnDirectionCorrectionProfileSample
  OnDynamicServiceSample                             latest sample buffer
          |                                                     |
          v                                                     v
  _lstDynamicLog (через очередь + таймер)            RefreshDynamicSampleDisplay
  _lblDynamicCh0/Ch1                                 timer 100 ms
                                                                |
                                                                v
                                                     _lblLiveAdcD/_lblLiveWeightD
```

## Граница ответственности

`Сервисный режим Динамика` и `Коэффициенты направлений` используют отдельные reader-ы,
подписки и workflow подключения. Они не должны снова делить reader, raw-поток,
журнал или состояние подключения.
