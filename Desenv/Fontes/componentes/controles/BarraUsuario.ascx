<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BarraUsuario.ascx.vb" Inherits="Orcamento.BarraUsuario" %>

<asp:Panel ID="pnlBarraUsuario" runat="server" CssClass="UsuarioCorrente">

<asp:Label ID="lblLocalAtual" runat="server" Text="&gt;&gt;Local atual" CssClass="PaginaCorrente" /> &nbsp;
     <asp:Label ID="lblTitNomeUsuario" runat="server" Text="Usuário:" /> &nbsp; 
     <asp:Label ID="lblNomeCompleto" runat="server" Text="Nome do usuário" />
</asp:Panel>
