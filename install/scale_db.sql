-- Vesy13 — установка локальной базы scale_db.
--
-- Выполняется один раз при развёртывании станции, суперпользователем:
--     psql -U postgres -f install/scale_db.sql
--
-- Роль создаётся без пароля: петлевые подключения аутентифицируются методом
-- trust, правила pg_hba.conf описаны в docs/configuration.md.
--
-- Таблицы создаются пустыми. Признаком заданной динамической калибровки служит
-- наличие активной строки в calibration_dynamic, поэтому на новой станции
-- таблица остаётся пустой до первой калибровки на эталонных грузах.

CREATE ROLE scale_user LOGIN;

CREATE DATABASE scale_db OWNER scale_user;

\connect scale_db

-- Владельцем таблиц становится scale_user: приложение работает под этой ролью
-- и создаёт audit_log самостоятельно, если её нет (AuditLogger.EnsureTableAsync).
SET ROLE scale_user;

-- Потележечные взвешивания вагонов, в тоннах.
-- transferred = false — запись ещё не перенесена в систему учёта предприятия.
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

-- Точки статической калибровки по каналам.
-- Несколько активных точек на канал — штатная ситуация, снятые точки
-- не удаляются физически, им проставляется deleted_at.
CREATE TABLE calibration_points (
    id         SERIAL PRIMARY KEY,
    channel    SMALLINT     NOT NULL CHECK (channel IN (0, 1)),
    adc_code   INTEGER      NOT NULL,
    mass       NUMERIC(6,2) NOT NULL CHECK (mass >= 0 AND mass <= 150),
    is_active  BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ  NOT NULL DEFAULT NOW(),
    deleted_at TIMESTAMPTZ
);

COMMENT ON COLUMN calibration_points.created_at IS 'Time when the calibration point was added.';

-- История коэффициентов динамической калибровки.
-- Рабочая строка выбирается условием is_active = TRUE AND deleted_at IS NULL;
-- created_at проставляет приложение (LocalRepository.SaveDynamicCalibAsync).
CREATE TABLE calibration_dynamic (
    id         SERIAL PRIMARY KEY,
    k_plus     DOUBLE PRECISION NOT NULL DEFAULT 0,
    k_minus    DOUBLE PRECISION NOT NULL DEFAULT 0,
    is_active  BOOLEAN          NOT NULL DEFAULT FALSE,
    created_at TIMESTAMPTZ      NOT NULL,
    deleted_at TIMESTAMPTZ
);

COMMENT ON COLUMN calibration_dynamic.is_active IS 'Current working dynamic calibration row.';
COMMENT ON COLUMN calibration_dynamic.created_at IS 'Dynamic calibration row creation time.';
COMMENT ON COLUMN calibration_dynamic.deleted_at IS 'Time when dynamic calibration row was deactivated.';

CREATE UNIQUE INDEX ux_calibration_dynamic_active
    ON calibration_dynamic (is_active)
    WHERE is_active = TRUE AND deleted_at IS NULL;

-- Журнал аудита: события форм, сервисов и ошибки.
CREATE TABLE audit_log (
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

CREATE INDEX ix_audit_log_time ON audit_log (time_created DESC);

RESET ROLE;
