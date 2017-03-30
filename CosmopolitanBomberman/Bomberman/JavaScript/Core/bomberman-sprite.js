'use strict';

class BombermanSprite extends Sprite {
    constructor(options) {
        super(options);

        this._sprites =
            localStorage.getItem('player-hero') === 'betty' ?
             [rightImg, downImg, leftImg, upImg] 
            : [georgeRight, georgeDown, georgeLeft, georgeUp];
    }

    updateSpriteSheet(dir) {
        this._spriteSheet = this._sprites[dir];
    }
}