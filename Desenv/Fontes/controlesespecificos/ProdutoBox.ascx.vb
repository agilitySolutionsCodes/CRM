Public Partial Class ProdutoBox
    Inherits System.Web.UI.UserControl
    Public Property Codigo() As String
        Get
            Return lblCodigo.Text.Trim
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value.Trim
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return lblNome.Text.Trim
        End Get
        Set(ByVal value As String)
            lblNome.Text = value.Trim
        End Set
    End Property
    Public Property CssClass() As String
        Get
            Return lblCodigo.CssClass
        End Get
        Set(ByVal value As String)
            lblCodigo.CssClass = value
            lblNome.CssClass = value
        End Set
    End Property
    Public ReadOnly Property Text() As String
        Get
            Return lblCodigo.Text.Trim + " " + lblNome.Text.Trim
        End Get
    End Property
    Public Shadows ReadOnly Property ToString() As String
        Get
            Return lblCodigo.Text.Trim + " " + lblNome.Text.Trim
        End Get
    End Property
End Class