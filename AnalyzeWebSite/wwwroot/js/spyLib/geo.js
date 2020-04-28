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
		+ position.coords.longitude + '&localityLanguage=ru',
		function (data) {
			$("#mapholder").text(data.countryName + ", "
				+ data.principalSubdivision + ", " + data.locality);
		}
	);
}

//если не дадут, вернём грустное сообщение
function showError(error) {
	$("#mapholder").text("Вы не дали доступ к своему местоположению :(");
} 