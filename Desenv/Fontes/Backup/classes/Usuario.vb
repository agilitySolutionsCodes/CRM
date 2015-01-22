Namespace Dados
    Public Class Usuario
        Dim sFilial As String = ""
        Dim sEmpresa As String = ""
        Dim sUserName As String = ""
        Dim sUserCode As String = ""
        Dim sUserFullName As String = ""
        Dim sDepartamento As String = ""
        Dim sMatricula As String = ""
        Dim sHash As String = ""
        Public Perfis As New List(Of String)
        Public Sub New()

        End Sub
        Public Sub New(ByVal oObjSiga As Object)
            sUserFullName = CStr(CallByName(oObjSiga, "Nome", CallType.Get))
            sUserCode = CStr(CallByName(oObjSiga, "UserId", CallType.Get))
            Dim oGrupos As String() = DirectCast(CallByName(oObjSiga, "Grupos", CallType.Get), String())
            For Each it As String In oGrupos
                Perfis.Add(it)
            Next
            sUserName = CStr(CallByName(oObjSiga, "UserName", CallType.Get))
            Dim oObj As Object = CallByName(oObjSiga, "Token", CallType.Get)
            sHash = CStr(CallByName(oObj, "Senha", CallType.Get))
        End Sub
        Public Property Filial() As String
            Get
                Return sFilial
            End Get
            Set(ByVal value As String)
                sFilial = value
            End Set
        End Property
        Public Property Empresa() As String
            Get
                Return sEmpresa
            End Get
            Set(ByVal value As String)
                sEmpresa = value
            End Set
        End Property
        Public Property UserCode() As String
            Get
                Return sUserCode
            End Get
            Set(ByVal value As String)
                sUserCode = value
            End Set
        End Property
        Public Property UserName() As String
            Get
                Return sUserName
            End Get
            Set(ByVal value As String)
                sUserName = value
            End Set
        End Property
        Public Property UserFullName() As String
            Get
                Return sUserFullName
            End Get
            Set(ByVal value As String)
                sUserFullName = value
            End Set
        End Property
        Public Property Departamento() As String
            Get
                Return sDepartamento
            End Get
            Set(ByVal value As String)
                sDepartamento = value
            End Set
        End Property
        Public Property Matricula() As String
            Get
                Return sMatricula
            End Get
            Set(ByVal value As String)
                sMatricula = value
            End Set
        End Property
        Public ReadOnly Property Hash As String
            Get
                Return sHash
            End Get
        End Property
    End Class
End Namespace