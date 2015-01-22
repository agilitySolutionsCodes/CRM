Partial Public Class VendedorView
    Inherits System.Web.UI.UserControl
    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event FecharOnClick()
    Public Event PesquisarOnClick()
    Public Property Codigo() As String
        Get
            Return txtCodigo.Text
        End Get
        Set(ByVal value As String)
            txtCodigo.Text = value
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
            Return txtCodigo.CssClass
        End Get
        Set(ByVal value As String)
            txtCodigo.CssClass = value
            lblNome.CssClass = value
        End Set
    End Property
    Public ReadOnly Property Text() As String
        Get
            Return txtCodigo.Text
        End Get
    End Property
    Public Sub Clear()
        txtCodigo.Text = ""
        lblNome.Text = ""
    End Sub
    Public Property Enabled() As Boolean
        Get
            Return txtCodigo.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.Enabled = value
            lblNome.Enabled = value
            btnBusca.Visible = value
        End Set
    End Property
    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        txtCodigo.Text = dr("CODIGO").ToString
        lblNome.Text = dr("NOME").ToString
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        lblFound.Text = (True).ToString
        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub
    Protected Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.TextChanged
        Dim sCodigo As String = DirectCast(sender, TextBox).Text.Trim
        If sCodigo.Length > 0 Then
            Dim cn As New Vendedor.ctlVendedor
            Dim dt As DataTable
            dt = cn.Selecionar(sCodigo)
            lblNome.Text = ""
            lblFound.Text = (False).ToString
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                lblNome.Text = dt.Rows(0)("Nome").ToString
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        End If
    End Sub
    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.Exibir()
    End Sub
    Public ReadOnly Property IsValid() As Boolean
        Get
            If txtCodigo.Text.Trim.Length > 0 And lblNome.Text.Trim.Length > 0 And Boolean.Parse(lblFound.Text) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Property ExibirCodigo() As Boolean
        Get
            Return txtCodigo.Visible
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.Visible = value
        End Set
    End Property
    Public Property ExibirBusca() As Boolean
        Get
            Return btnBusca.Visible
        End Get
        Set(ByVal value As Boolean)
            btnBusca.Visible = value
        End Set
    End Property
End Class