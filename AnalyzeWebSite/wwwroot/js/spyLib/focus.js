
// переменные и определение состояния
var hidden, visibilityChange;
var isQuitting = false;
if ( document.hidden !== "undefined") { 

	hidden = "hidden";
	visibilityChange = "visibilitychange";

} else if ( document.msHidden !== "undefined") {

	hidden = "msHidden";
	visibilityChange = "msvisibilitychange";
} else if ( document.webkitHidden !== "undefined") {

	hidden = "webkitHidden";
	visibilityChange = "webkitvisibilitychange";
}

//если документ потерял фокус, сообщим серверу
function handleVisibilityChange() {

	if (document[hidden])
		focusLost();
}

// если браузер не поддерживает Page Visibility API то запишем это
if (document.addEventListener === "undefined" || hidden === undefined) {

	console.log("Your browser doesn't support Page Visibility API!");
} else {
	// иначе будем наблюдать
	document.addEventListener(visibilityChange, handleVisibilityChange, false);

	document.addEventListener("beforeunload ", function () {
		isQuitting = true;
	});

	document.addEventListener("unload ", function () {
		isQuitting = true;
	});

	$(window).on("unload", function () {

	})
}

function focusLost() {

	var ip = getFromCookie('IPAdress');
	var page = $("#pageId").text();

	if (isQuitting)
		$.post(window.location.origin + "/Spy/FocusLost", { ip: ip, page: page });
}