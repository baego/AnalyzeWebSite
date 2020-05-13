var currentTab = 0; // первый шаг
showTab(currentTab); 
var isInnerRouteFilled = false;

function showTab(n) {

	$(window).ready(function () {

		var tab = document.getElementsByClassName("tab");

		if(tab[n] !== undefined)
			tab[n].style.display = "block";

		var xx = 0;
		//появление и смена надписей на кнопках
		if (xx === 0) {

			document.getElementById("prevBtn").style.display = "none";
		} else {

			//document.getElementById("prevBtn").style.display = "inline";
		}
		if (n === (tab.length - 1)) {

			document.getElementById("nextBtn").innerHTML = "Принять";
			document.getElementById("nextBtn").type = "submit";
		} else {
			document.getElementById("nextBtn").innerHTML = "Далее";
		}
		
		fixStepIndicator(n);
	});
}

//функция отвечает за переключение шагов
function nextPrev(n) {


	var tabs = document.getElementsByClassName("tab");

	// если что-то не так, то сразу выход
	if (n === 1 && !validateForm()) return false;

	tabs[currentTab].style.display = "none";

	currentTab = currentTab + n;

	//работа с выбором спезиализации в конкретной науке
	if (tabs[currentTab] !== undefined && tabs[currentTab].id === "innerRoute" && !isInnerRouteFilled) {

		var innerRoutes = document.getElementById('innerRouteSelect');

		//сначала удалим все опции, которые сейчас есть
		var length = innerRoutes.options.length;
		for (i = length - 1; i >= 0; i--) {
			innerRoutes.options[i] = null;
		}

		//затем вытащим из контроллера все нужное
		$.post(window.location.origin + "/Test/GetInnerRoute", { degreeRoute: $("#degreeRouteSelect").val() }, function (data) {

			isInnerRouteFilled = true;

			data.forEach(x => {
				var opt = document.createElement('option');
				opt.value = x.id;
				opt.innerHTML = x.name;
				innerRoutes.appendChild(opt);
			});
		});
	}

	// когда открывается последняя вкладка форма делает сабмит
	if (currentTab >= tabs.length) {
		document.getElementById("dissertationTest").submit();
	}

	// или показывает нужную вкладку
	showTab(currentTab);
}

//валидация
function validateForm() {
	var x, y, i, valid = true;
	x = document.getElementsByClassName("tab");
	y = x[currentTab].getElementsByTagName("input");

	// проверяет все поля
	for (i = 0; i < y.length; i++) {
		if (y[i].value === "" && y[i].id !== 'organizationText') {
			y[i].className += " invalid";
			valid = false;
		}
	}

	if (valid) {
		document.getElementsByClassName("step")[currentTab].className += " finish";
	}
	return valid; 
}

//функция правит индикатор шагов
function fixStepIndicator(n) {

	var i, x = document.getElementsByClassName("step");
	for (i = 0; i < x.length; i++) {
		x[i].className = x[i].className.replace(" active", "");
	}

	if(x[n] !== undefined)
		x[n].className += " active";
}