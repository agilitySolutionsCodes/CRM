<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Logotipos.aspx.vb" Inherits="Orcamento.Logotipos" %>

<%@ Register Src="controlesespecificos/ProdutoBox.ascx" TagName="ProdutoBox" TagPrefix="uc1" %>
<%@ Register Src="controlesespecificos/DescricaoDetalhada.ascx" TagName="DescricaoDetalhada"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orçamento</title>
    <style type="text/css">
        .corpo
        {
            font-family: Calibri;
            font-size: 11px;
            width: 800px;
            margin-left: 30px;
            margin-top: 30px;
            margin-right: 30px;
            margin-bottom: 30px;
            background-color: white;
            color: black;
            font-weight: normal;
        }
        .cabecalho
        {
            width: 800px;
        }
        
        .conteudo
        {
            width: 800px;
        }
        .rodape
        {
            width: 800px;
        }
        .itens
        {
            width: 800px;
        }
        .style1
        {
            width: 629px;
        }
        .style2
        {
            text-align: right;
        }
    </style>
</head>
<body class="corpo">
    <form id="frm" runat="server">
    <table id="tbCabecalho" class="cabecalho">
        <tr>
            <td class="style1">
                <asp:Image ID="imgLogotipo" runat="server" ImageUrl="logos/01617634000150.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="imgLogoIntermed" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="lblCNPJ" runat="server">01.617.634/0001-50</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="lblCNPJIntermed" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>

        <tr>
            <td class="style1">
                <asp:Image ID="Image1" runat="server" ImageUrl="logos/04933239000175.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label1" runat="server">04.933.239/0001-75</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label2" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image3" runat="server" ImageUrl="logos/05000571000140.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label3" runat="server">05.000.571/0001-40</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label4" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image5" runat="server" ImageUrl="logos/05316114000169.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label5" runat="server">05.316.114/0001-69</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label6" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image7" runat="server" ImageUrl="logos/06269451000105.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label7" runat="server">06.269.451/0001-05</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label8" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image9" runat="server" ImageUrl="logos/07311489000161.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label9" runat="server">07.311.489/0001-61</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label10" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image11" runat="server" ImageUrl="logos/08026041000169.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label11" runat="server">08.026.041/0001-69</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label12" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image13" runat="server" ImageUrl="logos/10404338000162.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label13" runat="server">10.404.338/0001-62</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label14" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image15" runat="server" ImageUrl="logos/10795899000130.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label15" runat="server">10.795.899/0001-30</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label16" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image17" runat="server" ImageUrl="logos/11619992000156.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image18" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label17" runat="server">11.619.992/0001-56</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label18" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image19" runat="server" ImageUrl="logos/12853727000109.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image20" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label19" runat="server">12.853.727/0001-09</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label20" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image21" runat="server" ImageUrl="logos/41784372000133.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image22" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label21" runat="server">41.784.372/0001-33</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label22" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image29" runat="server" ImageUrl="logos/63359863000170.jpg" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image30" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label29" runat="server">63.359.863/0001-70</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label30" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image31" runat="server" ImageUrl="logos/68653344000133.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image32" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label31" runat="server">68.653.344/0001-33</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label32" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:Image ID="Image23" runat="server" ImageUrl="logos/71631238000163.JPG" />
                &nbsp;
            </td>
            <td class="style2">
                <asp:Image ID="Image24" runat="server" ImageUrl="~/App_Themes/Intermed/Logo.png" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                CNPJ:
                <asp:Label ID="Label23" runat="server">71.631.238/0001-63</asp:Label>
            </td>
            <td class="style2">
                CNPJ:
                <asp:Label ID="Label24" runat="server" Text="49.520.521/0001-69"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        

    </table>
    </form>
</body>
</html>
