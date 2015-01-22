
function ShowMessage(oMsg, sPainelMensagem) {
    var oPainelMensagem = document.getElementById(sPainelMensagem);
    var oIcone = oPainelMensagem.children[0];
    oIcone.className = "Mensagem Icone Aguarde";
    var oLabel = oPainelMensagem.children[1];
    oLabel.className = "Mensagem Texto Aguarde";
    oLabel.innerHTML = oMsg;
    return true;
}
function Aguarde(oMsg, sPainelBotoes, sPainelMensagem) {
    document.getElementById(sPainelBotoes).style.visibility = "hidden";
    ShowMessage(oMsg, sPainelMensagem);
    return true;
}
function fnThisPostBack(sNomeControle) {
    setTimeout('__doPostBack(\'' + sNomeControle + '\',\'\')', 0);
    return true;
}
function fnClientViewPreenchido(sIdCodigo, sIdLoja, sNomeControle) {
    var oCodigo = document.getElementById(sIdCodigo);
    var oLoja = document.getElementById(sIdLoja);
    var bRet = false;
    if (oCodigo.value.length > 0) {
        if (oLoja.value.length > 0) {
            bRet = true;
            fnThisPostBack(sNomeControle);
        }
    }
    return bRet;
}

function fnClientViewCPFCNPJ(sIdCodigo, sNomeControle) {
    var oCodigo = document.getElementById(sIdCodigo);
    var bRet = false;
    if (oCodigo.value.length > 0) {
        bRet = true;
        fnThisPostBack(sNomeControle);
    }
    return bRet;
}

function fnCalcularTotal(sIdQuantidade, sIdPrecoLista, sIdPercentualDesconto, sIdValorDesconto, sIdPrecoVenda, sIdTotal, sIdTrigger) {
    var oQuantidade = document.getElementById(sIdQuantidade);
    var oPrecoLista = document.getElementById(sIdPrecoLista);
    var oPrecoVenda = document.getElementById(sIdPrecoVenda);
    var oTotal = document.getElementById(sIdTotal);
    var sQuantidade = oQuantidade.value.replace(',', '.');
    var sPrecoLista = oPrecoLista.innerHTML.replace(',', '.');
    var sPrecoVenda = oPrecoVenda.value.replace(',', '.');
    var sTotal = "0.00";
    oTotal.innerHTML = "0,00";

    var nQuantidade = 0;
    var nPrecoLista = 0.0;
    var nPrecoVenda = 0.0;
    var nTotal = 0.0;
    //receber os valores
    if (!isNaN(sQuantidade)) {
        nQuantidade = parseFloat(sQuantidade);
    }
    if (!isNaN(sPrecoLista)) {
        nPrecoLista = parseFloat(sPrecoLista);
    }
    if (!isNaN(sPrecoVenda)) {
        nPrecoVenda = parseFloat(sPrecoVenda);
    }
    nTotal = nQuantidade * nPrecoVenda;
    sPrecoVenda = Math.round(nPrecoVenda * 100) / 100;
    sTotal = Math.round(nTotal * 100) / 100;
    oPrecoVenda.value = sPrecoVenda.toString().replace('.', ',');
    oTotal.innerHTML = sTotal.toString().replace('.', ',');
}

function fnCalcularTotal2(sIdQuantidade, sIdPrecoLista, sIdPercentualDesconto, sIdValorDesconto, sIdPrecoVenda, sIdTotal, sIdTrigger) {
    var oQuantidade = document.getElementById(sIdQuantidade);
    var oPrecoLista = document.getElementById(sIdPrecoLista);
    var oPercentualDesconto = document.getElementById(sIdPercentualDesconto);
    var oValorDesconto = document.getElementById(sIdValorDesconto);
    var oPrecoVenda = document.getElementById(sIdPrecoVenda);
    var oTotal = document.getElementById(sIdTotal);

    var sQuantidade = oQuantidade.value.replace(',', '.');
    var sPrecoLista = oPrecoLista.innerHTML.replace(',', '.');
    var sPercentualDesconto = oPercentualDesconto.value.replace(',', '.');
    var sValorDesconto = oValorDesconto.innerHTML.replace(',', '.');
    var sPrecoVenda = oPrecoVenda.value.replace(',', '.');
    var sTotal = "0.00";
    oTotal.innerHTML = "0,00";

    var nQuantidade = 0;
    var nPrecoLista = 0.0;
    var nPercentualDesconto = 0.0;
    var nValorDesconto = 0.0;
    var nPrecoVenda = 0.0;
    var nTotal = 0.0;
    //receber os valores
    if (!isNaN(sQuantidade)) {
        nQuantidade = parseFloat(sQuantidade);
    }
    if (!isNaN(sPrecoLista)) {
        nPrecoLista = parseFloat(sPrecoLista);
    }
    if (!isNaN(sPercentualDesconto)) {
        nPercentualDesconto = parseFloat(sPercentualDesconto);
    }
    if (!isNaN(sPrecoVenda)) {
        nPrecoVenda = parseFloat(sPrecoVenda);
    }
    //calcular totais    
    if (nPrecoLista == 0) {
        //se o preco de lista é zero, sinal de que a base não possui o valor
        // portanto, considerar o preco informado pelo usuario e nao usar o desconto
        nTotal = nQuantidade * nPrecoVenda;
    }
    else {
        // se possui o preco de lista, calcular:
        // valor do desconto
        // preco da venda
        // valor total
        nValorDesconto = nPrecoLista * nQuantidade * (nPercentualDesconto / 100);
        nPrecoVenda = nPrecoLista * (1 - (nPercentualDesconto / 100));
        nTotal = nPrecoLista * nQuantidade * (1 - (nPercentualDesconto / 100));
    }
    sValorDesconto = Math.round(nValorDesconto * 100) / 100;
    sPrecoVenda = Math.round(nPrecoVenda * 100) / 100;
    sTotal = Math.round(nTotal * 100) / 100;

    oValorDesconto.innerHTML = sValorDesconto.toString().replace('.', ',');
    oPrecoVenda.value = sPrecoVenda.toString().replace('.', ',');
    oTotal.innerHTML = sTotal.toString().replace('.', ',');
}


function avisoAguarde() {
    if (document.getElementById('divProcessando')) {
        document.getElementById('divProcessando').style.display = '';
        return;
    } oDiv = document.createElement("div"); with (oDiv) { id = "divProcessando"; } document.body.appendChild(oDiv);
}