Imports System.ComponentModel
Imports System.Security.Principal
Imports System.Data.SqlClient
Imports Orcamento.WSUserAgility

Partial Public Class Login
    Inherits BaseWebUI
    'Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub


    Private Sub objLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles objLogin.Authenticate
        Dim oRet As New RetornoGenerico
        Dim lAuth As Boolean = False
        Dim oLogin As WebControls.Login = DirectCast(sender, WebControls.Login)
        Try
            Dim oAuth As VALIDASTRUCT = clsSeguranca.Autenticar(oLogin.UserName.ToString(), oLogin.Password.ToString())
            If oAuth.RETORNO.SUCESSO Then
                Dim oUsuario2 As New Dados.Usuario(oAuth.USUARIO)
                Dim cn As New Vendedor.ctlVendedor
                Dim dt = cn.SelecionarUsuarioID(oUsuario2.UserCode)
                If dt.Rows.Count = 0 Then
                    Dim cn2 As New Cliente.ctlCliente
                    Dim dr2 As SqlDataReader = cn2.SelecionarRevendedorUsuarioID(oUsuario2.UserCode)
                    If Not dr2.HasRows Then
                        oRet.Sucesso = False
                        oRet.Mensagem = "Seu usuário Microsiga não está associado a um Revendedor no sistema. Por favor, entre em contato com a Intermed."
                    Else
                        lAuth = True
                    End If
                Else
                    lAuth = True
                End If

                If lAuth Then
                    HttpContext.Current.Session.Add("Usuario2", oUsuario2)
                    HttpContext.Current.Session.Add("NomeUsuario", oUsuario2.UserName)
                    Dim aGrupos() As String = oUsuario2.Perfis.ToArray
                    Dim sGrupos As String = Join(aGrupos, "|")
                    Dim ticket As New FormsAuthenticationTicket(1, oUsuario2.UserCode, Now, Now.AddMinutes(15), True, sGrupos)
                    Response.Cookies(".ASPXAUTH").Value = System.Web.Security.FormsAuthentication.Encrypt(ticket)
                    FormsAuthentication.RedirectFromLoginPage(oUsuario2.UserCode, False)
                End If
            Else
                oRet.Sucesso = False
                'oRet.Mensagem = "Usuário e senha não conferem."
                Util.EscreverLogErro("Login - Authenticate: Login: " & oLogin.UserName.ToString() & " - Senha: " & oLogin.Password.ToString() & " - " & oAuth.RETORNO.MENSAGEM.Trim())
                oRet.Mensagem = oAuth.RETORNO.MENSAGEM.Trim()
                'oLogin.FailureText = 
            End If
        Catch ex As Exception
            'ologin.FailureText = ""
            oRet.Sucesso = False
            Util.EscreverLogErro("Login - objLogin_Authenticate: " & oLogin.UserName.ToString() & " - Senha: " & oLogin.Password.ToString() & " - " & oRet.Mensagem.Trim())
            oRet.Mensagem = Util.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

End Class


