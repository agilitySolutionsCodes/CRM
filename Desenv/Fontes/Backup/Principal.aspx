<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master"
    CodeBehind="Principal.aspx.vb" Inherits="Orcamento.Principal" %>

<%@ Register Src="controlesespecificos/ClienteView.ascx" TagName="ClienteView" TagPrefix="uc1" %>
<%@ Register Src="controlesespecificos/VendedorView.ascx" TagName="VendedorView" TagPrefix="uc2" %>
<%@ Register Src="componentes/controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc3" %>
<%@ Register Src="componentes/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="controlesespecificos/ClienteBox.ascx" TagName="ClienteBox" TagPrefix="uc5" %>
<%@ Register Src="controlesespecificos/VendedorBox.ascx" TagName="VendedorBox" TagPrefix="uc6" %>
<%@ Register Src="componentes/controles/AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc7" %>
<%@ Register src="controlesespecificos/StatusPropostaBox.ascx" tagname="StatusPropostaBox" tagprefix="uc9" %>
<%@ Register src="controlesespecificos/NFBox.ascx" tagname="NFBox" tagprefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubMaster" runat="server">
 
    <asp:Panel ID="pnlPesquisa" runat="server" CssClass="Pesquisa"> &nbsp;
        <asp:Label ID="lblTitPesquisar" runat="server" Text="Pesquisar:" CssClass="label" /> &nbsp;
        <asp:DropDownList ID="drpTipoPequisa" runat="server" AutoPostBack="True">
        </asp:DropDownList> &nbsp; &nbsp; 
        <span id="spanParametros" runat="server">
            <asp:TextBox ID="txtParametroPesquisa" runat="server" Visible="false" />
            <uc3:DateBox ID="txtEmissao" runat="server" />
            <uc1:ClienteView ID="txtCliente" runat="server" ExibirBusca="false" TipoBuscaSelecionado="CPF_CPNJ" />
            <uc7:AutoHideButton ID="btnBuscar" runat="server" CssButton="button" Text="Buscar" />
        </span> <br />        
    </asp:Panel> 

    <asp:Panel ID="pnlResultado" runat="server" Visible="False"> <br />
        
        <asp:Panel ID="pnlItens0" runat="server" Height="400px" ScrollBars="Auto" BorderStyle="Solid" BorderWidth="1px" >
            
            <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" 
                CssClass="GridViewStyle" Width="100%" EnableModelValidation="True" >
                
                <Columns>
                    
                    <asp:HyperLinkField DataTextField="Numero" HeaderText="Número" DataNavigateUrlFields="Numero"
                        DataNavigateUrlFormatString="~/Proposta.aspx?Numero={0}" Target="_parent" />
                    
                    <asp:BoundField DataField="Emissao" HeaderText="Emissão Prop." 
                        DataFormatString="{0:dd/MM/yyyy}" />
                    
                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <uc5:ClienteBox ID="oCliente" runat="server" Nome='<%# Bind("NomeCliente") %>' Codigo='<%# Bind("CodigoCliente") %>'
                                Loja='<%# Bind("LojaCliente") %>' CPF_CNPJ='<%# Bind("CPFCNPJ") %>' CssClass="label2" />
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    
                    <asp:TemplateField HeaderText="NF" Visible="false">
                        <ItemTemplate>
                            <uc8:NFBox ID="NFBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="EmissaoNF" HeaderText="Emissão NF" 
                        DataFormatString="{0:dd/MM/yyyy}" Visible="false" />
                    
                    <asp:TemplateField HeaderText="Situação">
                        <ItemTemplate>
                            <uc9:StatusPropostaBox ID="oStatusProposta" runat="server" Codigo='<%# Bind("CodigoStatus") %>' Descricao='<%# Bind("DescricaoStatus") %>' CodigoPersonalizado='<%# Bind("CodigoPersonalizadoStatus") %>' DescricaoPersonalizada='<%# Bind("DescricaoPersonalizadaStatus") %>' CssClass="label2" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="DataValidade" HeaderText="Validade" 
                        DataFormatString="{0:dd/MM/yyyy}" />
                
                </Columns>
                
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
            
            </asp:GridView> 
            
        </asp:Panel> <br/>

    </asp:Panel>

    <asp:Panel ID="pnlIncluir" runat="server">
        <asp:DropDownList ID="drpFaturadoPor" runat="server">
            <asp:ListItem>Selecione</asp:ListItem>
            <asp:ListItem Value="I">Incluir Proposta Faturada pela Intermed</asp:ListItem>
            <asp:ListItem Value="R">Incluir Proposta Faturada pelo Representante</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnIncluir" runat="server" CssClass="button" Text="Incluir" />
    </asp:Panel> <br/>

    <uc4:Mensagem ID="oMensagem" runat="server" /> <br/>
    <asp:HyperLink ID="lnkTreinamento" NavigateUrl="Treinamento/treinamento.html" runat="server" Target="_blank">Treinamento On Line</asp:HyperLink>

</asp:Content>
