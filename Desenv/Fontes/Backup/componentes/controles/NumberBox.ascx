<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NumberBox.ascx.vb"
    Inherits="Orcamento.NumberBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtNumberBox" runat="server"></asp:TextBox>
<cc1:NumericUpDownExtender ID="txtNumberBox_NumericUpDownExtender" runat="server"
    Enabled="False" Maximum="999999" Minimum="0" RefValues="" ServiceDownMethod=""
    ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="imgDown" TargetButtonUpID="imgUp"
    TargetControlID="txtNumberBox">
</cc1:NumericUpDownExtender>
<cc1:FilteredTextBoxExtender ID="txtNumberBox_FilteredTextBoxExtender" runat="server"
    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtNumberBox">
</cc1:FilteredTextBoxExtender>
<span runat="server" id="spanBotoes">
    <img id="imgDown" alt="" name="imgDown" src="/imagens/down.gif" style="height: 40px;
        width: 40px; cursor: hand;" />
    <img id="imgUp" alt="" name="imgUp" src="/imagens/up.gif" style="height: 40px; width: 40px;
        cursor: hand;" />
</span>