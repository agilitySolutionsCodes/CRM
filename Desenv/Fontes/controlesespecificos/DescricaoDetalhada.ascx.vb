Public Class DescricaoDetalhada
    Inherits System.Web.UI.UserControl
    Public Property Text As String
        Get
            Return lblDescricaoDetalhada.Text
        End Get
        Set(ByVal value As String)
            lblDescricaoDetalhada.Text = value.Replace(Chr(10), "<br />")
        End Set
    End Property
End Class