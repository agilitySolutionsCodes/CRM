Partial Public Class Mensagem
    Inherits System.Web.UI.UserControl
    Public ReadOnly Property Text() As String
        Get
            Return lblMensagem.Text
        End Get
    End Property
    Public Sub SetMessage(ByVal oMensagem As Object, Optional ByVal sTipo As String = "E")
        lblMensagem.Text = oMensagem.ToString()
        lblMensagem.CssClass = "Mensagem Texto Aguarde"
        imgIcone.CssClass = "Mensagem Icone Aguarde"
        If TypeOf (oMensagem) Is RetornoGenerico Then
            If Not DirectCast(oMensagem, RetornoGenerico).Sucesso Then
                lblMensagem.CssClass = "Mensagem Texto Erro"
                imgIcone.CssClass = "Mensagem Icone Erro"
            Else
                lblMensagem.CssClass = "Mensagem Texto Informacao"
                imgIcone.CssClass = "Mensagem Icone Informacao"
            End If
        Else
            If sTipo = "E" Then
                lblMensagem.CssClass = "Mensagem Texto Erro"
                imgIcone.CssClass = "Mensagem Icone Erro"
            ElseIf sTipo = "W" Then
                lblMensagem.CssClass = "Mensagem Texto Alerta"
                imgIcone.CssClass = "Mensagem Icone Alerta"
            ElseIf sTipo = "I" Then
                lblMensagem.CssClass = "Mensagem Texto Informacao"
                imgIcone.CssClass = "Mensagem Icone Informacao"
            End If
        End If
    End Sub
    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return pnlMensagem.ClientID
        End Get
    End Property
    Public Sub ClearMessage()
        lblMensagem.Text = ""
        lblMensagem.CssClass = "Mensagem Texto Clear"
        imgIcone.CssClass = "Mensagem Icone Clear"
    End Sub
End Class