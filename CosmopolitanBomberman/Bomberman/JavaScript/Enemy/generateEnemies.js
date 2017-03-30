function generateEnemies(field, enemiesCount, enemiesContext) {
    const enemies = [];

    while (enemiesCount > 0) {
        const row = getRandomInt(1, field.length - 1);
        const col = getRandomInt(1, field.length - 1);

        if (field[row][col] !== WALL_CHAR && field[row][col] !== BRICK_CHAR && checkForHeroFreedom(row, col)) {
            const enemyBody = new PhysicalBody(col * CELL_SIZE, row * CELL_SIZE, getRandomInt(0, 3), CELL_SIZE, CELL_SIZE);

            const enemySprite = new Sprite({
                width: CELL_SIZE,
                height: CELL_SIZE,
                context: enemiesContext,
                spriteSheet: enemyImg,
                totalTicksPerFrame: 4,
                totalSprites: 4,
            });

            enemies.push({body: enemyBody, sprite: enemySprite});

            enemiesCount -= 1;
        }
    }

    return enemies;
}