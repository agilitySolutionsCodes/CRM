<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DateBox.ascx.vb" Inherits="Orcamento.DateBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtDateBox" runat="server" CausesValidation="True" Columns="10"
    MaxLength="10" CssClass="custom-calendar"></asp:TextBox>
<cc1:MaskedEditExtender ID="txtDateBox_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
    CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
    CultureTimePlaceholder="" Enabled="True" TargetControlID="txtDateBox" Mask="99/99/9999"
    AutoComplete="False" ClearMaskOnLostFocus="False" MaskType="Date">
</cc1:MaskedEditExtender>
<cc1:CalendarExtender ID="txtDateBox_CalendarExtender" runat="server" ClearTime="True"
    DaysModeTitleFormat="MM/yyyy" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateBox"
    TodaysDateFormat="dd/MM/yyyy" >
</cc1:CalendarExtender>
