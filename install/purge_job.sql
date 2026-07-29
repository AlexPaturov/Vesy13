-- Vesy13 — регистрация задания pgAgent на очистку локальной базы.
--
-- Выполняется один раз при развёртывании станции, после установки pgAgent,
-- в базе, где живёт схема pgagent (обычно postgres):
--     psql -U postgres -f install/purge_job.sql
--
-- Задание запускается 1-го числа каждого месяца в 03:00 и удаляет из scale_db
-- строки старше 30 дней: взвешивания по wagon_weighing.when_insert и события
-- журнала по audit_log.time_created. Возраст данных на станции колеблется
-- от 30 до 60 дней — от момента прогона до следующего.
--
-- Калибровочные таблицы calibration_points и calibration_dynamic хранят
-- настройку весов: активные точки живут годами, поэтому задание работает
-- с двумя таблицами данных — wagon_weighing и audit_log.

WITH new_job AS (
    INSERT INTO pgagent.pga_job (jobjclid, jobname, jobdesc, jobenabled)
    SELECT cls.jclid,
           'vesy13_purge',
           'Vesy13: очистка взвешиваний и журнала аудита старше 30 дней',
           TRUE
    FROM pgagent.pga_jobclass cls
    WHERE cls.jclname = 'Routine Maintenance'
      AND NOT EXISTS (SELECT 1 FROM pgagent.pga_job WHERE jobname = 'vesy13_purge')
    RETURNING jobid
),
new_step AS (
    INSERT INTO pgagent.pga_jobstep (jstjobid, jstname, jstdesc, jstkind, jstdbname, jstonerror, jstcode)
    SELECT jobid,
           'purge',
           'Удаление строк старше 30 дней',
           's',
           'scale_db',
           'f',
           $step$
DELETE FROM wagon_weighing WHERE when_insert  < LOCALTIMESTAMP - INTERVAL '30 days';
DELETE FROM audit_log      WHERE time_created < NOW()          - INTERVAL '30 days';
$step$
    FROM new_job
    RETURNING jstjobid
)
INSERT INTO pgagent.pga_schedule (jscjobid, jscname, jscdesc, jscenabled,
                                  jscminutes, jschours, jscweekdays, jscmonthdays, jscmonths)
SELECT jobid,
       'monthly',
       '1-го числа в 03:00',
       TRUE,
       -- минута 0
       (SELECT array_agg(i = 0 ORDER BY i) FROM generate_series(0, 59) i),
       -- час 3
       (SELECT array_agg(i = 3 ORDER BY i) FROM generate_series(0, 23) i),
       -- дни недели: пустой набор, расписание задаётся числом месяца
       array_fill(FALSE, ARRAY[7]),
       -- 1-е число месяца (32-й элемент — «последний день»)
       (SELECT array_agg(i = 1 ORDER BY i) FROM generate_series(1, 32) i),
       -- каждый месяц
       array_fill(TRUE, ARRAY[12])
FROM new_job;
