Partial Public Class CondicaoPagamentoView
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

    Public Property Detalhe() As String
        Get
            Return txtDetalhe.Text.Trim
        End Get
        Set(ByVal value As String)
            txtDetalhe.Text = value
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

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.PreencherTiposPesquisa()
        oDetalhe.Pesquisar("1", "")
    End Sub

    Protected Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.TextChanged
        Dim cn As New CondicaoPagamento.ctlCondicaoPagamento
        Dim sCodigo As String = DirectCast(sender, TextBox).Text
        Dim dtResult As DataTable = cn.Selecionar(sCodigo)
        lblNome.Text = ""
        lblFound.Text = (False).ToString
        If dtResult.Rows.Count = 1 Then
            lblFound.Text = (True).ToString
            lblNome.Text = dtResult.Rows(0)("Nome").ToString
            RaiseEvent SelecionarOnClick(dtResult.Rows(0))
        ElseIf sCodigo = "000" Then
            lblFound.Text = (True).ToString
            Dim dr As DataRow = dtResult.NewRow
            dr("Codigo") = "000"
            dr("Nome") = "A Combinar"
            lblNome.Text = dr("Nome").ToString
            RaiseEvent SelecionarOnClick(dr)
        End If
        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
    End Sub

    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        lblFound.Text = (True).ToString
        txtCodigo.Text = dr("CODIGO").ToString
        lblNome.Text = dr("NOME").ToString
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub

    Public Sub Selecionar(ByVal sCodigo As String)
        If sCodigo.Length > 0 Then
            Dim aParm As New List(Of String)
            Dim cn As New CondicaoPagamento.ctlCondicaoPagamento
            Dim dt As DataTable
            dt = cn.Selecionar(sCodigo)
            lblNome.Text = ""
            lblFound.Text = (True).ToString
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                lblNome.Text = dt.Rows(0)("Nome").ToString
            ElseIf sCodigo = "000" Then
                lblFound.Text = (True).ToString
                lblNome.Text = "A Combinar"
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        End If
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

    Public ReadOnly Property IsValid() As Boolean
        Get
            If txtCodigo.Text.Trim.Length > 0 And lblNome.Text.Trim.Length > 0 And Boolean.Parse(lblFound.Text) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

End Class