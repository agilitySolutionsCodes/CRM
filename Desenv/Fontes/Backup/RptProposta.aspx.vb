Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class RptProposta
    Inherits System.Web.UI.Page

    Private nTotal As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim sNumero As String = Request("Numero")
            Dim sCNPJ As String = ""
            Dim cn As New Vendedor.ctlVendedor

            If HttpContext.Current.User.Identity.Name.Trim.Length > 0 Then

                Dim reader As SqlDataReader = cn.SelecionarUsuarioID(DirectCast(Session("Usuario2"), Dados.Usuario).UserCode)

                If reader.HasRows Then

                    reader.Read()

                    'Cabeçalho
                    sCNPJ = reader("CPFCNPJ").ToString.Trim
                    If Not String.IsNullOrEmpty(sCNPJ) Then
                        If sCNPJ.Length = 14 Then
                            lblCNPJ.Text = "CNPJ: " + Convert.ToDouble(sCNPJ).ToString("00\.000\.000\/0000\-00")
                        ElseIf sCNPJ.Length = 11 Then
                            Dim val As New Genericas.Validadores
                            If val.isCPF(sCNPJ) Then
                                lblCNPJ.Text = "CPF: " + Convert.ToDouble(sCNPJ).ToString("000\.000\.000\-00")
                            Else
                                lblCNPJ.Text = "CNPJ: " + sCNPJ
                            End If
                        Else
                            lblCNPJ.Text = "CNPJ: " + sCNPJ
                        End If
                    End If
                End If

                'lblInscricaoEstadual.Text = reader("InscricaoEstadual").ToString.Trim

                'Rodapé
                lblRazaoSocial.Text = reader("NOME").ToString
                Dim texto As String = ""
                Dim endereco As String = reader("Endereco").ToString.Trim()
                Dim cep As String = reader("CEP").ToString.Trim
                Dim telefone As String = reader("Telefone").ToString.Trim
                Dim fax As String = reader("Fax").ToString.Trim
                Dim cidade As String = reader("Cidade").ToString.Trim
                Dim uf As String = reader("UF").ToString.Trim
                If Not String.IsNullOrEmpty(endereco) Then
                    texto += endereco
                End If
                If Not String.IsNullOrEmpty(cep) Then
                    If cep <> "-" Then
                        texto += " - CEP: " + cep
                    End If
                End If
                If Not String.IsNullOrEmpty(telefone) Then
                    texto += " - Tel. " + telefone
                End If
                If Not String.IsNullOrEmpty(fax) Then
                    texto += " - Fax " + fax
                End If
                If Not String.IsNullOrEmpty(cidade) Then
                    texto += " - " + cidade
                End If
                If Not String.IsNullOrEmpty(uf) Then
                    texto += " - " + uf
                End If
                lblRodape1.Text = texto
                lblLocal.Text = cidade

                Dim sArquivo As String = "logos/" + sCNPJ.ToString.Trim + ".jpg"
                If Util.ExisteArquivo(Page, sArquivo) Then
                    imgLogotipo.ImageUrl = "logos/" + sCNPJ.ToString.Trim + ".jpg"
                Else
                    Dim nCNPJ As Double = Nothing
                    If Not String.IsNullOrEmpty(sCNPJ) AndAlso IsNumeric(sCNPJ) Then
                        nCNPJ = CDbl(sCNPJ)
                        sArquivo = "logos/" + nCNPJ.ToString + ".jpg"
                        If Util.ExisteArquivo(Page, sArquivo) Then
                            imgLogotipo.ImageUrl = "logos/" + nCNPJ.ToString.Trim + ".jpg"
                        Else
                            imgLogotipo.Visible = False
                        End If
                    Else
                        imgLogotipo.Visible = False
                    End If
                End If

                'Assinatura
                If Not String.IsNullOrEmpty(reader("NOME").ToString.Trim) Then
                    lblRazaoSocial2.Text = reader("NOME").ToString
                    lblRazaoSocial2.Visible = True
                End If
                If Not String.IsNullOrEmpty(telefone) Then
                    lblTelefone2.Text = telefone
                    lblTel.Visible = True
                End If
                If Not String.IsNullOrEmpty(fax) Then
                    lblFax2.Text = fax
                    lblTFax2.Visible = True
                End If
                lblEmail.Text = reader("Email").ToString

                reader.Close()

            End If

            Dim ct As New ctlProposta
            Dim dt As DataTable = ct.Selecionar(sNumero)

            Dim dr As DataRow = dt.Rows(0)
            lblProposta.Text = dr("Numero").ToString
            lblDataEmissao.Text = DateTime.Parse(dr("EMISSAO").ToString).ToString("d ' de 'MMMM' de 'yyyy")
            lblNomeCliente.Text = dr("RazaoSocial").ToString.Trim
            Dim cpfcnpj As String = dr("CpfCnpjCliente").ToString.Trim
            If Not String.IsNullOrEmpty(cpfcnpj) Then
                If cpfcnpj.Length = 14 Then
                    lblCnpjCliente.Text = "CNPJ: " + Convert.ToDouble(cpfcnpj).ToString("00\.000\.000\/0000\-00")
                ElseIf cpfcnpj.Length = 11 Then
                    Dim val As New Genericas.Validadores
                    If val.isCPF(cpfcnpj) Then
                        lblCnpjCliente.Text = "CPF: " + Convert.ToDouble(cpfcnpj).ToString("000\.000\.000\-00")
                    Else
                        lblCnpjCliente.Text = "CNPJ: " + cpfcnpj
                    End If
                Else
                    lblCnpjCliente.Text = "CNPJ: " + cpfcnpj
                End If
            End If
            lblEnderecoCliente.Text = dr("EnderecoCliente").ToString().Trim
            lblMunicipioCliente.Text = dr("MunicipioCliente").ToString().Trim
            lblUFCliente.Text = dr("UFCliente").ToString()
            'If CDate(dr("DataValidade")).ToShortDateString IsNot Nothing AndAlso CDate(dr("Emissao")).ToShortDateString IsNot Nothing Then
            '    Dim dataInicial As Date = CDate(dr("Emissao"))
            '    Dim dataFinal As Date = CDate(dr("DataValidade"))
            '    Dim qtdeDias As Integer = 0
            '    While dataInicial < dataFinal
            '        qtdeDias += 1
            '        dataInicial = dataInicial.AddDays(1)
            '    End While
            '    If qtdeDias > 0 Then
            '        lblValidade.Text = "Esta proposta é válida por " + CStr(qtdeDias) + " dias a contar da data de emissão."
            '    Else
            '        lblValidade.Text = "A data de validade da proposta está inspirada"
            '    End If
            'End If
            lblValidade.Text = "Está proposta é válida por 30 dias a contar da data de emissão."
            lblCondicao.Text = dr("DescricaoCondicao").ToString().Trim
            Dim detalheCondicao As String = dr("DetalheCondicaoPagamento").ToString().Trim
            If Not String.IsNullOrEmpty(detalheCondicao) Then
                lblTitDetCondicao.Visible = True
                If detalheCondicao.Length > 0 Then
                    lblDetCondicao.Text = detalheCondicao.Substring(0, detalheCondicao.Length - 1)
                Else
                    lblDetCondicao.Text = detalheCondicao
                End If
            End If
            lblRodape.Text = dr("Rodape").ToString.Replace(Chr(10), "</BR>")
            If dr("FaturadoPor").ToString = "R" Then
                pnlFaturadoPelaIntermed.Visible = False
            Else
                pnlFaturadoPelaIntermed.Visible = True
            End If
            Dim sTipoFrete As String = dr("TIPOFRETE").ToString.Trim
            Dim sDescricaoFrete As String = ""
            If sTipoFrete = "1" Then
                sDescricaoFrete = "FOB"
            ElseIf sTipoFrete = "2" Then
                sDescricaoFrete = "CIF"
            ElseIf sTipoFrete = "3" Then
                sDescricaoFrete = "FCA-LA"
            End If
            lblFrete.Text = sDescricaoFrete
            lblObservacao.Text = dr("Observacao").ToString.Trim.Replace(Chr(0), "").Replace(Chr(10), "<br />")
            pnlObservacao.Visible = lblObservacao.Text.Trim.Length > 0
            lblContato.Text = dr("Contato").ToString

            'cabecalho
            'CJ_SETOR
            'ASSINATURA VENDEDOR
            'Responsavel:
            'Cargo:
            'e(-Mail)
            'Dados do Vendedor
            'A3_NREDUZ

            grdItens.DataSource = dt
            grdItens.DataBind()
            grdDetalhe.DataSource = dt
            grdDetalhe.DataBind()
            'dt.DefaultView.RowFilter = "Descricao <> ''"

            'Dim nValorTotal As Decimal = 0
            'Dim nLinha As Integer = 1
            'For Each dr In dt.Rows
            '    nLinha += 1
            '    nValorTotal = nValorTotal + Decimal.Parse(dr("Valor").ToString())
            'Next
            'doc.Bookmarks("TotalGeral").Range.Text = nValorTotal.ToString("N")
        End If

    End Sub

    Private Sub grdItens_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItens.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim nEntrega As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "PrazoEntrega").ToString())
            Dim nPrecoUnitario As Decimal = Decimal.Parse(DataBinder.Eval(e.Row.DataItem, "PrecoUnitario").ToString())
            Dim nGarantia As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "PrazoGarantia").ToString())
            Dim nQuantidade As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "Quantidade").ToString())
            Dim nValor As Decimal = Decimal.Parse(DataBinder.Eval(e.Row.DataItem, "Valor").ToString())
            nTotal = nTotal + Decimal.Parse(DataBinder.Eval(e.Row.DataItem, "Valor").ToString())

            If Decimal.Parse(DataBinder.Eval(e.Row.DataItem, "PrazoEntrega").ToString) > 0 Then
                e.Row.Cells(2).Text = nEntrega.ToString + " dias"
            Else
                e.Row.Cells(2).Text = "-"
            End If

            If nPrecoUnitario > 0 Then
                Dim vPreco As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PrecoUnitario"))
                vPreco = String.Format("{0:C2}", Convert.ToDecimal(vPreco))
                e.Row.Cells(4).Text = vPreco
            Else
                e.Row.Cells(4).Text = "N/A"
            End If

            If nQuantidade < 1 Then
                e.Row.Cells(5).Text = "N/A"
            End If

            If nGarantia > 0 Then
                e.Row.Cells(3).Text = nGarantia.ToString + " meses"
            Else
                e.Row.Cells(3).Text = "N/A"
            End If

            If nValor > 0 Then
                Dim vPreco As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Valor"))
                vPreco = String.Format("{0:C2}", Convert.ToDecimal(vPreco))
                e.Row.Cells(6).Text = vPreco
            Else
                e.Row.Cells(6).Text = "N/A"
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "<b>" + "Valor Total da Proposta " + "</b>" + FormatCurrency(nTotal).ToString
        End If

    End Sub

    Private Sub grdDetalhe_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDetalhe.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblCodigoClassificacao As Label = DirectCast(e.Row.FindControl("lblCodigoClassificacao"), Label)
            Dim imgProduto As Image = DirectCast(e.Row.FindControl("imgProduto"), Image)
            Dim lblGarantia As Label = DirectCast(e.Row.FindControl("lblGarantia"), Label)
            Dim oDescricaoDetalhada As DescricaoDetalhada = DirectCast(e.Row.FindControl("oDescricaoDetalhada"), DescricaoDetalhada)
            imgProduto.ImageUrl = Util.ImageUrl(Me, DataBinder.Eval(e.Row.DataItem, "CodigoProduto").ToString())
            If Request.Browser.Browser.Equals("Firefox") OrElse Request.Browser.Browser.Equals("Opera") Then
                imgProduto.ImageUrl = imgProduto.ImageUrl.Insert(0, "~\")
            End If
            If Not imgProduto.ImageUrl.ToString.Contains("pixel.png") Then
                If lblCodigoClassificacao.Text.Trim = "E" Then
                    imgProduto.Style.Add("width", "8.5cm")
                    imgProduto.Style.Add("height", "8.5cm")
                    lblGarantia.Text = "A instalação dos equipamentos será realizada por nossa empresa, ou por empresa por nos autorizada, na data a ser combinada entre ambas as partes." + _
                                       "<br/>" + _
                                       "Garantia: 12 meses contra qualquer defeito oriundo de fabricação, exceto para os acessórios. " + _
                                                   "Será dada assistência técnica, porém remunerada após o período de garantia."
                Else
                    imgProduto.Style.Add("width", "5cm")
                    imgProduto.Style.Add("height", "5cm")
                End If
            Else
                imgProduto.Visible = False
            End If

            If oDescricaoDetalhada.Text.Length > 0 Then
                oDescricaoDetalhada.Text = oDescricaoDetalhada.Text.Substring(0, oDescricaoDetalhada.Text.Length - 1)
            Else
                oDescricaoDetalhada.Text = oDescricaoDetalhada.Text
            End If

        End If

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("Proposta.aspx?Numero=" + lblProposta.Text)
    End Sub

End Class