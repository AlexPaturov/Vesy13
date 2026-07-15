# Вкладка «Сервисный режим Динамика» — схема размещения

Вкладка `_tabDynamicService` формы `ServiceForm`. Ниже — то, что на ней есть сейчас, и куда встают
контролы фильтров входного потока динамики.

## Что есть сейчас

```
_tabDynamicService  «Сервисный режим Динамика»
│
├─ _pnlTop        Dock=Top, h=101     → _tlpTop   (6 колонок × 2 строки)
│   ├─ строка 0:  [_cmbDynamicPort] [_dotDynamicConn] [_btnDynamicConn] [_btnDynamicPortRefresh] [_lblDynamicConn] [_btnDynamicClearLog]
│   └─ строка 1:  [_lblDynamicRate] [               ] [               ] [                      ] [               ] [_chkDynamicLog]
│
├─ _pnlChannels   Dock=Top, h=183     → _tlpChannels (2 колонки × 1 строка)
│   ├─ _pnlDynamicCh0 → _tlpCh0 → _lblDynamicCh0Cap (подпись) + _lblDynamicCh0 (живой код CH0)
│   └─ _pnlDynamicCh1 → _tlpCh1 → _lblDynamicCh1Cap (подпись) + _lblDynamicCh1 (живой код CH1)
│
└─ _pnlLogs       Dock=Fill           → _lstDynamicLog (owner-drawn ListBox, живой лог 30 Гц)
```

## Что добавить

Новая панель `_pnlDynamicFilters` встаёт **между каналами и логом**: параметры подбираются
по живым кодам CH0/CH1, которые видны прямо над ней.

```
_tabDynamicService  «Сервисный режим Динамика»
│
├─ _pnlTop        Dock=Top, h=101      (без изменений)
├─ _pnlChannels   Dock=Top, h=183      (без изменений)
│
├─ _pnlDynamicFilters   Dock=Top, h≈185      ← НОВАЯ ПАНЕЛЬ
│   └─ _tlpDynamicFilters   TableLayoutPanel, 6 колонок × 3 строки
│
│    ┌─────────────────┬────────────────┬────────────────────────┬─────────────────┬────────────────────┬──────────────────────────┐
│стр0│ _chkDynamicClamp│ «Мин. код»     │ _txtDynamicClampMin    │ «Макс. код»     │ _txtDynamicClampMax│ (пусто)                  │
│    │ «Клэмп»         │ (лейбл)        │                        │ (лейбл)         │                    │                          │
│    ├─────────────────┼────────────────┼────────────────────────┼─────────────────┼────────────────────┼──────────────────────────┤
│стр1│ _chkDynamicDelta│ «Макс. скачок» │ _txtDynamicDeltaMax    │ _chkDynamicStuck│ «Кодов подряд»     │ _txtDynamicStuckSamples  │
│    │ «Дельта-фильтр» │ (лейбл)        │                        │ «Застревание»   │ (лейбл)            │                          │
│    ├─────────────────┼────────────────┼────────────────────────┼─────────────────┼────────────────────┼──────────────────────────┤
│стр2│ _chkDynamicEma  │ «Коэффициент α»│ _txtDynamicEmaAlpha    │ (пусто)         │ (пусто)            │ (пусто)                  │
│    │ «EMA»           │ (лейбл)        │                        │                 │                    │                          │
│    └─────────────────┴────────────────┴────────────────────────┴─────────────────┴────────────────────┴──────────────────────────┘
│
└─ _pnlLogs       Dock=Fill             (без изменений)
```

## Контролы, которые нужны коду

Код `ServiceForm.cs` обращается только к этим именам; лейблы можно называть как угодно —
их код не читает и не переписывает.

| Имя | Тип | Что содержит |
| --- | --- | --- |
| `_chkDynamicClamp` | CheckBox | клэмп включён |
| `_txtDynamicClampMin` | TextBox | нижняя граница кода АЦП |
| `_txtDynamicClampMax` | TextBox | верхняя граница кода АЦП |
| `_chkDynamicDelta` | CheckBox | дельта-фильтр включён |
| `_txtDynamicDeltaMax` | TextBox | макс. допустимый скачок кода между сэмплами |
| `_chkDynamicStuck` | CheckBox | детектор застревания включён |
| `_txtDynamicStuckSamples` | TextBox | сколько строго равных кодов подряд считать застреванием (30 Гц: 150 ≈ 5 с) |
| `_chkDynamicEma` | CheckBox | EMA-сглаживание включено |
| `_txtDynamicEmaAlpha` | TextBox | коэффициент α, 0.01…1.00 |

Порог застревания задаётся **в сэмплах**, а не в секундах: динамика идёт потоком 30 Гц, статика
опрашивается на 5 Гц, и одна и та же «секунда застревания» — это разное число сэмплов.

## Замечания по дизайнеру

- **Порядок докинга.** `Dock=Top` укладывается по порядку добавления в `Controls`: панель, добавленная
  позже, встаёт ближе к верхнему краю. Сейчас порядок такой: `_pnlLogs` (Fill), затем `_pnlChannels`,
  затем `_pnlTop`. Чтобы фильтры оказались под каналами и над логом, `_pnlDynamicFilters` должен
  попасть в `Controls` **раньше** `_pnlChannels`.
- **Вертикальное центрирование в ячейке `TableLayoutPanel`.** `TextBox` и `CheckBox` — `Anchor = Left | Right`
  и `Margin = (4, 0, 4, 0)`; лейблы — `Dock = Fill` + `TextAlign = MiddleRight`. Тот же паттерн, что в
  `CorrectionsForm` (см. `_dtpTrainDate`).
- Колонки задать равными долями и не растаскивать мышью: при ресайзе дизайнер пересчитывает
  `ColumnStyles` от пикселей и оставляет кривые дроби, из-за чего столбцы разъезжаются.
