'use strict';

let PLAYER_USERNAME;
let isPlayerDead = false;

const field = [
    "***************************",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "* * * * * * * * * * * * * *",
    "*                         *",
    "***************************"
];

// constants
const CELL_SIZE = 37,
    WALL_CHAR = '*',
    BRICK_CHAR = '-',
    INITIAL_BOMBS_COUNT = 3;
// bomberman sprite constants
const BOMBERMAN_SPRITE_TICKS_FRAME = 5,
    BOMBERMAN_TOTAL_SPRITESHEETS = 4;

// images and sprites
const wallImage = document.getElementById('wall-image'),
    brickImage = document.getElementById('brick-image'),
    leftImg = document.getElementById('left'),
    rightImg = document.getElementById('right'),
    upImg = document.getElementById('up'),
    downImg = document.getElementById('down'),
    bombImg = document.getElementById('bomb-sprite'),
    enemyImg = document.getElementById('enemy-sprite'),
    rightFireImg = document.getElementById('right-fire-sprite'),
    exitDoor = document.getElementById('exit-door'),
    georgeRight = document.getElementById('george-right'),
    georgeLeft = document.getElementById('george-left'),
    georgeUp = document.getElementById('george-up'),
    georgeDown = document.getElementById('george-down'),
    gameOverImage = document.getElementById('game-over-image');

let level = Number(localStorage.getItem('on_load_counter')) || 0;
level++;

localStorage.setItem("on_load_counter", level);

let numberOfBricks = 70 + level * 5;
let numberOfEnemies = 4 + level;