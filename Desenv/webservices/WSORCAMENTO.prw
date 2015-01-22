#INCLUDE "APWEBSRV.CH"
#INCLUDE "PROTHEUS.CH"
#INCLUDE "TOPCONN.CH"
#define CRLF Chr(13)+Chr(10)

/*Dummy*/
User Function WSORCAMENTO()

//O:=WSCLASSNEW("TOKENSTRUCT")
//O1:=WSCLASSNEW("FICHAORCAMENTOSTRUCT")
//O2:=WSCLASSNEW("ItemOrcamentoStruct") 

Local oApont:=WSClassNew("FichaOrcamentoStruct")
Local aItens:={}
Local oItem:=WSClassNew("ItemOrcamentoStruct")
dDataBase:=Date()
oToken:=WSClassNew("TokenStruct")
oToken:Conteudo:=""

oApont:Numero:="000001"
//oApont:CodigoVendedor:="000063"
oApont:CodigoCliente:="005747"
oApont:LojaCliente :="01"
oApont:DataSolicitacao:=CTOD("01/04/10")
oApont:TipoOrcamento="PRI"
oApont:TipoSolicitacao:="V"
//oApont:OrcamentoAnterior:=.F.
oApont:CondicaoPagamento:="001"
oApont:Emissao:=date()
oApont:DataValidade:=date()+30

oItem:=WSClassNew("ItemOrcamentoStruct")
oItem:CodigoProduto:="155.000PC" //"177.00000"
oItem:Quantidade:=1
oItem:PrecoVenda:=10000
//oItem:PercentualDesconto:=1

aAdd(aItens, oItem)

oApont:Itens:=aItens

u_ValToken(oToken)
//o:=Orcamento():GerarOrcamento(oToken, oApont)
fMail("I", oApont )

Return Nil

WSService WSOrcamento DESCRIPTION "Serviços de Relacionados ao Orçamento"
	WSDATA Token                 As TokenStruct
	WSDATA Retorno			     As RetornoStruct
	WSDATA FichaOrcamento        As FichaOrcamentoStruct
	WSDATA NumeroOrcamento		 AS String
	WSMETHOD Gerar      		 DESCRIPTION "Método de geração de Orçamento" 
	WSMETHOD Alterar      		 DESCRIPTION "Método de alteração de Orçamento" 
	WSMETHOD Cancelar      		 DESCRIPTION "Método de cancelamento de Orçamento" 
	WSMETHOD Aprovar      		 DESCRIPTION "Método de aprovação de Orçamento" 
EndWSService

WSSTRUCT FichaOrcamentoStruct
	WSDATA Numero          as String Optional
	WSDATA FaturadoPor	   as String Optional
	WSDATA CodigoCliente   as String
	WSDATA LojaCliente     as String
	WSDATA Contato    	   as String 
	WSDATA DataSolicitacao as Date 
	WSDATA DataValidade    as Date
	WSDATA MotivoProrrogacao as String 
	WSDATA DetalheCondicao as String 
	WSDATA TipoOrcamento   as String
	WSDATA TipoSolicitacao as String 
	WSDATA PossibilidadeVenda as String
	WSDATA CondicaoPagamento as String   
	WSDATA Observacao as String
	WSDATA TipoFrete as String 
	WSDATA ValorFrete as Float
	WSDATA CodVendedor as String Optional
	WSDATA Emissao as Date Optional
	WSDATA IDUsuario as String Optional	  
	WSDATA Itens As Array Of ItemOrcamentoStruct
ENDWSSTRUCT
              
WSSTRUCT ItemOrcamentoStruct
    WSDATA CodigoProduto As String
    WSDATA Quantidade as Float
    WSDATA PrazoEntrega as Float 
    WSDATA PrazoGarantia as Float 
    WSDATA PrecoLista as Float
    WSDATA PercentualDesconto as Float
    WSDATA ValorDesconto as Float
    WSDATA PrecoVenda as Float
	WSDATA DescricaoDetalhada as String
END WSSTRUCT
/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Alterar             ³Autor  ³ Marcelo Piazza³ Data ³   02/2011 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de alteracao de orcamento                             ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Alterar WSRECEIVE Token, FichaOrcamento WSSEND Retorno WSSERVICE WSOrcamento
Local 	oRetorno:=WSClassNew("RetornoStruct") 
Local lRet:=.T.
Local aArea:={}
//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga

