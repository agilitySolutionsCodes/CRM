Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO

Partial Public Class Proposta
    Inherits BaseWebUI
    'Inherits System.Web.UI.Page

    Private nPrecoTotal As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sNumero As String = ""
            btnSalvar.NomePainelMensagem = oMensagem.ClientID
            btnSalvar.NomePainelBotoes = pnlBotoes.ClientID
            btnAprovar.NomePainelMensagem = oMensagem.ClientID
            btnAprovar.NomePainelBotoes = pnlBotoes.ClientID
            If Not IsPostBack Then
                PreencherPossibilidadeVenda()
                If Request("Numero") Is Nothing Then
                    'inclusao
                    hdnFaturadoPor.Value = Request("FaturadoPor")
                    If hdnFaturadoPor.Value Is Nothing Or hdnFaturadoPor.Value = "" Then
                        hdnFaturadoPor.Value = "I"
                    End If
                    IncluirProposta()
                    DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = "Proposta de Venda - Incluir"
                    grdItens.Columns(1).Visible = False 'Número do Item
                Else
                    'exibicao/edicao
                    sNumero = Request("Numero").Trim
                    SelecionarProposta(sNumero)
                    DirectCast(Me.Master.Controls(0).Controls(3).Controls(7).FindControl("oBarraUsuario"), BarraUsuario).PaginaAtual = "Proposta de Venda - Visualizar"
                End If
            Else
                Recalcular()
            End If
        Catch ex As Exception
            Dim oRet As New RetornoGenerico
            Util.EscreverLogErro("Proposta - Page_Load: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub Recalcular()
        If grdItens.EditIndex <> -1 Then
            Dim gr As GridViewRow = grdItens.SelectedRow
            Dim oQuantidade As NumberBox = DirectCast(gr.FindControl("txtQuantidade"), NumberBox)
            Dim oPrecoUnitario As NumberBox = DirectCast(gr.FindControl("txtPrecoUnitario"), NumberBox)
            Dim oPrecoLista As Label = DirectCast(gr.FindControl("lblPrecoLista"), Label)
            Dim oTotal As Label = DirectCast(gr.FindControl("lblValorTotal"), Label)
            If oPrecoLista.Text.Trim.Length > 0 Then
                oTotal.Text = (oQuantidade.Value * oPrecoUnitario.Value).ToString("#0.00")
            End If
            If IsNumeric(oQuantidade.Text) And IsNumeric(oPrecoUnitario.Text) Then
                oTotal.Text = (Integer.Parse(oQuantidade.Text) * Decimal.Parse(oPrecoUnitario.Text)).ToString
            End If
        End If
        Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
        Dim dr As DataRow
        Dim nPrecoTotal As Decimal = 0
        For Each dr In dt.Rows
            nPrecoTotal += Decimal.Parse(dr("Valor").ToString)
        Next
        lblTotalProposta.Text = (nPrecoTotal).ToString("c")
    End Sub

    Private Sub PreencherPossibilidadeVenda()
        Dim i As Integer
        drpPossibilidadeVenda.Items.Add(New ListItem("Selecione", ""))
        For i = 0 To 10
            drpPossibilidadeVenda.Items.Add(New ListItem((i * 10).ToString("00") + "%", (i * 10).ToString("00") + "%"))
        Next
    End Sub

    Private Sub IncluirProposta()
        Dim dt As DataTable = ObterEstrutura()
        PreencherProposta(dt)
        btnSalvar.Text = "Confirmar"
        pnlCabecalho.Enabled = True
        txtVendedor.Enabled = False
    End Sub

    Private Sub SelecionarProposta(ByVal sNumero As String)
        Dim ct As New ctlProposta
        Dim dt As DataTable = ct.Selecionar(sNumero)
        If dt.Rows.Count = 0 Then
            Throw New Exception("A proposta '" + sNumero + "' não foi localizada.")
        Else
            PreencherProposta(dt)
            If dt.Rows(0)("CodigoStatus").ToString = "D" Then
                'se esta em analise, pode ser desativada
                btnAlterar.Visible = True
                btnSalvar.Text = "Desativar"
                btnSalvar.OnClientClick = "return confirm('Esta ação desabilitará a proposta selecionada. Confirmar?');"
                btnAprovar.Visible = True
                btnImprimir.Visible = True
                pnlCabecalho.Enabled = True
                grdItens.Columns(0).Visible = True
            ElseIf dt.Rows(0)("CodigoStatus").ToString = "A" Then
                btnAlterar.Visible = False
                btnSalvar.Visible = False
                btnImprimir.Visible = True
                btnAprovar.Visible = False
                pnlCabecalho.Enabled = False
                grdItens.Columns(0).Visible = False
            ElseIf dt.Rows(0)("CodigoStatus").ToString = "B" Then
                btnAlterar.Visible = False
                pnlCabecalho.Enabled = False
                btnSalvar.Visible = False
                btnImprimir.Visible = True
                btnAprovar.Visible = False
                grdItens.Columns(0).Visible = False
            Else
                'se nao esta ativa, nao há o que alterar
                btnAlterar.Visible = False
                btnSalvar.Visible = False
                btnImprimir.Visible = False
                btnAprovar.Visible = False
                pnlCabecalho.Enabled = False
                grdItens.Columns(0).Visible = False
            End If
            txtVendedor.Enabled = False
        End If
    End Sub

    Private Sub PreencherProposta(ByVal dt As DataTable)

        'preenche dados a partir do xml gravado
        If File.Exists(NomeArquivoXml()) Then
            Dim ds = New DataSet
            ds.ReadXml(NomeArquivoXml())
            If (ds.Tables.Count > 0) Then
                If String.IsNullOrEmpty(dt.Rows(0).Item("NUMERO").ToString) Then dt.Rows(0).Item("NUMERO") = "000000"
                If ds.Tables(0).Rows(0).Item("LICENCIADO").ToString.Trim = HttpContext.Current.User.Identity.Name.Trim _
                    And dt.Rows(0).Item("NUMERO").ToString = ds.Tables(0).Rows(0).Item("NUMERO").ToString Then
                    dt = ds.Tables(0)

                    'printar msg na tela informando que existem dados alterados e não salvos
                    Dim oRet As New RetornoGenerico
                    oRet.Mensagem = "Existem itens alterados e/ou incluídos que não foram salvos no servidor. Por favor clicar no botão confirmar."
                    oRet.Sucesso = True
                    oMensagem.SetMessage(oRet)

                End If
            End If
        End If


        Dim dr As DataRow = dt.Rows(0)
        If IsDate(dr("Emissao")) Then
            lblEmissao.Text = CDate(dr("Emissao")).ToString("dd/MM/yyyy")
        Else
            lblEmissao.Text = Date.Now.ToString("dd/MM/yyyy")
        End If

        lblNumero.Text = dr("Numero").ToString

        If Not IsDate(dr("DataValidade")) Then
            txtDataValidade.Text = Now.AddDays(30).ToString("dd/MM/yyyy")
        Else
            txtDataValidade.Text = CDate(dr("DataValidade")).ToString("dd/MM/yyyy")
            'If CDate(txtDataValidade.Text).AddDays(5) >= Now.Date AndAlso CDate(txtDataValidade.Text) <= Now.Date Then
            If Now.Date.AddDays(5) >= CDate(txtDataValidade.Text) AndAlso Now.Date <= CDate(txtDataValidade.Text) Then
                If Not CDate(txtDataValidade.Text) >= CDate(dt.Rows(0)("Emissao")).AddDays(90) Then
                    btnProrrogar.Visible = True
                End If
            End If
        End If

        ViewState.Add("DataValidade", txtDataValidade.Text)
        txtContato.Text = dr("Contato").ToString.Trim
        txtCliente.Codigo = dr("CodigoCliente").ToString
        txtVendedor.Codigo = dr("CodigoVendedor").ToString
        txtCliente.Loja = dr("LojaCliente").ToString
        txtCliente.Nome = dr("NomeCliente").ToString
        txtCliente.CPF_CNPJ = dr("CPFCNPJCliente").ToString
        txtVendedor.Nome = dr("NomeVendedor").ToString
        oCondicaoPagamento.Codigo = dr("CondicaoPagamento").ToString
        oCondicaoPagamento.Nome = dr("DescricaoCondicao").ToString
        oCondicaoPagamento.Selecionar(oCondicaoPagamento.Codigo)
        '============================================* IMPORTANTE *==============================================
        'Ocorrência: Surgimento de um caracter especial no campo referente a detalhe da condição de pagamento e descrição do produto apresentado no final de seu conteúdo 
        'quando o navegador utilizado pelo usuário é o Mozilla Firefox ou Google Chrome, 
        'Causa: Devido a presença do caracter especial, qualquer ação do usuário poderia causar um erro na página.
        'Solução: Remoção do caracter especial.
        Dim detalheCondicao As String = dr("DetalheCondicaoPagamento").ToString
        If detalheCondicao.Length > 0 Then
            txtDetalheCodicao1.Text = detalheCondicao.Substring(0, detalheCondicao.Length - 1)
        Else
            txtDetalheCodicao1.Text = detalheCondicao
        End If
        drpPossibilidadeVenda.SelectedValue = dr("PossibilidadeVenda").ToString.Trim

        If dr("TipoOrcamento").ToString.Trim.Length > 0 Then
            drpTipoOrcamento.SelectedValue = dr("TipoOrcamento").ToString.Trim
        End If
        If dr("TipoFrete").ToString.Trim.Length > 0 Then
            radTipoFrete.SelectedValue = dr("TipoFrete").ToString.Trim
        End If

        txtObservacao.Text = dr("Observacao").ToString.Replace(Chr(0), "")
        oStatusProposta.DescricaoPersonalizada = dr("DescricaoPersonalizadaStatus").ToString
        oStatusProposta.CodigoPersonalizado = dr("CodigoPersonalizadoStatus").ToString
        oStatusProposta.Codigo = dr("CodigoStatus").ToString
        oStatusProposta.Descricao = dr("DescricaoStatus").ToString

        If dr("CodigoStatus").ToString = "D" Or dr("CodigoStatus").ToString = "" Then
            btnSalvar.Visible = True
            btnAlterar.Visible = False
        Else
            btnSalvar.Visible = False
            btnImprimir.Visible = True
            btnAprovar.Visible = False
        End If

        If dr("CodigoStatus").ToString <> "B" And dr("CodigoStatus").ToString <> "C" And dr("CodigoStatus").ToString.Trim <> "" Then
            btnImprimir.Visible = True
            btnAprovar.Visible = True
        Else
            btnImprimir.Visible = False
            btnAprovar.Visible = False
        End If

        grdItens.DataSource = dt
        grdItens.DataBind()
        ViewState.Add("Proposta", dt)

    End Sub

    Private Sub Recarregar(ByVal sNumero As String)
        SelecionarProposta(sNumero)
    End Sub

    Private Function ObterEstrutura(Optional ByVal bAdicionarLinha As Boolean = True) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("NUMERO")
        dt.Columns.Add("FATURADOPOR")
        dt.Columns.Add("CODIGOVENDEDOR")
        dt.Columns.Add("NOMEVENDEDOR")
        dt.Columns.Add("CODIGOCLIENTE")
        dt.Columns.Add("LOJACLIENTE")
        dt.Columns.Add("NOMECLIENTE")
        dt.Columns.Add("CPFCNPJCLIENTE")
        dt.Columns.Add("CONTATO")
        dt.Columns.Add("EMISSAO", GetType(DateTime))
        dt.Columns.Add("DATAVALIDADE", GetType(DateTime))
        dt.Columns.Add("CODIGOSTATUS")
        dt.Columns.Add("DESCRICAOSTATUS")
        dt.Columns.Add("CODIGOPERSONALIZADOSTATUS")
        dt.Columns.Add("DESCRICAOPERSONALIZADASTATUS")
        dt.Columns.Add("ITEM")
        dt.Columns.Add("CodigoClassificacao")
        dt.Columns.Add("CODIGOPRODUTO")
        dt.Columns.Add("NOMEPRODUTO")
        dt.Columns.Add("DESCRICAO")
        dt.Columns.Add("PRECOUNITARIO", GetType(Decimal))
        dt.Columns.Add("VALOR", GetType(Decimal))
        dt.Columns.Add("PERCENTUALDESCONTO", GetType(Decimal))
        dt.Columns.Add("VALORDESCONTO", GetType(Decimal))
        dt.Columns.Add("PRECOLISTA", GetType(Decimal))
        dt.Columns.Add("QUANTIDADE", GetType(Integer))
        dt.Columns.Add("PRAZOENTREGA", GetType(Integer))
        dt.Columns.Add("PRAZOGARANTIA", GetType(Integer))
        'dt.Columns.Add("ENTREGA", GetType(DateTime))
        dt.Columns.Add("OrcamentoAnterior", GetType(Boolean))
        dt.Columns.Add("PossibilidadeVenda")
        dt.Columns.Add("TipoOrcamento")
        dt.Columns.Add("CondicaoPagamento")
        dt.Columns.Add("DescricaoCondicao")
        dt.Columns.Add("DetalheCondicaoPagamento")
        dt.Columns.Add("TipoFrete")
        dt.Columns.Add("Observacao")
        If bAdicionarLinha Then
            AdicionarItem(dt)
        End If
        Return dt
    End Function

    Private Sub AdicionarItem(ByRef dt As DataTable, Optional ByVal nIndice As Integer = 0)
        Dim nItem As Integer = 0
        Dim dr As DataRow = dt.NewRow
        If nIndice = 0 Then
            dr("ITEM") = "01"
        Else
            dr("ITEM") = ObterIndice()
        End If
        ViewState.Add("Item", dr("ITEM").ToString)
        dr("NUMERO") = "000000"
        dr("FATURADOPOR") = ""
        dr("CODIGOVENDEDOR") = ""
        dr("NOMEVENDEDOR") = ""
        dr("CODIGOCLIENTE") = ""
        dr("LOJACLIENTE") = ""
        dr("NOMECLIENTE") = ""
        dr("CPFCNPJCLIENTE") = ""
        dr("CONTATO") = ""
        dr("EMISSAO") = Now.Date.ToString("dd/MM/yyyy")
        dr("CodigoStatus") = ""
        dr("DescricaoStatus") = "Ativo"
        dr("CodigoPersonalizadoStatus") = ""
        dr("DescricaoPersonalizadaStatus") = "EM ELABORAÇÃO"
        dr("CodigoClassificacao") = ""
        dr("CODIGOPRODUTO") = ""
        dr("NOMEPRODUTO") = ""
        dr("DESCRICAO") = ""
        dr("PRECOUNITARIO") = 0.0
        dr("VALOR") = 0.0
        dr("PERCENTUALDESCONTO") = 0.0
        dr("VALORDESCONTO") = 0.0
        dr("PRECOLISTA") = 0.0
        dr("QUANTIDADE") = 0
        'INCLUIR CAMPO NA PROCEDURE DE SELECAO
        dr("PRAZOENTREGA") = 0
        dr("PRAZOGARANTIA") = 0
        'dr("ENTREGA") = System.DBNull.Value
        dr("OrcamentoAnterior") = False
        dr("TipoOrcamento") = ""
        dr("TipoFrete") = "1"
        dr("CondicaoPagamento") = ""
        dr("DescricaoCondicao") = ""
        dr("DetalheCondicaoPagamento") = ""
        dr("Observacao") = ""
        Dim cn As New Vendedor.ctlVendedor
        Dim dt2 = cn.SelecionarUsuarioID()
        If dt2.Rows.Count > 0 Then
            dr("CodigoVendedor") = dt2.Rows(0)("Codigo").ToString
            dr("NomeVendedor") = dt2.Rows(0)("Nome").ToString
        Else
            dr("CodigoVendedor") = ""
            dr("NomeVendedor") = ""
        End If
        If nIndice = 0 Then
            dt.Rows.Add(dr)
        Else
            dt.Rows.InsertAt(dr, nIndice)
        End If
    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
        If e.CommandName = "Busca" Or e.CommandName = "" Then
            '
        Else
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "New"
                    nIndice = nIndice + 1
                    AdicionarItem(dt, nIndice)
                    grdItens.EditIndex = nIndice
                    grdItens.SelectedIndex = nIndice
                Case "Edit"
                    grdItens.EditIndex = nIndice
                    grdItens.SelectedIndex = nIndice
                Case "Update"
                    AtualizarItem(grdItens.Rows(nIndice), nIndice)
                    grdItens.EditIndex = -1
                Case "Cancel"
                    'se for alteracao, basta cacelar. se for inclusao, eh necessário remover a linha
                    grdItens.EditIndex = -1
                Case "Delete"
                    dt.Rows.RemoveAt(nIndice)
                    If dt.Rows.Count = 0 Then
                        AdicionarItem(dt)
                    End If
            End Select
            ViewState.Add("Proposta", dt)
            grdItens.DataSource = dt
            grdItens.DataBind()
        End If
    End Sub

    Private Sub AtualizarItem(ByVal gr As GridViewRow, ByVal nIndice As Integer)
        Dim sItem As String = DirectCast(gr.FindControl("lblItem"), Label).Text.Trim
        Dim sCodigoClassificacao As String = DirectCast(gr.FindControl("lblCodigoClassificacao"), Label).Text.Trim
        Dim sCodigoProduto As String = ""
        Dim sNomeProduto As String = ""
        Dim SDescricao As String = ""
        Dim sQuantidade As String = ""
        Dim sPrecoUnitario As String = ""
        Dim sValorTotal As String = ""
        Dim sPrecoLista As String = ""
        Dim nPrazoEntrega As Integer = 0
        Dim nPrazoGarantia As Integer = 0
        Dim oProduto As ProdutoView = DirectCast(gr.FindControl("oProduto"), ProdutoView)
        Dim oDescricao As DetalheProduto = DirectCast(gr.FindControl("oDetalheProduto"), DetalheProduto)
        SDescricao = oDescricao.Descricao.Trim
        sCodigoProduto = oProduto.Codigo.Trim
        sNomeProduto = oProduto.Nome.Trim
        oProduto.HabilitarEdicao = False
        sQuantidade = DirectCast(gr.FindControl("txtQuantidade"), NumberBox).Text.Trim
        sPrecoUnitario = DirectCast(gr.FindControl("txtPrecoUnitario"), NumberBox).Text.Trim
        sPrecoLista = DirectCast(gr.FindControl("lblPrecoLista"), Label).Text.Trim
        nPrazoEntrega = CInt(DirectCast(gr.FindControl("lblPrazoEntrega"), Label).Text.Trim)
        If IsNumeric(sQuantidade) And IsNumeric(sPrecoUnitario) Then
            sValorTotal = (Integer.Parse(sQuantidade) * Decimal.Parse(sPrecoUnitario)).ToString.Trim
        End If
        Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
        Dim dr As DataRow = dt.Rows(nIndice)
        dr("Item") = sItem
        dr("CodigoClassificacao") = sCodigoClassificacao
        dr("CodigoProduto") = sCodigoProduto
        dr("NomeProduto") = sNomeProduto
        dr("Descricao") = SDescricao
        If Not String.IsNullOrEmpty(sPrecoLista) AndAlso IsNumeric(sPrecoLista) Then
            dr("PrecoLista") = sPrecoLista
        End If
        dr("Quantidade") = sQuantidade
        If Not String.IsNullOrEmpty(sPrecoUnitario) AndAlso IsNumeric(sPrecoUnitario) Then
            dr("PrecoUnitario") = sPrecoUnitario
        End If
        If Not String.IsNullOrEmpty(sValorTotal) AndAlso IsNumeric(sValorTotal) Then
            dr("Valor") = sValorTotal
        End If
        dr("PrazoEntrega") = nPrazoEntrega
        If sCodigoClassificacao = "E" Then
            dr("PrazoGarantia") = "12"
        Else
            dr("PrazoGarantia") = "0"
        End If
        dt.AcceptChanges()
    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting
    End Sub

    Private Sub grdItens_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdItens.RowEditing
    End Sub

    Private Sub grdItens_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdItens.RowUpdating
    End Sub

    Private Sub grdItens_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdItens.RowCancelingEdit
    End Sub

    Protected Sub oProduto_SelecionarOnClick(ByVal obj As Object)
        Dim gr As GridViewRow = grdItens.SelectedRow
        Dim lblCodigoClassificacao = DirectCast(gr.FindControl("lblCodigoClassificacao"), Label)
        If DirectCast(gr.FindControl("txtPrazoGarantia"), NumberBox) IsNot Nothing Then
            lblCodigoClassificacao.Text = DirectCast(obj, DataRow)("Class1").ToString
            If DirectCast(obj, DataRow)("Class1").ToString = "A" Then
                Dim txtPrazoGarantia As NumberBox = DirectCast(gr.FindControl("txtPrazoGarantia"), NumberBox)
                txtPrazoGarantia.Text = "2"
                txtPrazoGarantia.Enabled = False
            End If
        End If
        Dim oPrecoLista As Label = DirectCast(gr.FindControl("lblPrecoLista"), Label)
        oPrecoLista.Text = DirectCast(obj, DataRow)("PrecoVenda1").ToString
        Dim oPrazoEntrega As Label = DirectCast(gr.FindControl("lblPrazoEntrega"), Label)
        oPrazoEntrega.Text = DirectCast(obj, DataRow)("PrazoEntrega").ToString
        Dim oPrecoUnitario As NumberBox = DirectCast(gr.FindControl("txtPrecoUnitario"), NumberBox)
        oPrecoUnitario.Text = DirectCast(obj, DataRow)("PrecoVenda1").ToString
        If IsNumeric(DirectCast(gr.FindControl("txtQuantidade"), NumberBox).Text) And IsNumeric(oPrecoUnitario.Text) Then
            DirectCast(gr.FindControl("lblValorTotal"), Label).Text = (Integer.Parse(DirectCast(gr.FindControl("txtQuantidade"), NumberBox).Text) * Decimal.Parse(oPrecoUnitario.Text)).ToString
        End If
        Dim oProduto As ProdutoView = DirectCast(gr.FindControl("oProduto"), ProdutoView)
        Dim oDetalheProduto As DetalheProduto = DirectCast(gr.FindControl("oDetalheProduto"), DetalheProduto)
        Dim cn As New ctlProduto
        Dim dt As DataTable = cn.Selecionar(oProduto.Codigo)
        If dt.Rows.Count > 0 Then
            oDetalheProduto.Descricao = dt.Rows(0)("Descricao").ToString
        End If
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim oRet As New RetornoGenerico
        Try

            oRet = GravarProposta("I")

        Catch ex As Exception
            oRet.Sucesso = False
            Util.EscreverLogErro("Proposta - btnSalvar_Click: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Sub btnAlterar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlterar.Click
        Dim oRet As New RetornoGenerico
        Try

            oRet = GravarProposta("A")

        Catch ex As Exception
            oRet.Sucesso = False
            Util.EscreverLogErro("Proposta - btnAlterar_Click: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Sub btnAprovar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprovar.Click
        Dim oRet As New RetornoGenerico
        Try
            oRet = GravarProposta("AP")

        Catch ex As Exception
            oRet.Sucesso = False
            Util.EscreverLogErro("Proposta - btnAprovar_Click: " & ex.Message())
            oRet.Mensagem = Util.sMsgErroPadrao
        End Try
        oMensagem.SetMessage(oRet)
    End Sub

    Private Function GravarProposta(ByVal sTipoGravacao As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim ct As New ctlProposta
        Dim sNumero As String = lblNumero.Text

        If oStatusProposta.Codigo = "D" And btnSalvar.Text = "Desativar" And sTipoGravacao = "I" Then
            'esta desativando
            oRet = ct.Desativar(sNumero)
            If oRet.Sucesso Then
                Recarregar(sNumero)
            End If
        Else
            oRet = Validar()

            If oRet.Sucesso Then

                Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
                Dim nValor As Decimal = 0
                Dim oProposta1 As New Proposta1.Proposta

                CriarXmlComItensOS(dt)

                For Each dr As DataRow In dt.Rows
                    nValor += Decimal.Parse(dr("Valor").ToString)
                Next

                oProposta1.CodigoCliente = txtCliente.Codigo
                oProposta1.LojaCliente = txtCliente.Loja
                oProposta1.Contato = txtContato.Text
                oProposta1.CondicaoPagamento = oCondicaoPagamento.Codigo
                oProposta1.PossibilidadeVenda = drpPossibilidadeVenda.SelectedValue
                oProposta1.TipoOrcamento = drpTipoOrcamento.SelectedValue
                oProposta1.TipoFrete = radTipoFrete.SelectedValue
                oProposta1.Observacao = txtObservacao.Text()
                oProposta1.DetalheCondicao = txtDetalheCodicao1.Text.Trim
                oProposta1.DataValidade = txtDataValidade.GetDate
                oProposta1.FaturadoPor = hdnFaturadoPor.Value
                oProposta1.CodVendedor = txtVendedor.Codigo
                If sTipoGravacao <> "I" Then
                    oProposta1.MotivoProrrogacao = ddlProrrogacao.SelectedValue 'alteracao
                    oProposta1.Numero = sNumero 'alteracao
                End If

                oProposta1.PutItens(dt)

                Select Case sTipoGravacao
                    Case "I"
                        oRet = ct.Incluir(oProposta1)
                    Case "A"
                        oRet = ct.Alterar(oProposta1)
                    Case "AP"
                        oRet = ct.Aprovar(oProposta1)
                End Select

                If oRet.Sucesso Then
                    DeletaArquivoXml()
                    Recarregar(oRet.Chave.Substring(2))
                End If
            End If
        End If

        Return oRet

    End Function

    Private Function Validar() As RetornoGenerico
        Dim oRet As New RetornoGenerico
        oRet.Sucesso = False
        Dim nPrazoMaximo As Integer = CInt(Util.WebGetMV("MVW_MAXVAL")) '90 dias
        If Not txtDataValidade.IsValid Then
            oRet.Mensagem = "Forneça uma Data de Validade válida."
            Return oRet
        ElseIf CDate(txtDataValidade.Text) < Now Then
            oRet.Mensagem = "A Data de Validade está vencida." 'deve ser posterior ao dia de hoje."
            Return oRet
        ElseIf txtDataValidade.GetDate > CDate(lblEmissao.Text).AddDays(nPrazoMaximo) Then
            oRet.Mensagem = "A Data de Validade deve ser de até '" + nPrazoMaximo.ToString("00") + "' dias a contar da emissão da Proposta."
            Return oRet
        ElseIf btnAlterar.Visible = True Then
            Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
            If Not IsDBNull(dt.Rows(0)("DataValidade")) Then
                If ddlProrrogacao.SelectedValue = "0" AndAlso CDate(txtDataValidade.Text) <> CDate(dt.Rows(0)("DataValidade")) Then
                    oRet.Mensagem = "Selecione o Motivo de Prorrogação"
                    Return oRet
                End If
            End If
        End If

        If drpTipoOrcamento.SelectedIndex < 1 Then
            oRet.Mensagem = "Selecione um Tipo de Orçamento válido."
        ElseIf drpPossibilidadeVenda.SelectedIndex < 1 Then
            oRet.Mensagem = "Selecione uma Possibilidade de venda válida."
        ElseIf Not oCondicaoPagamento.IsValid Then
            oRet.Mensagem = "Selecione uma Condição de Pagamento."
        ElseIf CInt(oCondicaoPagamento.Codigo) = 0 AndAlso String.IsNullOrEmpty(txtDetalheCodicao1.Text.Trim) Then
            oRet.Mensagem = "Informe o detalhe do pagamento"
        ElseIf Not txtCliente.IsValid Then
            oRet.Mensagem = "Selecione um Cliente."
        ElseIf txtContato.Text.Trim.Length < 1 Then
            oRet.Mensagem = "Forneça o nome do Contato."
        ElseIf Not ValidarItens(oRet) Then
            '
        Else
            oRet.Sucesso = True
        End If
        Return oRet
    End Function

    Private Function ValidarItens(ByVal oRet As RetornoGenerico) As Boolean

        Dim bRet As Boolean = False
        Dim lblCodigoClassificacao As Label
        Dim oQuantidade As Label
        Dim oPrecoUnitario As Label
        Dim oProduto As ProdutoView
        Dim lblPrazoEntrega As Label
        Dim oPrazoGarantia As Label
        Dim oPrecoLista As Label

        Dim nPercentualMinimo As Decimal = CDec(Util.WebGetMV("MVW_PCMINO")) / 100 'ao menos 60% sobre o seu valor
        Dim nPercentualMaximo As Decimal = CDec(Util.WebGetMV("MVW_PCMAXO")) / 100 'máximo de 1% sobre o seu valor
        Dim nPrecoLista As Decimal = 0
        Dim nPrecoVenda As Decimal = 0
        Dim nPrecoMaximo As Decimal = 0
        Dim nPrecoMinimo As Decimal = 0

        oRet.Mensagem = ""
        If grdItens.EditIndex > -1 Then
            oRet.Mensagem = "A linha " + (grdItens.EditIndex + 1).ToString + " encontra-se em edição e precisa ser confirmada ou cancelada."
        Else
            For Each gr As GridViewRow In grdItens.Rows
                lblCodigoClassificacao = DirectCast(gr.FindControl("lblCodigoClassificacao"), Label)
                oProduto = DirectCast(gr.FindControl("oProduto"), ProdutoView)
                oQuantidade = DirectCast(gr.FindControl("lblQuantidade"), Label)
                oPrecoUnitario = DirectCast(gr.FindControl("lblPrecoUnitario"), Label)
                lblPrazoEntrega = DirectCast(gr.FindControl("lblPrazoEntrega"), Label)
                oPrazoGarantia = DirectCast(gr.FindControl("lblPrazoGarantia"), Label)
                oPrecoLista = DirectCast(gr.FindControl("lblPrecoLista"), Label)
                nPrecoLista = CDec(oPrecoLista.Text)
                nPrecoVenda = CDec(oPrecoUnitario.Text)
                nPrecoMaximo = nPrecoLista * nPercentualMaximo
                nPrecoMinimo = nPrecoLista * nPercentualMinimo
                If Not oProduto.IsValid Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Produto válido. Favor informar um Produto."
                ElseIf lblPrazoEntrega.Text.Trim.Length = 0 Or lblPrazoEntrega.Text.Trim = "0" Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Prazo de Entrega válido. Favor selecionar um Prazo de Entrega."
                ElseIf oPrazoGarantia.Text.Trim.Length = 0 Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Prazo de Garantia. Favor informar um Prazo de Garantia."
                ElseIf CInt(oPrazoGarantia.Text) > 24 Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " está com o Prazo de Garantia acima de 24 meses. Favor informe um Prazo de Garantia válido."
                ElseIf oQuantidade.Text.Trim.Length = 0 Or (Not IsNumeric(oQuantidade.Text)) Or Integer.Parse(oQuantidade.Text) = 0 Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém uma Quantidade válida. Favor informar uma Quantidade."
                ElseIf oPrecoUnitario.Text.Trim.Length = 0 Or (Not IsNumeric(oPrecoUnitario.Text)) Or Decimal.Parse(oPrecoUnitario.Text) = 0 Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " não contém um Preço de Venda válido. Favor informar um Preço Unitário."
                ElseIf nPrecoLista > 0 And nPrecoVenda < nPrecoMinimo Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " está com o preço abaixo do mínimo permitido. Favor Verificar"
                ElseIf nPrecoLista > 0 And nPrecoVenda > nPrecoMaximo Then
                    oRet.Mensagem = "A linha " + (gr.RowIndex + 1).ToString + " está com o preço acima do preço lista do produto. Favor Verificar"
                End If
                If oRet.Mensagem.Trim.Length > 0 Then
                    Exit For
                End If
            Next
        End If

        If oRet.Mensagem.Trim.Length = 0 Then
            bRet = True
        End If

        Return bRet

    End Function

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound

        Dim nColunaTotal As Integer = e.Row.Cells.Count - 1

        If e.Row.RowType = DataControlRowType.DataRow Then

            nPrecoTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Valor"))
            e.Row.Cells(nColunaTotal).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

            Dim lblItem As Label = DirectCast(e.Row.FindControl("lblItem"), Label)
            Dim lblQuantidade As Label = DirectCast(e.Row.FindControl("lblQuantidade"), Label)
            Dim lblPrecoUnitario As Label = DirectCast(e.Row.FindControl("lblPrecoUnitario"), Label)
            Dim lblValorTotal As Label = DirectCast(e.Row.FindControl("lblValorTotal"), Label)
            Dim lblPrecoLista As Label = DirectCast(e.Row.FindControl("lblPrecoLista"), Label)
            Dim drpPrazoEntrega As DropDownList = DirectCast(e.Row.FindControl("drpPrazoEntrega"), DropDownList)
            Dim lblPrazoEntrega As Label = DirectCast(e.Row.FindControl("lblPrazoEntrega"), Label)

            ViewState.Add("Item", lblItem.Text.Trim)

            If DirectCast(e.Row.FindControl("txtPrazoGarantia"), NumberBox) IsNot Nothing Then
                Dim lblCodigoClassificacao As Label = DirectCast(e.Row.FindControl("lblCodigoClassificacao"), Label)
                If lblCodigoClassificacao.Text.Trim = "A" Then
                    DirectCast(e.Row.FindControl("txtPrazoGarantia"), NumberBox).Enabled = False
                End If
            End If

            If DirectCast(e.Row.FindControl("txtQuantidade"), NumberBox) IsNot Nothing Then
                Dim oQuantidade As NumberBox = DirectCast(e.Row.FindControl("txtQuantidade"), NumberBox)
                Dim oPrecoUnitario As NumberBox = DirectCast(e.Row.FindControl("txtPrecoUnitario"), NumberBox)
                lblPrecoLista.Text = CDec(lblPrecoLista.Text).ToString
                lblValorTotal.Text = CDec(lblValorTotal.Text).ToString
                oQuantidade.Attributes.Add("onchange", "javascript:fnCalcularTotal('" + oQuantidade.ClientIDNumber + "', '" + lblPrecoLista.ClientID + "', '', '', '" + oPrecoUnitario.ClientIDNumber + "', '" + lblValorTotal.ClientID + "', '');")
                oPrecoUnitario.Attributes.Add("onchange", "javascript:fnCalcularTotal('" + oQuantidade.ClientIDNumber + "', '" + lblPrecoLista.ClientID + "', '', '', '" + oPrecoUnitario.ClientIDNumber + "', '" + lblValorTotal.ClientID + "', '');")
            End If

            If Not lblPrazoEntrega Is Nothing Then
                If IsNumeric(lblPrazoEntrega) Then
                    If CDec(lblPrazoEntrega.Text) < 0 Then
                        lblPrazoEntrega.Text = "N/A"
                    End If
                End If
                lblPrazoEntrega.Text = lblPrazoEntrega.Text
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(8).Text = "Total: "
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(nColunaTotal).Text = FormatCurrency(nPrecoTotal).ToString
            e.Row.Cells(nColunaTotal).HorizontalAlign = HorizontalAlign.Right
            lblTotalProposta.Text = FormatCurrency(nPrecoTotal).ToString
        End If

    End Sub

    Private Sub txtDataValidade_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataValidade.TextChanged
        If ViewState("DataValidade").ToString <> "" Then
            If IsDate(ViewState("DataValidade").ToString) Then
                If CDate(ViewState("DataValidade").ToString) < txtDataValidade.GetDate Then
                    pnlMotivoProrrogacao.Visible = True
                Else
                    pnlMotivoProrrogacao.Visible = False
                End If
            Else
                pnlMotivoProrrogacao.Visible = False
            End If
        Else
            pnlMotivoProrrogacao.Visible = False
        End If
    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Private Sub btnProrrogar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProrrogar.Click
        Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
        If CDate(dt.Rows(0)("DataValidade")).AddDays(30) >= CDate(dt.Rows(0)("Emissao")).AddDays(90) Then
            txtDataValidade.Text = CDate(dt.Rows(0)("Emissao")).AddDays(90).ToString
        Else
            txtDataValidade.Text = Date.Now.AddDays(30).ToString
        End If
        pnlMotivoProrrogacao.Visible = True
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Response.Redirect("RptProposta.aspx?Numero=" + lblNumero.Text.Trim)
    End Sub

    Private Function ObterIndice() As String
        Dim indice As Integer = -1
        Dim dt As DataTable = DirectCast(ViewState("Proposta"), DataTable)
        For Each dr As DataRow In dt.Rows
            If CInt(dr("Item")) > indice Then
                indice = CInt(dr("Item"))
            End If
        Next
        indice += 1
        If indice >= 10 Then
            Return indice.ToString
        Else
            Return "0" + indice.ToString
        End If
    End Function

    Private Sub CriarXmlComItensOS(ByVal dt As DataTable)

        Dim writer As New XmlTextWriter(NomeArquivoXml(), Nothing)

        'inicia o documento xml
        writer.WriteStartDocument()

        'define a indentação do arquivo
        writer.Formatting = Formatting.Indented

        'escreve um comentario
        writer.WriteComment("Cabecalho e itens da proposta")

        'escreve o elmento raiz
        writer.WriteStartElement("Proposta")

        For Each dr As DataRow In dt.Rows
            writer.WriteStartElement("itens")

            If CInt(lblNumero.Text) = 0 Then
                writer.WriteElementString("Atendente", "")
            Else
                writer.WriteElementString("Atendente", HttpContext.Current.User.Identity.Name)
            End If
            writer.WriteElementString("Licenciado", HttpContext.Current.User.Identity.Name)

            'Proposta
            writer.WriteElementString("NUMERO", lblNumero.Text)
            writer.WriteElementString("FATURADOPOR", hdnFaturadoPor.Value)
            writer.WriteElementString("CODIGOVENDEDOR", txtVendedor.Codigo)
            writer.WriteElementString("NOMEVENDEDOR", txtVendedor.Nome)
            writer.WriteElementString("CODIGOCLIENTE", txtCliente.Codigo)
            writer.WriteElementString("LOJACLIENTE", txtCliente.Loja)
            writer.WriteElementString("NOMECLIENTE", txtCliente.Nome)
            writer.WriteElementString("CPFCNPJCLIENTE", txtCliente.CPF_CNPJ)
            writer.WriteElementString("CONTATO", txtContato.Text)
            writer.WriteElementString("EMISSAO", lblEmissao.Text)
            writer.WriteElementString("DATAVALIDADE", txtDataValidade.GetDate.ToString("dd/MM/yyyy"))
            writer.WriteElementString("CODIGOSTATUS", oStatusProposta.Codigo)
            writer.WriteElementString("DESCRICAOSTATUS", oStatusProposta.Descricao)
            writer.WriteElementString("CODIGOPERSONALIZADOSTATUS", oStatusProposta.CodigoPersonalizado)
            writer.WriteElementString("DESCRICAOPERSONALIZADASTATUS", oStatusProposta.DescricaoPersonalizada)


            writer.WriteElementString("ITEM", dr.Item("Item").ToString)
            writer.WriteElementString("CodigoClassificacao", dr.Item("CodigoClassificacao").ToString)
            writer.WriteElementString("CODIGOPRODUTO", dr.Item("CodigoProduto").ToString)
            writer.WriteElementString("NOMEPRODUTO", dr.Item("NomeProduto").ToString)
            writer.WriteElementString("DESCRICAO", "")
            writer.WriteElementString("PRECOUNITARIO", dr.Item("PrecoUnitario").ToString)
            writer.WriteElementString("VALOR", dr.Item("Valor").ToString)
            writer.WriteElementString("PERCENTUALDESCONTO", "0.00")
            writer.WriteElementString("VALORDESCONTO", "0.00")
            writer.WriteElementString("PRECOLISTA", dr.Item("PrecoLista").ToString)
            writer.WriteElementString("QUANTIDADE", dr.Item("Quantidade").ToString)
            writer.WriteElementString("PRAZOENTREGA", dr.Item("PrazoEntrega").ToString)
            writer.WriteElementString("PRAZOGARANTIA", dr.Item("PrazoGarantia").ToString)
            'writer.WriteElementString("ENTREGA", "")



            writer.WriteElementString("OrcamentoAnterior", "false")
            writer.WriteElementString("PossibilidadeVenda", drpPossibilidadeVenda.SelectedValue)
            writer.WriteElementString("TipoOrcamento", drpTipoOrcamento.SelectedValue)
            writer.WriteElementString("CondicaoPagamento", oCondicaoPagamento.Codigo)
            writer.WriteElementString("DescricaoCondicao", oCondicaoPagamento.Detalhe)
            writer.WriteElementString("DetalheCondicaoPagamento", txtDetalheCodicao1.Text.Trim)
            writer.WriteElementString("TipoFrete", radTipoFrete.SelectedValue)
            writer.WriteElementString("Observacao", txtObservacao.Text)

            writer.WriteEndElement()
        Next

        ' encerra o elemento raiz
        writer.WriteFullEndElement()

        'Escreve o XML para o arquivo e fecha o objeto escritor
        writer.Close()
    End Sub

    ' Retorna o nome e caminho do arquivo xml
    Private Function NomeArquivoXml() As String

        Dim sNomeArquivo As String = Server.MapPath("~") & "\arquivos_xml_erros_orcamento"

        If Not My.Computer.FileSystem.DirectoryExists(sNomeArquivo) Then
            Directory.CreateDirectory(sNomeArquivo)
        End If

        Return sNomeArquivo & "\proposta_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    Private Sub DeletaArquivoXml()
        If File.Exists(NomeArquivoXml()) Then
            File.Delete(NomeArquivoXml())
        End If
    End Sub

End Class