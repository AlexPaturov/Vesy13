-- Vesy13 — миграция v2
-- Таблица для хранения взвешиваний вагонов (потележечно, в тоннах)
-- transferred = false — ещё не перенесено в систему учёта предприятия

CREATE TABLE wagon_weighing (
    id          SERIAL       PRIMARY KEY,
    train_time  TIMESTAMP    NOT NULL,
    wagon_time  TIMESTAMP    NOT NULL,
    wagon_num   INTEGER      NOT NULL,
    bogie1      NUMERIC(6,2) NOT NULL,
    bogie2      NUMERIC(6,2) NOT NULL,
    total       NUMERIC(6,2) NOT NULL,
    direction   VARCHAR(10),
    mode        VARCHAR(10)  NOT NULL,
    transferred BOOLEAN      NOT NULL DEFAULT FALSE,
    when_insert TIMESTAMP    NOT NULL DEFAULT NOW()
);

-- Таблица калибровки (одна строка, весь профиль в JSONB)
CREATE TABLE calibration_config (
    id         INTEGER   PRIMARY KEY DEFAULT 1,
    profile    JSONB     NOT NULL DEFAULT '{}',
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);

INSERT INTO calibration_config (id, profile) VALUES (1, '{}')
ON CONFLICT (id) DO NOTHING;




