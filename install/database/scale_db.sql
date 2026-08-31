-- Vesy13 - local database installation.
--
-- Runs once when a station is deployed, as a superuser:
--     psql -U postgres -f install/scale_db.sql
--
-- The role is created without a password: loopback connections authenticate
-- with the trust method, the pg_hba.conf rules are described in
-- docs/configuration.md.
--
-- Tables are created empty. Direction correction profiles remain empty until
-- the operator saves the first pair of direction correction factors.

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'scale_user') THEN
        CREATE ROLE scale_user LOGIN;
    END IF;
END $$;

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
    bogie1_calibration_point_id INTEGER,
    bogie2_calibration_point_id INTEGER,
    direction_correction_profile_id INTEGER,
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
    calibration_value NUMERIC(12,3) NOT NULL,
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

-- History of direction correction profiles.
-- The working profile is selected by is_active = TRUE AND deleted_at IS NULL.
CREATE TABLE direction_correction_profiles (
    id                                SERIAL PRIMARY KEY,
    right_direction_correction_factor DOUBLE PRECISION NOT NULL DEFAULT 0,
    left_direction_correction_factor  DOUBLE PRECISION NOT NULL DEFAULT 0,
    is_active                         BOOLEAN          NOT NULL DEFAULT FALSE,
    created_at                        TIMESTAMPTZ      NOT NULL,
    deleted_at                        TIMESTAMPTZ
);

COMMENT ON COLUMN direction_correction_profiles.is_active IS 'Current working direction correction profile.';
COMMENT ON COLUMN direction_correction_profiles.created_at IS 'Direction correction profile creation time.';
COMMENT ON COLUMN direction_correction_profiles.deleted_at IS 'Time when direction correction profile was deactivated.';

CREATE UNIQUE INDEX ux_direction_correction_profiles_active
    ON direction_correction_profiles (is_active)
    WHERE is_active = TRUE AND deleted_at IS NULL;

ALTER TABLE wagon_weighing
    ADD CONSTRAINT fk_wagon_weighing_bogie1_calibration_point
        FOREIGN KEY (bogie1_calibration_point_id) REFERENCES calibration_points(id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_wagon_weighing_bogie2_calibration_point
        FOREIGN KEY (bogie2_calibration_point_id) REFERENCES calibration_points(id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_wagon_weighing_direction_correction_profile
        FOREIGN KEY (direction_correction_profile_id) REFERENCES direction_correction_profiles(id) ON DELETE RESTRICT;

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
