-- Version 4: dynamic calibration is replaced by direction correction profiles.
-- Dynamic calibration coefficients have another meaning and are intentionally discarded.
DROP TABLE IF EXISTS calibration_dynamic;
DROP TABLE IF EXISTS direction_correction_profiles;

CREATE TABLE direction_correction_profiles (
    id                                SERIAL PRIMARY KEY,
    right_direction_correction_factor DOUBLE PRECISION NOT NULL DEFAULT 0,
    left_direction_correction_factor  DOUBLE PRECISION NOT NULL DEFAULT 0,
    is_active                         BOOLEAN          NOT NULL DEFAULT FALSE,
    created_at                        TIMESTAMPTZ      NOT NULL,
    deleted_at                        TIMESTAMPTZ
);

CREATE UNIQUE INDEX ux_direction_correction_profiles_active
    ON direction_correction_profiles (is_active)
    WHERE is_active = TRUE AND deleted_at IS NULL;
