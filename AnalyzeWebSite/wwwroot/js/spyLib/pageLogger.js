//проведенное на странице время и выход
function exitLog(time, page, ip) {

	$.post(window.location.origin + "/Spy/ExitLog", { time: time, page: page, ip: ip });
}

//время загрузки страницы у пользователя
function loadTime(loadStart) {

	$(window).on('load', function () {

		var ip = getFromCookie('IPAdress');
		var time = Date.now() - loadStart;
		var page = $("#pageId").text();

		$.post(window.location.origin + "/Spy/LoadTime", { ip: ip, time: time, page: page });
	});
}

//событие смены фокуса окна
function focusLost() {

	var ip = getFromCookie('IPAdress');
	var page = $("#pageId").text();

	$.post(window.location.origin + "/Spy/FocusLost", { ip: ip, page: page });
}

function linkLogger(e) {

	var ip = getFromCookie('IPAdress');
	var page = $("#pageId").text();

	var type;
	if (e.currentTarget.id.search("navigation") !== -1 ||
		e.currentTarget.id.search("navigate") !== -1 ) {
		type = 0;
	} if (e.currentTarget.id.search("external") !== -1) {
		type = 1;
	} if (e.currentTarget.id.search("source") !== -1 ||
		e.currentTarget.id.search("Source") !== -1) {
		type = 2;
	}

	$.post(window.location.origin + "/Spy/Links", { ip: ip, page: page, source: document.title, destination: e.currentTarget.href, type: type });

}