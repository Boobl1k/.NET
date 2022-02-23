let socket = new WebSocket('wss://localhost:7214/');

socket.onopen = function(e) {
    alert("[open] Соединение установлено");
};

socket.onmessage = function(event) {
    alert(event.data);
};

socket.onclose = function(event) {
    if (event.wasClean) {
        alert(`[close] Соединение закрыто чисто, код=${event.code} причина=${event.reason}`);
    } else {
        alert('[close] Соединение прервано');
    }
};

socket.onerror = function(error) {
    alert(`[error] ${error.message}`);
};