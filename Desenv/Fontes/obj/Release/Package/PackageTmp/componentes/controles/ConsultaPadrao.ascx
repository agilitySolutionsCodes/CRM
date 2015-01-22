<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ConsultaPadrao.ascx.vb" Inherits="Orcamento.ConsultaPadrao" %>
<%@ Register Src="Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<%@ Register Src="AutoHideButton.ascx" TagName="AutoHideButton" TagPrefix="uc2" %>

<asp:Panel ID="pnlConsultaPadrao" runat="server">
    <asp:Panel ID="pnlParametros" runat="server" DefaultButton="btnPesquisar"> &nbsp;
        <asp:DropDownList ID="drpTipoPesquisa" runat="server" CssClass="ParameterStyle" /> &nbsp;
        <asp:TextBox ID="txtParametroPesquisa" runat="server" CssClass="ParameterStyle" /> &nbsp;
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="ButtonStyle" />
    </asp:Panel>
    <uc1:Mensagem ID="oMensagem" runat="server" />
    <asp:GridView ID="grdPesquisar" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateSelectButton="True" PageSize="20" CssClass="GridViewStyle">
        <HeaderStyle CssClass="Resultado HeaderStyle" />
        <AlternatingRowStyle CssClass="Resultado AlternatingRowStyle" />
        <RowStyle CssClass="Resultado RowStyle" />
    </asp:GridView>
</asp:Panel>
