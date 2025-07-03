var thongbao1 = document.getElementById("thongbaofalse");
console.log(thongbao1);

function hienthithongbaothanhcong1() {
    if (thongbao1) {  // Kiểm tra nếu phần tử tồn tại
        thongbao1.style.visibility = "visible";

        setTimeout(function () {
            thongbao1.style.visibility = "hidden";
        }, 20000);
    } else {
        console.warn("Element with ID 'thongbaofalse' không tồn tại.");
    }
}

hienthithongbaothanhcong1();