Public Class BaseWebUI
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        'se o div de Aguarde ainda estiver mostrando ele tira
        Dim src As ScriptManager = ScriptManager.GetCurrent(Page)
        If src IsNot Nothing Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(System.Void), "TiraDivAguarde", "if(document.getElementById('divProcessando')) document.getElementById('divProcessando').style.display = 'none';", True)
        Else
            ClientScript.RegisterStartupScript(GetType(Page), "TiraDivAguarde", "if(document.getElementById('divProcessando'))document.getElementById('divProcessando').style.display = 'none';", True)
        End If
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "zerarfiltro", "if(document.getElementById('divProcessando') && document.getElementById('divProcessando').style.display!='none')return false;")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "Aguarde", "if (typeof(ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false) return false; avisoAguarde();")
        MyBase.OnInit(e)
    End Sub

End Class
