CREATE OR ALTER VIEW [vwQuantidadeDePedidosPorStatusReport] AS
SELECT
    COUNT(CASE WHEN p.Status = 1 THEN 1 END) AS "Recebidos",
    COUNT(CASE WHEN p.Status = 2 THEN 1 END) AS "EmProducao",
    COUNT(CASE WHEN p.Status = 3 THEN 1 END) AS "Finalizados",
    COUNT(CASE WHEN p.Status = 4 THEN 1 END) AS "Cancelados"
FROM
    [Pedido] AS p;