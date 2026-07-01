# Текущие потоки данных: сервисная динамика и калибровка динамики

Дата фиксации: 2026-07-01.

Документ описывает текущее фактическое устройство `ServiceForm` после выполнения группы 2: обработчики данных разделены по вкладкам, но connection workflow ещё общий.

## Общая точка входа

Сейчас обе вкладки формы сервиса всё ещё используют один экземпляр:

```text
ServiceForm._dynamicSim : SimA04ReaderDynamic
```

Это относится к группе 1 и ещё не исправлено.

`SimA04ReaderDynamic` читает COM-порт, собирает 5-байтовый динамический сэмпл и публикует два события:

```text
COM port bytes
  -> SimA04ReaderDynamic.ProcessByte
  -> RawSampleReceived(raw bytes)
  -> SampleReceived(parsed SimA04DynamicSample)
```

## Подписки на данные

Подписки на raw/sample поток теперь переключаются по активной вкладке:

```text
Tabs_SelectedIndexChanged
  -> UpdateDynamicDataSubscriptions()
  -> SetDynamicServiceDataSubscription(active tab == _tabDynamicService)
  -> SetDynamicCalibDataSubscription(active tab == _tabCalibD)
```

Для `Сервисный режим Динамика`:

```text
_dynamicSim.RawSampleReceived += OnDynamicServiceRawSample
_dynamicSim.SampleReceived    += OnDynamicServiceSample
```

Для `Калибровка Динамика`:

```text
_dynamicSim.SampleReceived += OnDynamicCalibSample
```

Raw-подписки для калибровки динамики больше нет.

## Вкладка "Сервисный режим Динамика"

Назначение вкладки: смотреть поток динамического АЦП, CH0/CH1 и raw-log.

Текущий путь подключения всё ещё общий:

```text
_btnDynamicConn.Click
  -> BtnDynamicConn_Click
  -> ToggleDynamicConnection(_cmbDynamicPort.SelectedItem, ...)
  -> _dynamicSim.Open(port) / _dynamicSim.Close()
```

Текущий путь parsed sample:

```text
_dynamicSim.SampleReceived
  -> OnDynamicServiceSample(sample)
  -> BeginInvoke(...), если событие пришло не на UI-потоке
  -> _lblDynamicCh0.Text
  -> _lblDynamicCh1.Text
```

Текущий путь raw-log:

```text
_dynamicSim.RawSampleReceived
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

Текущий путь подключения всё ещё общий:

```text
_btnDynamicCalibConn.Click
  -> BtnDynamicCalibConn_Click
  -> ToggleDynamicConnection(_cmbDynamicCalibPort.SelectedItem, ...)
  -> _dynamicSim.Open(port) / _dynamicSim.Close()
```

Текущий путь parsed sample для калибровки:

```text
_dynamicSim.SampleReceived
  -> OnDynamicCalibSample(sample)
  -> сохранить sample как latest
  -> RefreshDynamicSampleDisplay() по timer 100 ms
  -> _lastDynCh0 / _lastDynCh1
  -> UpdateLiveDynamicCalibrationLabels()
  -> _lblLiveAdcD.Text
  -> _lblLiveWeightD.Text
```

Текущий расчёт live-веса на вкладке калибровки:

```text
CurrentDynamicAdcCode()
  -> выбранный канал из _dynamicSim.Channel
  -> code * KPlus / code * KMinus
  -> FormatServiceDynamicWeight(...)
  -> _lblLiveWeightD
```

Raw-log калибровка динамики больше не получает и не пишет.

## Переключение вкладок

При смене вкладки сейчас выполняются две вещи:

```text
Tabs_SelectedIndexChanged
  -> UpdateDynamicDataSubscriptions()
  -> если вкладка _tabDynamicService или _tabCalibD: CloseStaticConnection(); динамическое подключение не закрывать
  -> иначе закрыть неактуальные подключения
```

Connection workflow всё ещё общий: переход между `_tabDynamicService` и `_tabCalibD` удерживает один stream.

## Текущая блок-схема

```text
                         +------------------------+
                         | SimA04ReaderDynamic    |
                         | _dynamicSim общий      |
                         +-----------+------------+
                                     |
                         active tab selects subscriptions
                                     |
          +--------------------------+--------------------------+
          |                                                     |
          v                                                     v
  _tabDynamicService                                     _tabCalibD
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

Группа 2 выполнена для обработчиков данных, но остаются связи из других групп:

```text
- общий _dynamicSim
- общий ToggleDynamicConnection(...)
- общий UpdateDynamicMonitorConn(...)
- Tabs_SelectedIndexChanged всё ещё считает две вкладки владельцами одного stream
- диагностика всё ещё частично использует общие счётчики
```
