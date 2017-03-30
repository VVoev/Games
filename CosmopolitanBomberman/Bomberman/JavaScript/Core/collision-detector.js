'use strict';

class CollisionDetector {
    constructor() {

    }

    areColliding(firstBody, secondBody, width, height) {
        const areColliding = (firstBody.x < secondBody.x + width && firstBody.x + width > secondBody.x) &&
            (firstBody.y < secondBody.y + height && firstBody.y + height > secondBody.y);

        return areColliding;
    }

    haveSameCoordinates(firstBody, secondBody) {
        const haveSameCoordinates = firstBody.x === secondBody.x && firstBody.y === secondBody.y;
        return haveSameCoordinates;
    }

    areCollidingAsCircles(firstBody, secondBody) {
        const offset = 10;

        const firstBodyCenterPoint = { x: firstBody.x + firstBody.width / 2, y: firstBody.y + firstBody.height / 2 };
        const secondBodyCenterPoint = { x: secondBody.x + secondBody.width / 2, y: secondBody.y + secondBody.height / 2 };

        const firstBodyRadius = (firstBody.width + firstBody.height) / 4;
        const secondBodyRadius = (secondBody.width + secondBody.height) / 4;

        const diffX = firstBodyCenterPoint.x - secondBodyCenterPoint.x;
        const diffY = firstBodyCenterPoint.y - secondBodyCenterPoint.y;

        const distance = Math.sqrt(diffX * diffX + diffY * diffY);

        const areColliding = distance < firstBodyRadius + secondBodyRadius - offset;

        return areColliding;
    }
}