if u_ValToken(Token)
	aArea:=GetArea()
	oRetorno:=fAlterar(Token, FichaOrcamento)
	RestArea(aArea)
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
::Retorno:=oRetorno
RpcClearEnv()
Return(lRet)

/**/
static function fAlterar(Token, FichaOrcamento)
Local aCab:={}
Local aItens:={}
Local oRetorno:=WSClassNew("RetornoStruct")
Local cVend1:=""
Local cCliente:=""
Local cLoja:=""
Local n:=0
Local nTam:=0
Local cProduto:=""
Local nQtdVen:=0
Local nPrUnit:=0
Local nPrcVen:=0
Local nValor:= 0
Local dSolCli:=CTOD("  /  /  ")
Local cTipOrc:=""
Local cSolicit:=""
Local cPercent:=""
Local cCondPag:=""
Local cTPreco:=""
Local nDescont:=0
Local nValDesc:=0
Local cObserv:=""
Local cDetCond:=""
	//posicionar arquivos cj e ck
	oRetorno:Sucesso:=.T.
	oRetorno:Codigo:=0
	oRetorno:Mensagem:="Orçamento '" + FichaOrcamento:Numero + "' alterado com sucesso."
	oRetorno:Chave:=xfilial("SCJ") + FichaOrcamento:Numero
	
	SCJ->(DBSETORDER(1))
	SCJ->(DBSEEK(oRetorno:Chave))
	SCK->(DBSETORDER(1))
	SCK->(DBSEEK(oRetorno:Chave))
	dEmissao:=SCJ->CJ_EMISSAO
	
	if FichaOrcamento:CondicaoPagamento = "000" .AND. len(Alltrim(FichaOrcamento:DetalheCondicao)) = 0
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O detalhe da condição de pagamento deve ser informado quando a condição de pagamento selecionada é 'A combinar'."
		oRetorno:Chave:=xfilial("SCJ") + FichaOrcamento:Numero
	elseif FichaOrcamento:DataValidade > SCJ->CJ_VALIDI .AND.  (LEN(ALLTRIM(FichaOrcamento:MotivoProrrogacao)) = 0 .or. ALLTRIM(FichaOrcamento:MotivoProrrogacao) = "0")
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=1
		oRetorno:Mensagem:="O motivo da prorrogação deve ser informado."
		oRetorno:Chave:=xfilial("SCJ") + FichaOrcamento:Numero
	endif
	if oRetorno:Sucesso==.T.		
		aCab := {}
		aAdd(aCab, {"CJ_FILIAL"          	, xFilial("SCJ"), nil})
		aAdd(aCab, {"CJ_NUM"          	    , FichaOrcamento:Numero		    , nil})
		aAdd(aCab, {"CJ_MATRIC"          	, __cUserID, nil})   
		if  SCJ->CJ_TIPORC != FichaOrcamento:TipoOrcamento
			aAdd(aCab, {"CJ_TIPORC"			    , FichaOrcamento:TipoOrcamento		, nil})
		endif
		if  SCJ->CJ_PERCENT != FichaOrcamento:PossibilidadeVenda
			aAdd(aCab, {"CJ_PERCENT"     		, FichaOrcamento:PossibilidadeVenda      , nil})
		endif
		if  SCJ->CJ_CONDPAG !=FichaOrcamento:CondicaoPagamento
			if FichaOrcamento:CondicaoPagamento=="000"
				aAdd(aCab, {"CJ_CONDPAG"     		, FichaOrcamento:CondicaoPagamento      , ".T."})
			else
				aAdd(aCab, {"CJ_CONDPAG"     		, FichaOrcamento:CondicaoPagamento      , nil})
			endif
		endif
		if  SCJ->CJ_VALIDI != FichaOrcamento:DataValidade
			aAdd(aCab, {"CJ_VALIDI"          	, FichaOrcamento:DataValidade  		, nil})
		endif
		if  SCJ->CJ_TPRECO !=FichaOrcamento:TipoFrete
			aAdd(aCab, {"CJ_TPRECO"     		, FichaOrcamento:TipoFrete       , nil})
		endif
		if  SCJ->CJ_CPLCDPG != FichaOrcamento:DetalheCondicao
			aAdd(aCab, {"CJ_CPLCDPG"    		, FichaOrcamento:DetalheCondicao      , nil})
			aAdd(aCab, {"CJ_DETCOND"    		, FichaOrcamento:DetalheCondicao      , nil})
		endif
		if  SCJ->CJ_ACUI !=FichaOrcamento:Contato
			aadd(aCab, {"CJ_ACUI"     		    , FichaOrcamento:Contato, nil})
		endif
		if  SCJ->CJ_OBSERV !=FichaOrcamento:Observacao
			aAdd(aCab, {"CJ_OBSERV"     		, FichaOrcamento:Observacao       , nil})
		endif
		aAdd(aCab, {"CJ_MOTPRO"    		, FichaOrcamento:MotivoProrrogacao      , nil})
		aAdd(aCab, {"CJ_DULTALT"    		, dDatabase, nil})
		aItens:={}
		nTam :=len(FichaOrcamento:Itens)
		for n:=1 to nTam
			nQtdVen:=FichaOrcamento:Itens[n]:Quantidade
			nPrcVen:=FichaOrcamento:Itens[n]:PrecoVenda
			nPrUnit:=FichaOrcamento:Itens[n]:PrecoLista
			nDescont:=FichaOrcamento:Itens[n]:PercentualDesconto
			nValDesc:=FichaOrcamento:Itens[n]:ValorDesconto
			nValor  :=nQtdVen * nPrcVen
			IF nPrUnit==0
				nPrUnit:=nPrcVen
			endif
			aItem:={}
			aadd(aItem, {"CK_ITEM"		,StrZero(n,2),Nil})
			aadd(aItem, {"CK_PRODUTO"	,FichaOrcamento:Itens[n]:CodigoProduto	,NIL})
			aadd(aItem, {"CK_DESCDET"	,FichaOrcamento:Itens[n]:DescricaoDetalhada	,Nil})
			aadd(aItem, {"CK_QTDVEN"	,nQtdVen	,NIL})
			aadd(aItem, {"CK_PRCVEN"	,nPrcVen	,NIL})
			aadd(aItem, {"CK_PRUNIT"	,nPrUnit	,Nil})
			aadd(aItem, {"CK_VALOR"		,nValor		,Nil})
			if nDescont!=0
				aadd(aItem, {"CK_DESCONT"	,nDescont	,Nil})
			endif
			aadd(aItem, {"CK_ENTREG"	,dEmissao + FichaOrcamento:Itens[n]:PrazoEntrega		,Nil})
			aadd(aItem, {"CK_GARANT"	,FichaOrcamento:Itens[n]:PrazoGarantia	,Nil})
			aAdd(aItens, aItem)     
			conout(" Item sendo gravado "+FichaOrcamento:Itens[n]:DescricaoDetalhada)
		next
		//Begin Transaction
		lMsErroAuto := .F.
		MSExecAuto({|x,y,z|Mata415(x,y,z)},aCab,aItens,4)
		If lMsErroAuto
			DisarmTransaction()
			oRetorno:Sucesso:=.F.
			oRetorno:Codigo:=2
			oRetorno:Mensagem:="Erro ao atualizar orçamento: #" + MemoRead(NOMEAUTOLOG()) + "#."
			oRetorno:Chave:=""
			ferase(NOMEAUTOLOG())
		endif
		//End Transaction
		IF !lMsErroAuto
			TCSQLEXEC("UPDATE " + RETSQLNAME("SCJ") + " SET CJ_STATUS = 'D' WHERE D_E_L_E_T_ = '' AND CJ_FILIAL = '" + XFILIAL("SCJ") + "' AND CJ_NUM = '"+ FichaOrcamento:Numero +"'")
			//GrvGaran(FichaOrcamento, cDoc)
			if FichaOrcamento:CondicaoPagamento=="000"
				RECLOCK("SCJ")
				SCJ->CJ_CONDPAG=""
				SCJ->(MSUNLOCK())
			endif
			RECLOCK("SCJ")
			SCJ->CJ_MATRIC=""
			SCJ->(MSUNLOCK())
		Endif
	endif
