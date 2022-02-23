let socket = new WebSocket('wss://localhost:7214/');

const currentFishes = [];

socket.onopen = function (e) {
    alert("[open] Соединение установлено");
};

socket.onmessage = function (event) {
    //alert(event.data);
    const fishDataArray = JSON.parse(event.data.toString());
    ProcessFishDataArray(fishDataArray);
};

socket.onclose = function (event) {
    if (event.wasClean) {
        alert(`[close] Соединение закрыто чисто, код=${event.code} причина=${event.reason}`);
    } else {
        alert('[close] Соединение прервано');
    }
};

socket.onerror = function (error) {
    alert(`[error] ${error.message}`);
};

function ProcessFishDataArray(fishDataArray) {
    let currentFish;
    fishDataArray.forEach(fishData => {
        const exists = currentFishes.some(fish => {
            if(fish.Id === fishData.Id)
                currentFish = fish;
            return fish.Id === fishData.Id;
        });
        if (!exists) {
            const fishDiv = CreateFish(fishData);
            aquarium.append(fishDiv);
            currentFishes.push(fishData);
        } else {
            const div = document.getElementById(fishData.Id.toString())
            div.style.left = fishData.X.toString() + 'px';
            div.innerText = fishData.ThreadId.toString();
        }
    });
}

function CreateFish(fishData) {
    const fish = document.createElement('div');
    fish.style.width = '50px';
    fish.style.height = '50px';
    if(fishData.Type === 1)
        fish.style.backgroundColor = 'red';
    else
        fish.style.backgroundColor = 'yellow';
    fish.style.position = 'absolute';
    fish.style.top = fishData.Y.toString() + 'px';
    fish.style.left = fishData.X.toString() + 'px';
    fish.id = fishData.Id.toString();
    fish.innerText = fishData.ThreadId.toString();
    return fish;
    //aquarium.append(fish);
}