function showSection(sectionId) {
    const sections = document.querySelectorAll('.content-section');
    sections.forEach(section => {
        section.style.display = 'none'; // Ẩn tất cả các phần
    });

    const activeSection = document.getElementById(sectionId);
    if (activeSection) {
        activeSection.style.display = 'block'; // Hiện phần được chọn
    }
}

document.addEventListener("DOMContentLoaded", function() {
    var close_button = document.querySelector('.close-button');
    var nuthienthithongtinchitiet = document.querySelectorAll('.nutthongtinchitiet');
    var hienthithongtinchitiet = document.querySelector('.thongtinchitiet');

    // Thêm sự kiện cho tất cả các nút có class "nutthongtinchitiet"
    nuthienthithongtinchitiet.forEach(function(nut) {
        nut.onclick = function() {
            hienthithongtinchitiet.style.display = "block";
        }
    });

    // Thêm sự kiện đóng cho nút close-button
    if (close_button && hienthithongtinchitiet) {
        close_button.onclick = function() {
            hienthithongtinchitiet.style.display = "none";
        };
    }
});
