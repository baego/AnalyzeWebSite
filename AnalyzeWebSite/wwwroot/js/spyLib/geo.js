function checkGeo() {
	if (!document.cookie.includes('Geography')) {

		getGeo();
	}
}


//получение геолокации
function getGeo() {

	if (navigator.geolocation) {
		navigator.geolocation.getCurrentPosition(showPosition, showError);
	}
}

//если дадут геолокацию
function showPosition(position) {

	//отправляем в api бесплатного сервиса bigdatacloud
	$.get('https://api.bigdatacloud.net/data/reverse-geocode-client?latitude='
		+ position.coords.latitude + '&longitude='
		+ position.coords.longitude + '&localityLanguage=en',
		function (data) {

			//возьмем только самое интересное
			var geography = data.countryName + ", "
				+ data.principalSubdivision + ", " + data.locality;

			document.cookie = "Geography=" + geography;
			sendGeographyToServer(geography);
		}
	);
}

//если не дадут, вернём грустное сообщение
function showError(error) {

	var msg = "Не дано разрешение на получение геопозиции";
	document.cookie = "Geography=" + msg;

	sendGeographyToServer(msg);
}

//метод для отправки географии на сервер
function sendGeographyToServer(geography) {

	var ipAddress = getFromCookie('IPAdress');

	$.post("Spy/GetGeo", { geo: geography, ip: ipAddress });
}