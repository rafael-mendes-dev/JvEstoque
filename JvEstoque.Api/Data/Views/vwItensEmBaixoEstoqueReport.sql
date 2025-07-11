CREATE OR ALTER VIEW [vwItensEmBaixoEstoqueReport] AS   
    SELECT
        COUNT(vp.Id) AS "Quantidade"
    FROM
        [VariacaoProduto] AS vp
            INNER JOIN
        [Estoque] AS e ON vp.Id = e.VariacaoProdutoId
    WHERE
        e.Quantidade <= 20;