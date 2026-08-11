-- Vesy13 - local database installation.
--
-- Runs once when a station is deployed, as a superuser:
--     psql -U postgres -f install/scale_db.sql
--
-- The role is created without a password: loopback connections authenticate
-- with the trust method, the pg_hba.conf rules are described in
-- docs/configuration.md.
--
-- Tables are created empty. An active row in calibration_dynamic is what marks
-- the dynamic calibration as set, so on a new station the table stays empty
-- until the first calibration with reference loads.

CREATE ROLE scale_user LOGIN;

CREATE DATABASE scale_db OWNER scale_user;

\connect scale_db

-- scale_user owns the tables: the application works under this role and creates
-- audit_log on its own when it is absent (AuditLogger.EnsureTableAsync).
SET ROLE scale_user;

-- Bogie-by-bogie wagon weighings, in tonnes.
-- transferred = false marks a record still to be moved into the plant system.
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

-- Static calibration points per channel.
-- Several active points on a channel are normal; inactive points keep their row
-- and carry deleted_at.
CREATE TABLE calibration_points (
    id         SERIAL PRIMARY KEY,
    channel    SMALLINT     NOT NULL CHECK (channel IN (0, 1)),
    adc_code   INTEGER      NOT NULL,
    mass       NUMERIC(6,2) NOT NULL CHECK (mass >= 0 AND mass <= 150),
    is_active  BOOLEAN      NOT NULL DEFAULT TRUE,
    calibration_value NUMERIC(12,5) NOT NULL,
    created_at TIMESTAMPTZ  NOT NULL DEFAULT NOW(),
    deleted_at TIMESTAMPTZ
);

CREATE UNIQUE INDEX ux_calibration_points_active_channel_mass
    ON calibration_points (channel, mass)
    WHERE is_active = TRUE AND deleted_at IS NULL;

CREATE UNIQUE INDEX ux_calibration_points_active_channel_adc_code
    ON calibration_points (channel, adc_code)
    WHERE is_active = TRUE AND deleted_at IS NULL;

COMMENT ON COLUMN calibration_points.created_at IS 'Time when the calibration point was added.';

-- History of the dynamic calibration coefficients.
-- The working row is selected by is_active = TRUE AND deleted_at IS NULL;
-- created_at is supplied by the application (LocalRepository.SaveDynamicCalibAsync).
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

-- Audit trail: form and service events, and errors.
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
