Imports System
Imports System.IO
Imports System.Data

Partial Public Class Erro
    Inherits System.Web.UI.Page

    Private filePath As String
    Private fileStream As FileStream
    Private streamWriter As StreamWriter

    Public Sub AbreArquivoLog()
        Dim strPath As String
        Dim sArquivo As String = "\Erros_Orcamento.log"
        Dim strCaminho As String = Server.MapPath("~") & "\arquivos_xml_erros_orcamento"
        If Not Directory.Exists(strCaminho) Then
            Directory.CreateDirectory(strCaminho)
        End If
        strPath = strCaminho & sArquivo
        If File.Exists(strPath) Then
            If FileLen(strPath) >= 512000 Then '500KB 
                Dim strNewPath = strCaminho & "\Erros_orcamento_" & Date.Now.ToShortDateString.Replace("/", "") & ".log"
                If File.Exists(strNewPath) Then
                    File.Delete(strNewPath)
                End If
                File.Copy(strPath, strNewPath)
                fileStream = New FileStream(strPath, FileMode.Truncate, FileAccess.Write)
                fileStream.Close()
            End If
            fileStream = New FileStream(strPath, FileMode.Append, FileAccess.Write)
        Else
            fileStream = New FileStream(strPath, FileMode.Create, FileAccess.Write)
        End If
        streamWriter = New StreamWriter(fileStream)
    End Sub

    Public Sub EscreverLog(ByVal strComments As String)
        Dim sMsgErro As String
        If HttpContext.Current.Session("NomeUsuario") Is Nothing Then
            sMsgErro = "Erro: Session nome usuario em branco."
        Else
            sMsgErro = "Erro: " & HttpContext.Current.Session("NomeUsuario").ToString.Trim
        End If
        sMsgErro += " - " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & " --- " & strComments

        AbreArquivoLog()
        streamWriter.WriteLine(sMsgErro)
        CloseFile()
    End Sub

    Public Sub CloseFile()
        streamWriter.Close()
        fileStream.Close()
    End Sub

End Class