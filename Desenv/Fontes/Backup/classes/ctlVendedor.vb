﻿Imports System.Data.SqlClient
Namespace Vendedor
    Public Class ctlVendedor
        Public Enum TipoPesquisa
            [Código]
            [Nome]
            [Apelido]
        End Enum

        Public Function SelecionarUsuarioID(ByVal oParametros As Object) As SqlDataReader
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_VENDEDORUSUARIOID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim parmCodigo As New SqlParameter("@P_USERID", DBNull.Value)
            parmCodigo.Value = oParametros.ToString.Trim
            cmd.Parameters.Add(parmCodigo)
            conn.Open()
            Dim dt As SqlDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
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