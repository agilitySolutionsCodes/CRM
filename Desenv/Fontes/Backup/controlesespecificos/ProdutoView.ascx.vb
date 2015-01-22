Partial Public Class ProdutoView
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
            lblCodigo.Text = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return txtNome2.Text
        End Get
        Set(ByVal value As String)
            txtNome2.Text = value
            lblNome.Text = value
        End Set
    End Property

    Public Property HabilitarEdicao() As Boolean
        Get
            Return pnlEdicao.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlEdicao.Visible = value
            pnlVisualizacao.Visible = Not pnlEdicao.Visible
        End Set
    End Property

    Public Property CssClass() As String
        Get
            Return txtCodigo.CssClass
        End Get
        Set(ByVal value As String)
            txtCodigo.CssClass = value
            txtNome2.CssClass = value
            lblNome.CssClass = value
            lblCodigo.CssClass = value
        End Set
    End Property

    Public ReadOnly Property Text() As String
        Get
            Return txtCodigo.Text
        End Get
    End Property

    Public Sub Clear()
        txtCodigo.Text = ""
        lblCodigo.Text = ""
        txtNome2.Text = ""
        lblNome.Text = ""
        lblPrecoVenda1.Text = ""
        lblClass1.Text = ""
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return txtCodigo.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.Enabled = value
            txtNome2.Enabled = value
            lblCodigo.Enabled = value
            lblNome.Enabled = value
            btnBusca.Visible = value
        End Set
    End Property

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.Exibir()
    End Sub

    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        txtCodigo.Text = dr("CODIGO").ToString
        txtNome2.Text = dr("NOME").ToString
        lblNome.Text = dr("NOME").ToString
        lblPrecoVenda1.Text = dr("PRECOVENDA1").ToString
        lblClass1.Text = dr("CLASS1").ToString
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        lblFound.Text = (True).ToString
        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub

    Protected Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.TextChanged
        Dim sCodigo As String = DirectCast(sender, TextBox).Text.Trim
        If sCodigo.Length > 0 Then
            Dim cn As New ctlProduto
            Dim dt As New DataTable
            dt = cn.Selecionar(sCodigo)
            txtNome2.Text = ""
            lblNome.Text = ""
            lblPrecoVenda1.Text = ""
            lblClass1.Text = ""
            lblFound.Text = (False).ToString
            Dim dr As DataRow
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                lblFound.Text = (True).ToString
                txtNome2.Text = dr("Nome").ToString
                lblNome.Text = dr("Nome").ToString
                lblPrecoVenda1.Text = dr("PrecoVenda1").ToString
                lblClass1.Text = dr("Class1").ToString
            Else
                dr = dt.NewRow()
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            RaiseEvent SelecionarOnClick(dr)
        End If
    End Sub

    Public ReadOnly Property IsValid() As Boolean
        Get
            If txtCodigo.Text.Trim.Length > 0 And lblNome.Text.Trim.Length > 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property PrecoVenda1() As Decimal
        Get
            Return Decimal.Parse(lblPrecoVenda1.Text)
        End Get
    End Property

    Public ReadOnly Property Class1() As String
        Get
            Return lblClass1.Text.Trim
        End Get
    End Property

End Class