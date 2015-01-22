Namespace Proposta1
    Public Class ItemProposta
        Public Property CodigoProduto As String
        Public Property Quantidade As Integer
        Public Property PrazoEntrega As Integer
        Public Property PrecoUnitario As Decimal
        Public Property PrecoLista As Decimal
        Public Property PrecoVenda As Decimal
        Public Property PrazoGarantia As Decimal = 0
        Public Property PercentualDesconto As Decimal = 0
        Public Property ValorDesconto As Decimal = 0
        Public Property DescricaoDetalhada As String = ""

        Public Function PutSiga(ByVal oItem As Object) As Object
            CallByName(oItem, "CodigoProduto", CallType.Let, CodigoProduto)
            CallByName(oItem, "Quantidade", CallType.Let, Quantidade)
            CallByName(oItem, "PrazoEntrega", CallType.Let, PrazoEntrega)
            CallByName(oItem, "PrazoGarantia", CallType.Let, PrazoGarantia)
            CallByName(oItem, "PrecoLista", CallType.Let, PrecoLista)
            CallByName(oItem, "PrecoVenda", CallType.Let, PrecoVenda)
            CallByName(oItem, "PercentualDesconto", CallType.Let, PercentualDesconto)
            CallByName(oItem, "ValorDesconto", CallType.Let, ValorDesconto)
            CallByName(oItem, "DescricaoDetalhada", CallType.Let, DescricaoDetalhada)
            Return oItem
        End Function
    End Class
End Namespace
