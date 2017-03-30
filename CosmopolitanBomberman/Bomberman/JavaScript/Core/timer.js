class Timer {
    constructor() {
        this.h2 = document.getElementById('timer');
        this.h2.innerText += 500;
    }

    updateTimer() {
        let number = Number(this.h2.innerText.split(':')[1]);
        let text = this.h2.innerText.split(':')[0] + ":";
        number -= 1;
        this.h2.innerText = text + number;

        return number;
    }

    get time() {
        return Number(this.h2.innerText.split(':')[1]);
    }
}