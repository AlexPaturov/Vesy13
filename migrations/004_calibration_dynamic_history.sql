-- Migration 004: dynamic calibration history and explicit active row.
--
-- k_plus/k_minus keep 0 as a valid coefficient value.
-- The current working profile is selected by is_active = true and deleted_at IS NULL.
-- updated_at is kept temporarily for compatibility with the current application code;
-- after repository code is switched to insert historical rows, it can be removed separately.

BEGIN;

ALTER TABLE calibration_dynamic
    ADD COLUMN IF NOT EXISTS is_active BOOLEAN NOT NULL DEFAULT FALSE;

ALTER TABLE calibration_dynamic
    ADD COLUMN IF NOT EXISTS created_at TIMESTAMPTZ;

ALTER TABLE calibration_dynamic
    ADD COLUMN IF NOT EXISTS deleted_at TIMESTAMPTZ;

UPDATE calibration_dynamic
SET created_at = COALESCE(created_at, updated_at, NOW())
WHERE created_at IS NULL;

ALTER TABLE calibration_dynamic
    ALTER COLUMN created_at SET NOT NULL;

CREATE SEQUENCE IF NOT EXISTS calibration_dynamic_id_seq;

ALTER TABLE calibration_dynamic
    ALTER COLUMN id SET DEFAULT nextval('calibration_dynamic_id_seq');

ALTER SEQUENCE calibration_dynamic_id_seq OWNED BY calibration_dynamic.id;

INSERT INTO calibration_dynamic (k_plus, k_minus, is_active, created_at, deleted_at)
SELECT 0, 0, TRUE, NOW(), NULL
WHERE NOT EXISTS (SELECT 1 FROM calibration_dynamic);

WITH active_row AS (
    SELECT id
    FROM calibration_dynamic
    ORDER BY created_at DESC, id DESC
    LIMIT 1
)
UPDATE calibration_dynamic AS cd
SET is_active = (cd.id = active_row.id),
    deleted_at = CASE
        WHEN cd.id = active_row.id THEN NULL
        ELSE COALESCE(cd.deleted_at, NOW())
    END
FROM active_row;

SELECT setval(
    'calibration_dynamic_id_seq',
    GREATEST(COALESCE((SELECT MAX(id) FROM calibration_dynamic), 0) + 1, 1),
    FALSE
);

DROP INDEX IF EXISTS ux_calibration_dynamic_active;

CREATE UNIQUE INDEX ux_calibration_dynamic_active
    ON calibration_dynamic (is_active)
    WHERE is_active = TRUE AND deleted_at IS NULL;

COMMENT ON COLUMN calibration_dynamic.is_active IS 'Current working dynamic calibration row.';
COMMENT ON COLUMN calibration_dynamic.created_at IS 'Dynamic calibration row creation time.';
COMMENT ON COLUMN calibration_dynamic.deleted_at IS 'Time when dynamic calibration row was deactivated.';

COMMIT;
