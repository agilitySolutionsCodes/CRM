Public Partial Class ClienteBox
    Inherits System.Web.UI.UserControl
    Public Property Codigo() As String
        Get
            Return lblCodigo.Text
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value
        End Set
    End Property
    Public Property Loja() As String
        Get
            Return lblLoja.Text
        End Get
        Set(ByVal value As String)
            lblLoja.Text = value
        End Set
    End Property
    Public Property CPF_CNPJ() As String
        Get
            Return lblCPF_CNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "")
        End Get
        Set(ByVal value As String)
            lblCPF_CNPJ.Text = value
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
            lblLoja.CssClass = value
            lblNome.CssClass = value
            lblCPF_CNPJ.CssClass = value
        End Set
    End Property

End Class