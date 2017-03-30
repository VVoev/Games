'use strict';

const canvasContainer = document.getElementById('canvas-container'),
    WIDTH = 600,
    HEIGHT = 500;

const paper = new Raphael(450, 150, WIDTH, HEIGHT);

var img;

const bettyImages = [
    document.getElementById('betty-first'),
    document.getElementById('betty-second'),
    document.getElementById('betty-third'),
    document.getElementById('betty-fourth')
];

const georgeImages = [
    document.getElementById('george-first'),
    document.getElementById('george-second'),
    document.getElementById('george-third'),
    document.getElementById('george-fourth')
];

let currentImageSprites = georgeImages;

const bettySelect = document.getElementById('betty-select'),
    georgeSelect = document.getElementById('george-select'),
    startButton = document.getElementsByTagName('BUTTON')[0];

startButton.addEventListener('click', function() {

    const playerHero = georgeSelect.checked ? "george" : "betty";

    localStorage.setItem('player-hero', playerHero);

    setTimeout(function() {
        window.location = 'game.html';
    }, 500);
});

bettySelect.checked = true;
const ticksPerFrame = 5;

let currentTicks = 0,
    spriteIndex = 0;

function selectionLoop() {

    if (georgeSelect.checked) {
        currentImageSprites = georgeImages;
    } else {
        currentImageSprites = bettyImages;
    }

    if (currentTicks >= ticksPerFrame) {
        spriteIndex = (spriteIndex + 1) % bettyImages.length;

        paper.clear();
        img = paper.image(currentImageSprites[spriteIndex].src, 0, 0, 300, 300)
            .attr({ "clip-rect": "0,0,300,300" });

        currentTicks = 0;
    }

    currentTicks += 1;

    window.requestAnimationFrame(selectionLoop);
}

selectionLoop();