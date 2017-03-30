'use strict';

const door = {
    x: 1,
    y: 1,
    isPlaced: false,
    isVisible: false
};

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

function generateStones(matrix) {
    for (let i = 0; i < numberOfBricks; i += 1) {

        const row = getRandomInt(1, field.length - 1);
        const col = getRandomInt(1, field[0].length - 1);

        // check if random brick is in start position of bomberman
        if (!checkForHeroFreedom(row, col)) {
            i -= 1;
            continue;
        } else if ((row % 2 === 0 && col % 2 === 0)) {
            i -= 1;
            continue;
        } else {
            matrix[row] = matrix[row].substr(0, col) + BRICK_CHAR + matrix[row].substr(col + 1);
        }

        // place door
        if (!door.isPlaced) {
            door.x = col * CELL_SIZE;
            door.y = row * CELL_SIZE;
            console.log(row);
            console.log(col);
            door.isPlaced = true;
        }
    }
}

function drawGameField(field, context) {

    const nonWalkables = [];
    const bricks = [];

    for (let i = 0; i < field.length; i++) {
        for (let j = 0; j < field[0].length; j++) {

            const currentSymbol = field[i][j];

            let stoneToDraw;
            if (currentSymbol === WALL_CHAR) {
                stoneToDraw = wallImage;
            } else if (currentSymbol === BRICK_CHAR) {
                stoneToDraw = brickImage;
                bricks.push({x: CELL_SIZE * j, y: CELL_SIZE * i});
            } else {
                continue;
            }

            context.drawImage(stoneToDraw,
                0,
                0,
                wallImage.width,
                wallImage.height,
                j * CELL_SIZE,
                i * CELL_SIZE,
                CELL_SIZE,
                CELL_SIZE
            );

            nonWalkables.push({x: CELL_SIZE * j, y: CELL_SIZE * i});
        }
    }

    return {nonWalkables: nonWalkables, bricks: bricks};
}

function checkForHeroFreedom(row, col) {
    if ((row === 3 && col === 1) || (row === 4 && col === 1) || (row === 5 && col === 1) || (row === 3 && col === 2) ||
        (row === 2 && col === 1) || (row === 1 && col === 1) || (row === 1 && col === 2) || (row === 3 && col === 3)) {
        return false;
    }
    return true;
}
