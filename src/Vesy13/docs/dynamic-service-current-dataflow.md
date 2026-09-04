# Текущие потоки данных: сервисная динамика и калибровка динамики

## Точки входа


## Подписки на данные

Подписки на raw/sample поток переключаются по активной вкладке:

```text
Tabs_SelectedIndexChanged
  -> UpdateDynamicDataSubscriptions()
  -> SetDynamicServiceDataSubscription(active tab == _tabDynamicService)
  -> SetDirectionCorrectionProfileDataSubscription(active tab == _tabDirectionCorrections)
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
