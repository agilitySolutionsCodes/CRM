Imports System.Data.SqlClient
Namespace Cliente
    Public Class ctlCliente
        Public Enum TipoPesquisa
            [Código]
            [Nome]
            [Apelido]
            [CPF_CNPJ]
        End Enum
        Public Function Pesquisar(ByVal oTipo As TipoPesquisa, ByVal oParametros As Object) As DataTable
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_LISTAR_CLIENTE", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim aParametros As String()
            Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
            Dim parmLoja As New SqlParameter("@P_LOJA", DBNull.Value)
            Dim parmNome As New SqlParameter("@P_NOME", DBNull.Value)
            Dim parmApelido As New SqlParameter("@P_APELIDO", DBNull.Value)
            Dim parmCPFCNPJ As New SqlParameter("@P_CPFCNPJ", DBNull.Value)
            Dim dt As New DataTable
            Select Case oTipo
                Case TipoPesquisa.Código
                    If IsArray(oParametros) Then
                        aParametros = DirectCast(oParametros, String())
                        parmCodigo.Value = aParametros(0).ToString
                        parmLoja.Value = aParametros(1).ToString
                    Else
                        If oParametros.ToString.Trim.Length >= 6 Then
                            parmCodigo.Value = oParametros.ToString.Substring(0, 6)
                        Else
                            parmCodigo.Value = ""
                        End If
                        If oParametros.ToString.Trim.Length = 8 Then
                            parmLoja.Value = oParametros.ToString.Substring(6, 2)                        
                        End If
                    End If
                Case TipoPesquisa.Nome
                    parmNome.Value = oParametros.ToString()
                Case TipoPesquisa.Apelido
                    parmApelido.Value = oParametros
                Case TipoPesquisa.CPF_CNPJ
                    parmCPFCNPJ.Value = oParametros.ToString().Replace(".", "").Replace("-", "").Replace("/", "")
            End Select
            cmd.Parameters.Add(parmCodigo)
            cmd.Parameters.Add(parmLoja)
            cmd.Parameters.Add(parmNome)
            cmd.Parameters.Add(parmApelido)
            cmd.Parameters.Add(parmCPFCNPJ)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        End Function
        Public Function Selecionar(ByVal oParametros As Object) As DataTable
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_CLIENTE", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim aParametros As String()
            Dim parmCodigo As New SqlParameter("@P_CODIGO", DBNull.Value)
            Dim parmLoja As New SqlParameter("@P_LOJA", DBNull.Value)
            Dim dt As New DataTable
            If IsArray(oParametros) Then
                aParametros = DirectCast(oParametros, String())
                parmCodigo.Value = aParametros(0).ToString
                parmLoja.Value = aParametros(1).ToString
            Else
                If oParametros.ToString.Trim.Length >= 6 Then
                    parmCodigo.Value = oParametros.ToString.Substring(0, 6)
                Else
                    parmCodigo.Value = ""
                End If
                If oParametros.ToString.Trim.Length = 8 Then
                    parmLoja.Value = oParametros.ToString.Substring(6, 2)
                End If
            End If
            cmd.Parameters.Add(parmCodigo)
            cmd.Parameters.Add(parmLoja)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        End Function
        Public Function SelecionarCPFCNPJ(ByVal oParametros As Object) As SqlDataReader
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_CNPJCLIENTE_02", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim parmCPFCPNJ As New SqlParameter("@P_CPFCNPJ", DBNull.Value)
            parmCPFCPNJ.Value = oParametros.ToString
            cmd.Parameters.Add(parmCPFCPNJ)
            conn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        End Function
        Public Function SelecionarRevendedorUsuarioID(ByVal oParametros As Object) As SqlDataReader
            Dim conn = Util.GetConnection
            Dim cmd As New SqlCommand("PR_SEL_REVENDEDORSUARIOID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim parmCodigo As New SqlParameter("@P_USERID", DBNull.Value)
            parmCodigo.Value = oParametros.ToString.Trim
            cmd.Parameters.Add(parmCodigo)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
            Return dr
        End Function
        Public Function ListarTiposPesquisa() As Array
            Return System.Enum.GetValues(GetType(TipoPesquisa))
        End Function
    End Class
End Namespace
