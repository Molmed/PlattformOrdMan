--List all functions and stored procedures by using:
--
USE BookKeeping_devel

SELECT 'GRANT EXECUTE ON ' + name + ' TO BKuser'
FROM sys.all_objects
WHERE is_ms_shipped = 0
AND (type = 'P' or type = 'FN')
ORDER BY name
