
function getIp() {

	//получаем ip клиента через cloudflare
	$.get('https://www.cloudflare.com/cdn-cgi/trace', function (data) {

		//отправляем в контроллер для обработки
		$.post("Spy/GetIp", { val: data.split('\n').map(function (el) { return el.split(/\s+/); }) },
			function (ip) {
				$("#ipSign").text(ip);
			});
	});
	getGeo();
}

