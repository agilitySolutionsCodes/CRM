Imports System.Data.SqlClient

Public Class ctlProduto

    Public Enum TipoPesquisa
        [Código]
        [Nome]
    End Enum

    Public Function Pesquisar(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object) As DataTable
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_PRODUTO_02", conn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
        Dim parmNome As New SqlParameter("@P_NOME", DBNull.Value)
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisa.Código
                parmCodigo.Value = oParametros.ToString()
            Case TipoPesquisa.Nome
                parmNome.Value = oParametros.ToString()
        End Select
        cmd.Parameters.Add(parmCodigo)
        cmd.Parameters.Add(parmNome)
        cmd.Parameters.Add(New SqlParameter("@P_CODIGOTABELA", 8))
        cmd.Parameters.Add(New SqlParameter("@P_EXIBIR", "O"))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", Util.GetFilial()))
        If oTipo = TipoPesquisa.Nome Then
            If oParametros.ToString.Trim.Length < 3 Then
                Throw New Exception("Forneça parte do nome do produto (no mínimo 3 letras)")
            End If
        End If
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function Selecionar(ByVal oParametros As Object) As DataTable
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_SEL_PRODUTO_02", conn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
        Dim dt As New DataTable
        parmCodigo.Value = oParametros.ToString()
        cmd.Parameters.Add(parmCodigo)
        cmd.Parameters.Add(New SqlParameter("@P_CODIGOTABELA", 8))
        cmd.Parameters.Add(New SqlParameter("@P_EXIBIR", "O"))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", Util.GetFilial()))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function ListarTiposPesquisa() As Array
        Return System.Enum.GetValues(GetType(TipoPesquisa))
    End Function

End Class