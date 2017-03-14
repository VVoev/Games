function createGame(selector) {
    let isMouthOpen = false;
    let canvas = document.querySelector(selector);
    let ctx = canvas.getContext('2d');
    let speed = 5;
    let packman = {
        x:30,
        y:30,
        size:30
    }
    let dir = 0;
    let keyCodeDirs = {
        37:2,
        38:3,
        39:0,
        40:1
    }
    let dirDeltas =[{
        x:+speed,
        y:0
    },
        {
            x:0,
            y:+speed
        },
        {
            x:-speed,
            y:0
        },
        {
            x:0,
            y:-speed
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
        console.log(dir)
    })



    
    function gameLoop() {
        ctx.fillStyle = 'yellow';
        ctx.clearRect(0,0,1000,800)
        ctx.beginPath();

        if(isMouthOpen){
            let delta = dir*Math.PI/2;
            ctx.arc(packman.x,packman.y,packman.size,delta+Math.PI /4,delta+7 * Math.PI / 4);
            ctx.lineTo(packman.x,packman.y);
        }
        else{
            ctx.arc(packman.x,packman.y,packman.size,0,2*Math.PI);
        }
        ctx.fill();
        steps +=1;
        if(steps>stepsToChangeMouth){
            isMouthOpen = !isMouthOpen;
            steps=1;
        }
        packman.x +=dirDeltas[dir].x;
        packman.y +=dirDeltas[dir].y;
        window.requestAnimationFrame(gameLoop)
    }


    return{
        start:gameLoop()
    }

}