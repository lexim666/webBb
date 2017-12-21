var fieldHeight = 1000;
var fieldWidth = 1000;
var field = Array(fieldWidth);
var mirrorField = Array(fieldWidth);
var c = document.getElementById("myCanvas");
var ctx = c.getContext("2d");
ctx.fillStyle = "black";
ctx.scale(5, 5);


Rand(); //создание исходного вида поля путем случайного заполнения ячеек

pampam(); //вызов главного цикла


function pampam() {
    requestId = undefined;
    draw();
    update();
    start();
}

function start() {
    if (!requestId) {
        requestId = window.requestAnimationFrame(pampam);
    }
}

function Array(rows) {
  
    var array = [];
    for (var i = 0; i < rows; i++) {
        array[i] = [];
    }
    return array;
}

function Rand() {
    for (var j = 1; j < fieldHeight - 1; j++) {

        for (var k = 1; k < fieldWidth - 1; k++) {

            field[j][k] = Math.round(Math.random());
        }
    }
}

function draw() {
    ctx.clearRect(0, 0, fieldHeight, fieldWidth);
    for (var j = 1; j < fieldHeight; j++) {
        for (var k = 1; k < fieldWidth; k++) {
            if (field[j][k] === 1) {
                ctx.fillRect(j, k, 1, 1);
            }
        }
    }
}

function update() {
    for (var j = 1; j < fieldHeight - 1; j++) {

        for (var k = 1; k < fieldWidth - 1; k++) {

            var totalCells = 0;
            totalCells += field[j - 1][k - 1];
            totalCells += field[j - 1][k];
            totalCells += field[j - 1][k + 1];

            totalCells += field[j][k - 1];
            totalCells += field[j][k + 1];

            totalCells += field[j + 1][k - 1];
            totalCells += field[j + 1][k];
            totalCells += field[j + 1][k + 1];
     
            switch (totalCells) {
                case 2:
                    mirrorField[j][k] = field[j][k];
                    break;
                case 3:
                    mirrorField[j][k] = 1;
                    break;

                default:
                    mirrorField[j][k] = 0;
            }
        }
    }
    
    for (var l = 1; l < fieldHeight - 1; l++) {
        mirrorField[l][0] = mirrorField[l][fieldHeight - 3];
        mirrorField[l][fieldHeight - 2] = mirrorField[l][1];
        mirrorField[0][l] = mirrorField[fieldHeight - 3][l];
        mirrorField[fieldHeight - 2][l] = mirrorField[1][l];
    }

    
    var temp = field;
    field = mirrorField;
    mirrorField = temp;
}
