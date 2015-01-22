<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CondicaoPagamentoView.ascx.vb" Inherits="Orcamento.CondicaoPagamentoView" %>
<%@ Register Src="../componentes/controles/DetalheConsultaPadrao.ascx" TagName="DetalheConsultaPadrao" TagPrefix="uc1" %>
<asp:Label ID="lblFound" runat="server" Visible="False" Text="False" />
<asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="pnlVisualizacao" runat="server" CssClass="inlineButton">
            <asp:TextBox ID="txtCodigo" runat="server" Columns="3" MaxLength="3" AutoPostBack="True" />
            <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" />
            <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" Visible="False" ToolTip="Não encontrado" />
            <asp:Label ID="lblNome" runat="server" CssClass="label2"/> &nbsp; 
            <asp:Label ID="lblDetalhe" runat="server" Text="Detalhe: " CssClass="label" Visible="false" /> 
            <asp:TextBox ID="txtDetalhe" runat="server" CssClass="100pt" Visible="false" />
        </asp:Panel>
        <uc1:DetalheConsultaPadrao ID="oDetalhe" runat="server" Procedure="Orcamento.CondicaoPagamento.ctlCondicaoPagamento" />
    </ContentTemplate>
</asp:UpdatePanel>
