var button = document.getElementById('btnxemthemmenu');
const allmenu = document.getElementById('allmenu');
console.log(allmenu);
    console.log(button);

button.onclick = function() {
    if(allmenu.style.visibility === "visible") {
        allmenu.style.visibility = "hidden";
        console.log("đã ẩn thành công");
    }
    else {
        allmenu.style.visibility = "visible";
        console.log("đã hiện thành công");
    }
}

document.addEventListener('DOMContentLoaded', function () {
    var thongbao = document.getElementById("thongbaothanhcong");
    console.log(thongbao); // Kiểm tra có null không

    function hienthithongbaothanhcong() {
        thongbao.style.visibility = "visible";

        setTimeout(function () {
            thongbao.style.visibility = "hidden";
        }, 3000);
    }

    hienthithongbaothanhcong();
});


document.addEventListener('DOMContentLoaded', function () {
    // Chọn tất cả các thẻ có class hiển thị giá tiền
    const giaTienElements = document.querySelectorAll(".gia, .giasanphamslide, .giasanpham, .giaslide, .giatien");

    giaTienElements.forEach(element => {
        // Lấy nội dung văn bản và chuyển sang số
        const giaTien = parseFloat(element.textContent.replace(/[^0-9.-]+/g, ""));

        // Kiểm tra nếu là số hợp lệ
        if (!isNaN(giaTien)) {
            // Chuyển thành định dạng VNĐ và cập nhật lại nội dung
            element.innerHTML = giaTien.toLocaleString('vi-VN') + ' ₫';
        }
    });
});