return oRetorno

/**/
//Static Function fMail(cOper, cCNPJ, cNmCli, cNumero, dEmissao, cNmUsu, dValid ,nValor, aItens )
Static Function fMail(cOper, FichaOrcamento )
Local cPathLoja:="\Orcamento
Local cPathHtml:=cPathLoja+"\HTML"
Local cPathRes:=cPathLoja+"\Resources"
Local cArqRes:=cPathRes+"\Posicionamento.htm"
Local cArqHTML:=cPathHtml+"\Template_Cliente.html"
Local cContRes:=""
Local cContHtml:=""
Local cAssunto:="Orçamento Intermed no. " + FichaOrcamento:Numero
Local cTitulo:="Orçamento Intermed"
Local cAdmin:=""
Local i:=1
lOCAL nTotValor:=0
aCab:={}
aLinha:={}
aAdd(aLinha, "Produto")
aAdd(aLinha, "Quantidade")
aAdd(aLinha, "Preço")
aAdd(aLinha, "Total Item")
aAdd(aCab, aLinha)

aLinhas:={}
aItens:=FichaOrcamento:Itens
for i:=1 to len(aItens)
	aLinDet:={}
	aadd(aLinDet, aItens[i]:CodigoProduto + " - NomeProduto")
	aadd(aLinDet, transform(aItens[i]:Quantidade, "999,999,999.99"))
	aadd(aLinDet, transform(aItens[i]:PrecoVenda, "999,999,999.99"))
	aadd(aLinDet, transform(aItens[i]:Quantidade*aItens[i]:PrecoVenda, "999,999,999.99"))
	nTotValor:= nTotValor+aItens[i]:Quantidade*aItens[i]:PrecoVenda
	aadd(aLinhas, aLinDet)
