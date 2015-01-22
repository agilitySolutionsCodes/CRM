Public Class RetornoGenerico
    Dim nCodigo As Integer = 0
    Dim bSucesso As Boolean = True
    Dim sMensagem As String = ""
    Dim sChave As String = ""
    Public Property Codigo() As Integer
        Get
            Return nCodigo
        End Get
        Set(ByVal value As Integer)
            nCodigo = value
        End Set
    End Property
    Public Property Mensagem() As String
        Get
            Return sMensagem
        End Get
        Set(ByVal value As String)
            sMensagem = value
        End Set
    End Property

    Public Property Sucesso() As Boolean
        Get
            Return bSucesso
        End Get
        Set(ByVal value As Boolean)
            bSucesso = value
        End Set
    End Property
    Public Property Chave() As String
        Get
            Return sChave
        End Get
        Set(ByVal value As String)
            sChave = value
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return sMensagem
    End Function
    Public Sub New()

    End Sub
    Public Sub New(ByVal oRetSiga As Object)
        ReadRetornoSiga(oRetSiga)
    End Sub
    Public Sub ReadRetornoSiga(ByVal oRetSiga As Object)
        bSucesso = CBool(CallByName(oRetSiga, "Sucesso", CallType.Get))
        sMensagem = CStr(CallByName(oRetSiga, "Mensagem", CallType.Get))
        sChave = CStr(CallByName(oRetSiga, "Chave", CallType.Get))
        nCodigo = CInt(CallByName(oRetSiga, "Codigo", CallType.Get))
    End Sub
End Class
