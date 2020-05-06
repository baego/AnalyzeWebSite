
//функция, заполняющая раздел "что мы о вас знаем"
$(window).ready(function () {

	if (document.cookie.includes('IPAdress')) {

		var ip = getFromCookie('IPAdress');
		$("#ipEl").css("display","block");
		$("#ipSign").text(ip);
	}

	if (document.cookie.includes('Geography')) {

		var geo = getFromCookie('Geography');
		$("#geoEl").css("display", "block");
		$("#mapholder").text(geo);
	}

	if (document.cookie.includes('Browser')) {

		var browser = getFromCookie('Browser');
		$("#browsEl").css("display", "block");
		$("#brows").text(browser);
	}


});