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


-- Таблица аудита (Serilog + AuditLogger)
CREATE TABLE IF NOT EXISTS audit_log (
    id                  BIGSERIAL    PRIMARY KEY,
    time_created        TIMESTAMPTZ,
    event_id            INTEGER,
    keywords            VARCHAR(20),
    computer            VARCHAR(100),
    subject_user_sid    VARCHAR(200),
    subject_user_name   VARCHAR(200),
    subject_domain_name VARCHAR(200),
    subject_logon_id    VARCHAR(100),
    object_server       VARCHAR(200),
    object_type         VARCHAR(100),
    object_name         TEXT,
    object_handle       VARCHAR(200),
    process_id          INTEGER,
    process_name        TEXT,
    workstation_name    VARCHAR(100),
    ip_address          VARCHAR(50)
);

CREATE INDEX IF NOT EXISTS ix_audit_log_time ON audit_log (time_created DESC);