next

cHead:='<TR>'
For n:=1 to len(aCab[1])
	cHead:=cHead+"<th><span>"+aCab[1,n]+"</span></th>"
Next
cHead:=cHead+'</TR>'
cTab:=""
For x:=1 to len(aLinhas)
	if mod(x, 2) != 0
		cLin:='<tr class="ProdutoCarrinhoItem">'
	Else
		cLin:='<tr class="ProdutoCarrinhoItemAlternativo">'
	Endif
	for y:=1 to len(aLinhas[x])
		cLin:=cLin+"<td><span>"+aLinhas[x,y]+"</span></td>"
	next
	cLin:=cLin+"</TR>"
	cTab:=cTab+cLin
Next
cRod:='<TR>'
For n:=1 to len(aCab[1])
	if n==1
		cRod:=cRod+"<td><span>Total</span></td>"
	elseif n==4
		cRod:=cRod+"<td><span>" + transform(nTotValor, "999,999,999.99") + "</span></td>"
	else
		cRod:=cRod+"<td><span>&nbsp;</span></td>"
	endif
Next
cRod:=cRod+'</TR>'

/*Dados do Orcamento*/
cContRes:=MEMOREAD(cArqRes)

cNmCLi:="--Nome Cliente"
cNmUsu:="--Nome Usuario"
cNmRep:="--Nome Representante"
cNmOper:="--Nome Operacao"

if cOper=="I"
	cNmOper:="Incluído"
elseif cOper=="A"
	cNmOper:="Alterado"
endif

cNmOper:="--Nome Operacao"


cContRes:=replace(cContRes, '%NumeroOrcamento%',FichaOrcamento:Numero)

cContRes:=replace(cContRes, '%NomeCliente%',cNmCLi)
cContRes:=replace(cContRes, '%NomeUsuario%',cNmUsu)
cContRes:=replace(cContRes, '%NomeRepresentante%',cNmRep)
cContRes:=replace(cContRes, '%DescricaoOperacao%',cNmOper)
cContRes:=replace(cContRes, '%Emissao%',DTOC(FichaOrcamento:Emissao))
cContRes:=replace(cContRes, '%Validade%',DTOC(FichaOrcamento:DataValidade))

/*itens*/
cContRes:=replace(cContRes, '%LinhaCabecalho%',cHead)
cContRes:=replace(cContRes, '%LinhasDetalhe%',cTab)
cContRes:=replace(cContRes, '%LinhaRodape%',cRod)

