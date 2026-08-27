# SimA04DynamicFrame — текущая согласованная модель

## Назначение

`SimA04DynamicFrame` представляет результат обработки одного кандидата динамического кадра SIM A04.

Объект создаётся через:

```csharp
public static SimA04DynamicFrame Parse(byte[]? data)
```

`Parse` принимает вход любого состояния:

- `null`;
- пустой массив;
- массив из 1–4 байт;
- ровно 5 байт;
- больше 5 байт.

Внешний код не обязан предварительно проверять:

- `null`;
- длину;
- checksum.

Эти проверки выполняются внутри `SimA04DynamicFrame`.

Возвращаемый объект никогда не является `null`.

---

# Формат динамического кадра

Полный динамический кадр SIM A04 содержит 5 байт:

```text
B0 B1 B2 B3 AUX
```

Декодирование каналов:

```text
CH0 = B1 * 256 + B0
CH1 = B3 * 256 + B2
```

Checksum:

```text
AUX = (B0 + B1 + B2 + B3) & 0xFF
```

Размер кадра `5` является внутренней деталью `Parse`:

```csharp
const int frameSize = 5;
```

Он не должен быть публичной константой объекта.

---

# Типы полей

```csharp
public byte? B0 { get; private set; }
public byte? B1 { get; private set; }
public byte? B2 { get; private set; }
public byte? B3 { get; private set; }
public byte? Aux { get; private set; }

public int? Ch0 { get; private set; }
public int? Ch1 { get; private set; }

public int ReceivedByteCount { get; private set; }

public FrameState State { get; private set; }

public bool IsValid => State == FrameState.Valid;
```

## Почему `byte?`

`0` является допустимым реальным значением протокола.

Поэтому нельзя использовать `0` как признак отсутствия данных.

Семантика:

```text
null   -> байт не был получен
0      -> реально получен 0x00
1..255 -> реально получено соответствующее значение
```

Сам объект `SimA04DynamicFrame` при этом не nullable.

## Валидация диапазона byte

Если значение уже имеет тип:

```csharp
byte
```

C# гарантирует диапазон:

```text
0..255
```

Дополнительная проверка диапазона внутри `SimA04DynamicFrame` не нужна.

`-1` и `300` не могут существовать внутри `byte`.

---

# FrameState

```csharp
public enum FrameState
{
    Created,
    NullInput,
    Empty,
    Incomplete,
    InvalidLength,
    CandidateReady,
    InvalidChecksum,
    Valid
}
```

---

# Описание состояний

## Created

Начальное состояние объекта.

```text
объект создан
вход ещё не классифицирован
```

Допустимые переходы:

```text
Created -> NullInput
Created -> Empty
Created -> Incomplete
Created -> InvalidLength
Created -> CandidateReady
```

---

## NullInput

В `Parse` передано:

```csharp
data == null
```

Никакие байты не получены.

Финальное состояние данного объекта.

---

## Empty

Передан существующий массив:

```csharp
data.Length == 0
```

Массив существует, но данных в нём нет.

Финальное состояние.

---

## Incomplete

Получено от 1 до 4 байт:

```text
1 <= data.Length < 5
```

Объект сохраняет только реально полученные значения.

Пример:

```text
data = [15, 0, 27]
```

Состояние объекта:

```text
B0  = 15
B1  = 0
B2  = 27
B3  = null
Aux = null

Ch0 = 15
Ch1 = null

State = Incomplete
```

Значения, которых не было на входе, вручную `null` не присваиваются.  
Они просто остаются неполученными.

Финальное состояние данного объекта.

---

## InvalidLength

Получено больше 5 байт:

```csharp
data.Length > 5
```

Такой массив не считается одним корректно ограниченным кандидатом кадра.

Финальное состояние.

---

## CandidateReady

Получено ровно 5 байт:

```csharp
data.Length == 5
```

На переходе в это состояние:

- заполнены `B0`;
- `B1`;
- `B2`;
- `B3`;
- `Aux`;
- вычислен `Ch0`;
- вычислен `Ch1`.

После этого объект готов к проверке checksum.

Это промежуточное состояние.

Допустимые переходы:

```text
CandidateReady -> InvalidChecksum
CandidateReady -> Valid
```

---

## InvalidChecksum

Получено ровно 5 байт, но:

```text
AUX != (B0 + B1 + B2 + B3) & 0xFF
```

