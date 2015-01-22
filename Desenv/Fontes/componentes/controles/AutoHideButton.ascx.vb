Partial Public Class AutoHideButton
    Inherits System.Web.UI.UserControl
    Public Event Click(ByVal sender As Object, ByVal e As EventArgs)
    Private sPainelBotoes As String = ""
    Private sNomePainelMensagem As String = ""
    Public Property NomePainelBotoes() As String
        Get
            Return sPainelBotoes
        End Get
        Set(ByVal value As String)
            sPainelBotoes = value
            btnAutoHide.OnClientClick = "javascript:Aguarde('Aguarde, processando solicitação...', '" + sPainelBotoes + "', '" + sNomePainelMensagem + "');"
        End Set
    End Property
    Public Property NomePainelMensagem() As String
        Get
            Return sNomePainelMensagem
        End Get
        Set(ByVal value As String)
            sNomePainelMensagem = value
            btnAutoHide.OnClientClick = "javascript:Aguarde('Aguarde, processando solicitação...', '" + sPainelBotoes + "', '" + sNomePainelMensagem + "');"
        End Set
    End Property
    Public Property CssButton() As String
        Get
            Return btnAutoHide.CssClass
        End Get
        Set(ByVal value As String)
            btnAutoHide.CssClass = value
        End Set
    End Property
    Protected Sub btnAutoHide_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAutoHide.Click
        RaiseEvent Click(sender, e)
    End Sub
    Public Property CommandArgument As String
        Get
            Return btnAutoHide.CommandArgument
        End Get
        Set(ByVal value As String)
            btnAutoHide.CommandArgument = value
        End Set
    End Property
    Public Property Text() As String
        Get
            Return btnAutoHide.Text
        End Get
        Set(ByVal value As String)
            btnAutoHide.Text = value
        End Set
    End Property
    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return pnlAutoHide.ClientID
        End Get
    End Property
    Public Property OnClientClick() As String
        Get
            Return btnAutoHide.OnClientClick
        End Get
        Set(ByVal value As String)
            btnAutoHide.OnClientClick = value
        End Set
    End Property

End Class