cContHTML:=MEMOREAD(cArqHTML)
cContHtml:=replace(cContHTML, '%Data%', DTOC(dDatabase))
cContHtml:=replace(cContHTML, '%Conteudo%',cContRes)
cContHtml:=replace(cContHTML, '%Assunto%',cAssunto)
cContHtml:=replace(cContHTML, '%Titulo%',cTitulo)
//cAdmin:=getmv("MVW_MAILVD")
cAdmin:="marcelo.piazza@agilitysolutions.com.br"
u_WEBMAIL(cAssunto, cTitulo, cAdmin, "", cContHTML, "Orçamento Intermed")


return Nil

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Gerar             ³Autor  ³ Marcelo Piazza³ Data ³05.04.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de geracao de orcamento                               ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/
WSMETHOD Gerar WSRECEIVE Token, FichaOrcamento WSSEND Retorno WSSERVICE WSOrcamento
Local aCab:={}
Local aItens:={}
Local oRetorno:=WSClassNew("RetornoStruct")
Local aArea := {}
Local cVend1:=""
Local cCliente:=""
Local cLoja:=""
Local n:=0
Local nTam:=0
Local cProduto:=""
Local nQtdVen:=0
Local nPrUnit:=0
Local nPrcVen:=0
Local nValor:= 0
Local dSolCli:=CTOD("  /  /  ")
Local cTipOrc:=""
Local cSolicit:=""
Local cPercent:=""
Local cCondPag:=""
Local cTPreco:=""
Local nDescont:=0
Local nValDesc:=0
Local cObserv:=""
Local cDetCond:=""


