Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim pagina As String = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath)
        If pagina <> "Login.aspx" Then
            If Session("Usuario2") Is Nothing Then
                Session.Abandon()
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub

End Class