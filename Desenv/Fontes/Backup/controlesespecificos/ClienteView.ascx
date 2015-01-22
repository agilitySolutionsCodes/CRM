<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClienteView.ascx.vb" Inherits="Orcamento.ClienteView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../componentes/controles/DetalheConsultaPadrao.ascx" TagName="DetalheConsultaPadrao" TagPrefix="uc1" %>

<asp:Label ID="lblFound" runat="server" Visible="False" Text="False"/>

<asp:UpdatePanel ID="updCabecalho" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">

    <ContentTemplate>

        <asp:Panel ID="pnlEdicao" runat="server" CssClass="inlineButton">

            <asp:TextBox ID="txtCodigo" runat="server" Columns="6" MaxLength="6" Visible="False" />
            <asp:Label runat="server" ID="lblBarra" Text="/"/>
            <asp:TextBox ID="txtLoja" runat="server" Columns="2" MaxLength="2" Visible="False" />

            <asp:TextBox ID="txtCPF_CNPJ" runat="server" Columns="18" MaxLength="18" Visible="False" 
                         Width="170px" Height="16px" ValidationGroup="MKE" />

            <cc1:MaskedEditExtender ID="txtCnpj_MaskedEditExtender" runat="server"
                TargetControlID="txtCPF_CNPJ" 
                Mask="99,999,999/9999-99"
                MessageValidatorTip="true"
                OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError"
                MaskType="None"
                AcceptAMPM="False"
                ErrorTooltipEnabled="True" />
        
            <cc1:MaskedEditValidator ID="txtCnpj_MaskedEditValidator" runat="server"
                ControlExtender="txtCnpj_MaskedEditExtender"
                ControlToValidate="txtCPF_CNPJ"
                IsValidEmpty="False"
                EmptyValueMessage=""
                InvalidValueMessage=""
                Display="Static"
                TooltipMessage=""
                EmptyValueBlurredText=""
                InvalidValueBlurredMessage=""
                ValidationGroup="MKE"/>

            <asp:Button ID="btnBusca" runat="server" CssClass="Lupa" />
            <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/imagens/severidade.png" 
                       Visible="False" ToolTip="Não encontrado" />
            <asp:Label ID="lblNome" runat="server" Text="" CssClass="label2"/>

        </asp:Panel>

        <uc1:DetalheConsultaPadrao ID="oDetalhe" runat="server" Procedure="Orcamento.Cliente.CtlCliente" />               
    
    </ContentTemplate>

</asp:UpdatePanel>
