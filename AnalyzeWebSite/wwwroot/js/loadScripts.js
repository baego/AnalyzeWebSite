﻿
//события при первой загрузке главной страницы
$(window).ready(function () {

	var clock = new Date();
	var start = clock.getTime();
	handleVisibilityChange();
	window.onbeforeunload = function () {

		var clock2 = new Date();
		var end = clock2.getTime();
		var diff = end - start;
		var ip = getFromCookie('IPAdress');
		var name = $("#pageId").text();
		exitLog(diff, name, ip);
	};

	//если уже согласился на куки, то не показываем
	if (document.cookie.includes('CookiesAgree')) {
		$("#cookieBanner").css("display", "none");
	}

	var pic = $(".magister-logo-pic");
	var cookie = $("#cookieBanner");
	if (document.documentElement.clientWidth > 990) {
		pic.css("display", "block ");
		cookie.css("display", "block ");
	}
	//красивое появление заглавной надписи 
	$("#hi").fadeIn("slow");

	//наблюдение за изменением размера окна, чтобы убирать/добавлять картинку по необходимости
	window.addEventListener(`resize`, event => {

		//если совсем небольшой экран, вообще не показывать
		if (document.documentElement.clientWidth < 990) {


			if (!pic.is(":hidden"))
				pic.hide("fast");

			cookie.css("display", "none ");
		} else {

			if (pic.is(":hidden"))
				pic.show("fast");

		}
	}, false);

	//для увеличения изображения
	//TODO: поправить появление картинок
	$('.img-block img').click(function () {

		var ip = getFromCookie('IPAdress');

		$.post(window.location.origin + "/Spy/Pics", { ip: ip, name: $(this).attr("src") });

		var imgAddr = $(this).attr("src");
		var width = (window.screen.width/100) * 50;
		$('#img-big-block img').attr({ src: imgAddr, width: width });

		$('#img-big-block').fadeIn('slow');
	});

	// Обрабатывает клик по большой картинке
	$('#img-big-block').click(function () {
		$(this).fadeOut();
	});
});

//баннер о куках
function cookieAgree() {
	document.cookie = "CookiesAgree=checked";

	var ip = getFromCookie('IPAdress');
	$("#cookieBanner").css("display", "none");

	$.post(window.location.origin + "/Spy/CookieAgree", { ip: ip });
}

