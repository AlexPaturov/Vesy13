-- Vesy13 — очистка локальной базы, глубина хранения 30 дней.
--
-- Выполняется заданием планировщика Windows «Vesy13 purge», которое создаёт
-- install.ps1. Ручной запуск:
--     psql -h 127.0.0.1 -U scale_user -d scale_db -f purge.sql
--
-- Взвешивания отбираются по when_insert, события журнала — по time_created.
-- Калибровочные таблицы calibration_points и calibration_dynamic хранят
-- настройку весов: активные точки живут годами, поэтому очистка работает
-- с двумя таблицами данных.

DELETE FROM wagon_weighing WHERE when_insert  < LOCALTIMESTAMP - INTERVAL '30 days';

DELETE FROM audit_log      WHERE time_created < NOW()          - INTERVAL '30 days';
