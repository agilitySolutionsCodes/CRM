Public Partial Class Principal
    Inherits BaseWebUI
    'Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnBuscar.NomePainelBotoes = btnBuscar.ClientID
            btnBuscar.NomePainelMensagem = oMensagem.ClientID
            If Not IsPostBack Then
                CarregarTiposPesquisa()
                FormatarParametroPesquisa()
                DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = "Proposta de Venda"
            End If
        Catch ex As Exception
            Dim oRet As New RetornoGenerico
            oRet.Sucesso = False
            Util.EscreverLogErro("Principal - Page_Load: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub CarregarTiposPesquisa()
        drpTipoPequisa.Items.Clear()
        Dim aValor As Array = System.Enum.GetValues(GetType(ctlProposta.TipoPesquisaProposta))
        For Each oItem In aValor
            drpTipoPequisa.Items.Add(New ListItem(Aplicacao.GetEnumDescription(CType(oItem, [Enum])), DirectCast(oItem, Integer).ToString))
        Next
    End Sub

    Private Sub Pesquisar()
        Dim oRet As New RetornoGenerico
        Try
            Dim oTipoSelecionado As ctlProposta.TipoPesquisaProposta
            oTipoSelecionado = DirectCast(Integer.Parse(drpTipoPequisa.SelectedValue), ctlProposta.TipoPesquisaProposta)
            Dim oParametros As Object
            Select Case oTipoSelecionado
                Case ctlProposta.TipoPesquisaProposta.Cliente
                    oParametros = txtCliente.GetArray
                    'Case ctlProposta.TipoPesquisaProposta.Vendedor
                    '    oParametros = txtVendedor.Text
                Case ctlProposta.TipoPesquisaProposta.Emissão
                    oParametros = txtEmissao.GetDate
                Case ctlProposta.TipoPesquisaProposta.A_Vencer
                    oParametros = Nothing
                Case Else
                    oParametros = txtParametroPesquisa.Text
            End Select
            oRet = Validar(oTipoSelecionado, oParametros)
            If oRet.Sucesso Then
                Dim dt As DataTable = New ctlProposta().Pesquisar(oTipoSelecionado, oParametros)
                oMensagem.ClearMessage()
                If oTipoSelecionado = ctlProposta.TipoPesquisaProposta.A_Vencer Then
                    'dt.DefaultView.RowFilter = "DataValidade > #" + Now.AddDays(-5).ToString("dd/MM/yyyy") + "#"
                    'dt.DefaultView.RowFilter = "CODIGOSTATUS in ('A','D') and  DataValidade >= #" + Now.ToString("yyyy-MM-dd") + "# and  DataValidade <= # " + Now.AddDays(10).ToString("yyyy-MM-dd") + "#"
                    dt.DefaultView.RowFilter = " DataValidade <= # " + Now.AddDays(10).ToString("yyyy-MM-dd") + " # and DataValidade >= # " + Now.ToString("yyyy-MM-dd") + " #"
                    dt.DefaultView.Sort = "DataValidade"
                End If
                grdItens.DataSource = dt
                grdItens.DataBind()
                If grdItens.Rows.Count = 0 Then
                    oMensagem.SetMessage("A pesquisa não retornou resultados")
                    pnlResultado.Visible = False
                Else
                    pnlResultado.Visible = True

                    'Caso exista resultados na busca oculta painel de inclusão
                    pnlIncluir.Visible = False
                End If
            Else
                    oMensagem.SetMessage(oRet)
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            Util.EscreverLogErro("Principal - Pesquisar: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Public Function Validar(ByVal oTipoSelecionado As ctlProposta.TipoPesquisaProposta, ByVal oParametros As Object) As RetornoGenerico
        Dim oRet As New RetornoGenerico
        Dim bSucesso As Boolean = False
        Dim sMensagem As String = ""
        If oTipoSelecionado = ctlProposta.TipoPesquisaProposta.Emissão Then
            If Not IsDate(oParametros.ToString) Or oParametros.ToString = "01/01/0001 00:00:00" Or oParametros.ToString = "1/1/0001 00:00:00" Then
                sMensagem = "Forneça uma data válida"
            Else
                bSucesso = True
            End If
        ElseIf oTipoSelecionado = ctlProposta.TipoPesquisaProposta.Todas OrElse oTipoSelecionado = ctlProposta.TipoPesquisaProposta.A_Vencer Then
            bSucesso = True
        Else
            If Not IsArray(oParametros) Then
                If oParametros.ToString.Trim.Length = 0 Then
                    sMensagem = "Forneça um parâmetro de pesquisa"
                Else
                    bSucesso = True
                End If
            Else
                If DirectCast(oParametros, String())(2).Length = 0 And (DirectCast(oParametros, String())(0).Length = 0 Or DirectCast(oParametros, String())(1).Length = 0) Then
                    sMensagem = "Forneça um parâmetro de pesquisa"
                Else
                    bSucesso = True
                End If
            End If
        End If
        With oRet
            .Chave = ""
            .Codigo = 0
            .Mensagem = sMensagem
            .Sucesso = bSucesso
        End With
        Return oRet
    End Function

    Private Sub FormatarParametroPesquisa()
        Dim oTipoSelecionado As New ctlProposta.TipoPesquisaProposta
        Limpar()
        oTipoSelecionado = DirectCast(Integer.Parse(drpTipoPequisa.SelectedValue), ctlProposta.TipoPesquisaProposta)
        txtParametroPesquisa.Visible = False
        txtCliente.Visible = False
        txtEmissao.Visible = False
        Select Case oTipoSelecionado
            Case ctlProposta.TipoPesquisaProposta.Cliente
                txtCliente.Visible = True
            Case ctlProposta.TipoPesquisaProposta.Todas
                txtCliente.Visible = False
            Case ctlProposta.TipoPesquisaProposta.Emissão
                txtEmissao.Visible = True
            Case ctlProposta.TipoPesquisaProposta.A_Vencer
                txtCliente.Visible = False
            Case Else
                txtParametroPesquisa.Visible = True
        End Select
    End Sub

    Protected Sub drpTipoPequisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpTipoPequisa.SelectedIndexChanged
        FormatarParametroPesquisa()
    End Sub

    Public Sub Limpar()
        txtParametroPesquisa.Text = ""
        txtEmissao.Clear()
        txtCliente.Clear()
        'txtVendedor.Clear()
        oMensagem.ClearMessage()
        grdItens.DataSource = ""
        grdItens.DataBind()
        pnlResultado.Visible = False
    End Sub

    Private Sub txtCliente_SelecionarOnClick(ByVal oSelecao As Object) Handles txtCliente.SelecionarOnClick
        Pesquisar()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Pesquisar()
    End Sub

    Protected Sub btnIncluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIncluir.Click
        If drpFaturadoPor.SelectedIndex < 1 Then
            oMensagem.SetMessage("Por favor, informe quem irá faturar a Proposta")
        Else
            Response.Redirect("Proposta.aspx?FaturadoPor=" + drpFaturadoPor.SelectedValue, True)
            'btnIncluir.OnClientClick = "javascript:window.open('Proposta.aspx','_new','directories=no,location=no,menubar=no,status=no,toolbar=no,scrollbars=yes,resizable=yes'); return false;"
        End If
    End Sub

End Class