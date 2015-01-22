<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master" CodeBehind="Erro.aspx.vb" Inherits="Orcamento.Erro" %>
<asp:Content ID="Principal" ContentPlaceHolderID="SubMaster" runat="server">

    <asp:Label ID="lblTitulo" runat="server" Text="Erro" CssClass="title"></asp:Label>
    <br />
    <asp:Panel ID="pnlErro" runat="server">
        <br />
        <asp:Label ID="lblSubTitulo1" runat="server" CssClass="title" 
            Text="Erro ao acessar o recurso solicitado."></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblMensagem" runat="server" CssClass="title">Por favor, tente novamente mais tarde. Caso a falha persista entre em contato com o admnistrador do sistema.</asp:Label>
        <br />
        <asp:Label ID="lblDetalhe" runat="server" CssClass="txt"></asp:Label>
    </asp:Panel>
</asp:Content>
