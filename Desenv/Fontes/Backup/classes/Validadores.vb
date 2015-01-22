Imports System.Math

Namespace Genericas

    ''' <summary>
    ''' Obtem regras de negócio de itens que são imprescindíveis em um cadastro
    ''' </summary>
    ''' <remarks></remarks>

    Public Class Validadores

        ''' <summary>
        ''' Verifica se o valor informado é ou não é um cpf válido
        ''' </summary>
        ''' <param name="texto">Recebe o número do CPF com ou sem formatação</param>
        ''' <returns>Retorna se o cpf é válido ou inválido</returns>
        ''' <remarks>
        ''' Fórmula para cálculo de dígitos verificadores do CPF válido
        ''' Descrição
        ''' No Brasil existe o CPF (Cadastro de Pessoas Físicas) que serve para identificar cada indivíduo no país. O número do CPF é composto de 11 dígitos, sendo os dois últimos os dígitos de verificação. A fórmula para verificar a validade do número do CPF é simples e é explicada abaixo:
        '''
        ''' Vamos tomar como exemplo o número 123.456.789-09
        '''
        ''' 1º Dígito Verificador
        ''' Primeiro calculamos a soma da multiplicação dos 9 primeiros dígitos por 10, 9, 8, ... , 3, 2, respectivamente. Ou seja
        ''' Soma = (1*10) + (2*9) + ... + (8*3) + (9*2)
        '''
        ''' Em seguida, dividimos e multiplicamos por 11. (Nota: Ao multiplicarmos utilizamos o valor inteiro da divisão).
        ''' Valor = (Soma/11) * 11
        '''
        ''' Por fim, subtraímos Valor de Soma.
        ''' Resultado = Soma - Valor
        '''
        ''' Note que acabamos de realizar o módulo de Soma e 11. As duas operações anteriores podem ser substituídas por Resultado = Soma módulo 11. 
        '''
        ''' Agora analisamos Resultado:
        ''' •	Se Resultado for igual à 1 ou à 0, então o 1º dígito verificador é 0.
        ''' •	Caso contrário, o 1º dígito verificador é o resultado da subtração de Resultado de 11.
        ''' 
        ''' 2º Dígito Verificador
        ''' Primeiro calculamos a soma da multiplicação dos 9 primeiros dígitos por 11, 10, 9, ... , 4, 3, respectivamente e em seguida somamos com (Digito1*2), sendo que Digito1 é o valor encontrado para o 1º dígito verificador. Ou seja
        ''' Soma = (1*11) + (2*10) + ... + (8*4) + (9*3) + (Digito1*2)
        '''
        ''' O resto é semelhante ao que foi feito anteriormente. Dividimos e multiplicamos por 11. (Nota: Ao multiplicarmos utilizamos o valor inteiro da divisão).
        ''' Valor = (Soma/11) * 11
        '''
        ''' Por fim, subtraímos Valor de Soma.
        ''' Resultado = Soma - Valor
        '''
        ''' Agora analisamos Resultado:
        ''' •	Se Resultado for igual à 1 ou à 0, então o 2º dígito verificador é 0.
        ''' •	Caso contrário, o 2º dígito verificador é o resultado da subtração de Resultado de 11.
        ''' No nosso exemplo (123.456.789-09) o número é válido.
        ''' </remarks>
        Public Function isCPF(ByVal texto As String) As Boolean
            Dim cpf(11), i, dv1, dv2, cont, aux, soma, valor As Integer
            texto = texto.Trim()
            texto = texto.Replace(".", "")
            texto = texto.Replace("-", "")
            If Not IsNumeric(texto) OrElse texto.Length <> 11 Then
                Return False
            End If
            For i = 0 To 10
                cpf(i) = CInt(texto.Substring(i, 1))
            Next
            'Verificação do 1º Dígito
            cont = 10
            soma = 0
            For i = 0 To 8
                aux = cont * cpf(i)
                soma = soma + aux
                cont = cont - 1
            Next
            valor = CInt(Floor(soma / 11))
            valor = valor * 11
            dv1 = soma - valor
            If dv1 = 0 OrElse dv1 = 1 Then
                dv1 = 0
            Else
                dv1 = 11 - dv1
            End If
            'Verificação do 2º Dígito
            cont = 11
            soma = 0
            For i = 0 To 8
                aux = cont * cpf(i)
                soma = soma + aux
                cont = cont - 1
            Next
            soma = soma + (dv1 * 2)
            valor = CInt(Floor(soma / 11))
            valor = valor * 11
            dv2 = soma - valor
            If dv2 = 0 OrElse dv2 = 1 Then
                dv2 = 0
            Else
                dv2 = 11 - dv2
            End If
            'Resultado
            If dv1 = cpf(9) AndAlso dv2 = cpf(10) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Verifica se o valor informado é ou não é um CNPJ válido
        ''' </summary>
        ''' <param name="texto">Recebe o número do CNPJ com ou sem formatação</param>
        ''' <returns>Retorna se o cnpj é válido ou inválido</returns>
        ''' <remarks></remarks>
        Public Function isCNPJ(ByVal texto As String) As Boolean
            Dim cnpj(14), i, dig1, dig2, soma1, soma2, valor1, valor2 As Integer
            texto = texto.Trim()
            texto = texto.Replace(".", "")
            texto = texto.Replace("-", "")
            texto = texto.Replace("/", "")
            If Not IsNumeric(texto) Or texto.Length <> 14 Then
                Return False
            End If
            For i = 0 To 13
                cnpj(i) = CInt(texto.Substring(i, 1))
            Next
            'Verificação do 1º Dígito
            soma1 = ((cnpj(0) * 5) + _
                     (cnpj(1) * 4) + _
                     (cnpj(2) * 3) + _
                     (cnpj(3) * 2) + _
                     (cnpj(4) * 9) + _
                     (cnpj(5) * 8) + _
                     (cnpj(6) * 7) + _
                     (cnpj(7) * 6) + _
                     (cnpj(8) * 5) + _
                     (cnpj(9) * 4) + _
                     (cnpj(10) * 3) + _
                     (cnpj(11) * 2))
            valor1 = soma1 Mod 11
            If (valor1 < 2) Then
                dig1 = 0
            Else
                dig1 = 11 - valor1
            End If
            'Verificação do 2º Dígito
            soma2 = ((cnpj(0) * 6) + _
                     (cnpj(1) * 5) + _
                     (cnpj(2) * 4) + _
                     (cnpj(3) * 3) + _
                     (cnpj(4) * 2) + _
                     (cnpj(5) * 9) + _
                     (cnpj(6) * 8) + _
                     (cnpj(7) * 7) + _
                     (cnpj(8) * 6) + _
                     (cnpj(9) * 5) + _
                     (cnpj(10) * 4) + _
                     (cnpj(11) * 3) + _
                     (dig1 * 2))
            valor2 = soma2 Mod 11
            If valor2 < 2 Then
                dig2 = 0
            Else
                dig2 = 11 - valor2
            End If
            'Resultado
            If dig1 = cnpj(12) And dig2 = cnpj(13) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Verifica se o valor informado é uma inscrição estadual válida de acordo com o seu respectivo estado
        ''' </summary>
        ''' <param name="pUF">Recebe a sigla do estado</param>
        ''' <param name="pInscr">Recebe o valor da inscrição estadual</param>
        ''' <returns>Retorna se a inscrição estadual é válido ou inválido</returns>
        ''' <remarks></remarks>
        Public Function isInscricaoEstadual(ByVal pUF As String, ByVal pInscr As String) As Boolean
            isInscricaoEstadual = False
            Dim strBase As String
            Dim strBase2 As String
            Dim strOrigem As String
            Dim strDigito1 As String
            Dim strDigito2 As String
            Dim intPos As Integer
            Dim intValor As Integer
            Dim intSoma As Integer
            Dim intResto As Integer
            Dim intNumero As Integer
            Dim intPeso As Integer
            Dim intDig As Integer
            strBase = ""
            strBase2 = ""
            strOrigem = ""
            If Trim(pInscr) = "ISENTO" Then
                isInscricaoEstadual = True
                Exit Function
            End If
            For intPos = 1 To Len(Trim(pInscr))
                If InStr(1, "0123456789P", Mid$(pInscr, intPos, 1), vbTextCompare) > 0 Then
                    strOrigem = strOrigem & Mid$(pInscr, intPos, 1)
                End If
            Next
            Select Case pUF
                Case "AC"    ' Acre
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "01" And Mid$(strBase, 3, 2) <> "00" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "AL"    ' Alagoas
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "24" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intSoma = intSoma * 10
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto = 10, "0", Str(intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "AM"    ' Amazonas
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    If intSoma < 11 Then
                        strDigito1 = Right(Str(11 - intSoma), 1)
                    Else
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    End If
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "AP"    ' Amapa
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intPeso = 0
                    intDig = 0
                    If Left(strBase, 2) = "03" Then
                        intNumero = CInt(Val(Left(strBase, 8)))
                        If intNumero >= 3000001 And _
                        intNumero <= 3017000 Then
                            intPeso = 5
                            intDig = 0
                        ElseIf intNumero >= 3017001 And _
                            intNumero <= 3019022 Then
                            intPeso = 9
                            intDig = 1
                        ElseIf intNumero >= 3019023 Then
                            intPeso = 0
                            intDig = 0
                        End If
                        intSoma = intPeso
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        intValor = 11 - intResto
                        If intValor = 10 Then
                            intValor = 0
                        ElseIf intValor = 11 Then
                            intValor = intDig
                        End If
                        strDigito1 = Right(Str(intValor), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "BA"    ' Bahia
                    strBase = Left(Trim(strOrigem) & "00000000", 8)
                    If InStr(1, "0123458", Left(strBase, 1), vbTextCompare) > 0 Then
                        intSoma = 0
                        For intPos = 1 To 6
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (8 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 10
                        strDigito2 = Right(CStr(IIf(intResto = 0, "0", Str(10 - intResto))), 1)
                        strBase2 = Left(strBase, 6) & strDigito2
                        intSoma = 0
                        For intPos = 1 To 7
                            intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                            intValor = intValor * (9 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 10
                        strDigito1 = Right(CStr(IIf(intResto = 0, "0", Str(10 - intResto))), 1)
                    Else
                        intSoma = 0
                        For intPos = 1 To 6
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (8 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito2 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 6) & strDigito2
                        intSoma = 0
                        For intPos = 1 To 7
                            intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                            intValor = intValor * (9 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    End If
                    strBase2 = Left(strBase, 6) & strDigito1 & strDigito2
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "CE"    ' Ceara
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    intValor = 11 - intResto
                    If intValor > 9 Then
                        intValor = 0
                    End If
                    strDigito1 = Right(Str(intValor), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "DF"    ' Distrito Federal
                    strBase = Left(Trim(strOrigem) & "0000000000000", 13)
                    If Left(strBase, 3) = "073" Then
                        intSoma = 0
                        intPeso = 2
                        For intPos = 11 To 1 Step -1
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso > 9 Then
                                intPeso = 2
                            End If
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 11) & strDigito1
                        intSoma = 0
                        intPeso = 2
                        For intPos = 12 To 1 Step -1
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso > 9 Then
                                intPeso = 2
                            End If
                        Next
                        intResto = intSoma Mod 11
                        strDigito2 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 12) & strDigito2
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "ES"    ' Espirito Santo
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "GO"    ' Goias
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If InStr(1, "10,11,15", Left(strBase, 2), vbTextCompare) > 0 Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        If intResto = 0 Then
                            strDigito1 = "0"
                        ElseIf intResto = 1 Then
                            intNumero = CInt(Val(Left(strBase, 8)))
                            strDigito1 = Right(CStr(IIf(intNumero >= 10103105 And intNumero <= 10119997, "1", "0")), 1)
                        Else
                            strDigito1 = Right(Str(11 - intResto), 1)
                        End If
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "MA"    ' Maranhão
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "12" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "MT"    ' Mato Grosso
                    strBase = Left(Trim(strOrigem) & "0000000000", 10)
                    intSoma = 0
                    intPeso = 2
                    For intPos = 10 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 9 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 10) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "MS"    ' Mato Grosso do Sul
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "28" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "MG"    ' Minas Gerais
                    strBase = Left(Trim(strOrigem) & "0000000000000", 13)
                    strBase2 = Left(strBase, 3) & "0" & Mid$(strBase, 4, 8)
                    intNumero = 2
                    For intPos = 1 To 12
                        intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                        intNumero = CInt(IIf(intNumero = 2, 1, 2))
                        intValor = intValor * intNumero
                        If intValor > 9 Then
                            strDigito1 = Format(intValor, "00")
                            intValor = CInt(Val(Left(strDigito1, 1)) + _
                                          Val(Right(strDigito1, 1)))
                        End If
                        intSoma = intSoma + intValor
                    Next
                    intValor = intSoma
                    While Right(Format(intValor, "000"), 1) <> "0"
                        intValor = intValor + 1
                    End While
                    strDigito1 = Right(Format(intValor - intSoma, "00"), 1)
                    strBase2 = Left(strBase, 11) & strDigito1
                    intSoma = 0
                    intPeso = 2
                    For intPos = 12 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 11 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    strDigito2 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = strBase2 & strDigito2
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "PA"    ' Para
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "15" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "PB"    ' Paraiba
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    intValor = 11 - intResto
                    If intValor > 9 Then
                        intValor = 0
                    End If
                    strDigito1 = Right(Str(intValor), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "PE"    ' Pernambuco
                    strBase = Left(Trim(strOrigem) & "00000000000000", 14)
                    intSoma = 0
                    intPeso = 2
                    For intPos = 13 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 9 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    intValor = 11 - intResto
                    If intValor > 9 Then
                        intValor = intValor - 10
                    End If
                    strDigito1 = Right(Str(intValor), 1)
                    strBase2 = Left(strBase, 13) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "PI"    ' Piaui
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "PR"    ' Parana
                    strBase = Left(Trim(strOrigem) & "0000000000", 10)
                    intSoma = 0
                    intPeso = 2
                    For intPos = 8 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 7 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    intSoma = 0
                    intPeso = 2
                    For intPos = 9 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 7 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    strDigito2 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = strBase2 & strDigito2
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "RJ"    ' Rio de Janeiro
                    strBase = Left(Trim(strOrigem) & "00000000", 8)
                    intSoma = 0
                    intPeso = 2
                    For intPos = 7 To 1 Step -1
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * intPeso
                        intSoma = intSoma + intValor
                        intPeso = intPeso + 1
                        If intPeso > 7 Then
                            intPeso = 2
                        End If
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 7) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "RN"    ' Rio Grande do Norte
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "20" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intSoma = intSoma * 10
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto > 9, "0", Str(intResto))), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "RO"    ' Rondonia
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    strBase2 = Mid$(strBase, 4, 5)
                    intSoma = 0
                    For intPos = 1 To 5
                        intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                        intValor = intValor * (7 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    intValor = 11 - intResto
                    If intValor > 9 Then
                        intValor = intValor - 10
                    End If
                    strDigito1 = Right(Str(intValor), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "RR"    ' Roraima
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    If Left(strBase, 2) = "24" Then
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 9
                        strDigito1 = Right(Str(intResto), 1)
                        strBase2 = Left(strBase, 8) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "RS"    ' Rio Grande do Sul
                    strBase = Left(Trim(strOrigem) & "0000000000", 10)
                    intNumero = CInt(Val(Left(strBase, 3)))
                    If intNumero > 0 And intNumero < 468 Then
                        intSoma = 0
                        intPeso = 2
                        For intPos = 9 To 1 Step -1
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso > 9 Then
                                intPeso = 2
                            End If
                        Next
                        intResto = intSoma Mod 11
                        intValor = 11 - intResto
                        If intValor > 9 Then
                            intValor = 0
                        End If
                        strDigito1 = Right(Str(intValor), 1)
                        strBase2 = Left(strBase, 9) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
                Case "SC"    ' Santa Catarina
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "SE"    ' Sergipe
                    strBase = Left(Trim(strOrigem) & "000000000", 9)
                    intSoma = 0
                    For intPos = 1 To 8
                        intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                        intValor = intValor * (10 - intPos)
                        intSoma = intSoma + intValor
                    Next
                    intResto = intSoma Mod 11
                    intValor = 11 - intResto
                    If intValor > 9 Then
                        intValor = 0
                    End If
                    strDigito1 = Right(Str(intValor), 1)
                    strBase2 = Left(strBase, 8) & strDigito1
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "SP"    ' São Paulo
                    If Left(strOrigem, 1) = "P" Then
                        strBase = Left(Trim(strOrigem) & "0000000000000", 13)
                        strBase2 = Mid$(strBase, 2, 8)
                        intSoma = 0
                        intPeso = 1
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso = 2 Then
                                intPeso = 3
                            End If
                            If intPeso = 9 Then
                                intPeso = 10
                            End If
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(Str(intResto), 1)
                        strBase2 = Left(strBase, 8) & strDigito1 & Mid$(strBase, 11, 3)
                    Else
                        strBase = Left(Trim(strOrigem) & "000000000000", 12)
                        intSoma = 0
                        intPeso = 1
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso = 2 Then
                                intPeso = 3
                            End If
                            If intPeso = 9 Then
                                intPeso = 10
                            End If
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(Str(intResto), 1)
                        strBase2 = Left(strBase, 8) & strDigito1 & Mid$(strBase, 10, 2)
                        intSoma = 0
                        intPeso = 2
                        For intPos = 11 To 1 Step -1
                            intValor = CInt(Val(Mid$(strBase, intPos, 1)))
                            intValor = intValor * intPeso
                            intSoma = intSoma + intValor
                            intPeso = intPeso + 1
                            If intPeso > 10 Then
                                intPeso = 2
                            End If
                        Next
                        intResto = intSoma Mod 11
                        strDigito2 = Right(Str(intResto), 1)
                        strBase2 = strBase2 & strDigito2
                    End If
                    If strBase2 = strOrigem Then
                        isInscricaoEstadual = True
                    End If
                Case "TO"    ' Tocantins
                    strBase = Left(Trim(strOrigem) & "00000000000", 11)
                    If InStr(1, "01,02,03,99", Mid$(strBase, 3, 2), vbTextCompare) > 0 Then
                        strBase2 = Left(strBase, 2) & Mid$(strBase, 5, 6)
                        intSoma = 0
                        For intPos = 1 To 8
                            intValor = CInt(Val(Mid$(strBase2, intPos, 1)))
                            intValor = intValor * (10 - intPos)
                            intSoma = intSoma + intValor
                        Next
                        intResto = intSoma Mod 11
                        strDigito1 = Right(CStr(IIf(intResto < 2, "0", Str(11 - intResto))), 1)
                        strBase2 = Left(strBase, 10) & strDigito1
                        If strBase2 = strOrigem Then
                            isInscricaoEstadual = True
                        End If
                    End If
            End Select
        End Function

    End Class

    Public Class Formatadores

        ''' <summary>
        ''' Essa função recebe o número do CNPJ e retorna o valor formatado
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns>Retorna o número do CNPJ formatado</returns>
        Public Function FormatarCNPJ(ByVal texto As String) As String
            Dim cont As Integer = 0
            Dim cnpj As String = Nothing
            While cont < texto.Length
                cnpj = cnpj & texto.Chars(cont)
                If cnpj.Length = 2 Or cnpj.Length = 6 Then
                    cnpj = cnpj & "."
                ElseIf cnpj.Length = 10 Then
                    cnpj = cnpj & "/"
                ElseIf cnpj.Length = 15 Then
                    cnpj = cnpj & "-"
                End If
                cont += 1
            End While
            Return cnpj
        End Function

        ''' <summary>
        ''' Essa função recebe o número do CNAE e retorna o valor formatado
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns>Retorna o número do CNAE formatado</returns>
        ''' <remarks></remarks>
        Public Function FormatarCNAE(ByVal texto As String) As String
            Dim cont As Integer = 0
            Dim cnae As String = Nothing
            'If Not texto.Contains("/") AndAlso Not texto.Contains("-") Then
            texto = texto.Replace("/", "").Replace("-", "")
            While cont < texto.Length
                cnae = cnae & texto.Chars(cont)
                If cnae.Length = 4 Then
                    cnae = cnae & "-"
                ElseIf cnae.Length = 6 Then
                    cnae = cnae & "/"
                End If
                cont += 1
            End While
            Return cnae
            'Else
            'Return texto
            'End If
        End Function

        ''' <summary>
        ''' Essa função recebe o número do CEP e retorna o valor formatado
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns>Retorna o número do CEP dormatado</returns>
        ''' <remarks></remarks>
        Public Function FormatarCEP(ByVal texto As String) As String
            Dim cep As String = Nothing
            texto = texto.Replace("/", "").Replace("-", "").Trim
            If texto.Length = 8 Then
                cep = texto.Substring(0, 5) + "-" + texto.Substring(5, 3)
            Else
                cep = texto
            End If
            Return cep
        End Function

    End Class

End Namespace
