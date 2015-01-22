Namespace Proposta1

    Public Class Proposta

        Public Property Itens As New List(Of ItemProposta)
        Public Property Numero As String = ""
        Public Property CodigoCliente As String
        Public Property LojaCliente As String
        Public Property Contato As String
        Public Property CondicaoPagamento As String
        Public Property DataSolicitacao As Date = Now
        Public Property PossibilidadeVenda As String
        Public Property DetalheCondicao As String = ""
        Public Property MotivoProrrogacao As String = ""
        Public Property TipoSolicitacao As String = "E"
        Public Property TipoFrete As String
        Public Property ValorFrete As Decimal = 0
        Public Property Observacao As String
        Public Property DataValidade As Date
        Public Property FaturadoPor As String = ""
        Private sTipoOrcamento As String
        Public Property IdUsuario As String = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).UserCode

        Public Property TipoOrcamento As String
            Get
                Return sTipoOrcamento
            End Get
            Set(ByVal value As String)
                If value = "PUB" Then
                    sTipoOrcamento = "04"
                ElseIf value = "PRI" Then
                    sTipoOrcamento = "05"
                End If
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Function PutSiga(ByVal oFicha As Object) As Object
            CallByName(oFicha, "CODIGOCLIENTE", CallType.Let, CodigoCliente)
            CallByName(oFicha, "LOJACLIENTE", CallType.Let, LojaCliente)
            CallByName(oFicha, "CONTATO", CallType.Let, Contato)
            CallByName(oFicha, "CONDICAOPAGAMENTO", CallType.Let, CondicaoPagamento)
            CallByName(oFicha, "DATASOLICITACAO", CallType.Let, DataSolicitacao)
            CallByName(oFicha, "POSSIBILIDADEVENDA", CallType.Let, PossibilidadeVenda)
            CallByName(oFicha, "TIPOORCAMENTO", CallType.Let, TipoOrcamento)
            CallByName(oFicha, "TIPOSOLICITACAO", CallType.Let, TipoSolicitacao)
            CallByName(oFicha, "TIPOFRETE", CallType.Let, TipoFrete)
            CallByName(oFicha, "VALORFRETE", CallType.Let, ValorFrete)
            CallByName(oFicha, "OBSERVACAO", CallType.Let, Observacao)
            CallByName(oFicha, "Numero", CallType.Let, Numero)
            CallByName(oFicha, "DataValidade", CallType.Let, DataValidade)
            CallByName(oFicha, "DetalheCondicao", CallType.Let, DetalheCondicao.Trim)
            CallByName(oFicha, "MotivoProrrogacao", CallType.Let, MotivoProrrogacao)
            CallByName(oFicha, "FaturadoPor", CallType.Let, FaturadoPor)
            CallByName(oFicha, "IDUsuario", CallType.Let, IdUsuario)
            Return oFicha
        End Function

        Public Sub PutItens(ByVal dt As DataTable)
            For Each dr As DataRow In dt.Rows
                Dim oItem As New ItemProposta
                oItem.CodigoProduto = dr("CodigoProduto").ToString
                oItem.Quantidade = Integer.Parse(dr("Quantidade").ToString)
                oItem.PrecoVenda = Decimal.Parse(dr("PrecoUnitario").ToString)
                oItem.PrecoLista = Decimal.Parse(dr("PrecoLista").ToString)
                oItem.PercentualDesconto = Decimal.Parse(dr("PercentualDesconto").ToString)
                oItem.ValorDesconto = Decimal.Parse(dr("ValorDesconto").ToString)
                oItem.PrazoEntrega = Integer.Parse(dr("PrazoEntrega").ToString)
                oItem.PrazoGarantia = Integer.Parse(dr("PrazoGarantia").ToString)
                oItem.DescricaoDetalhada = dr("DESCRICAO").ToString.Replace(Chr(0), "")
                Itens.Add(oItem)
            Next
        End Sub

    End Class

End Namespace