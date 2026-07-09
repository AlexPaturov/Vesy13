-- Migration 005: creation timestamp for static calibration points.
-- Mirrors calibration_dynamic.created_at (migration 004) so the static
-- calibration grid can show "Добавлена" alongside "Снята" (deleted_at).

BEGIN;

ALTER TABLE calibration_points
    ADD COLUMN IF NOT EXISTS created_at TIMESTAMPTZ;

UPDATE calibration_points
SET created_at = NOW()
WHERE created_at IS NULL;

ALTER TABLE calibration_points
    ALTER COLUMN created_at SET NOT NULL;

ALTER TABLE calibration_points
    ALTER COLUMN created_at SET DEFAULT NOW();

COMMENT ON COLUMN calibration_points.created_at IS 'Time when the calibration point was added.';

COMMIT;
