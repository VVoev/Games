'use strict';

class Sprite {
    constructor(options) {
        this.width = options.width;
        this.height = options.height;

        this._context = options.context;
        this._spriteSheet = options.spriteSheet;
        this._totalTicksPerFrame = options.totalTicksPerFrame;
        this._totalSprites = options.totalSprites;

        this._currentTicks = 0;
        this._currentSpriteIndex = 0;
    }

    render(drawCoordinates) {
        // void ctx.drawImage(image, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight); 
        this._context.drawImage(
            this._spriteSheet,
            this._currentSpriteIndex * (this._spriteSheet.width / this._totalSprites),
            0,
            this._spriteSheet.width / this._totalSprites,
            this._spriteSheet.height,
            drawCoordinates.x,
            drawCoordinates.y,
            this.width,
            this.height
        );

        return this;
    }

    update() {

        this._currentTicks += 1;

        if (this._currentTicks >= this._totalTicksPerFrame) {

            this._currentSpriteIndex += 1;

            if (this._currentSpriteIndex >= this._totalSprites) {

                this._currentSpriteIndex = 0;
            }

            this._currentTicks = 0;
        }

        return this;
    }
}