<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="RptProposta.aspx.vb" Inherits="Orcamento.RptProposta" MasterPageFile="~/MasterPrint.Master"
    Debug="true" %>

<%@ Register Src="controlesespecificos/ProdutoBox.ascx" TagName="ProdutoBox" TagPrefix="uc1" %>
<%@ Register Src="controlesespecificos/DescricaoDetalhada.ascx" TagName="DescricaoDetalhada" TagPrefix="uc2" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" runat="server">
        .corpo {
            font-family: Calibri;
            font-size: 19px;
            width: 980px;
            /*margin-top: 30px;
            margin-bottom: 30px; */
            background-color: white;
            color: black;
            font-weight: normal;
        }

        .cabecalho {
            width: 980px;
        }

        .conteudo {
            width: 980px;
        }

        .rodape {
            width: 980px;
        }

        .itens {
            width: 980px;
        }

        .style1 {
            width: 629px;
        }

        .style2 {
            text-align: right;
        }

        .style3 {
            width: 100%;
        }

        .texto {
            text-transform: capitalize;
        }
    </style>
    <script type="text/javascript" language="JavaScript">
        function Imprimir() {
            document.getElementById("tbBotoes").setAttribute("style", "display:none");
            self.print();
            document.getElementById("tbBotoes").setAttribute("style", "display:inherit");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table align="center" class="corpo">
        <tr>
            <td>
                <table id="tbCabecalho" class="cabecalho">
                    <tr>
                        <td class="style1">
                            <asp:Image ID="imgLogotipo" runat="server" />
                            &nbsp;
                        </td>
                        <td class="style2">
                            <asp:Image ID="imgLogoIntermed" runat="server" Height="52px" ImageUrl="~/App_Themes/Intermed/Logo.jpg" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCNPJ" runat="server" />
                        </td>
                        <td class="style2" colspan="2">CNPJ:
                            <asp:Label ID="lblCNPJIntermed" runat="server" Text="49.520.521/0001-69" />
                        </td>
                    </tr>
                </table>
                <hr />

                <asp:Panel ID="pnlCorpo" runat="server" CssClass="conteudo">
                    <br />

                    <b>Proposta: </b>
                    <asp:Label ID="lblProposta" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblLocal" runat="server" Text="São Paulo" CssClass="texto" />
                    ,
        <asp:Label ID="lblDataEmissao" runat="server" />
                    <br />
                    <br />
                    Para:
                    <asp:Label ID="lblNomeCliente" runat="server" CssClass="texto" />
                    <br />
                    <asp:Label ID="lblCnpjCliente" runat="server" />
                    <br />
                    <asp:Label ID="lblEnderecoCliente" runat="server" CssClass="texto" />
                    <br />
                    <asp:Label ID="lblMunicipioCliente" runat="server" CssClass="texto" />
                    &nbsp;
        <asp:Label ID="lblUFCliente" runat="server" />
                    <br />
                    <asp:Label ID="lblTitContato" runat="server" Text="A.C.:" />
                    &nbsp;
        <asp:Label ID="lblContato" runat="server" CssClass="texto" />
                    &nbsp;
                    <br />
                    <br />

                    <asp:Label ID="lblSaudacao" runat="server">
        Prezados Senhores, <br/> <br />
        Temos a grata satisfação  de submeter a sua apreciação a seguinte proposta:
                    </asp:Label>
                    <br />
                    <br />

                    OBSERVAÇÃO:
                    <br />
                    O valor total para pedido e faturamento mínimo não deverá ser inferior a R$ 250,00 (Duzentos e Cinqüenta Reais).
                    <br />
                    <br />

                    NOTA:
                    <br />
                    Conforme norma brasileira NBR 14136:2002 e em concordância com a Resolução nº 08 de 31 de agosto de 2009
        publicada pelo Ministério do desenvolvimento, Industria e Comercio Exterior, a partir de 1º de janeiro de 2010
        os plugues e tomadas para rede elétrica deverão atender a norma em questão.
                    <br />
                    Informamos que os produtos Intermed já estão aderentes a norma vigente e pedimos a
        nossos clientes que providenciem as adequações necessárias.
                    <br />
                    <br />

                    O uso de partes, peças e/ou acessórios não originais nos equipamentos da marca Intermed, 
        além de representar risco aos usuários dos mencionados equipamentos, 
        contraria o disposto na Resolução-ANVISA RDC n° 59/2000 e a orientação contida no Manual de Operação do Produto.
                    <br />
                    <br />

                    A garantia não cobre defeitos causados por acidente, uso inadequado, condições de uso, instalação ou esterilização inadequadas, serviço, instalação, operação ou alteração realizados por pessoal não autorizado ou desqualificado.
                    <br />
                    Peças sujeitas a desgaste ou deterioração normal pelo uso, condições de uso adversas, uso inadvertido ou acidentes não são cobertas pela GARANTIA.
                    <br />
                    <br />

                    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        ShowFooter="True" CssClass="itens" FooterStyle-BorderStyle="None" FooterStyle-CssClass="rodape">

                        <Columns>

                            <asp:BoundField DataField="Item" HeaderText="Item">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Produto">
                                <ItemTemplate>
                                    <uc1:ProdutoBox ID="oProduto" runat="server" Codigo='<%# Bind("CodigoProduto") %>' Nome='<%# Bind("NomeProduto") %>' CssClass="texto" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="PrazoEntrega" HeaderText="Prz. Entrega">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="PrazoGarantia" HeaderText="Prz. Garantia" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="PrecoUnitario" HeaderText="Vl. Unitário" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Quantidade" HeaderText="Quant.">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Valor" HeaderText="Vl. Total" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <b>Condição de pagamento: </b>
                    <asp:Label ID="lblCondicao" runat="server" CssClass="texto" />
                    &nbsp;
        <asp:Label ID="lblTitDetCondicao" runat="server" Style="font-weight: 700" Text="Detalhe: " Visible="false" />
                    &nbsp;
        <asp:Label ID="lblDetCondicao" runat="server" CssClass="texto" />
                    <br />
                    <b>Frete: </b>
                    <asp:Label ID="lblFrete" runat="server" />
                    <br />
                    <asp:Label ID="lblValidade" runat="server" Style="font-weight: 700" />
                    <br />
                    <br />
                    <asp:Label ID="lblFaturadoPeloRepresentante" runat="server" Font-Bold="True">ESTA PROPOSTA ESTÁ SUJEITA A APROVAÇÃO DA INTERMED</asp:Label><br />
                    <asp:Label ID="lblTextoObservacao" runat="server"> ** O prazo de entrega terá validade a partir da data de aprovação desta proposta. <br/>
            **** Em caso de aprovação da proposta, é necessário que a mesma seja realizada através de fax ou e-mail citados abaixo. </asp:Label>
                    <br />
                    <br />

                    <asp:Panel ID="pnlFaturadoPelaIntermed" runat="server" Visible="true">
                        <asp:Label ID="lblFaturadoPelaIntermed" runat="server" Font-Bold="True">Os itens cotados serão faturados diretamente pela fábrica:</asp:Label><br />
                        <asp:Label ID="Label1" runat="server">Intermed Equip. Médico Hosp. Ltda. </asp:Label>
                        <br />
                        <asp:Label ID="Label2" runat="server">R. Santa Mônica, 980 - Pq. Industrial San Jose</asp:Label>
                        &nbsp;- CEP:
                <asp:Label ID="Label3" runat="server">06715-865</asp:Label>
                        - Tel.
                <asp:Label ID="Label6" runat="server">(55)(11) 4615-9300</asp:Label>
                        &nbsp; &nbsp;-
                <asp:Label ID="Label8" runat="server">Cotia</asp:Label>
                        -
                <asp:Label ID="Label9" runat="server">SP</asp:Label>
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Label ID="lblRodape" runat="server" Visible="false" />
                    <asp:GridView ID="grdDetalhe" runat="server" AutoGenerateColumns="False" CssClass="itens"
                        EnableModelValidation="True" ShowFooter="True" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="Item" HeaderText="Item">
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Class." Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoClassificacao" runat="server" Text='<%# Bind("CodigoClassificacao") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCodigoClassificacao" runat="server" Text='<%# Bind("CodigoClassificacao") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Produto">
                                <ItemTemplate>
                                    <br />
                                    <b>
                                        <uc1:ProdutoBox ID="oProduto" runat="server" Codigo='<%# Bind("CodigoProduto") %>'
                                            Nome='<%# Bind("NomeProduto") %>' />
                                    </b>
                                    <br />
                                    <asp:Image ID="imgProduto" runat="server" ImageUrl="~/imagens/pixel.png" />
                                    <br />
                                    <asp:Label ID="lblGarantia" runat="server" Text="" />
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    </td> </Ttr>
                        <tr>
                            <td colspan="3">
                                <uc2:DescricaoDetalhada ID="oDescricaoDetalhada" Text='<%# Bind("Descricao") %>'
                                    runat="server" />
                                <hr />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Panel ID="pnlAssintura" runat="server">
                        <pre>
Atenciosamente,
 
________________________________

<asp:Label ID="lblRazaoSocial2" runat="server" Text="" CssClass="texto" />
<asp:Label ID="lblTel" runat="server" Text="Fone: " Visible="false" /> <asp:Label ID="lblTelefone2" runat="server" Text="" />
<asp:Label ID="lblTFax2" runat="server" Text="Fax: " Visible="false"/> <asp:Label ID="lblFax2" runat="server" Text="" />
<asp:Label ID="lblEmail" runat="server" Text="" />

            </pre>
                    </asp:Panel>
                </asp:Panel>
                <hr />

                <table id="tbRodape" class="rodape">
                    <tr>
                        <td>
                            <asp:Label ID="lblRazaoSocial" runat="server" CssClass="texto" /><br />
                            <asp:Label ID="lblRodape1" runat="server" CssClass="texto" />
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <asp:Panel ID="pnlBotoes" runat="server">
        <table id="tbBotoes" class="style3">
            <caption>
                <br />
                <tr>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir()" class="button" />
                    </td>
                    <td dir="rtl">
                        <uc7:AutoHideButton ID="btnVoltar" runat="server" CssButton="button"
                            Text="Voltar" />
                    </td>
                </tr>
            </caption>
        </table>
        <br />
    </asp:Panel>
    <br />

</asp:Content>
