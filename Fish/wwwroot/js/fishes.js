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

const redStart = 0;
const greenStart = 255;
const blueStart = 128;

const redEnd = 255;
const greenEnd = 0;
const blueEnd = 128;

let iteration = 0;

function ProcessFishDataArray(fishDataArray) {
    let currentFish;
    fishDataArray.forEach(fishData => {
        const exists = currentFishes.some(fish => {
            if (fish.Id === fishData.Id)
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
            div.style.top = fishData.Y.toString() + 'px';
            div.innerText = fishData.ThreadId.toString();
            if (fishData.Type === 3) {
                iteration = (iteration + 1) % 300;
                div.style.backgroundColor = 'rgb(' + Math.floor(256 + (redEnd - redStart) * iteration/300) % 256 + ',' +
                    Math.floor(256 + (greenEnd - greenStart) * iteration/300) % 256 + ',' +
                    Math.floor(256 + (blueEnd - blueStart) * iteration/300) % 256 + ')';
            }
        }
    });
}

function CreateFish(fishData) {
    const fish = document.createElement('img');
    
    fish.style.width = '50px';
    fish.style.height = '50px';
    if (fishData.Type === 1)
        fish.src='https://sun1-93.userapi.com/s/v1/if1/dpkXjn6pzHF175jemfuiiXDexiquMmiCI3ZiH9oWzozomD0O37ifRPfIP4h7L_ZzmjUIUlCX.jpg?size=50x0&quality=96&crop=480,120,960,960&ava=1';
    else if (fishData.Type === 2)
        fish.src='https://sun6-20.userapi.com/s/v1/ig1/fxZ8ZC5zhxjWp2Mv7xZbv2OfEKoa2s7hF7avdLRaXLhe3yMEUXx83yCBcw2yVqJuJPz5KPvw.jpg?size=50x50&quality=96&crop=277,0,799,799&ava=1';
    else
        fish.style.backgroundColor = '#000000';

    fish.style.position = 'absolute';
    fish.style.top = fishData.Y.toString() + 'px';
    fish.style.left = fishData.X.toString() + 'px';
    fish.id = fishData.Id.toString();
    fish.innerText = fishData.ThreadId.toString();
    return fish;
    //aquarium.append(fish);
}