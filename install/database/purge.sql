-- Vesy13 - local database purge, retention 30 days.
--
-- Executed by the Windows scheduled task "Vesy13 purge" that install.ps1
-- creates. Manual run:
--     psql -h 127.0.0.1 -U scale_user -d scale_db -f purge.sql
--
-- Weighings are selected by when_insert, audit events by time_created.
-- calibration_points and direction_correction_profiles hold the scale configuration:
-- active points live for years, so the purge works on the two data tables.

DELETE FROM wagon_weighing WHERE when_insert  < LOCALTIMESTAMP - INTERVAL '30 days';

DELETE FROM audit_log      WHERE time_created < NOW()          - INTERVAL '30 days';
