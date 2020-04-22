
//события при первой загрузке главной страницы
$(window).ready(function () {

	var pic = $(".magister-logo-pic");

	if (document.documentElement.clientWidth > 990) {
		pic.css("display", "block ");
	}
	//красивое появление заглавной надписи 
	$("#hi").fadeIn("slow");

	//наблюдение за изменением размера окна, чтобы убирать/добавлять картинку по необходимости
	window.addEventListener(`resize`, event => {

		//если совсем небольшой экран, вообще не показывать
		if (document.documentElement.clientWidth < 990) {

			if (!pic.is(":hidden"))
				pic.hide("fast");

		} else {

			if (pic.is(":hidden"))
				pic.show("fast");
		}
	}, false);

	//для увеличения изображения
	//TODO: поправить появление картинок
	$('.img-block img').click(function () {

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
