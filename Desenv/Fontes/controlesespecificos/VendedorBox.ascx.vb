Public Partial Class VendedorBox
    Inherits System.Web.UI.UserControl
    Public Property Codigo() As String
        Get
            Return lblCodigo.Text
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return lblNome.Text
        End Get
        Set(ByVal value As String)
            lblNome.Text = value
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
End Class