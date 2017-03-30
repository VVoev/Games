function createGame(selector) {

    fillPlayerInfo();

    const bombermanCanvas = document.querySelector(selector),
        ctxBomberman = bombermanCanvas.getContext('2d'),
        bomb = document.getElementById('bomb-image'),
        bombCanvas = document.getElementById('bomb-canvas'),
        ctxBomb = bombCanvas.getContext('2d');

    bombermanCanvas.width = field[0].length * CELL_SIZE;
    bombermanCanvas.height = field.length * CELL_SIZE;

    bombCanvas.width = field[0].length * CELL_SIZE;
    bombCanvas.height = field.length * CELL_SIZE;

    const timer = new Timer();
    setInterval(function() {
        timer.updateTimer();
    }, 1000);

    generateStones(field);

    const nonWalkablesObj = drawGameField(field, ctxBomb);

    const nonWalkables = nonWalkablesObj.nonWalkables,
        bricks = nonWalkablesObj.bricks;

    const collisionDetector = new CollisionDetector();
    const bombs = [],
        fires = [];

    const bombermanInitialSpritesheet = localStorage.getItem('player-hero') === 'betty' ? rightImg : georgeRight;

    const bombermanBody = new PhysicalBody(CELL_SIZE, CELL_SIZE * 3, 0, CELL_SIZE, CELL_SIZE),
        bombermanSprite = new BombermanSprite({
            context: ctxBomberman,
            spriteSheet: bombermanInitialSpritesheet,
            totalTicksPerFrame: BOMBERMAN_SPRITE_TICKS_FRAME,
            width: CELL_SIZE,
            height: CELL_SIZE,
            totalSprites: BOMBERMAN_TOTAL_SPRITESHEETS,
        });

    const bomberman = getGameObject(bombermanSprite, bombermanBody),
        enemies = generateEnemies(field, numberOfEnemies, ctxBomberman);

    bomberman.bombsCount = INITIAL_BOMBS_COUNT;

    // 0 => right 1 => down 2 => left 3 => up 
    const keyCodeDirs = {
            37: 2,
            38: 3,
            39: 0,
            40: 1
        },
        speed = CELL_SIZE / 4,
        enemyDirDeltas = [{ x: 1, y: 0 }, { x: 0, y: 1 }, { x: -1, y: 0 }, { x: 0, y: -1 }],
        bombermanDirDeltas = [{ x: +speed, y: 0 }, { x: 0, y: +speed }, { x: -speed, y: 0 }, { x: 0, y: -speed }];

    function BodyHitsNonWalkable(bomberman, nonWalkables) {
        for (let i = 0; i < nonWalkables.length; i += 1) {

            if (collisionDetector.areColliding(bomberman, nonWalkables[i], CELL_SIZE, CELL_SIZE)) {
                return true;
            }
        }

        return false;
    }

    // moving bomberman logic
    document.body.addEventListener("keydown", function(ev) {
        if (!keyCodeDirs.hasOwnProperty(ev.keyCode)) {
            return;
        }

        const futureCoordinates = bomberman.body.getFutureCoordinates(bombermanDirDeltas, ev.keyCode, keyCodeDirs);

        if (BodyHitsNonWalkable(futureCoordinates, nonWalkables)) {
            return;
        }

        bomberman.body.updateDirection(ev.keyCode, keyCodeDirs);
        bomberman.body.updatePosition(bombermanDirDeltas);

        bomberman.sprite.updateSpriteSheet(bombermanBody.direction);
    });

    // placing bombs event
    document.body.addEventListener("keydown", function(ev) {

        if (ev.keyCode !== 32 || bomberman.bombsCount <= 0) {
            return;
        }

        bomberman.bombsCount -= 1;

        // creating new bomb sprite and coordinates body and adding to the field bombs
        const bombToPlaceSprite = new Sprite({
            context: ctxBomb,
            spriteSheet: bombImg,
            totalTicksPerFrame: 5,
            width: CELL_SIZE,
            height: CELL_SIZE,
            totalSprites: 5,
        });

        function checkIfCoordinatesAreModuleOfCellSize(cords) {
            if (cords % CELL_SIZE === 0) {
                return cords;
            } else {
                let reminder = cords % CELL_SIZE;
                let toAdd = CELL_SIZE - reminder;
                //backward && right
                if (bomberman.body.direction === 1 || bomberman.body.direction === 0) {
                    return cords + toAdd;
                }
                //upward && left
                if (bomberman.body.direction === 3 || bomberman.body.direction === 2) {
                    return cords - reminder;
                }
            }
        }

        let x = checkIfCoordinatesAreModuleOfCellSize(bombermanBody.x);
        let y = checkIfCoordinatesAreModuleOfCellSize(bombermanBody.y);

        const bombToPlaceBody = new PhysicalBody(x, y, 0, CELL_SIZE, CELL_SIZE),
            bombToPlace = getGameObject(bombToPlaceSprite, bombToPlaceBody);

        bombs.push(bombToPlace);

        setInterval(function() {
            bombs.forEach(b => b.sprite.update().render({ x: b.body.x, y: b.body.y }));
        }, 100);

        setTimeout(function() {
            const firstBomb = bombs.shift();

            destroyBricksInRange(firstBomb.body);
            ctxBomb.clearRect(firstBomb.body.x, firstBomb.body.y, firstBomb.body.width, firstBomb.body.height);

            bomberman.bombsCount += 1;
        }, 3000);
    });

    function destroyBricksInRange(bomb) {
        let targetBricks = bricks.filter((b) => {

            const diffX = Math.abs(b.x - bomb.x),
                diffY = Math.abs(b.y - bomb.y);

            let isInRange = (diffX === 0 && diffY <= CELL_SIZE) || (diffY === 0 && diffX <= CELL_SIZE);

            return isInRange;
        });

        const rightFire = createBombFire(bomb.x + CELL_SIZE, bomb.y, ctxBomberman),
            leftFire = createBombFire(bomb.x - CELL_SIZE, bomb.y, ctxBomberman),
            upFire = createBombFire(bomb.x, bomb.y + CELL_SIZE, ctxBomberman),
            downFire = createBombFire(bomb.x, bomb.y - CELL_SIZE, ctxBomberman),
            centerFire = createBombFire(bomb.x, bomb.y, ctxBomberman);

        fires.push(rightFire, leftFire, upFire, downFire, centerFire);

        targetBricks.forEach(b => {
            ctxBomb.clearRect(b.x, b.y, CELL_SIZE, CELL_SIZE);

            let targetIndex = nonWalkables.findIndex(brick => brick.x === b.x && brick.y === b.y);
            nonWalkables.splice(targetIndex, 1);
            // console.log(targetIndex);
        });
    }

    function gameLoop() {
        ctxBomberman.clearRect(0, 0, 1000, 800);
        bomberman.sprite.render({ x: bombermanBody.x, y: bombermanBody.y }).update();

        updateEnemies(enemies);

        fires.forEach(function(fire, index) {
            fire.sprite.render({ x: fire.body.x, y: fire.body.y }).update();

            if (collisionDetector.haveSameCoordinates(fire.body, door)) {
                door.isVisible = true;
            }

            if (fire.sprite.isBlown) {
                fires.splice(index, 1);
            }

            if (collisionDetector.areColliding(bomberman.body, fire.body, CELL_SIZE, CELL_SIZE)) {
                setTimeout(function() {
                    if (collisionDetector.areColliding(bomberman.body, fire.body, CELL_SIZE, CELL_SIZE)) {
                        isPlayerDead = true;
                    }
                }, 750);
            }

            enemies.forEach((enemy, index) => {
                if (collisionDetector.areColliding(enemy.body, fire.body, CELL_SIZE, CELL_SIZE)) {
                    enemies.splice(index, 1);
                }
            });

        });

        if (door.isVisible) {
            drawExitGate(exitDoor, ctxBomb, { x: door.x, y: door.y });
        }

        if (collisionDetector.haveSameCoordinates(bomberman.body, door)) {
            ctxBomberman.fillStyle = 'yellowgreen';
            ctxBomberman.font = "150px Georgia";
            let msg = 'Level ' + (level) + ' complete!';
            ctxBomberman.fillText(msg, 5, bombermanCanvas.height / 2, 1000);

            setTimeout(function() {
                window.location.reload(true);
            }, 3000);
            return;
        }

        if (isGameOver() || isPlayerDead || timer.time <= 0) {
            level = 0;
            localStorage.setItem("on_load_counter", level);

            setTimeout(drawGameOverImage, 200);

            return;
        }

        window.requestAnimationFrame(gameLoop);
    }

    function isGameOver() {
        let isBombermanDead = enemies.some(e => collisionDetector.areCollidingAsCircles(bomberman.body, e.body));

        return isBombermanDead;
    }

    function getGameObject(spriteObj, physicalBodyObj) {
        const gameObject = { sprite: spriteObj, body: physicalBodyObj };

        return gameObject;
    }


    window.addEventListener('keydown', function(ev) {
        if (ev.keyCode === 82 || ev.keyCode === 116) {
            window.location.reload(true);
            level = 0;
            localStorage.setItem('on_load_counter', level);
        }
    });

    function updateEnemies(enemies) {
        enemies.forEach(enemy => {
            enemy.sprite.render({ x: enemy.body.x, y: enemy.body.y }).update();

            const futureEnemyCoordinates = {
                x: enemy.body.x + enemyDirDeltas[enemy.body.direction].x,
                y: enemy.body.y + enemyDirDeltas[enemy.body.direction].y,
            };


            if (BodyHitsNonWalkable(futureEnemyCoordinates, nonWalkables)) {
                const initialDirection = enemy.body.direction;

                while (initialDirection === enemy.body.direction) {
                    enemy.body.direction = enemy.body.direction = getRandomInt(0, 4);
                }

                return;
            }

            enemy.body.updatePosition(enemyDirDeltas);
        });
    }

    function drawGameOverImage() {
        ctxBomberman.drawImage(gameOverImage, 0, 0, gameOverImage.width, gameOverImage.height, 0, 0, bombermanCanvas.width, bombermanCanvas.height);

        ctxBomberman.fillStyle = 'pink';
        ctxBomberman.font = "150px Georgia";
        let msg = 'You have reached level ' + (level + 1) + ', press R to reset game!';
        ctxBomberman.fillText(msg, 5, bombermanCanvas.height / 2 + 100, 1000);
    }

    return {
        start: gameLoop
    };
}