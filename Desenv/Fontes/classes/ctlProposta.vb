Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.ComponentModel

Public Class ctlProposta

    Public Enum TipoPesquisaProposta
        [Todas]
        [Número]
        [Cliente]
        [Emissão]
        <Description("A vencer")> _
        [A_Vencer]
    End Enum

    Public Function Pesquisar(ByVal oTipo As TipoPesquisaProposta, ByVal oParametros As Object) As DataTable
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_LISTAR_PROPOSTA", conn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim aParametros As String()
        Dim parmMatricula As New SqlParameter("@P_MATRICULA", HttpContext.Current.User.Identity.Name)
        Dim parmCliente As New SqlParameter("@P_CODIGOCLIENTE", DBNull.Value)
        Dim parmLoja As New SqlParameter("@P_LOJACLIENTE", DBNull.Value)
        Dim parmCPFCNPJ As New SqlParameter("@P_CPFCNPJ", DBNull.Value)
        Dim parmEmissao As New SqlParameter("@P_EMISSAO", DBNull.Value)
        Dim parmNumero As New SqlParameter("@P_NUMERO", DBNull.Value)
        Dim parmVendedor As New SqlParameter("@P_CODIGOVENDEDOR", DBNull.Value)
        Dim dt As New DataTable
        Select Case oTipo
            Case TipoPesquisaProposta.Cliente
                aParametros = DirectCast(oParametros, String())
                parmCliente.Value = aParametros(0).ToString
                parmLoja.Value = aParametros(1).ToString
                parmCPFCNPJ.Value = aParametros(2).ToString
            Case TipoPesquisaProposta.Emissão
                parmEmissao.Value = oParametros
            Case TipoPesquisaProposta.Número
                parmNumero.Value = oParametros.ToString
                'Case TipoPesquisaProposta.Todas
                '    parmVendedor.Value = oParametros.ToString
        End Select
        cmd.Parameters.Add(parmMatricula)
        cmd.Parameters.Add(parmNumero)
        cmd.Parameters.Add(parmCliente)
        cmd.Parameters.Add(parmLoja)
        cmd.Parameters.Add(parmCPFCNPJ)
        cmd.Parameters.Add(parmEmissao)
        cmd.Parameters.Add(parmVendedor)
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", Util.GetFilial()))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt
    End Function

    Public Function Selecionar(ByVal sNumero As String) As DataTable
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_SEL_PROPOSTACOMPLETA_02", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_NUMERO", sNumero))
        cmd.Parameters.Add(New SqlParameter("@P_MATRICULA", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_FILIAL", Util.GetFilial()))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Return dt
    End Function

    Public Function Desativar(ByVal sNumero As String) As RetornoGenerico
        Dim oToken As New WSOrcamento.TKNSTRUCT
        oToken.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
            oToken.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).Hash
            Dim wsOrcamento As New WSOrcamento.WSORCAMENTO
            Return New RetornoGenerico(wsOrcamento.CANCELAR(oToken, sNumero))
    End Function


    Public Function Alterar(ByVal oProposta As Proposta1.Proposta) As RetornoGenerico
        Dim oToken As New WSOrcamento.TKNSTRUCT
        oToken.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oToken.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).Hash
        Dim wsOrcamento As New WSOrcamento.WSORCAMENTO
        Dim oFicha As New WSOrcamento.FICHAORCAMENTOSTRUCT
        oFicha = DirectCast(oProposta.PutSiga(oFicha), WSOrcamento.FICHAORCAMENTOSTRUCT)
        Dim oItens As New List(Of WSOrcamento.ITEMORCAMENTOSTRUCT)
        For Each oItemProposta As Proposta1.ItemProposta In oProposta.Itens
            Dim oItem As New WSOrcamento.ITEMORCAMENTOSTRUCT
            oItem = DirectCast(oItemProposta.PutSiga(oItem), WSOrcamento.ITEMORCAMENTOSTRUCT)
            oItens.Add(oItem)
        Next
        oFicha.ITENS = oItens.ToArray
        Return New RetornoGenerico(wsOrcamento.ALTERAR(oToken, oFicha))
    End Function
    Public Function Incluir(ByVal oProposta As Proposta1.Proposta) As RetornoGenerico

        Dim oToken As New WSOrcamento.TKNSTRUCT
        oToken.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oToken.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).Hash
        Dim wsOrcamento As New WSOrcamento.WSORCAMENTO
        Dim oFicha As New WSOrcamento.FICHAORCAMENTOSTRUCT
        oFicha = DirectCast(oProposta.PutSiga(oFicha), WSOrcamento.FICHAORCAMENTOSTRUCT)
        Dim oItens As New List(Of WSOrcamento.ITEMORCAMENTOSTRUCT)
        For Each oItemProposta As Proposta1.ItemProposta In oProposta.Itens
            Dim oItem As New WSOrcamento.ITEMORCAMENTOSTRUCT
            oItem = DirectCast(oItemProposta.PutSiga(oItem), WSOrcamento.ITEMORCAMENTOSTRUCT)
            oItens.Add(oItem)
        Next
        oFicha.ITENS = oItens.ToArray
        Return New RetornoGenerico(wsOrcamento.GERAR(oToken, oFicha))

    End Function
    Public Function Aprovar(ByVal oProposta As Proposta1.Proposta) As RetornoGenerico
        Dim oTokenD As New WSOrcamento.TKNSTRUCT
        oTokenD.CONTEUDO = HttpContext.Current.Session("NomeUsuario").ToString
        oTokenD.SENHA = DirectCast(HttpContext.Current.Session("Usuario2"), Dados.Usuario).Hash
        Dim wsOrcamentoD As New WSOrcamento.WSORCAMENTO
        Dim oFichaD As New WSOrcamento.FICHAORCAMENTOSTRUCT
        oFichaD = DirectCast(oProposta.PutSiga(oFichaD), WSOrcamento.FICHAORCAMENTOSTRUCT)
        Dim oItensD As New List(Of WSOrcamento.ITEMORCAMENTOSTRUCT)
        For Each oItemProposta As Proposta1.ItemProposta In oProposta.Itens
            Dim oItemD As New WSOrcamento.ITEMORCAMENTOSTRUCT
            oItemD = DirectCast(oItemProposta.PutSiga(oItemD), WSOrcamento.ITEMORCAMENTOSTRUCT)
            oItensD.Add(oItemD)
        Next
        oFichaD.ITENS = oItensD.ToArray
        Dim oRetSigaD As WSOrcamento.RETSTRUCT = wsOrcamentoD.APROVAR(oTokenD, oFichaD)
        Return New RetornoGenerico(oRetSigaD)
 
    End Function
    'Public Sub Imprimir(ByVal sNumero As String)
    '    Dim dt As DataTable = Selecionar(sNumero)
    '    Download(Imprimir(dt))
    'End Sub
    'Public Function Imprimir(ByVal dt As Data.DataTable) As String
    '    Dim word As New Microsoft.Office.Interop.Word.Application()
    '    Dim oMissing As Object = System.Reflection.Missing.Value
    '    word.Visible = False
    '    word.ScreenUpdating = False
    '    Dim filename As Object = DirectCast(HttpContext.Current.Server.MapPath("Proposta.dotx"), Object)
    '    Dim doc As Microsoft.Office.Interop.Word.Document
    '    doc = DirectCast(word.Documents.Add(filename), Microsoft.Office.Interop.Word.Document) ',  oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing), Document)
    '    doc.Activate()
    '    Dim dr As DataRow = dt.Rows(0)
    '    Dim outputFileName As Object = HttpContext.Current.Server.MapPath("arquivos") + "\" + Guid.NewGuid().ToString + dr("NUMERO").ToString + ".pdf"
    '    doc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageHeader
    '    Dim oRange = word.Selection.HeaderFooter.Range
    '    doc.Bookmarks("Logotipo").Range.InlineShapes.AddPicture(HttpContext.Current.Server.MapPath("logos\" + dr("CodigoVendedor").ToString.Trim + ".jpg"), False, True, DirectCast(oRange, Object))
    '    doc.Bookmarks("CNPJ").Range.Text = dr("CPFCNPJCLIENTE").ToString
    '    doc.Bookmarks("IE").Range.Text = dr("INSCRICAOESTADUAL").ToString
    '    doc.Bookmarks("Proposta").Range.Text = dr("Numero").ToString
    '    doc.Bookmarks("NomeVendedor").Range.Text = dr("NOMEVENDEDOR").ToString
    '    doc.Bookmarks("DataEmissao").Range.Text = DateTime.Parse(dr("EMISSAO").ToString).ToString("d ' de 'MMMM' de 'yyyy")
    '    doc.Bookmarks("NomeCliente").Range.Text = dr("NomeCliente").ToString.Trim
    '    doc.Bookmarks("EnderecoCliente").Range.Text = dr("EnderecoCliente").ToString().Trim
    '    doc.Bookmarks("MunicipioCliente").Range.Text = dr("MunicipioCliente").ToString().Trim
    '    doc.Bookmarks("UFCliente").Range.Text = dr("UFCliente").ToString()
    '    Dim oTable As Microsoft.Office.Interop.Word.Table
    '    Dim wrdRng As Microsoft.Office.Interop.Word.Range = doc.Bookmarks("TabelaItens").Range
    '    oTable = doc.Tables.Add(wrdRng, dt.Rows.Count + 1, 5)
    '    oTable.Cell(1, 1).Range.Text = "Item"
    '    oTable.Cell(1, 2).Range.Text = "Produto"
    '    oTable.Cell(1, 3).Range.Text = "Preço"
    '    oTable.Cell(1, 4).Range.Text = "Quant."
    '    oTable.Cell(1, 5).Range.Text = "Valor"
    '    oTable.Cell(1, 1).Width = 30
    '    oTable.Cell(1, 2).Width = 200
    '    oTable.Cell(1, 3).Width = 60
    '    oTable.Cell(1, 4).Width = 60
    '    oTable.Cell(1, 5).Width = 60
    '    oTable.Cell(1, 1).Range.Words(1).Font.Bold = 1
    '    oTable.Cell(1, 2).Range.Words(1).Font.Bold = 1
    '    oTable.Cell(1, 3).Range.Words(1).Font.Bold = 1
    '    oTable.Cell(1, 4).Range.Words(1).Font.Bold = 1
    '    oTable.Cell(1, 5).Range.Words(1).Font.Bold = 1
    '    oTable.Cell(1, 3).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
    '    oTable.Cell(1, 4).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '    oTable.Cell(1, 5).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderHorizontal).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    oTable.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    '    Dim nValorTotal As Decimal = 0
    '    Dim nLinha As Integer = 1
    '    For Each dr In dt.Rows
    '        nLinha += 1
    '        oTable.Rows(nLinha).Cells(1).Range.Text = dr("Item").ToString
    '        oTable.Rows(nLinha).Cells(2).Range.Text = dr("CodigoProduto").ToString + " - " + dr("NomeProduto").ToString
    '        oTable.Rows(nLinha).Cells(3).Range.Text = Decimal.Parse(dr("PrecoUnitario").ToString()).ToString("N")
    '        oTable.Rows(nLinha).Cells(4).Range.Text = dr("Quantidade").ToString
    '        oTable.Rows(nLinha).Cells(5).Range.Text = Decimal.Parse(dr("Valor").ToString()).ToString("N")
    '        oTable.Cell(nLinha, 1).Width = 30
    '        oTable.Cell(nLinha, 2).Width = 200
    '        oTable.Cell(nLinha, 3).Width = 60
    '        oTable.Cell(nLinha, 4).Width = 60
    '        oTable.Cell(nLinha, 5).Width = 60
    '        oTable.Cell(nLinha, 3).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
    '        oTable.Cell(nLinha, 4).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        oTable.Cell(nLinha, 5).Range.Paragraphs(1).Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
    '        nValorTotal = nValorTotal + Decimal.Parse(dr("Valor").ToString())
    '    Next
    '    doc.Bookmarks("TotalGeral").Range.Text = nValorTotal.ToString("N")
    '    doc.Bookmarks("Observacoes").Range.Text = dr("Rodape").ToString
    '    Dim fileFormat As Object = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
    '    doc.SaveAs(outputFileName, fileFormat)
    '    Dim saveChanges As Object = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges
    '    DirectCast(doc, Microsoft.Office.Interop.Word._Document).Close(saveChanges, oMissing, oMissing)
    '    doc = Nothing
    '    DirectCast(word, Microsoft.Office.Interop.Word._Application).Quit(oMissing, oMissing, oMissing)
    '    word = Nothing
    '    Return outputFileName.ToString
    'End Function
    Public Sub Download(ByVal sArquivo As String)
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(sArquivo) '-- if the file exists on the server  
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
        HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.WriteFile(file.FullName)
        HttpContext.Current.Response.End() 'if file does not exist  
    End Sub
End Class
