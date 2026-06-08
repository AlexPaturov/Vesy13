-- Migration 002: replace calibration_config (jsonb blob) with
-- calibration_points (relational) + calibration_dynamic tables.

DROP TABLE IF EXISTS calibration_config;

CREATE TABLE IF NOT EXISTS calibration_points (
    id         SERIAL PRIMARY KEY,
    channel    SMALLINT     NOT NULL CHECK (channel IN (0, 1)),
    adc_code   INTEGER      NOT NULL,
    mass       NUMERIC(6,2) NOT NULL CHECK (mass >= 0 AND mass <= 150),
    is_active  BOOLEAN      NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS calibration_dynamic (
    id         INTEGER PRIMARY KEY DEFAULT 1,
    k_plus     DOUBLE PRECISION NOT NULL DEFAULT 0,
    k_minus    DOUBLE PRECISION NOT NULL DEFAULT 0,
    updated_at TIMESTAMPTZ      NOT NULL DEFAULT NOW()
);

INSERT INTO calibration_dynamic (id, k_plus, k_minus)
VALUES (1, 0, 0)
ON CONFLICT DO NOTHING;
