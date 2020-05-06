function exitLog(time, page, ip) {

	$.post("Spy/ExitLog", { time: time, page: page, ip: ip });
}