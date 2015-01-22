<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="VendedorView.ascx.vb" Inherits="Orcamento.VendedorView" %>
<%@ Register Src="../componentes/controles/DetalheConsultaPadrao.ascx" TagName="DetalheConsultaPadrao" TagPrefix="uc1" %>

<asp:Label ID="lblFound" runat="server" Visible="False" Text="False" />

<asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
    
    <ContentTemplate>
    
        <asp:Panel ID="pnlEdicao" runat="server" Visible="true" CssClass="inlineButton">
            <asp:TextBox ID="txtCodigo" runat="server" Columns="6" MaxLength="6" AutoPostBack="True" />
            <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" />
            <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" Visible="False"
                ToolTip="Não encontrado" />
            <asp:Label ID="lblNome" runat="server" />
        </asp:Panel>

        <uc1:DetalheConsultaPadrao ID="oDetalhe" runat="server" Procedure="Orcamento.Vendedor.ctlVendedor" />

    </ContentTemplate>

</asp:UpdatePanel>
