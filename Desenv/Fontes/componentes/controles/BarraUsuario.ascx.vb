Public Partial Class BarraUsuario
    Inherits System.Web.UI.UserControl
    Public Property PaginaAtual() As String
        Get
            Return lblLocalAtual.Text.Substring(2)
        End Get
        Set(ByVal value As String)
            lblLocalAtual.Text = ">> " + value
        End Set
    End Property
    Public Property NomeCompletoUsuario() As String
        Get
            Return lblNomeCompleto.text
        End Get
        Set(ByVal value As String)
            lblNomeCompleto.text = value
        End Set
    End Property
    Public ReadOnly Property UserName() As String
        Get
            Return oLoginName.ToString
        End Get
    End Property
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("Usuario2") Is Nothing Then
            NomeCompletoUsuario = DirectCast(Session("Usuario2"), Dados.Usuario).UserFullName
        End If
    End Sub
End Class