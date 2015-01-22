<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DetalheProduto.ascx.vb" Inherits="Orcamento.DetalheProduto" %>

<asp:ImageButton ID="imgDescricao" runat="server" ImageUrl="~/imagens/Editar.png" ToolTip="Editar Detalhe do Produto" />

<asp:Panel ID="pnlDescricao" runat="server" CssClass="DetalheProduto" Visible="false">

    <asp:Panel ID="pnlTopBar" runat="server" CssClass="TopBar">
        <asp:ImageButton ID="imgFechar" runat="server" ImageAlign="Right" ImageUrl="~/imagens/fechar.jpg" /> &nbsp;
        <asp:Label ID="lblTopBar" runat="server" />
    </asp:Panel>

    <asp:TextBox ID="txtDescricao" runat="server" Height="348px" TextMode="MultiLine" Width="800px" /> <br/>
    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" /> <br/>

</asp:Panel>
