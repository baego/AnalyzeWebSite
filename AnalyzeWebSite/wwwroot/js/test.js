var currentTab = 0; // первый шаг
showTab(currentTab); 

function showTab(n) {

	$(window).ready(function () {
		
		var x = document.getElementsByClassName("tab");
		x[n].style.display = "block";

		//появление и смена надписей на кнопках
		if (n === 0) {
			document.getElementById("prevBtn").style.display = "none";
		} else {
			document.getElementById("prevBtn").style.display = "inline";
		}
		if (n === (x.length - 1)) {
			//пока так, для демонстрационной версии
			document.getElementById("nextBtn").style.display = "none";
			//document.getElementById("nextBtn").innerHTML = "Принять";
		} else {
			document.getElementById("nextBtn").innerHTML = "Далее";
		}
		
		fixStepIndicator(n);
	});
}

//функция отвечает за переключение шагов
function nextPrev(n) {
	
	var x = document.getElementsByClassName("tab");
	// если что-то не так, то сразу выход
	if (n === 1 && !validateForm()) return false;

	x[currentTab].style.display = "none";

	currentTab = currentTab + n;

	// когда открывается последняя вкладка форма делает сабмит
	if (currentTab >= x.length) {
		document.getElementById("regForm").submit();
		return false;
	}

	// или показывает нужную вкладку
	showTab(currentTab);
}

//валидацтя
function validateForm() {
	var x, y, i, valid = true;
	x = document.getElementsByClassName("tab");
	y = x[currentTab].getElementsByTagName("input");

	// проверяет все поля
	for (i = 0; i < y.length; i++) {
		if (y[i].value === "") {
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

	x[n].className += " active";
}