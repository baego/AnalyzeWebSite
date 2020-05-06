$(window).ready(function () {
	var acc = document.getElementsByClassName("accordion");
	var i;

	$("#averageForm").on('submit', function (e) {
		e.preventDefault();
		$.post("/AboutYou/Submit", {
			average: $("input[name='average']:checked").val(), uiux: $("input[name='uiux']:checked").val(),
			loadTime: $("input[name='loadTime']:checked").val(), usability: $("input[name='usability']:checked").val(),
			trust: $("input[name='trust']:checked").val(), errors: $("input[name='errors']:checked").val()
		}, function (data) {
			document.querySelector("#aboutYouForm").remove();

		});
	});

	for (i = 0; i < acc.length; i++) {
		acc[i].addEventListener("click", function () {
			this.classList.toggle("active");
			var panel = this.nextElementSibling;
			if (panel.style.display === "block") {
				panel.style.display = "none";
			} else {
				panel.style.display = "block";
			}
		});

	}
});

function submitAboutYouFormClick() {

}


