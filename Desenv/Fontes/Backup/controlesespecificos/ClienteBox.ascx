<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClienteBox.ascx.vb" Inherits="Orcamento.ClienteBox" %>
<asp:Panel ID="pnlConteudo" runat="server">
    <asp:Label ID="lblCodigo" runat="server" Visible="false"/>
    <asp:Label ID="lblBarra" runat="server" Text="/" Visible="false"/>
    <asp:Label ID="lblLoja" runat="server" Visible="false"/>
    <asp:Label ID="lblCPF_CNPJ" runat="server"/>
    -<asp:Label ID="lblNome" runat="server" CssClass="label2"/>
</asp:Panel>
