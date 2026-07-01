# Checklist: нежелательные связи сервисной динамики и калибровки динамики

Дата фиксации: 2026-07-01.

Документ нужен как рабочий checklist для поэтапной проверки и устранения связей. Текущее устройство описано в `dynamic-service-current-dataflow.md`.

## Точки связи

- [ ] Общий экземпляр `ServiceForm._dynamicSim` используется двумя процессами:
  `Сервисный режим Динамика` и `Калибровка Динамика`.

- [x] Общая подписка на raw-поток:
  `_dynamicSim.RawSampleReceived += OnDynamicRawSample`.
  Raw-поток нужен сервисному режиму для raw-log, но не нужен калибровке динамики.

- [x] Общая подписка на parsed samples:
  `_dynamicSim.SampleReceived += OnDynamicSample`.
  Один обработчик обслуживает и сервисные CH0/CH1 панели, и live-поля калибровки.

- [x] `OnDynamicSample` обновляет данные обеих вкладок:
  `_lblDynamicCh0/_lblDynamicCh1` и `_lblLiveAdcD/_lblLiveWeightD`.

- [x] `OnDynamicRawSample` пишет в `_rtbDynamicLog`, даже когда активна вкладка `Калибровка Динамика`.
  `_rtbDynamicLog` физически находится на вкладке `Сервисный режим Динамика`.

- [x] `_chkDynamicLog` находится на сервисной вкладке, но его состояние влияет на обработку raw-событий во время калибровки динамики.

- [ ] `ToggleDynamicConnection` общий для двух кнопок подключения:
  `BtnDynamicConn_Click` и `BtnDynamicCalibConn_Click`.
  Подключение/отключение одной вкладки меняет состояние другой.

- [ ] `UpdateDynamicMonitorConn` обновляет UI обеих вкладок из одного метода:
  `_lblDynamicConn`, `_btnDynamicConn`, `_cmbDynamicPort`, `_btnDynamicCalibConn`, `_cmbDynamicCalibPort`, `_lblDynamicCalibConn`.

- [ ] `Tabs_SelectedIndexChanged` считает `_tabDynamicService` и `_tabCalibD` одним владельцем динамического подключения:
  переход между ними не разделяет процессы, а удерживает общий stream.

- [ ] Диагностика `AdcDynamicService` и `AdcDynamicCalib` использует общие счётчики:
  `_dynamicSampleUiPosted`, `_dynamicSampleUiApplied`, `_dynamicLogAppended`, `_dynamicSim.RawBytesReceived`, `_dynamicSim.SamplesReceived`.

## Главная проблемная цепочка

```text
Калибровка Динамика
  -> общий _dynamicSim
  -> общий RawSampleReceived
  -> OnDynamicRawSample
  -> _rtbDynamicLog на вкладке Сервисный режим Динамика
```

Статус: устранено в группе 2. Raw-подписка теперь включается только для вкладки `Сервисный режим Динамика`, а вкладка `Калибровка Динамика` получает только parsed samples через свой обработчик.
