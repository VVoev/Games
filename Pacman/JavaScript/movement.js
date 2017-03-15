function updatePackManPosition(packman,canvas,dirDeltas,dir) {
    packman.x = (packman.x+canvas.width)% canvas.width
    packman.y = (packman.y+canvas.height)% canvas.height;
    packman.x +=dirDeltas[dir].x;
    packman.y +=dirDeltas[dir].y;
}