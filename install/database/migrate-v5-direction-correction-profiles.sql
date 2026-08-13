-- Version 5: replace obsolete dynamic calibration and guarantee application ownership.
-- Existing direction correction profiles are retained on repeated installer runs.
DROP TABLE IF EXISTS calibration_dynamic;

CREATE TABLE IF NOT EXISTS direction_correction_profiles (
    id                                SERIAL PRIMARY KEY,
    right_direction_correction_factor DOUBLE PRECISION NOT NULL DEFAULT 0,
    left_direction_correction_factor  DOUBLE PRECISION NOT NULL DEFAULT 0,
    is_active                         BOOLEAN          NOT NULL DEFAULT FALSE,
    created_at                        TIMESTAMPTZ      NOT NULL,
    deleted_at                        TIMESTAMPTZ
);

CREATE UNIQUE INDEX IF NOT EXISTS ux_direction_correction_profiles_active
    ON direction_correction_profiles (is_active)
    WHERE is_active = TRUE AND deleted_at IS NULL;

ALTER TABLE direction_correction_profiles OWNER TO scale_user;
ALTER SEQUENCE direction_correction_profiles_id_seq OWNER TO scale_user;
