Imports System.Data.SqlClient
Partial Public Class ClienteView
    Inherits System.Web.UI.UserControl
    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event FecharOnClick()
    Public Event PesquisarOnClick()
    Public Enum TipoBusca
        Codigo
        CPF_CPNJ
    End Enum
    Public Property Codigo() As String
        Get
            Return txtCodigo.Text
        End Get
        Set(ByVal value As String)
            txtCodigo.Text = value
        End Set
    End Property
    Public Property Loja() As String
        Get
            Return txtLoja.Text
        End Get
        Set(ByVal value As String)
            txtLoja.Text = value
        End Set
    End Property
    Public Property CPF_CNPJ() As String
        Get
            Return txtCPF_CNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "")
        End Get
        Set(ByVal value As String)
            txtCPF_CNPJ.Text = value
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
            txtLoja.CssClass = value
            lblNome.CssClass = value
            lblBarra.CssClass = value
        End Set
    End Property
    Public ReadOnly Property Text() As String
        Get
            Return txtCodigo.Text + txtLoja.Text
        End Get
    End Property
    Public Function GetArray() As String()
        Dim aRet As New List(Of String)
        aRet.Add(txtCodigo.Text)
        aRet.Add(txtLoja.Text)
        aRet.Add(txtCPF_CNPJ.Text)
        Return aRet.ToArray()
    End Function
    Public Sub Clear()
        txtCodigo.Text = ""
        txtLoja.Text = ""
        txtCPF_CNPJ.Text = ""
        lblNome.Text = ""
    End Sub
    Public Property Enabled() As Boolean
        Get
            Return txtCodigo.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtCodigo.Enabled = value
            txtLoja.Enabled = value
            txtCPF_CNPJ.Enabled = value
            lblNome.Enabled = value
            btnBusca.Visible = value
        End Set
    End Property
    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oDetalhe.SelecionarOnClick
        Dim dr As DataRow
        dr = DirectCast(oSelecao, DataRow)
        txtCodigo.Text = dr("CODIGO").ToString
        txtLoja.Text = dr("LOJA").ToString
        txtCPF_CNPJ.Text = dr("CPFCPNJ").ToString
        lblNome.Text = dr("NOME").ToString
        updCabecalho.Update()
        oDetalhe.FecharJanela()
        lblFound.Text = (True).ToString
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCodigo.Attributes.Add("onchange", "javascript:fnClientViewPreenchido('" + txtCodigo.ClientID + "','" + txtLoja.ClientID + "', '" + txtCodigo.ClientID.Replace("_", "$") + "'); ")
        txtLoja.Attributes.Add("onchange", "javascript:fnClientViewPreenchido('" + txtCodigo.ClientID + "','" + txtLoja.ClientID + "', '" + txtLoja.ClientID.Replace("_", "$") + "'); ")
        txtCPF_CNPJ.Attributes.Add("onchange", "javascript:fnClientViewCPFCNPJ('" + txtCPF_CNPJ.ClientID + "', '" + txtCPF_CNPJ.ClientID.Replace("_", "$") + "'); ")
        If IsPostBack Then
            If TypeOf sender Is ClienteView Then
                Dim sCodigo As String = txtCodigo.Text.Trim
                Dim sLoja As String = txtLoja.Text.Trim
                Dim sCPF_CNPJ As String = txtCPF_CNPJ.Text.Trim
                lblFound.Text = (False).ToString
                If sCodigo.Length > 0 And sLoja.Length > 0 And DirectCast(sender, ClienteView).TipoBuscaSelecionado = TipoBusca.Codigo Then
                    Selecionar(sCodigo, sLoja)
                ElseIf sCPF_CNPJ.Length > 0 And DirectCast(sender, ClienteView).TipoBuscaSelecionado = TipoBusca.CPF_CPNJ Then
                    Selecionar(sCPF_CNPJ)
                End If
                imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            End If
        End If
    End Sub
    Private Sub Selecionar(ByVal sCPF_CNPJ As String)
        sCPF_CNPJ = sCPF_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Trim
        If sCPF_CNPJ.Length >= 11 Then
            Dim cn As New Cliente.ctlCliente
            Dim reader As SqlDataReader = cn.SelecionarCPFCNPJ(sCPF_CNPJ)
            Dim dt As New DataTable
            dt.Load(reader)
            reader.Close()
            lblNome.Text = ""
            lblFound.Text = (False).ToString
            Dim dr As DataRow
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                dr = dt.Rows(0)
                lblNome.Text = dr("Nome").ToString
                txtCodigo.Text = dr("CODIGO").ToString
                txtLoja.Text = dr("LOJA").ToString
                txtCPF_CNPJ.Text = dr("CPFCNPJ").ToString
                'If dr("MSBLQL").ToString.Contains("1") Then
                '    dr = dt.NewRow
                '    lblNome.Text = "Cliente bloqueado"
                '    lblFound.Text = "True"
                'End If
            Else
                dr = dt.NewRow
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            RaiseEvent SelecionarOnClick(dr)
        End If
    End Sub
    Private Sub Selecionar(ByVal sCodigo As String, ByVal sLoja As String)
        If sCodigo.Length > 0 And sLoja.Length > 0 Then
            Dim aParm As New List(Of String)
            aParm.Add(sCodigo)
            aParm.Add(sLoja)
            Dim cn As New Cliente.ctlCliente
            Dim dt As DataTable
            dt = cn.Selecionar(aParm.ToArray)
            lblNome.Text = ""
            lblFound.Text = (False).ToString
            Dim dr As DataRow
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                dr = dt.Rows(0)
                lblNome.Text = dr("Nome").ToString
                txtCodigo.Text = dr("CODIGO").ToString
                txtLoja.Text = dr("LOJA").ToString
                txtCPF_CNPJ.Text = dr("CPFCNPJ").ToString
            Else
                dr = dt.NewRow
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
            RaiseEvent SelecionarOnClick(dr)
        End If
    End Sub
    Private Sub Pesquisar(ByVal sCodigo As String, ByVal sLoja As String)
        If sCodigo.Length > 0 And sLoja.Length > 0 Then
            Dim aParm As New List(Of String)
            aParm.Add(sCodigo)
            aParm.Add(sLoja)
            Dim cn As New Cliente.ctlCliente
            Dim dt As DataTable
            dt = cn.Pesquisar(Cliente.ctlCliente.TipoPesquisa.Código, aParm.ToArray)
            lblNome.Text = ""
            lblFound.Text = (False).ToString
            If dt.Rows.Count > 0 Then
                lblFound.Text = (True).ToString
                Dim dr As DataRow = dt.Rows(0)
                lblNome.Text = dr("Nome").ToString
                txtCodigo.Text = dr("CODIGO").ToString
                txtLoja.Text = dr("LOJA").ToString
                txtCPF_CNPJ.Text = dr("CPFCPNJ").ToString
            End If
            imgNotFound.Visible = Not Boolean.Parse(lblFound.Text)
        End If
    End Sub
    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBusca.Click
        oDetalhe.Exibir()
    End Sub
    Public ReadOnly Property IsValid() As Boolean
        Get
            If lblNome.Text.Trim.Length > 0 And Boolean.Parse(lblFound.Text) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Property ExibirBusca() As Boolean
        Get
            Return btnBusca.Visible
        End Get
        Set(ByVal value As Boolean)
            btnBusca.Visible = value
        End Set
    End Property
    Public Property TipoBuscaSelecionado() As TipoBusca
        Get
            If txtLoja.Visible Then
                Return TipoBusca.Codigo
            Else
                Return TipoBusca.CPF_CPNJ
            End If
        End Get
        Set(ByVal value As TipoBusca)
            If value = TipoBusca.Codigo Then
                txtCodigo.Visible = True
                txtLoja.Visible = True
                lblBarra.Visible = True
                txtCPF_CNPJ.Visible = False
            ElseIf value = TipoBusca.CPF_CPNJ Then
                txtCodigo.Visible = False
                txtLoja.Visible = False
                lblBarra.Visible = False
                txtCPF_CNPJ.Visible = True
            End If
        End Set
    End Property
End Class