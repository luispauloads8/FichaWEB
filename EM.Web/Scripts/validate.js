
function validate() 
{
	let matricula = document.getElementById('matricula').value;
	let nome = document.getElementById('nome').value;
	let limitemininome = 2;
	let limitemaxnome = 100;
	let Nascimento = document.getElementById('Nascimento').value;


	let data = new Date();
	let datanasc = new Date(Nascimento);


	//VERIFICA SE MATRICULA ESTA VAZIA
	if (matricula == "") {
		alert('Favor preencher a matricula');
		document.getElementById('matricula').focus();
		return false;
	}

	//VERIFICA SE NOME ESTA VAZIO
	if (nome == "") {
		alert('Favor preencher o Nome');
		document.getElementById('nome').focus();
		return false;
	} else if (nome.length <= limitemininome) {
		alert('Informe no minino 3 caracteres no campo nome');
		return false;
	} else if (nome.length > limitemaxnome) {
		alert('Informe maximo 100 caracteres no campo nome');
		return false;
    }

	//VERIFICA SE DATA DE NASCIMENTO ESTA VAZIA
	

	if (Nascimento == "") {
		alert('Favor informe a data de nascimento');
		return false;
	} else if (datanasc.getDate() >= data.getDate() && datanasc.getMonth() >= data.getMonth() && data.getFullYear() >= datanasc.getFullYear()) {
		alert('Aluno nao pode ser matricula com data no futuro');
		return false;
    }

	//VERIFICA SE CPF ESTA VAZIO
	let CPF = document.getElementById('CPF').value;
	if (CPF == "") {
		alert('Favor informe CPF');
		document.getElementById('CPF').focus();
		return false;
	}


	//VALIDA CPF
	let cpf = document.getElementById('CPF').value;

	function isCPF(cpf = 0) {
		console.log(cpf);
		cpf = cpf.replace(/\.|-/g, "");
		console.log(cpf);
		if (!validaPrimeiroDigito(cpf))
			return false;
		if (!validaSegundoDigito(cpf))
			return false;

		return true;

	}
	let sum = 0;

	function validaPrimeiroDigito(cpf = null) {
		let fDigit = (sumFristDigit(cpf) * 10) % 11;
		fDigit = (fDigit == 10 || fDigit == 11) ? 0 : fDigit;
		if (fDigit != cpf[9])
			return false
		return true;
	}
	function validaSegundoDigito(cpf = null) {
		let sDigit = (sumSecondDigit(cpf) * 10) % 11;
		sDigit = (sDigit == 10 || sDigit == 11) ? 0 : sDigit;

		if (sDigit != cpf[10])
			return false
		return true;
	}


	sumFristDigit = function (cpf, position = 0, sum = 0) {
		if (position > 9)
			return 0;
		return sum + sumFristDigit(cpf, position + 1, cpf[position] * ((cpf.length - 1) - position));
	}


	sumSecondDigit = function (cpf, position = 0, sum = 0) {
		if (position > 10)
			return 0;
		return sum + sumSecondDigit(cpf, position + 1, cpf[position] * ((cpf.length) - position));
	}
	if (isCPF(cpf) != true) {
		alert('CPF INVALIDO!!');
		return false;
	}
	
}
//VALIDA EXCLUSAO
function exclusao() {

	var ok = confirm('certeza que deseja excluir?');
	if (ok == true) {
		alert('Excluido com Sucesso!');
		return true;

	}
	return false;
}

//FORMATA CAMPO CPF E CNPJ
function formatarCampo(CPF) {


	if (CPF.value.length <= 11) {
		CPF.value = mascaraCpf(CPF.value);
	} else {
		CPF.value = mascaraCnpj(CPF.value);
	}
}
function retirarFormatacao(CPF) {
	CPF.value = CPF.value.replace(/(\.|\/|\-)/g, "");
}
function mascaraCpf(valor) {
	return valor.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
}
function mascaraCnpj(valor) {
	return valor.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, "\$1.\$2.\$3\/\$4\-\$5");
}





