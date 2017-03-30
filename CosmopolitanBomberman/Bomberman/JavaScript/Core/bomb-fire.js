function createBombFire(x, y, context) {
    const rightFireSprite = new BombFireSprite({
        context: context,
        spriteSheet: rightFireImg,
        totalTicksPerFrame: 10,
        width: CELL_SIZE,
        height: CELL_SIZE,
        totalSprites: 6,
    });

    const rightFireBody = new PhysicalBody(x, y, 0, CELL_SIZE, CELL_SIZE);

    return { sprite: rightFireSprite, body: rightFireBody };
}

class BombFireSprite extends Sprite {
    constructor(options) {
        super(options);

        this.isBlown = false;
    }

    update() {

        this._currentTicks += 1;

        if (this._currentTicks >= this._totalTicksPerFrame) {

            this._currentSpriteIndex += 1;

            if (this._currentSpriteIndex >= this._totalSprites) {
                this.isBlown = true;
                this._currentSpriteIndex = 0;
            }

            this._currentTicks = 0;
        }

        return this;
    }
}