Imports System.Data.SqlClient
Namespace CondicaoPagamento
    Public Class ctlCondicaoPagamento
        Public Enum TipoPesquisa
            [Código]
            [Nome]
        End Enum
        Public Function Pesquisar(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object) As DataTable
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_LISTAR_CONDICAOPAGAMENTO", conn)
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
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As DataRow = dt.NewRow
            dr("Codigo") = "000"
            dr("Nome") = "A combinar"
            dt.Rows.InsertAt(dr, 0)
            Return dt
        End Function
        Public Function Selecionar(ByVal oParametros As Object) As DataTable
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_CONDICAOPAGAMENTO", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
            Dim dt As New DataTable
            parmCodigo.Value = oParametros.ToString()
            cmd.Parameters.Add(parmCodigo)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        End Function
        Public Function ListarTiposPesquisa() As Array
            Return System.Enum.GetValues(GetType(TipoPesquisa))
        End Function
    End Class
End Namespace