//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga
if u_ValToken(Token)
	cCliente:=FichaOrcamento:CodigoCliente
	cLoja:=FichaOrcamento:LojaCliente
	dSolCli:=FichaOrcamento:DataSolicitacao
	cTipOrc:=FichaOrcamento:TipoOrcamento//"PRI"
	cSolicit:=FichaOrcamento:TipoSolicitacao //"V"
	cPercent:=FichaOrcamento:PossibilidadeVenda
	cCondPag:=FichaOrcamento:CondicaoPagamento  //"001"
	cTPreco:=FichaOrcamento:TipoFrete
	cObserv:=FichaOrcamento:Observacao
	nValorFrete:=FichaOrcamento:ValorFrete
	cDetCond:=FichaOrcamento:DetalheCondicao
	cProrrog:=FichaOrcamento:MotivoProrrogacao
	dValidi:=FichaOrcamento:DataValidade
	
	cDoc := GetSxeNum("SCJ","CJ_NUM")
	RollBAckSx8()
	
	//cVend1:=GetAdvFVal("SA3","A3_COD",xFilial("SA3")+__cUserID,7)
	cVend1 := FichaOrcamento:CodVendedor
	if len(ALLtrim(cVEND1)) = 0
		cVend1:=SUPERGETMV("MVW_CRMVND")
	ENDIF                 

	nValid := dValidi - dDataBase

	aCab := {}
	aAdd(aCab, {"CJ_FILIAL"        	    , xFilial("SCJ"), nil})
	aAdd(aCab, {"CJ_NUM"          	    , cDoc		    , nil})
	aAdd(aCab, {"CJ_MATRIC"          	, __cUserID		, nil})
	aAdd(aCab, {"CJ_SOLCLI"          	, ddatabase     , nil})

	aAdd(aCab, {"CJ_SOLICIT"            , cSolicit     	, nil})
	aAdd(aCab, {"CJ_TIPORC"			    , cTipOrc		, nil})
	aAdd(aCab, {"CJ_LICIT"          	, "N"  	 		, nil})
	aAdd(aCab, {"CJ_IMPDIR"          	, "N"     		, nil})
	aAdd(aCab, {"CJ_ATUALIZ"            , "N"     		, nil})
	aAdd(aCab, {"CJ_IDINTER"            , "PD"     		, nil})
	aAdd(aCab, {"CJ_ORCANT"          	, "N"  			, nil})
	aAdd(aCab, {"CJ_CHECK"         	    , dDataBase     , nil})
	aAdd(aCab, {"CJ_PRZVAL"          	, nValid   		, nil})
	aAdd(aCab, {"CJ_VALIDI"          	, dValidi  		, nil})
	aAdd(aCab, {"CJ_EMIT"          	    , 'DVEN'     	, nil})
	aAdd(aCab, {"CJ_CLIENTE"     		, cCliente   	, nil})
	aAdd(aCab, {"CJ_LOJA"     			, cLoja  		, nil})
	aAdd(aCab, {"CJ_LOJAENT"			, cLoja			, Nil})
	if cCondPag=="000"
		aAdd(aCab, {"CJ_CONDPAG"     		, cCondPag     , ".t."})
	else
		aAdd(aCab, {"CJ_CONDPAG"     		, cCondPag     , nil})
	endif
	aAdd(aCab, {"CJ_TABELA"     		, "8"           , nil})
	aAdd(aCab, {"CJ_TPRECO"     		, cTPreco       , nil})
	aAdd(aCab, {"CJ_OBSERV"     		, cObserv       , nil})
	aAdd(aCab, {"CJ_FRETE"     		    , nValorFrete   , nil})
	aAdd(aCab, {"CJ_EMISSAO"     		, dDataBase     , nil})
	aAdd(aCab, {"CJ_PERCENT"     		, cPercent      , nil})
	aAdd(aCab, {"CJ_VEND1"          	, cVend1     	, nil})
	aAdd(aCab, {"CJ_ALOCAC"     		, "N"           , nil})
	aAdd(aCab, {"CJ_XORI"     		    , "W"           , nil})
	aAdd(aCab, {"CJ_CODUSR"     		, Token:Conteudo, nil})
	aAdd(aCab, {"CJ_IDUSR"     		    , FichaOrcamento:IDUsuario, nil})
	aAdd(aCab, {"CJ_CPLCDPG"    		, cDetCond      , nil})
	aAdd(aCab, {"CJ_MOTPRO"    		    , cProrrog      , nil})
	aadd(aCab, {"CJ_ACUI"     		    , FichaOrcamento:Contato, nil}) 
	aAdd(aCab, {"CJ_FATPOR"    		    , FichaOrcamento:FaturadoPor, nil})
	if FichaOrcamento:FaturadoPor=="R"
		aAdd(aCab, {"CJ_MARCA"          	, "22"     		, nil})	
	else
		aAdd(aCab, {"CJ_MARCA"          	, "05"     		, nil})
	endif
	aItens:={}
	nTam :=len(FichaOrcamento:Itens)
	for n:=1 to nTam
		nQtdVen:=FichaOrcamento:Itens[n]:Quantidade
		nPrcVen:=FichaOrcamento:Itens[n]:PrecoVenda
		nPrUnit:=FichaOrcamento:Itens[n]:PrecoLista
		nDescont:=FichaOrcamento:Itens[n]:PercentualDesconto
		nValDesc:=	FichaOrcamento:Itens[n]:ValorDesconto
		nValor      := nQtdVen * nPrcVen
		IF nPrUnit==0
			nPrUnit:=nPrcVen
		endif
		aItem:={}                                              
		aAdd(aItem, {"CK_FILIAL"    , xFilial("SCK"), nil})
		aadd(aItem, {"CK_ITEM"		,StrZero(n,2),Nil})
		aadd(aItem, {"CK_PRODUTO"	,FichaOrcamento:Itens[n]:CodigoProduto	,NIL})
		aadd(aItem, {"CK_QTDVEN"	,nQtdVen	,NIL})
		aadd(aItem, {"CK_PRCVEN"	,nPrcVen	,NIL})
		aadd(aItem, {"CK_PRUNIT"	,nPrUnit	,Nil})
		aadd(aItem, {"CK_VALOR"		,nValor		,Nil})
		if nDescont!=0
			aadd(aItem, {"CK_DESCONT"	,nDescont	,Nil})
		endif
		aadd(aItem, {"CK_ENTREG"	,dDatabase + FichaOrcamento:Itens[n]:PrazoEntrega		,Nil})
		aadd(aItem, {"CK_DESCDET"	,FichaOrcamento:Itens[n]:DescricaoDetalhada	,Nil})
		aadd(aItem, {"CK_GARANT"	,FichaOrcamento:Itens[n]:PrazoGarantia	,Nil})
		aAdd(aItens, aItem)
	next
	
	Begin Transaction
	lMsErroAuto := .F.
	MSExecAuto({|x,y,z|Mata415(x,y,z)},aCab,aItens,3)
	If lMsErroAuto
		DisarmTransaction()
		RollBackSX8()
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=2
		oRetorno:Mensagem:="Erro ao gerar orçamento: #" + MemoRead(NOMEAUTOLOG()) + "#."
		oRetorno:Chave:=""
		ferase(NOMEAUTOLOG())
	Else  
		ConfirmSX8()               
		
		oRetorno:Sucesso:=.T.
		oRetorno:Codigo:=0
		oRetorno:Mensagem:="Orçamento '" + SCJ->CJ_NUM + "' gerado com sucesso."
		oRetorno:Chave:=SCJ->(CJ_FILIAL + CJ_NUM)
		if cCondPag=="000"   
			RECLOCK("SCJ", .F.)
			SCJ->CJ_CONDPAG:=""
			SCJ->(MSUNLOCK())
		endif
		RECLOCK("SCJ", .F.)
		SCJ->CJ_MATRIC:=""
		SCJ->(MSUNLOCK())
	endif
	End Transaction
	IF !lMsErroAuto
		//if FichaOrcamento:FaturadoPor=="R"
		//	TCSQLEXEC("UPDATE " + RETSQLNAME("SCJ") + " SET CJ_STATUS = 'B' WHERE D_E_L_E_T_ = '' AND CJ_FILIAL = '" + XFILIAL("SCJ") + "' AND CJ_NUM = '"+ cDoc +"'")
	   //	else
			TCSQLEXEC("UPDATE " + RETSQLNAME("SCJ") + " SET CJ_STATUS = 'D' WHERE D_E_L_E_T_ = '' AND CJ_FILIAL = '" + XFILIAL("SCJ") + "' AND CJ_NUM = '"+ cDoc +"'")
	   //	endif	
		//GrvGaran(FichaOrcamento, cDoc)
	Endif
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif        