Все пять байт реально существуют.

`Ch0` и `Ch1` также вычислены.

Однако кадр целиком невалиден.

Финальное состояние.

---

## Valid

Получено ровно 5 байт и checksum корректен.

Объект полностью материализован и может использоваться далее.

Финальное состояние.

---

# Схема конечного автомата

```text
                         Parse(data)
                             │
                             ▼
                         ┌─────────┐
                         │ Created │
                         └────┬────┘
                              │
          ┌───────────────────┼────────────────────┐
          │                   │                    │
       data=null          Length=0             Length>5
          │                   │                    │
          ▼                   ▼                    ▼
     ┌───────────┐        ┌───────┐       ┌───────────────┐
     │ NullInput │        │ Empty │       │ InvalidLength │
     └───────────┘        └───────┘       └───────────────┘


Created
   │
   │ Length 1..4
   │ + загрузить реально пришедшие байты
   ▼
┌────────────┐
│ Incomplete │
└────────────┘


Created
   │
   │ Length == 5
   │ + загрузить B0..Aux
   │ + вычислить Ch0/Ch1
   ▼
┌────────────────┐
│ CandidateReady │
└───────┬────────┘
        │
        │ checksum
        │
   ┌────┴─────┐
   │          │
 wrong       correct
   │          │
   ▼          ▼
┌─────────────────┐   ┌───────┐
│ InvalidChecksum │   │ Valid │
└─────────────────┘   └───────┘
```

---

# Таблица переходов

| Текущее состояние | Условие | Следующее состояние |
|---|---|---|
| `Created` | `data == null` | `NullInput` |
| `Created` | `data.Length == 0` | `Empty` |
| `Created` | `data.Length` 1..4 | `Incomplete` |
| `Created` | `data.Length > 5` | `InvalidLength` |
| `Created` | `data.Length == 5` | `CandidateReady` |
| `CandidateReady` | checksum неверен | `InvalidChecksum` |
| `CandidateReady` | checksum верен | `Valid` |

Другие переходы запрещены.

Примеры запрещённых переходов:

```text
Valid -> Incomplete
Empty -> CandidateReady
InvalidChecksum -> Valid
Incomplete -> Valid
```

Для нового входного кандидата создаётся новый объект.

---

# Управление переходами

Состояние не должно присваиваться произвольно из разных мест класса.

Переход выполняется через единый механизм:

```csharp
TransitionTo(FrameState nextState)
```

Допустимость перехода проверяется централизованно:

```csharp
CanTransitionTo(FrameState nextState)
```

Это позволяет сохранить корректную цепочку состояний автомата.

---

# Разделение ответственности внутри объекта

## Parse

`Parse` отвечает за сценарий обработки входа:

```text
получить data
    ↓
классифицировать вход
    ↓
загрузить данные
    ↓
перевести объект в соответствующее состояние
    ↓
при полном кандидате проверить checksum
    ↓
вернуть объект в финальном состоянии
```

## Методы загрузки

Методы вроде:

```csharp
LoadIncomplete(...)
LoadCandidate(...)
```

должны связывать:

- изменение данных объекта;
- соответствующий переход состояния.

Данные и состояние не должны изменяться независимо друг от друга.

## ValidateCandidate

Выполняет проверку checksum только после перехода:

```text
Created -> CandidateReady
```

После проверки возможны только:

```text
CandidateReady -> Valid
CandidateReady -> InvalidChecksum
```

---

# Использование в DynamicDumpDec

## Что происходит сейчас

Текущий `DynamicDumpDec` самостоятельно выполняет:

- накопление 5 байт;
- checksum;
- декодирование `Ch0`;
- декодирование `Ch1`;
- чтение `Aux`;
- resync через `ShiftLeft`.

После подключения `SimA04DynamicFrame` часть протокольной логики должна уйти из `Program.cs`.

---

# Первый этап интеграции

Потоковое накопление пока оставляем в `DynamicDumpDec`.

Остаются:

```text
ReadByte(...)
AddByte(...)
ShiftLeft(...)
sampleBytes
incomFrame
```

После накопления 5 байт создаётся объект:

```csharp
var frame = SimA04DynamicFrame.Parse(incomFrame);
```

Далее внешний код работает только через состояние объекта.

Пример:

