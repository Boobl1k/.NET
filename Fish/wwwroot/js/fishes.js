const type1Src = 'https://sun1-93.userapi.com/s/v1/if1/dpkXjn6pzHF175jemfuiiXDexiquMmiCI3ZiH9oWzozomD0O37ifRPfIP4h7L_ZzmjUIUlCX.jpg?size=50x0&quality=96&crop=480,120,960,960&ava=1';
const type2Src = 'https://sun6-20.userapi.com/s/v1/ig1/fxZ8ZC5zhxjWp2Mv7xZbv2OfEKoa2s7hF7avdLRaXLhe3yMEUXx83yCBcw2yVqJuJPz5KPvw.jpg?size=50x50&quality=96&crop=277,0,799,799&ava=1';

function CreateFishAvatar() {
    const avatar = document.createElement('img');
    avatar.style.width = '50px';
    avatar.style.height = '50px';
    avatar.style.position = 'absolute';
    return avatar;
}

class Fish {
    constructor(id) {
        this.id = id;
        this.avatar = CreateFishAvatar();
        this.avatar.innerText = id.toString();
    }

    move(x, y) {
        this.x = x;
        this.y = y;
        this.avatar.style.left = x.toString() + 'px';
        this.avatar.style.top = y.toString() + 'px';
    }

    set threadId(newThreadId) {
        this.avatar.innerText = newThreadId.toString();
    }
}

class ThreadFish extends Fish {
    constructor(id) {
        super(id);
        this.avatar.src = type1Src;
    }
}

class TaskFish extends Fish {
    constructor(id) {
        super(id);
        this.avatar.src = type2Src;
    }
}

class RetardedFish extends Fish{
    constructor(id) {
        super(id);
        this.avatar.style.backgroundColor = '#000000';
    }
}

let socket = new WebSocket('wss://localhost:7214/');

socket.onmessage = function (event) {
    //console.log(event.data);
    ProcessFishDataArray(JSON.parse(event.data.toString()));
};

const redStart = 0;
const greenStart = 255;
const blueStart = 128;
const redEnd = 255;
const greenEnd = 0;
const blueEnd = 128;
let iteration = 0;

const currentFishes = [];

function ProcessFishDataArray(fishDataArray) {
    iteration = (iteration + 1) % 300;
    let currentFish;
    fishDataArray.forEach(fishData => {
        const exists = currentFishes.some(fish => {
            if (fish.id === fishData.Id)
                currentFish = fish;
            return fish.id === fishData.Id;
        });
        if (!exists) {
            const id = fishData.Id;
            const fish = fishData.Type === 1
                ? new ThreadFish(id)
                : fishData.Type === 2
                    ? new TaskFish(id)
                    : new RetardedFish(id);
            currentFishes.push(fish);
            aquarium.append(fish.avatar);
        } else {
            currentFish.move(fishData.X, fishData.Y);
            currentFish.threadId = fishData.ThreadId.toString();
            if (fishData.Type === 3) {
                currentFish.avatar.style.backgroundColor = 'rgb(' + Math.floor(256 + (redEnd - redStart) * iteration / 300) % 256 + ',' +
                    Math.floor(256 + (greenEnd - greenStart) * iteration / 300) % 256 + ',' +
                    Math.floor(256 + (blueEnd - blueStart) * iteration / 300) % 256 + ')';
            }
        }
    });
}

