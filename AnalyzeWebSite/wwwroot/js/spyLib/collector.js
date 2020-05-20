
//проверяем наличие выявленного IP
function checkIp(callbackFunction) {

	//смотрим IP адрес
	if (!document.cookie.includes('IPAdress')) {

		getIp(callbackFunction);
	} else {

		callbackFunction();
	}
}

//проверяет всю остальную информацию о пользователе
function checkData() {

	checkGeo();
	checkBrowser();
	checkReferer();
}

function getIp(callbackFunction) {

	//получаем ip клиента через cloudflare
	$.get('https://www.cloudflare.com/cdn-cgi/trace', function (data) {

		//из всей информации вычленяем только ip
		var ip = data.split('\n').map(function (el) { return el.split(/\s+/); })[2];
		var address = ip[0].replace(/^.{3}/, '');

		document.cookie = "IPAdress=" + address;
		callbackFunction();
		//отправляем в контроллер для обработки
		$.post(window.location.origin + "/Spy/GetIp", { ip: address });
	});
}


//достает ip из куки
function getFromCookie(name) {
	var value = "; " + document.cookie;
	var parts = value.split("; " + name + "=");
	if (parts.length === 2) return parts.pop().split(";").shift();
}

function checkBrowser() {

	if (!document.cookie.includes('Browser')) {

		var ip = getFromCookie('IPAdress');

		defineBrowser();
		var browser = getFromCookie('Browser');
		$.post(window.location.origin + "/Spy/GetBrowser", { browser: browser, ip: ip });

	}
}

//смотрим, откуда пришел пользователь
function checkReferer() {

	if (!document.cookie.includes('Referer')) {

		var ip = getFromCookie('IPAdress');
		var ref = document.referrer;
		document.cookie = 'Referer=' + ref;

		$.post(window.location.origin + "/Spy/GetReferer", { referer: ref, ip: ip });

	}
}

//функция определяет браузер пользователя
function defineBrowser() {
	var a = "Some browser";

	//обычные браузеры
	if (navigator.userAgent.search(/Safari/) > 0) { a = 'Safari' };
	if (navigator.userAgent.search(/Firefox/) > 0) { a = 'Mozilla Firefox' };
	if (navigator.userAgent.search(/MSIE/) > 0 || navigator.userAgent.search(/NET CLR /) > 0) { a = 'Internet Explorer' };
	if (navigator.userAgent.search(/Chrome/) > 0) { a = 'Google Chrome' };
	if (navigator.userAgent.search(/YaBrowser/) > 0) { a = 'Yandex Browser' };
	if (navigator.userAgent.search(/OPR/) > 0) { a = 'Opera' };
	if (navigator.userAgent.search(/Konqueror/) > 0) { a = 'Konqueror' };
	if (navigator.userAgent.search(/Iceweasel/) > 0) { a = 'Debian Iceweasel' };
	if (navigator.userAgent.search(/SeaMonkey/) > 0) { a = 'SeaMonkey' };
	if (navigator.userAgent.search(/Edge/) > 0) { a = 'Microsoft Edge (Old)' };
	if (navigator.userAgent.search(/Edg/) > 0) { a = 'Microsoft Edge (Chrome Engine)' };

	//мобильные браузеры
	if (navigator.userAgent.search(/Android/i) > 0) { a = 'Android Device' };
	if (navigator.userAgent.search(/BlackBerry/i) > 0) { a = 'Blackberry Device' };
	if (navigator.userAgent.search(/iPhone|iPad|iPod/i) > 0) { a = 'Apple iOS' };
	if (navigator.userAgent.search(/Opera Mini/i) > 0) { a = 'Opera Mini' };
	if (navigator.userAgent.search(/IEMobile/i) > 0) { a = 'Internet Explorer Mobile' };

	document.cookie = "Browser=" + a;
}

