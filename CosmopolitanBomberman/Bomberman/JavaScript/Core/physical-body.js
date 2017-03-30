'use strict';

class PhysicalBody {
    constructor(startX, startY, direction, width, height) {

        this.x = startX;
        this.y = startY;
        this.direction = direction;
        this.width = width;
        this.height = height;
    }

    updatePosition(dirDeltas) {
        this.x += dirDeltas[this.direction].x;
        this.y += dirDeltas[this.direction].y;
    }

    getFutureCoordinates(dirDeltas, keyCode, keyCodeDirs) {
        const futureDir = this._getFutureDirection(keyCode, keyCodeDirs);

        const x = this.x + dirDeltas[futureDir].x;
        const y = this.y + dirDeltas[futureDir].y;

        return {x: x, y: y};
    }

    updateDirection(keyCode, keyCodeDirs) {
        this.direction = keyCodeDirs[keyCode];
    }

    _getFutureDirection(keyCode, keyCodeDirs) {
        return keyCodeDirs[keyCode];
    }
}