CREATE OR ALTER VIEW [vwQuantidadeDePedidosPorStatusReport] AS
SELECT
    p.Status,
    COUNT(p.Id) AS "Quantidade"
FROM
    [Pedido] AS p
GROUP BY
    p.Status;