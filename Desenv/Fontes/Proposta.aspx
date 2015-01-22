<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master"
    CodeBehind="Proposta.aspx.vb" Inherits="Orcamento.Proposta" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="controlesespecificos/VendedorView.ascx" TagName="VendedorView" TagPrefix="uc2" %>
<%@ Register Src="controlesespecificos/ProdutoView.ascx" TagName="ProdutoView" TagPrefix="uc3" %>
<%@ Register Src="componentes/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc4" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc5" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register Src="controlesespecificos/CondicaoPagamentoView.ascx" TagName="CondicaoPagamentoView" TagPrefix="uc8" %>
<%@ Register Src="controlesespecificos/StatusPropostaBox.ascx" TagName="StatusPropostaBox" TagPrefix="uc9" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="componentes/controles/DetalheProduto.ascx" TagName="DetalheProduto" TagPrefix="uc10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
   <asp:HiddenField ID="hdnFaturadoPor" runat="server" />
   
    <asp:Panel ID="pnlCabecalho" runat="server"> <br/> <br/>
        
        <asp:Label ID="lblTitNumero" runat="server" Text="Número:" CssClass="label" /> &nbsp;
        <asp:Label ID="lblNumero" runat="server" Text="000000" /> &nbsp;
        <uc9:StatusPropostaBox ID="oStatusProposta" runat="server" /> &nbsp;
        <asp:Label ID="lblTitEmissao" runat="server" CssClass="label" Text="Emissão:" /> &nbsp;
        <asp:Label ID="lblEmissao" runat="server" Text="__/__/____" /> <br/> <br/>

        <asp:Label ID="lblTitValidade" runat="server" CssClass="label" Text="Validade:" /> &nbsp;
        <uc5:DateBox ID="txtDataValidade" runat="server" AutoPostBack="true" Enabled="False" /> 
        <uc7:AutoHideButton ID="btnProrrogar" runat="server" CssButton="button" Text="Prorrogar" Visible="False" /> &nbsp;
        
        <asp:Panel ID="pnlMotivoProrrogacao" runat="server" Visible="False" STYLE="display:inline" >
            <asp:Label ID="lblObservacaoDataValidade" runat="server" Text="Mot. Prorrogação: " CssClass="label" /> &nbsp;
            <asp:DropDownList ID="ddlProrrogacao" runat="server" Width="235px">
                <asp:ListItem Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Solicitado pelo Cliente</asp:ListItem>
                <asp:ListItem Value="2">Solicitado pelo Representante</asp:ListItem>
                <asp:ListItem Value="3">Solicitado pela Intermed</asp:ListItem>
            </asp:DropDownList>
        </asp:Panel> <br/> <br/>

        <asp:Label ID="lblTitPossibilidade" runat="server" CssClass="label" Text="Possib. Venda:" /> &nbsp;
        <asp:DropDownList ID="drpPossibilidadeVenda" runat="server" />
        <asp:Label ID="lblTitTipoOrcamento" runat="server" CssClass="label" Text="Tipo Orçamento:" />
        <asp:DropDownList ID="drpTipoOrcamento" runat="server">
            <asp:ListItem Selected="True" Value="">Selecione</asp:ListItem>
            <asp:ListItem Value="PUB">Público</asp:ListItem>
            <asp:ListItem Value="PRI">Privado</asp:ListItem>
        </asp:DropDownList> <br/> <br/>

        <asp:Label ID="lblTitCondicao" runat="server" CssClass="label" Text="Cond. Pag.:" /> &nbsp;
        <uc8:CondicaoPagamentoView ID="oCondicaoPagamento" runat="server" /> &nbsp;
        <asp:Label ID="lblDetalhe" runat="server" Text="Det. Cond.: " CssClass="label" /> &nbsp;
        <asp:TextBox ID="txtDetalheCodicao1" runat="server" Width="100pt" />
        <asp:Label ID="lblTitTipoFrete" runat="server" CssClass="label" Text="Frete pago pelo Cliente?" />
        <asp:RadioButtonList ID="radTipoFrete" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Text="Sim" Value="1"></asp:ListItem>
            <asp:ListItem Text="Não" Value="2"></asp:ListItem>
        </asp:RadioButtonList> <br/> <br/>

        <asp:Label ID="lblTitCliente" runat="server" CssClass="label" Text="CNPJ do Cliente:" /> &nbsp;
        <span id="spanParametros" runat="server">
            <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="true" TipoBuscaSelecionado="CPF_CPNJ" /> <br /> <br />
            <asp:Label ID="lblTitContato" runat="server" CssClass="label" Text="Contato:" /> &nbsp; 
            <asp:TextBox ID="txtContato" runat="server" CssClass="label2" /> &nbsp;
            <asp:Label ID="lblTitRevendedor" runat="server" CssClass="label" Text="Revendedor:" /> &nbsp;
            <uc2:VendedorView ID="txtVendedor" runat="server" ExibirBusca="false" ExibirCodigo="false"  CssClass="label2" />
        </span> <br /> <br/>
        <asp:Label ID="lblTitObservacao" runat="server" CssClass="label" Text="Observações:" /> <br />
        <asp:TextBox ID="txtObservacao" runat="server" cssClass="textarea" TextMode="MultiLine" Width="993px" /> <hr />
    </asp:Panel>
    
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        
        <ContentTemplate>
            
            <asp:Panel ID="pnlItens" runat="server"> <br />
                
                <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    ShowFooter="True" EnableModelValidation="True">
                    
                    <Columns>
                        
                        <asp:CommandField InsertText="Incluir" NewText="Novo" ShowCancelButton="True" ShowDeleteButton="true"
                            ShowEditButton="true" ShowInsertButton="True" ShowSelectButton="False" ButtonType="Image"
                            CancelText="Cancelar" CancelImageUrl="~/imagens/desfazer.png" EditImageUrl="~/imagens/editar.png"
                            DeleteImageUrl="~/imagens/excluir.png" InsertImageUrl="~/imagens/Novo.png" NewImageUrl="~/imagens/novo.png"
                            UpdateImageUrl="~/imagens/confirmar.png" />

                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Class.">
                            <ItemTemplate>
                                <asp:Label ID="lblCodigoClassificacao" runat="server" Text='<%# Bind("CodigoClassificacao") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCodigoClassificacao" runat="server" Text='<%# Bind("CodigoClassificacao") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Produto">
                            <ItemTemplate>
                                <uc3:ProdutoView ID="oProduto" runat="server" Codigo='<%# Bind("CodigoProduto") %>'
                                    HabilitarEdicao="false" Nome='<%# Bind("NomeProduto") %>' OnSelecionarOnClick="oProduto_SelecionarOnClick" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uc3:ProdutoView ID="oProduto" runat="server" Codigo='<%# Bind("CodigoProduto") %>'
                                    HabilitarEdicao="true" Nome='<%# Bind("NomeProduto") %>' OnSelecionarOnClick="oProduto_SelecionarOnClick" />
                                <uc10:DetalheProduto ID="oDetalheProduto" runat="server" Descricao='<%# Bind("Descricao") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prz. Entrega">
                            <EditItemTemplate>
                                <asp:Label ID="lblPrazoEntrega" runat="server" Text='<%# Bind("PrazoEntrega") %>' /> Dias
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrazoEntrega" runat="server" Text='<%# Bind("PrazoEntrega") %>' /> Dias
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Prz. Garantia (Meses)" Visible="false">
                            <EditItemTemplate>
                                <uc4:NumberBox ID="txtPrazoGarantia" runat="server" Columns="2" EnableUpDown="false"
                                    MaxLength="2" Text='<%# Bind("PrazoGarantia") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrazoGarantia" runat="server" Text='<%# Bind("PrazoGarantia") %>' /> 
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qtd.">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantidade" runat="server" Text='<%# Bind("Quantidade") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uc4:NumberBox ID="txtQuantidade" runat="server" Columns="3" EnableUpDown="false"
                                    MaxLength="3" Text='<%# Bind("Quantidade") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prc. Lista">
                            <ItemTemplate>
                              R$ <asp:Label ID="lblPrecoLista" runat="server" Text='<%# Bind("PrecoLista") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prc. Venda">
                            <ItemTemplate>
                               R$ <asp:Label ID="lblPrecoUnitario" runat="server" Text='<%# Bind("PrecoUnitario") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                               R$ <uc4:NumberBox ID="txtPrecoUnitario" runat="server" Columns="10" EnableUpDown="false"
                                    MaxLength="10" Text='<%# Bind("PrecoUnitario") %>' ValidChars="," />
                            </EditItemTemplate>
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Vl. Total">
                            <ItemTemplate>
                              R$ <asp:Label ID="lblValorTotal" runat="server" Text='<%# Bind("Valor") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <RowStyle CssClass="RowStyle" />

                </asp:GridView>

            </asp:Panel>

            <asp:Panel ID="pnlTotais" runat="server"> <br />
                <table style="width: 50%;">
                    <tr>
                        <td style="width: 300px">
                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Total da Proposta:" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalProposta" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
    
    </asp:UpdatePanel>

    <asp:Panel ID="pnlBotoes" runat="server"> <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <uc7:AutoHideButton ID="btnAlterar" runat="server" CssButton="button" Text="Alterar" />
                    <uc7:AutoHideButton ID="btnSalvar" runat="server" CssButton="button" Text="Salvar" />
                    <uc7:AutoHideButton ID="btnImprimir" runat="server" CssButton="button" Text="Imprimir" Visible="false" />
                    <uc7:AutoHideButton ID="btnAprovar" runat="server" CssButton="button" Text="Aprovar" Visible="false" />
                </td>
                <td dir="rtl">
                    <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button" Text="Voltar" />
                </td>
            </tr>
        </table> <br/>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlMensagm">
        <uc6:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>

</asp:Content>
