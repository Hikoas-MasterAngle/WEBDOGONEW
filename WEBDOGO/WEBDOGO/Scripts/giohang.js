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