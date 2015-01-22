Imports System.Data.SqlClient
Namespace Vendedor
    Public Class ctlVendedor
        Public Enum TipoPesquisa
            [Código]
            [Nome]
            [Apelido]
        End Enum

        Public Function SelecionarUsuarioID(Optional ByVal sUsuario As String = "") As DataTable
            Dim conn = Util.GetConnection
            Dim dt As New DataTable
            Dim cmd As New SqlCommand("PR_SEL_VENDEDORUSUARIOID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            If sUsuario = "" Then
                sUsuario = HttpContext.Current.User.Identity.Name
            End If
            cmd.Parameters.Add(New SqlParameter("@P_USERID", sUsuario))

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        End Function
        Public Function Selecionar(ByVal oParametros As Object) As DataTable
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_VENDEDOR", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
            Dim dt As New DataTable
            parmCodigo.Value = oParametros.ToString
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
