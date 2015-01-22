<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DetalheConsultaPadrao.ascx.vb" Inherits="Orcamento.DetalheConsultaPadrao" %>
<%@ Register Src="ConsultaPadrao.ascx" TagName="ConsultaPadrao" TagPrefix="uc1" %>

<asp:UpdatePanel ID="updDetalhe" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="pnlConsulta" runat="server" Visible="false" CssClass="ConsultaPadrao">
            <asp:Panel ID="pnlTopBar" runat="server" CssClass="TopBar">
                <asp:ImageButton ID="imgFechar" runat="server" ImageAlign="Right" ImageUrl="~/imagens/fechar.jpg" /> &nbsp;
                <asp:Label ID="lblTopBar" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlConteudoConsulta" runat="server">
                <uc1:ConsultaPadrao ID="oConsulta" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
