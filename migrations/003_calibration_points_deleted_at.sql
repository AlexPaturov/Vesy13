-- Migration 003: add soft-delete timestamp for calibration points.

ALTER TABLE calibration_points
    ADD COLUMN IF NOT EXISTS deleted_at TIMESTAMPTZ;
