/* ============================================================
   Пользователь
   ============================================================ */

CREATE USER MRV PASSWORD 'FFUU';

/* ============================================================
   Исключения
   ============================================================ */

CREATE EXCEPTION EX_BAD_PERIOD 'Не задан период.';
CREATE EXCEPTION EX_PERIOD_TOO_LONG 'Период не должен превышать 30 дней.';

/* ============================================================
   Процедура
   ============================================================ */

CREATE PROCEDURE MRV_GPRI (
    P_DATE_FROM DATE,
    P_DATE_TO   DATE,
    P_NDOK      NUMERIC(8,0) = NULL
)
RETURNS (
    DT DATE,
    VR TIME,
    NVAG VARCHAR(10),
    NDOK NUMERIC(8,0),
    BRUTTO NUMERIC(6,2),
    TAR_BRS NUMERIC(6,2),
    TAR_DOK NUMERIC(6,2),
    NETTO NUMERIC(6,2),
    NET_DOK NUMERIC(6,2),
    POTR VARCHAR(30),
    PLAT VARCHAR(30),
    VESY SMALLINT
)
AS
BEGIN
    IF (P_DATE_FROM IS NULL OR P_DATE_TO IS NULL) THEN
        EXCEPTION EX_BAD_PERIOD;

    IF (P_DATE_FROM > P_DATE_TO) THEN
        EXCEPTION EX_BAD_PERIOD;

    IF (DATEDIFF(DAY, P_DATE_FROM, P_DATE_TO) > 30) THEN
        EXCEPTION EX_PERIOD_TOO_LONG;

    FOR
        SELECT
            DT, VR, NVAG, NDOK, BRUTTO, TAR_BRS, TAR_DOK, NETTO,
            NET_DOK, POTR, PLAT, VESY
        FROM GPRI
        WHERE DT BETWEEN :P_DATE_FROM AND :P_DATE_TO
          AND (:P_NDOK IS NULL OR NDOK = :P_NDOK)
        ORDER BY DT, VR
        INTO
            :DT, :VR, :NVAG, :NDOK, :BRUTTO, :TAR_BRS, :TAR_DOK,
            :NETTO, :NET_DOK, :POTR, :PLAT, :VESY
    DO
        SUSPEND;
END;

/* ============================================================
   Права
   ============================================================ */

/* Разрешить пользователю выполнять процедуру */
GRANT EXECUTE ON PROCEDURE MRV_GPRI TO USER MRV;

/* Разрешить использовать пользовательские исключения */
GRANT USAGE ON EXCEPTION EX_BAD_PERIOD TO USER MRV;
GRANT USAGE ON EXCEPTION EX_PERIOD_TOO_LONG TO USER MRV;