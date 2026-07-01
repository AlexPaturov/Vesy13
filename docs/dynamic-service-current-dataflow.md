# Текущие потоки данных: сервисная динамика и калибровка динамики

Дата фиксации: 2026-07-01.

Документ описывает текущее фактическое устройство `ServiceForm` после выполнения групп 1 и 2: обработчики данных и workflow подключения разделены по вкладкам. Диагностические счётчики ещё частично общие, это осталось для группы 3.

## Точки входа

Внутри `ServiceForm` динамические процессы используют разные экземпляры reader-а:

```text
ServiceForm._dynamicServiceSim : SimA04ReaderDynamic
ServiceForm._dynamicCalibSim   : SimA04ReaderDynamic
```

`_dynamicServiceSim` приходит извне, как и раньше, и обслуживает вкладку `Сервисный режим Динамика`.
`_dynamicCalibSim` создаётся внутри `ServiceForm` и обслуживает только вкладку `Калибровка Динамика`.

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
  -> SetDynamicCalibDataSubscription(active tab == _tabCalibD)
```

Для `Сервисный режим Динамика`:

```text
_dynamicServiceSim.RawSampleReceived += OnDynamicServiceRawSample
_dynamicServiceSim.SampleReceived    += OnDynamicServiceSample
```

Для `Калибровка Динамика`:

```text
_dynamicCalibSim.SampleReceived += OnDynamicCalibSample
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
  -> CloseDynamicCalibConnection()
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
  -> OnDynamicServiceRawSample(raw)
  -> BeginInvoke(...), если событие пришло не на UI-потоке
  -> guard: активная вкладка должна быть _tabDynamicService
  -> if (_chkDynamicLog.Checked)
  -> SimA04DynamicSample.Parse(raw)
  -> AppendDynamicLog(...)
  -> _rtbDynamicLog.AppendText(...)
  -> _rtbDynamicLog.ScrollToCaret()
```

`_rtbDynamicLog` физически находится на вкладке `_tabDynamicService`.

## Вкладка "Калибровка Динамика"

Назначение вкладки: калибровка коэффициентов динамики, просмотр текущего кода АЦП и расчёт live-веса.

Путь подключения:

```text
_btnDynamicCalibConn.Click
  -> BtnDynamicCalibConn_Click
  -> ToggleDynamicCalibConnection(_cmbDynamicCalibPort.SelectedItem, ...)
  -> CloseStaticConnection()
  -> CloseDynamicServiceConnection()
  -> _dynamicCalibSim.Open(port) / CloseDynamicCalibConnection()
  -> UpdateDynamicCalibMonitorConn(...)
```

Путь parsed sample для калибровки:

```text
_dynamicCalibSim.SampleReceived
  -> OnDynamicCalibSample(sample)
  -> сохранить sample как latest
  -> RefreshDynamicSampleDisplay() по timer 100 ms
  -> _lastDynCh0 / _lastDynCh1
  -> UpdateLiveDynamicCalibrationLabels()
  -> _lblLiveAdcD.Text
  -> _lblLiveWeightD.Text
```

Расчёт live-веса на вкладке калибровки:

```text
CurrentDynamicAdcCode()
  -> выбранный канал из _dynamicCalibSim.Channel
  -> code * KPlus / code * KMinus
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
  -> если вкладка _tabDynamicService: CloseStaticConnection(); CloseDynamicCalibConnection()
  -> если вкладка _tabCalibD: CloseStaticConnection(); CloseDynamicServiceConnection()
  -> иначе CloseStaticConnection(); CloseDynamicConnections()
```

Переход между `_tabDynamicService` и `_tabCalibD` больше не удерживает общий stream.

## Текущая блок-схема

```text
  _tabDynamicService                                      _tabCalibD
          |                                                     |
          v                                                     v
  _dynamicServiceSim                                   _dynamicCalibSim
          |                                                     |
          | RawSampleReceived + SampleReceived                  | SampleReceived only
          |                                                     |
          v                                                     v
  OnDynamicServiceRawSample                          OnDynamicCalibSample
  OnDynamicServiceSample                             latest sample buffer
          |                                                     |
          v                                                     v
  _rtbDynamicLog                                     RefreshDynamicSampleDisplay
  _lblDynamicCh0/Ch1                                 timer 100 ms
                                                                |
                                                                v
                                                     _lblLiveAdcD/_lblLiveWeightD
```

## Что ещё не разделено

Осталась группа 3:

```text
- диагностика всё ещё частично использует общие счётчики
```
