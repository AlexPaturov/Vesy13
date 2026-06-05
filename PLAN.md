# План доработки CorrectionsForm

## Задача 1 — Режимы Тара / Брутто

### 1. `Models/FbRecord.cs`
- Добавить поле `Id` (int) — нужен для UPDATE в задаче 2
- Добавить поле `Table` (string) — "GPRI" или "GRAS", чтобы знать в какую таблицу делать UPDATE

### 2. `Services/FirebirdService.cs` — `GetRecentAsync`
- Добавить в SELECT поля `ID` и метку источника (`'GPRI' AS SRC` / `'GRAS' AS SRC`)
- Обновить маппинг в `FbRecord`

### 3. `Services/FirebirdService.cs` — `InsertAsync`
- Добавить параметр `bool isTara`
- При `isTara = true`:
  - `BRUTTO = 0`
  - `TAR_BRS = row.Total` (вес с весов идёт в тару)
  - `NETTO = NULL`
  - `GRUZ = "Тара"` (принудительно, UI уже заблокирует поле)
- При `isTara = false` — поведение как сейчас

### 4. `Forms/CorrectionsForm.designer` — UI
- Добавить новую группу радиокнопок: `_rbTara` / `_rbBrutto`
  (размещается под существующими `_rbGpri` / `_rbGras`, место уже освобождено)
- Добавить кнопку `_btnSave` ("Сохранить") рядом с `_btnTransfer`
  (изначально скрыта/неактивна)

### 5. `Forms/CorrectionsForm.cs` — логика Тара/Брутто
- Обработчик `_rbTara.CheckedChanged`:
  - Заблокировать `_txtGruz`, вписать "Тара"
- Обработчик `_rbBrutto.CheckedChanged`:
  - Разблокировать `_txtGruz`, очистить если там было "Тара"
- Обновить `BtnTransfer_Click` — передавать `_rbTara.Checked` в `InsertAsync`
- Обновить `ClearTopPanel` — сбрасывать `_rbBrutto.Checked = true` (дефолт)

---

## Задача 2 — Корректировка записей из нижнего грида

### 6. `Services/FirebirdService.cs` — `UpdateAsync` (новый метод)
- Принимает: `table`, `id`, `nvag`, `ndok`, `gruz`, `tarBrs`, `netto`, `potr`, `plat`, `isTara`
- SQL: `UPDATE {table} SET NVAG=..., NDOK=..., GRUZ=..., TAR_BRS=..., NETTO=..., POTR=..., PLAT=... WHERE ID=@id`
- При `isTara`: `BRUTTO = 0`, `TAR_BRS = существующий BRUTTO`, `NETTO = NULL`

### 7. `Forms/CorrectionsForm.cs` — поле `_selectedFb`
- Добавить `private FbRecord? _selectedFb`
- Три взаимоисключающих состояния формы:
  - **Ожидание** — оба грида без выделения, обе кнопки неактивны
  - **Перенос** — выбрана строка в левом гриде → активна "Перенести"
  - **Редактирование** — выбрана строка в правом гриде → активна "Сохранить"

### 8. `Forms/CorrectionsForm.cs` — `GridDone_SelectionChanged`
- При клике на строку в `_gridDone`:
  - Снять выделение в `_gridPend`
  - Сохранить `_selectedFb = row.Tag as FbRecord`
  - Заполнить поля: `_txtNvag`, `_txtNdok`, `_txtGruz`, `_tbPotr`, `_tbPlat`
  - Определить режим: если `r.Gruz == "Тара"` → `_rbTara.Checked = true`, иначе `_rbBrutto`
  - Определить направление: `r.Table == "GPRI"` → `_rbGpri.Checked = true`, иначе `_rbGras`
  - Загрузить тара-опции по NVAG (как в `TxtNvag_Leave`), выбрать совпадающую по `TarDok`
  - Показать `_btnSave`, скрыть/деактивировать `_btnTransfer`

### 9. `Forms/CorrectionsForm.cs` — `BtnSave_Click`
- Собрать данные из полей (те же что и для `BtnTransfer_Click`)
- Вызвать `_fb.UpdateAsync(...)`
- Обновить строку в `_gridDone` на месте (не перезагружать весь грид)
- Вызвать `ClearTopPanel`

### 10. `Forms/CorrectionsForm.cs` — `ClearTopPanel`
- Сбросить `_selectedFb = null`
- Скрыть `_btnSave`, вернуть `_btnTransfer` в исходное состояние
- Разблокировать `_txtGruz`
- Сбросить режим на `_rbBrutto.Checked = true`
- Снять выделение в обоих гридах

---

## Порядок выполнения

1. `FbRecord` — поля Id, Table  
2. `GetRecentAsync` — добавить ID и SRC в SELECT  
3. `InsertAsync` — параметр isTara  
4. `UpdateAsync` — новый метод  
5. Designer — радиокнопки Тара/Брутто + кнопка Сохранить  
6. Логика Тара/Брутто в форме  
7. `GridDone_SelectionChanged` + заполнение полей  
8. `BtnSave_Click`  
9. `ClearTopPanel` — расширить  
