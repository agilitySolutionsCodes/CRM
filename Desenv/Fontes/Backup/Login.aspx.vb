Imports System.ComponentModel
Imports System.Security.Principal
Imports System.Data.SqlClient
Imports Orcamento.wshumberto.wsusermicrosiga

Partial Public Class Login
    Inherits System.Web.UI.Page

    Private Sub objLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles objLogin.Authenticate
        Dim oRet As New RetornoGenerico
        Dim lAuth As Boolean = False
        Dim oLogin As WebControls.Login = DirectCast(sender, WebControls.Login)
        Try
            Dim oAuth As AUTHSTRUCT = clsSeguranca.Autenticar(oLogin.UserName.ToString(), oLogin.Password.ToString())
            If oAuth.RETORNO.SUCESSO Then
                Dim oUsuario2 As New Dados.Usuario(oAuth.USUARIO)
                Dim cn As New Vendedor.ctlVendedor
                Dim dr As SqlDataReader = cn.SelecionarUsuarioID(oUsuario2.UserCode)
                If Not dr.HasRows Then
                    Dim cn2 As New Cliente.ctlCliente
                    Dim dr2 As SqlDataReader = cn2.SelecionarRevendedorUsuarioID(oUsuario2.UserCode)
                    If Not dr2.HasRows Then
                        oLogin.FailureText = "Seu usuário Microsiga não está associado a um Revendedor no sistema. Por favor, entre em contato com a Intermed."
                    Else
                        lAuth = True
                    End If
                    dr.Close()
                Else
                    lAuth = True
                End If
                dr.Close()
                If lAuth Then
                    Session("Usuario2") = oUsuario2
                    Dim aGrupos() As String = oUsuario2.Perfis.ToArray
                    Dim sGrupos As String = Join(aGrupos, "|")
                    Dim ticket As New FormsAuthenticationTicket(1, oUsuario2.UserName, Now, Now.AddMinutes(15), True, sGrupos)
                    Response.Cookies(".ASPXAUTH").Value = System.Web.Security.FormsAuthentication.Encrypt(ticket)
                    FormsAuthentication.RedirectFromLoginPage(oUsuario2.UserName, False)
                End If
            Else
                oLogin.FailureText = "Usuário e senha não conferem."
            End If
        Catch ex As Exception
            'ologin.FailureText = ""
            oRet.Sucesso = False
            oRet.Mensagem = "Ocorreu um erro ao autenticar o usuário: " + ex.ToString
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

End Class