::Retorno:=oRetorno
RpcClearEnv()
Return .t.   
/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Cancelar          ³Autor  ³ Marcelo Piazza³ Data ³29.06.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de cancelamento de orcamento                           ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³Apontamento: estrutura do apontamento                        ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/

WSMETHOD Cancelar WSRECEIVE Token, NumeroOrcamento WSSEND Retorno WSSERVICE WSOrcamento
Local aCab:={}
Local aItens:={}
Local lRet:=.T.
Local oRetorno:=WSClassNew("RetornoStruct")
Local aArea:=GetArea()

//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga
if u_ValToken(Token)
	dbSelectArea("SCJ")
	If !SCJ->(dbSeek(xFilial("SCJ") + NumeroOrcamento))
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=2
		oRetorno:Mensagem:="Orçamento '" + NumeroOrcamento + "' não localizado."
		oRetorno:Chave:=""
	Else
		Begin Transaction
		RecLock("SCJ",.F.)
		SCJ->CJ_MARCA:=POSICIONE("SX5",1,xfilial("SX5")+"Z721", "X5_DESCRI")
		SCJ->CJ_DCANC:=ddatabase
		MaAvalOrc("SCJ",14)
		MsUnlock()
		End Transaction
		oRetorno:Sucesso:=.T.
		oRetorno:Codigo:=0
		oRetorno:Mensagem:="Orçamento '" + NumeroOrcamento + "' cancelado com sucesso."
		oRetorno:Chave:=xFilial("SCJ") + NumeroOrcamento
	Endif
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
RestArea(aArea)
::Retorno:=oRetorno
/**/
RpcClearEnv()
/**/
Return(lRet)

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³Aprovar          ³Autor  ³ Marcelo Piazza³ Data ³03.08.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Método de aprovacao de orcamento                             ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³NumeroOrcamento: numero do orçamento                         ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/

WSMETHOD Aprovar WSRECEIVE Token, FichaOrcamento WSSEND Retorno WSSERVICE WSOrcamento
Local aCab:={}
Local aItens:={}
Local lRet:=.T.
Local oRetorno:=WSClassNew("RetornoStruct")
Local aArea:=GetArea()

