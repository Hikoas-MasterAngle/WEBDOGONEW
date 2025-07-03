const congsoluong = document.getElementById('them');
const giamsoluong = document.getElementById('giam');
const textthemsoluong = document.getElementById('textthemsoluong');
var soluong = 1;

congsoluong.onclick = function () {
    soluong++;
    textthemsoluong.value = soluong;
}

giamsoluong.onclick = function () {
    if (soluong > 1) {
        soluong--;
    }
    textthemsoluong.value = soluong;
}