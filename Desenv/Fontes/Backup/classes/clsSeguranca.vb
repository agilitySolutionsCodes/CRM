Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports System
Public Class clsSeguranca
    Public Shared Sub CriarMenu(ByRef oMenu As Menu)
        Dim h As New XmlDataSource
        h.DataFile = IdentificarPerfil()
        If h.DataFile.ToString.Trim.Length = 0 Then
            Throw New Exception("Este usuário não possui perfil configurado no sistema.")
        Else
            'h.XPath = "/siteMap/siteMapNode"
            oMenu.StaticDisplayLevels = 3
            oMenu.DataSource = h
            oMenu.DataBind()
        End If
    End Sub
    Private Shared Function IdentificarPerfil() As String
        Dim sRet As String = ""
        Dim sValor As String = ""
        Dim h As New XmlDataSource
        Dim o As XmlDocument
        Dim oNode As XmlNode
        h.DataFile = "~/perfis.xml"
        h.DataBind()
        o = h.GetXmlDocument()
        For Each oNode In o.ChildNodes(1)
            If HttpContext.Current.User.IsInRole(oNode.Attributes("value").Value) Then
                sRet = oNode.Attributes("url").Value
                Exit For
            End If
        Next
        Return sRet
    End Function
    Public Shared Function ObterPaginaPreferencial() As String
        Dim sRet As String = ""
        Dim sValor As String = ""
        Dim h As New XmlDataSource
        Dim o As XmlDocument
        Dim oNode As XmlNode
        h.DataFile = "~/PaginaPreferencial.xml"
        h.DataBind()
        o = h.GetXmlDocument()
        For Each oNode In o.ChildNodes(1)
            If HttpContext.Current.User.IsInRole(oNode.Attributes("value").Value) Then
                sRet = oNode.Attributes("url").Value
                Exit For
            End If
        Next
        Return sRet
    End Function
    Public Shared Function ExibirPermissoes() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim sValor As String = ""
        Dim h As New XmlDataSource
        Dim o As XmlDocument
        Dim oNode As XmlNode

        dt.Columns.Add("CodigoPerfil")
        dt.Columns.Add("url")
        h.DataFile = "~/permissoes.xml"
        h.DataBind()
        o = h.GetXmlDocument()
        For Each oNode In o.ChildNodes(1)
            If HttpContext.Current.User.IsInRole(oNode.Attributes("value").Value) Or oNode.Attributes("value").Value = "0" Then
                dr = dt.NewRow()
                dr("CodigoPerfil") = oNode.Attributes("value").Value
                dr("url") = oNode.Attributes("url").Value.ToString()
                dt.Rows.Add(dr)
            End If
        Next
        Return dt
    End Function
    Public Shared Function ConfirmarPermissao(ByVal sPagina As String) As Boolean
        Dim sValor As String = ""
        Dim h As New XmlDataSource
        Dim o As XmlDocument
        Dim oNode As XmlNode
        Dim bRet As Boolean = False
        sPagina = ObterNomePagina(sPagina)
        h.DataFile = "~/permissoes.xml"
        h.DataBind()
        o = h.GetXmlDocument()
        For Each oNode In o.ChildNodes(1)
            If HttpContext.Current.User.IsInRole(oNode.Attributes("value").Value) Or oNode.Attributes("value").Value = "999999" Then
                If ObterNomePagina(oNode.Attributes("url").Value.ToString()) = sPagina Then
                    bRet = True
                    Exit For
                End If
            End If
        Next
        Return bRet
    End Function
    Private Shared Function ObterNomePagina(ByVal sPagina As String) As String
        Dim iFim As Integer = InStr(sPagina.ToUpper(), ".ASPX") + 4
        Dim sRet As String = sPagina.Substring(0, iFim)
        Dim iInicio As Integer = InStrRev(sRet, "/")
        Return sRet.Substring(iInicio).ToUpper()
    End Function
    Public Shared Function Autenticar(ByVal sUsername As String, ByVal sPassword As String) As wshumberto.wsusermicrosiga.AUTHSTRUCT
        Dim oWs As New wshumberto.wsusermicrosiga.WSUSERMICROSIGA
        Dim oToken As New wshumberto.wsusermicrosiga.TOKENSTRUCT
        Dim oAuth As New wshumberto.wsusermicrosiga.AUTHSTRUCT

        If Util.GetAmbiente().ToUpper = "DESENV" Then
            Dim oWsD As New wshumberto.wsusermicrosiga.WSUSERMICROSIGA
            Dim oTokenD As New wshumberto.wsusermicrosiga.TOKENSTRUCT
            Dim oAuthD As New wshumberto.wsusermicrosiga.AUTHSTRUCT
            oTokenD.CONTEUDO = sUsername
            oTokenD.SENHA = sPassword
            oAuthD = oWsD.AUTENTICAR(oTokenD)
            oAuth.RETORNO = New wshumberto.wsusermicrosiga.RETORNOSTRUCT
            oAuth.RETORNO.CHAVE = oAuthD.RETORNO.CHAVE
            oAuth.RETORNO.CODIGO = oAuthD.RETORNO.CODIGO
            oAuth.RETORNO.MENSAGEM = oAuthD.RETORNO.MENSAGEM
            oAuth.RETORNO.SUCESSO = oAuthD.RETORNO.SUCESSO
            oAuth.USUARIO = New wshumberto.wsusermicrosiga.USERSTRUCT
            oAuth.USUARIO.GRUPOS = oAuthD.USUARIO.GRUPOS
            oAuth.USUARIO.NOME = oAuthD.USUARIO.NOME
            oAuth.USUARIO.USERID = oAuthD.USUARIO.USERID
            oAuth.USUARIO.USERNAME = oAuthD.USUARIO.USERNAME
            oAuth.USUARIO.TOKEN = New wshumberto.wsusermicrosiga.TOKENSTRUCT
            If Not oAuthD.USUARIO.TOKEN Is Nothing Then
                oAuth.USUARIO.TOKEN.CONTEUDO = oAuthD.USUARIO.TOKEN.CONTEUDO
                oAuth.USUARIO.TOKEN.MENSAGEM = oAuthD.USUARIO.TOKEN.MENSAGEM
                oAuth.USUARIO.TOKEN.SENHA = oAuthD.USUARIO.TOKEN.SENHA
                oAuth.USUARIO.TOKEN.SESSIONID = oAuthD.USUARIO.TOKEN.SESSIONID
            End If
        Else
            oToken.CONTEUDO = sUsername
            oToken.SENHA = sPassword
            oAuth = oWs.AUTENTICAR(oToken)
        End If
        Return oAuth
    End Function
    Private Shared Function Hash() As Object
        Return New Object
    End Function
End Class
