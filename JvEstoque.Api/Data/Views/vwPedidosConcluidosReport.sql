CREATE OR ALTER VIEW [vwPedidosConcluidosReport] AS
SELECT
    COUNT(p.Id) as "Quantidade"
FROM
    [Pedido] as p
WHERE
    p.Status = 3;