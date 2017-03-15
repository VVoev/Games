function createGame(selector) {
    let isMouthOpen = false;
    let canvas = document.querySelector(selector);
    let ctx = canvas.getContext('2d');
    let isEaten = false;
    const offset = 15;
    let packman = {
        x:30,
        y:30,
        size:30,
        speed:10
    }
    let food = {
        x:300,
        y:300,
        size:7.5
    }
    let dir = 0;
    let keyCodeDirs = {
        37:2,
        38:3,
        39:0,
        40:1
    }
    let dirDeltas =[
        {
            x:+packman.speed,
            y:0
        },
        {
            x:0,
            y:+packman.speed
        },
        {
            x:-packman.speed,
            y:0
        },
        {
            x:0,
            y:-packman.speed
        },
    ]
    /*
     0 => right
     1 => down
     2 => left
     3 => up
     */


    let steps = 0 ;
    const stepsToChangeMouth = 10;

    document.body.addEventListener("keydown",function (ev) {
        if(!keyCodeDirs.hasOwnProperty(ev.keyCode)){
            return;
        }
        dir = keyCodeDirs[ev.keyCode];
    })



    
    function gameLoop() {
        ctx.clearRect(0,0,1000,800);
        drawPackMan();
        isFoodEaten();
        openAndCloseMouth();
        updatePackManPosition(packman,canvas,dirDeltas,dir);
        window.requestAnimationFrame(gameLoop)
    }

    function openAndCloseMouth() {
        steps +=1;
        if(steps>stepsToChangeMouth){
            isMouthOpen = !isMouthOpen;
            steps=1;
        }
    }

    function drawPackMan() {
        ctx.beginPath();
        ctx.fillStyle = 'yellow';

        if(isMouthOpen){
            let delta = dir*Math.PI/2;
            ctx.arc(packman.x,packman.y,packman.size,delta+Math.PI /4,delta+7 * Math.PI / 4);
            ctx.lineTo(packman.x,packman.y);
        }
        else{
            ctx.arc(packman.x,packman.y,packman.size,0,2*Math.PI);
        }
        ctx.fill();
    }

    function generateFood() {
        ctx.fillStyle = 'red';
        ctx.beginPath();
        food.x = Math.random(0,canvas.width)*canvas.width;
        food.y = Math.random(0,canvas.height)*canvas.height;
        ctx.arc(food.x,food.y,food.size,0,2*Math.PI);
        ctx.fill()
    }

    function isFoodEaten() {
        if(packman.x === food.x && packman.y === food.y){
            isEaten = true;
        }
        else if(!isEaten){
            generateFood();
            isEaten = true;
        }



    }


    return{
        start:gameLoop()
    }

}