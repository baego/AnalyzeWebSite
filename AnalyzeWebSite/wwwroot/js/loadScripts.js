
//события при первой загрузке главной страницы
$(window).ready(function () {

	//красивое появление надписи "добро пожаловать"
	$("#hi").fadeIn("slow");

	//наблюдение за изменением размера окна, чтобы убирать/добавлять картинку по необходимости
	window.addEventListener(`resize`, event => {

		var pic = $(".magister-logo-pic");

		if (document.documentElement.clientWidth < 990) {

			if (!pic.is(":hidden"))
				pic.hide("fast");

		} else {

			if (pic.is(":hidden"))
				pic.show("fast");
		}
	}, false);
});
