'TODO: PERMITIR A SELECAO DAS COLUNAS EXIBIDAS
'TODO: PERMITIR NOMEAR AS COLUNAS EXIBIDAS
'
Imports System.Reflection
Partial Public Class ConsultaPadrao
    Inherits System.Web.UI.UserControl
    Private sProcedure As String = ""
    Private Const sListarTiposPesquisa As String = "ListarTiposPesquisa"
    Private Const sPesquisar As String = "Pesquisar"
    Private sDataKeys As String = "CODIGO"
    Private oItemSelecionado As New Object
    Private drSelecionado As DataRow
    Public Event Fechar()
    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event PesquisarOnClick(ByVal oTipoPesquisa As Object, ByVal oParametros As Object, ByVal oResultado As Object)
    Public Event Ordenar()
    Public ReadOnly Property Item() As Object
        Get
            Return drSelecionado
        End Get
    End Property
    Public Sub PreencherTiposPesquisa()
        Dim oObj As Object = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(sProcedure, True, True))
        drpTipoPesquisa.Items.Clear()
        Dim aValor As Array = DirectCast(CallByName(oObj, sListarTiposPesquisa, CallType.Method), Array)
        Dim oItem As Object
        For Each oItem In aValor
            drpTipoPesquisa.Items.Add(New ListItem(oItem.ToString, DirectCast(oItem, Integer).ToString))
        Next
    End Sub
    Public Property TipoPesquisaCssClass() As String
        Get
            Return drpTipoPesquisa.CssClass
        End Get
        Set(ByVal value As String)
            drpTipoPesquisa.CssClass = value
        End Set
    End Property
    Public Property ParametroCssClass() As String
        Get
            Return txtParametroPesquisa.CssClass
        End Get
        Set(ByVal value As String)
            txtParametroPesquisa.CssClass = value
        End Set
    End Property
    Public Property Procedure() As String
        Get
            Return sProcedure
        End Get
        Set(ByVal value As String)
            sProcedure = value
        End Set
    End Property
    Public Property ResultadoCabecalhoCssClass() As String
        Get
            Return grdPesquisar.HeaderStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.HeaderStyle.CssClass = value
        End Set
    End Property
    Public Sub Pesquisar(ByVal sTipo As String, ByVal oConteudo As Object)
        Try
            Dim oObj As Object = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(sProcedure, True, True))
            Dim dt As DataTable
            Dim oParm As New List(Of Object)
            oParm.Add(sTipo)
            oParm.Add(oConteudo)
            dt = DirectCast(CallByName(oObj, sPesquisar, CallType.Method, oParm.ToArray()), DataTable)
            grdPesquisar.DataSource = dt
            grdPesquisar.DataBind()
            ViewState.Add("Resultado", dt)
            If dt.Rows.Count = 0 Then
                oMensagem.SetMessage("A pesquisa não retornou nenhum resultado", "W")
            Else
                oMensagem.ClearMessage()
            End If
            RaiseEvent PesquisarOnClick(sTipo, oConteudo, dt)
        Catch ex As Exception
            oMensagem.SetMessage(ex.Message.ToString, "W")
        End Try
    End Sub
    Public Sub Pesquisar()
        Pesquisar(drpTipoPesquisa.SelectedValue, txtParametroPesquisa.Text)
    End Sub
    Private Sub grdPesquisar_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPesquisar.PageIndexChanging
        Dim dt As DataTable = DirectCast(ViewState("Resultado"), DataTable)
        grdPesquisar.PageIndex = e.NewPageIndex
        grdPesquisar.DataSource = dt
        grdPesquisar.DataBind()
        RaiseEvent PaginarOnClick()
    End Sub
    Private Sub grdPesquisar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPesquisar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As GridViewRow = e.Row
            Dim selectCell As TableCell = row.Cells(0)
            If (selectCell.Controls.Count > 0) Then
                Dim selectControl As LinkButton = DirectCast(selectCell.Controls(0), LinkButton)
                If Not IsNothing(selectControl) Then
                    selectControl.Text = "Selecionar"
                End If
            End If
        End If
    End Sub
    Protected Sub grdPesquisar_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdPesquisar.SelectedIndexChanged
        Dim dt As New DataTable
        Dim gh As GridViewRow = grdPesquisar.HeaderRow
        For Each cel As TableCell In gh.Cells
            If cel.HasControls Then
                If TypeOf cel.Controls(0) Is LinkButton Then
                    dt.Columns.Add(New DataColumn(DirectCast(cel.Controls(0), LinkButton).Text, GetType(Object)))
                Else
                    dt.Columns.Add(New DataColumn("", GetType(Object)))
                End If
                'Else
                'dt.Columns.Add(New DataColumn("", GetType(Object)))
            End If
        Next
        drSelecionado = dt.NewRow
        Dim gr As GridViewRow
        Dim i As Integer = -1
        gr = DirectCast(sender, GridView).SelectedRow
        For Each cel As TableCell In gr.Cells
            If i >= 0 Then
                drSelecionado(i) = cel.Text
            End If
            i += 1
        Next
        RaiseEvent SelecionarOnClick(drSelecionado)
        Limpar()
    End Sub
    Private Sub grdPesquisar_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPesquisar.Sorting
        Dim dv As New DataView(DirectCast(ViewState("Resultado"), DataTable))
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim sCampoSort As String = e.SortExpression
        If gv.SortDirection = SortDirection.Descending Then
            sCampoSort += " DESC"
        End If
        dv.Sort = sCampoSort
        ViewState("Resultado") = dv.ToTable
        grdPesquisar.DataSource = DirectCast(ViewState("Resultado"), DataTable)
        grdPesquisar.DataBind()
        RaiseEvent Ordenar()
    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btnPesquisar.NomePainelBotoes = pnlParametros.ClientID
        'btnPesquisar.NomePainelMensagem = oMensagem.ClientID
        If Not IsPostBack Then
            oMensagem.ClearMessage()
            PreencherTiposPesquisa()
        End If
    End Sub
    Private Sub Limpar()
        Dim dt As DataTable = DirectCast(ViewState("Resultado"), DataTable)
        oMensagem.ClearMessage()
        If Not dt Is Nothing Then
            dt.Rows.Clear()
            grdPesquisar.DataSource = dt
            grdPesquisar.DataBind()
            ViewState.Remove("Resultado")
        End If
        txtParametroPesquisa.Text = ""
    End Sub
    Public Property AlternatingRowStyle() As String
        Get
            Return grdPesquisar.AlternatingRowStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.AlternatingRowStyle.CssClass = value
        End Set
    End Property
    Public Property RowStyle() As String
        Get
            Return grdPesquisar.RowStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.RowStyle.CssClass = value
        End Set
    End Property
    Public Property HeaderStyle() As String
        Get
            Return grdPesquisar.HeaderStyle.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.HeaderStyle.CssClass = value
        End Set
    End Property
    Public Property GridCssClass() As String
        Get
            Return grdPesquisar.CssClass
        End Get
        Set(ByVal value As String)
            grdPesquisar.CssClass = value
        End Set
    End Property
    Public Property PesquisarCssClass() As String
        Get
            Return btnPesquisar.CssClass 'btnPesquisar.CssButton
        End Get
        Set(ByVal value As String)
            btnPesquisar.CssClass = value 'btnPesquisar.CssButton = value
        End Set
    End Property
    Public Sub FecharJanela()
        Limpar()
        RaiseEvent Fechar()
        RaiseEvent CancelarOnClick()
    End Sub
    Public Property ParametroPesquisa() As String
        Get
            Return txtParametroPesquisa.Text
        End Get
        Set(ByVal value As String)
            txtParametroPesquisa.Text = value
        End Set
    End Property
    Public Property ExibirParametros() As Boolean
        Get
            Return pnlParametros.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlParametros.Visible = value
        End Set
    End Property
    'Private Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPesquisar.Click
    '    Pesquisar()
    'End Sub
    Public Property ExibirPainelParametros() As Boolean
        Get
            Return pnlParametros.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlParametros.Visible = value
        End Set
    End Property
    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPesquisar.Click
        Pesquisar()
    End Sub
End Class