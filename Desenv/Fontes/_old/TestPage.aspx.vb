Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class TestPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cn As SqlConnection = Util.GetConnection()
        cn.Open()
    End Sub

End Class