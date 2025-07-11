CREATE OR ALTER VIEW vwFaturamentoReport AS
SELECT
    SUM(p.ValorTotal) AS "Entradas"
FROM
    [Pedido] p
WHERE
    p.Status = 3;