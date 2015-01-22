Partial Public Class DetalheConsultaPadrao
    Inherits System.Web.UI.UserControl

    Public Event SelecionarOnClick(ByVal oSelecao As Object)
    Public Event PaginarOnClick()
    Public Event CancelarOnClick()
    Public Event FecharOnClick()
    Public Event PesquisarOnClick()

    Protected Sub imgFechar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgFechar.Click
        oConsulta.FecharJanela()
        pnlConsulta.Visible = False
    End Sub

    Public Sub Exibir()
        oConsulta.PreencherTiposPesquisa()
        pnlConsulta.Visible = True
    End Sub

    Public Sub Buscar()
        oConsulta.PreencherTiposPesquisa()
        pnlConsulta.Visible = True
        oConsulta.Pesquisar()
    End Sub

    Public Sub PreencherTiposPesquisa()
        oConsulta.PreencherTiposPesquisa()
    End Sub

    Public Sub Pesquisar()
        oConsulta.Pesquisar()
        pnlConsulta.Visible = True
    End Sub

    Public Sub Pesquisar(ByVal sTipo As String, ByVal oConteudo As Object)
        oConsulta.Pesquisar(sTipo, oConteudo)
        pnlConsulta.Visible = True
    End Sub

    Public Property ParametroPesquisa() As String
        Get
            Return oConsulta.ParametroPesquisa
        End Get
        Set(ByVal value As String)
            oConsulta.ParametroPesquisa = value
        End Set
    End Property

    Public Sub FecharJanela()
        oConsulta.FecharJanela()
        pnlConsulta.Visible = False
    End Sub

    Private Sub oConsulta_CancelarOnClick() Handles oConsulta.CancelarOnClick
        RaiseEvent CancelarOnClick()
    End Sub

    Private Sub oConsulta_Fechar() Handles oConsulta.Fechar
        RaiseEvent FecharOnClick()
    End Sub

    Private Sub oConsulta_PaginarOnClick() Handles oConsulta.PaginarOnClick
        RaiseEvent PaginarOnClick()
    End Sub

    Private Sub oConsulta_PesquisarOnClick(ByVal oTipoPesquisa As Object, ByVal oParametros As Object, ByVal oResultado As Object) Handles oConsulta.PesquisarOnClick
        RaiseEvent PesquisarOnClick()
    End Sub

    Private Sub oConsulta_SelecionarOnClick(ByVal oSelecao As Object) Handles oConsulta.SelecionarOnClick
        RaiseEvent SelecionarOnClick(oSelecao)
    End Sub

    Public Property Procedure() As String
        Get
            Return oConsulta.Procedure
        End Get
        Set(ByVal value As String)
            oConsulta.Procedure = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return lblTopBar.Text
        End Get
        Set(ByVal value As String)
            lblTopBar.Text = value
        End Set
    End Property

    Public Property ExibirPainelParametros() As Boolean
        Get
            Return oConsulta.ExibirPainelParametros
        End Get
        Set(ByVal value As Boolean)
            oConsulta.ExibirPainelParametros = value
        End Set
    End Property

End Class