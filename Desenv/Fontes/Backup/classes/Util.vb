Imports System.IO
Imports System.Web
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Configuration

Public Class Util

    Public Shared Function GetConnection() As SqlConnection
        'Permite ler o web.config
        Dim reader As New System.Configuration.AppSettingsReader
        'Retorna o ambiente e o caminho do banco de dados definido no web.config
        Return New SqlConnection(ConfigurationManager.ConnectionStrings(reader.GetValue("Ambiente", GetType(String)).ToString()).ConnectionString)
    End Function

    Public Shared Function GetFilial() As String
        Dim reader As New System.Configuration.AppSettingsReader
        Dim sAmbiente As String = reader.GetValue("Ambiente", GetType(String)).ToString()
        Return reader.GetValue("Filial" + sAmbiente, GetType(String)).ToString()
    End Function

    Public Shared Function GetAmbiente() As String
        Dim reader As New System.Configuration.AppSettingsReader
        Return reader.GetValue("Ambiente", GetType(String)).ToString()
    End Function

    Public Shared Function GetCaminhoWS(ByVal sAmbiente As String) As String
        Dim reader As New System.Configuration.AppSettingsReader
        Return reader.GetValue("CaminhoWS" + sAmbiente, GetType(String)).ToString()
    End Function

    Public Shared Function ImageUrl(ByVal oPage As Page, ByVal sCodigo As String) As String
        If Util.ExisteArquivo(oPage, "Fotos\" + sCodigo.Trim + ".jpg") Then
            Return "Fotos\" + sCodigo.Trim + ".jpg"
        Else
            Return "pixel.png"
        End If
    End Function

    Public Shared Function ExisteArquivo(ByVal oPage As Page, ByVal sArquivo As String) As Boolean
        Return File.Exists(oPage.MapPath(sArquivo))
    End Function

    Shared Function WebGetMV(ByVal sNomeParametro As String) As Object
        Dim oToken As New wshumberto.wsutil.TOKENSTRUCT
        oToken.CONTEUDO = HttpContext.Current.User.Identity.Name
        oToken.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).Hash
        Dim oWs As New wshumberto.wsutil.WSUTIL
        Return oWs.WEBGETMV(oToken, sNomeParametro)
    End Function

End Class
