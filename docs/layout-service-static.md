# Вкладка «Сервисный режим Статика» — схема размещения

Вкладка `_tabMonitor` формы `ServiceForm`. Ниже — то, что на ней есть сейчас, и куда встают
контролы фильтров входного потока статики.

## Что есть сейчас

```
_tabMonitor  «Сервисный режим Статика»
│
├─ _pnlMonitorTop        Dock=Top, h=101     → _tlpMonitorTop   (6 колонок × 2 строки)
│   ├─ строка 0:  [_cmbPort] [_dotConn] [_btnConn] [_btnPortRefresh] [_lblConn] [_btnClearLog]
│   └─ строка 1:  [_lblRate] [       ] [        ] [               ] [        ] [_chkLog]
│
├─ _pnlMonitorChannels   Dock=Top, h=183     → _tlpMonitorChannels (2 колонки × 1 строка)
│   ├─ _pnlCh0 → _lblCh0Cap (подпись) + _lblCh0 (живой код CH0)
│   └─ _pnlCh1 → _lblCh1Cap (подпись) + _lblCh1 (живой код CH1)
│
└─ _pnlMonitorLogs       Dock=Fill           → _lstLog (owner-drawn ListBox, живой лог)
```

## Что добавить

Новая панель `_pnlMonitorFilters` встаёт **между каналами и логом**: параметры подбираются
по живым кодам CH0/CH1, которые видны прямо над ней.

```
_tabMonitor  «Сервисный режим Статика»
│
├─ _pnlMonitorTop        Dock=Top, h=101      (без изменений)
├─ _pnlMonitorChannels   Dock=Top, h=183      (без изменений)
│
├─ _pnlMonitorFilters    Dock=Top, h≈130      ← НОВАЯ ПАНЕЛЬ
│   └─ _tlpMonitorFilters   TableLayoutPanel, 6 колонок × 2 строки
│
│       ┌────────────────┬──────────────┬──────────────────────┬──────────────┬──────────────────────┬──────────────────────┐
│  стр0 │ _chkStaticClamp│ «Мин. код»   │ _txtStaticClampMin   │ «Макс. код»  │ _txtStaticClampMax   │ (пусто)              │
│       │ «Клэмп»        │ (лейбл)      │                      │ (лейбл)      │                      │                      │
│       ├────────────────┼──────────────┼──────────────────────┼──────────────┼──────────────────────┼──────────────────────┤
│  стр1 │ _chkStaticDelta│ «Макс.скачок»│ _txtStaticDeltaMax   │ _chkStaticEma│ «Коэффициент α»      │ _txtStaticEmaAlpha   │
│       │ «Дельта-фильтр»│ (лейбл)      │                      │ «EMA»        │ (лейбл)              │                      │
│       └────────────────┴──────────────┴──────────────────────┴──────────────┴──────────────────────┴──────────────────────┘
│
└─ _pnlMonitorLogs       Dock=Fill            (без изменений)
```

## Контролы, которые нужны коду

Код `ServiceForm.cs` обращается только к этим именам; лейблы можно называть как угодно —
их код не читает и не переписывает.

| Имя | Тип | Что содержит |
| --- | --- | --- |
| `_chkStaticClamp` | CheckBox | клэмп включён |
| `_txtStaticClampMin` | TextBox | нижняя граница кода АЦП |
| `_txtStaticClampMax` | TextBox | верхняя граница кода АЦП |
| `_chkStaticDelta` | CheckBox | дельта-фильтр включён |
| `_txtStaticDeltaMax` | TextBox | макс. допустимый скачок кода между кадрами |
| `_chkStaticEma` | CheckBox | EMA-сглаживание включено |
| `_txtStaticEmaAlpha` | TextBox | коэффициент α, 0.01…1.00 |

Детектора застревания на статике нет намеренно: вагон, расцепленный под башмак, законно стоит
неподвижно, и постоянный код с датчика — признак нормального взвешивания, а не неисправности.

## Замечания по дизайнеру

- **Порядок докинга.** `Dock=Top` укладывается по порядку добавления в `Controls`: панель, добавленная
  позже, встаёт ближе к верхнему краю. Сейчас порядок такой: `_pnlMonitorLogs` (Fill), затем
  `_pnlMonitorChannels`, затем `_pnlMonitorTop`. Чтобы фильтры оказались под каналами и над логом,
  `_pnlMonitorFilters` должен попасть в `Controls` **раньше** `_pnlMonitorChannels`.
- **Вертикальное центрирование в ячейке `TableLayoutPanel`.** `TextBox` и `CheckBox` — `Anchor = Left | Right`
  и `Margin = (4, 0, 4, 0)`; лейблы — `Dock = Fill` + `TextAlign = MiddleRight`. Тот же паттерн, что в
  `CorrectionsForm` (см. `_dtpTrainDate`).
- Колонки задать равными долями и не растаскивать мышью: при ресайзе дизайнер пересчитывает
  `ColumnStyles` от пикселей и оставляет кривые дроби, из-за чего столбцы разъезжаются.
