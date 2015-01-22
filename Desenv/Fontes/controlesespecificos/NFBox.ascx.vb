Public Class NFBox
    Inherits System.Web.UI.UserControl
    Public Property Nota As String
        Get
            Return lblNota.Text
        End Get
        Set(ByVal value As String)
            lblNota.Text = value
        End Set
    End Property
    Public Property Serie As String
        Get
            Return lblSerie.Text
        End Get
        Set(ByVal value As String)
            lblSerie.Text = value
        End Set
    End Property
End Class