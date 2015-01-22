Public Partial Class StatusPropostaBox
    Inherits System.Web.UI.UserControl
    Public Property Codigo() As String
        Get
            Return lblCodigo.Text
        End Get
        Set(ByVal value As String)
            lblCodigo.Text = value
        End Set
    End Property
    Public Property Descricao() As String
        Get
            Return lblDescricao.Text
        End Get
        Set(ByVal value As String)
            lblDescricao.Text = value
        End Set
    End Property
    Public Property CodigoPersonalizado() As String
        Get
            Return lblCodigoPersonalizado.Text
        End Get
        Set(ByVal value As String)
            lblCodigoPersonalizado.Text = value
        End Set
    End Property
    Public Property DescricaoPersonalizada() As String
        Get
            Return lblDescricaoPersonalizada.Text
        End Get
        Set(ByVal value As String)
            lblDescricaoPersonalizada.Text = value
        End Set
    End Property
End Class