```csharp
for (var index = 0; index < TargetFrameCount;)
{
    AddByte(incomFrame, ref sampleBytes, ReadByte(sp));
    rawBytes++;

    if (sampleBytes < IncommingMessageSize)
        continue;

    var frame = SimA04DynamicFrame.Parse(incomFrame);

    switch (frame.State)
    {
        case FrameState.InvalidChecksum:
            ShiftLeft(incomFrame, ref sampleBytes);
            skippedBytes++;
            continue;

        case FrameState.Valid:
            break;

        default:
            throw new InvalidOperationException(
                $"Unexpected frame state: {frame.State}");
    }

    var timeMs = (int)Math.Round(sw.Elapsed.TotalMilliseconds);

    var ch0 = frame.Ch0!.Value;
    var ch1 = frame.Ch1!.Value;
    var aux = frame.Aux!.Value;

    // дальнейшая статистика и вывод

    index++;
    sampleBytes = 0;
}
```

---

# Почему здесь ожидаются только два состояния

Перед `Parse` текущий `DynamicDumpDec` уже гарантирует:

```text
sampleBytes == 5
```

и `incomFrame` имеет длину 5.

Поэтому в данной конкретной точке нормальными финальными состояниями являются только:

```text
Valid
InvalidChecksum
```

Если здесь внезапно появятся:

```text
NullInput
Empty
Incomplete
InvalidLength
```

это означает ошибку в логике вызывающего кода.

Поэтому `default` в `switch` должен считаться нарушением контракта.

---

# Что удаляется из DynamicDumpDec

После подключения `SimA04DynamicFrame` удаляется собственная проверка checksum:

```csharp
private static bool IsValidSample(byte[] sample)
```

Checksum теперь принадлежит модели протокола.

Удаляется собственное декодирование little-endian каналов:

```csharp
private static int ReadUInt16Le(byte[] data, int offset)
```

`Ch0` и `Ch1` предоставляет `SimA04DynamicFrame`.

Вместо:

```csharp
var aux = incomFrame[4];
```

используется:

```csharp
var aux = frame.Aux!.Value;
```

---

# Что DynamicDumpDec больше не должен знать

После первого этапа `Program.cs` не должен содержать знания о:

```text
B0 + B1 -> Ch0
B2 + B3 -> Ch1
B0 + B1 + B2 + B3 -> checksum
```

Это ответственность `Vesy13.Protocol`.

---

# Что пока остаётся в DynamicDumpDec

На первом этапе остаётся логика потока:

```text
COM
ReadByte
накопление
окно из 5 байт
ShiftLeft
resync
статистика
вывод
```

Это сделано специально, чтобы сначала заменить только протокольную логику и проверить отсутствие изменения поведения программы.

---

# Следующий этап

Позже потоковая часть может быть вынесена в отдельный:

```text
DynamicStreamDecoder
```

Тогда из `DynamicDumpDec` дополнительно уйдут:

```csharp
AddByte(...)
ShiftLeft(...)
sampleBytes
IncommingMessageSize
```

И архитектура станет:

```text
SerialPort
    ↓
DynamicStreamDecoder
    ↓
SimA04DynamicFrame
    ↓
DynamicDumpDec
    ↓
статистика / вывод
```

---

# Зафиксированные решения

- Имя модели: `SimA04DynamicFrame`.
- Модель имеет конечный автомат состояний.
- Вход: `byte[]? data`.
- `Parse` всегда возвращает существующий объект.
- Тип объекта: `sealed class`.
- Конструктор приватный.
- Начальное состояние: `Created`.
- Все переходы контролируются.
- `State` — источник истины о состоянии кадра.
- `IsValid` вычисляется через `State == FrameState.Valid`.
- `B0`–`B3` и `Aux` имеют тип `byte?`.
- `Ch0` и `Ch1` имеют тип `int?`.
- Отсутствующее значение представляется `null`.
- Реальный `0x00` представляется значением `0`.
- Значение `byte` повторно по диапазону не валидируется.
- Проверка checksum находится только в `SimA04DynamicFrame`.
- Декодирование `Ch0`/`Ch1` находится только в `SimA04DynamicFrame`.
- Нет `TryParse`.
- Нет `out`.
- Нет nullable-результата.
- Нет `DecodeResult<T>`.
- Для нового кандидата создаётся новый объект.
- На первом этапе потоковое накопление и resync остаются в `DynamicDumpDec`.
- Состояние кадра и состояние COM-потока разделены.
