<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="Orcamento.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<HTML>

    <HEAD runat="server"><!--- JavaScript to operate the activeX object -->

        <script language="Javascript">

            function doprint() {
            //save existing user's info  
            var h = factory.printing.header;  
            var f =     factory.printing.footer;  
            //hide the button  
            document.all("printbtn").style.visibility = 'hidden';  
            //set header and footer to blank  
            factory.printing.header = "";  
            factory.printing.footer = "";  
            //print page without prompt  
            factory.DoPrint(false);  
            //restore user's info  
            factory.printing.header = h;  
            factory.printing.footer = f;
            //show the print button  
            document.all("printbtn").style.visibility = 'visible';
        }

       /*
        function PrintIt() {
            if (!factory.object) {
                alert("Erro ao tentar imprimir");
                return false;
            }
            cabecalho = factory.printing.header;
            rodape = factory.printing.footer;

            factory.printing.header = "";
            factory.printing.footer = "";

            window.print();

            factory.printing.header = cabecalho;
            factory.printing.footer = rodape;
        } 
        */

</script>

</HEAD>

<BODY runat="server">

<object id="factory" viewastext  style="display:none"  classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814"  codebase="./ScriptX.cab#Version=6,1,431,2"></object> 
<input type="button" name="printbtn" onClick="doprint()" value="Imprimir">

<!-- 
<object id="Object1" style="display:none"
    classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814" viewastext 
    codebase="../Includes/ScriptX.cab#Version=5,0,4,185">
</object>
<input type="button" name="printbtn" onClick="doprint()" value="doprint()">
-->

<!-- MeadCo Security Manager
<object style="display:none"
classid="clsid:5445be81-b796-11d2-b931-002018654e2e"
codebase="[path de instalação aqui]/smsx.cab#Version=6,1,432,1">
<param name="GUID" value="{67533199-D16A-46D3-BA23-5AA77981F726}">
<param name="Path" value="[path de instalação aqui]/sxlic.mlf">
<param name="Revision" value="0">
</object>

 MeadCo ScriptX
<object id=factory style="display:none"
classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814">
</object> -->

<!--
<script defer>
    function window.onload() {
        if (!factory.object) {
            alert("MeadCo's ScriptX Control is not properly installed!");
            navigate("scriptx-install-error.htm");
            return;
        }
        if (!secmgr.object) {
            alert("MeadCo's Security Manager Control is not properly installed!");
            navigate("secmgr-install-error.htm");
            return;
        }
        if (!secmgr.validLicense) {
            alert("The MeadCo Publishing License is invalid or has been declined by the user!");
            navigate("license-error.htm");
            return;
        }
        alert("Ready to script MeadCo's ScriptX!")
    }
</script>

<input type="button" name="printbtn" onClick="PrintIt()" value="Print">
-->
<br/>
The above code will create a button with the text "Print" if the user
clicks it, the printout will have no header/footer text added by the
browser.  The user will be prompted to install the activeX by a
security dialogue if their security settings are set to medium or high
on internet explorer.  However, they will only need to click yes the
first time they visit your page. 

</BODY>
</HTML>