//verifica se o usuario pode executar a operação e atualiza as variaveis do microsiga
if u_ValToken(Token)
	
	dbSelectArea("SCJ")
	If !SCJ->(dbSeek(xFilial("SCJ") + FichaOrcamento:Numero))
		oRetorno:Sucesso:=.F.
		oRetorno:Codigo:=2
		oRetorno:Mensagem:="Orçamento '" + FichaOrcamento:Numero + "' não localizado."
		oRetorno:Chave:=""
	Else
		oRetorno:=falterar(Token, FichaOrcamento)
		if oRetorno:Sucesso
			RecLock("SCJ",.F.)
			SCJ->CJ_STATUS:="A"
			SCJ->CJ_MARCA:=POSICIONE("SX5",1,xfilial("SX5")+"Z720", "X5_DESCRI")
			SCJ->CJ_DAPROV:=ddatabase
			MsUnlock()
			oRetorno:Sucesso:=.T.
			oRetorno:Codigo:=0
			oRetorno:Mensagem:="Orçamento '" +  FichaOrcamento:Numero + "' aprovado com sucesso."
			oRetorno:Chave:=xFilial("SCJ") +  FichaOrcamento:Numero
		endif
	Endif
Else
	oRetorno:Sucesso:=.F.
	oRetorno:Codigo:=1
	oRetorno:Mensagem:="O usuário não possui permissão para esta operação."
	oRetorno:Chave:=""
endif
RestArea(aArea)
::Retorno:=oRetorno
/**/
RpcClearEnv()
/**/
Return(lRet)

/*/
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
±±ÚÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÂÄÄÄÄÄÄÂÄÄÄÄÄÄÄÄÄÄÄ¿±±
±±³Fun‡„o    ³GrvGarantia       ³Autor  ³ Marcelo Piazza³ Data ³04.08.2010 ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³          ³Atualiza a mensagem de garantia                              ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Parametros³Token: token de segurançac                                   ³±±
±±³          ³NumeroOrcamento: numero do orçamento                         ³±±
±±ÃÄÄÄÄÄÄÄÄÄÄÅÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ´±±
±±³Retorno   ³aRet : Estrutura de retorno                                  ³±±
±±ÀÄÄÄÄÄÄÄÄÄÄÁÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ±±
±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±±
ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß
/*/

Static Function GrvGaran(FichaOrcamento, cNum)
Local cCliente:=FichaOrcamento:CodigoCliente
Local cLoja:=FichaOrcamento:LojaCliente
Local cUFCli:=POSICIONE("SA1",1,xfilial("SA1")+cCliente+cLoja,"A1_EST")
Local a12:={}
Local a24:={}
Local n
Local nTam:=len(FichaOrcamento:Itens)
Local c12:=getmv("MVW_12MES") //via parametro
Local cMensa:=''
For n:=1 to nTam
	oItem:=FichaOrcamento:Itens[n]
	cProd:=Alltrim(oItem:CodigoProduto)
	if POSICIONE("SB1",1,xfilial("SB1")+cProd,"B1_GARANT")>0
		if cProd $ c12 .or. cUFCli!="SP"
			aadd(a12, ALLTRIM(POSICIONE("SB1",1,xfilial("SB1")+cProd,"B1_DESC"))) //nomeproduto
		else
			aadd(a24, ALLTRIM(POSICIONE("SB1",1,xfilial("SB1")+cProd,"B1_DESC"))) //nomeproduto
		endif
	endif
Next
if len(a12)> 0
	cMensa+="Os equipamentos a seguir possuem 12 meses de garantia: " + CRLF
	AEVAL(a12,{ | X | cMensa+=' - ' + X + CRLF } )
endif
if len(a24)> 0
	cMensa+="Os equipamentos a seguir possuem 24 meses de garantia: " + CRLF
	AEVAL(a24,{ | X | cMensa+=' - ' + X  + CRLF } )
endif
cSQL = "UPDATE " + RETSQLNAME("SCJ") + " SET CJ_RODAPE = ' " + cmensa +  "' WHERE D_E_L_E_T_ = '' AND CJ_FILIAL = '" + XFILIAL("SCJ") + "' AND CJ_NUM = '"+cNum +"'"
TCSQLEXEC(CSQL)
Return Nil
