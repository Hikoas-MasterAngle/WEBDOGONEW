document.addEventListener("DOMContentLoaded", function () {
    const vanchuyen = document.querySelectorAll('.xemvanchuyen');
    const bangchitietvanchuyen = document.querySelector('.chitietvanchuyen');
    const closechitietvanchuyen = document.querySelector('.closechitietvanchuyen');
    const nenlammo = document.querySelector('.overlay');

    // Đảm bảo rằng bạn đã định nghĩa chitietvanchuyen
    const chitietvanchuyen = document.querySelector('.chitietvanchuyen'); // Giả sử bạn có class chitietvanchuyen

    // Kiểm tra và in ra các phần tử
    console.log(bangchitietvanchuyen);
    console.log(closechitietvanchuyen);
    console.log(nenlammo);
    console.log(chitietvanchuyen); // Kiểm tra xem nó có đúng không

    vanchuyen.forEach(duyet => {
        if (duyet && duyet.innerHTML.toLowerCase() === "hoàn thành") {
            duyet.style.backgroundColor = "green";
        } else if (duyet && duyet.innerHTML.toLowerCase() === "hoàn hàng" || duyet && duyet.innerHTML.toLowerCase() === "giao hàng thất bại") {
            duyet.style.backgroundColor = "#FF4545";
        }
        else {
            duyet.style.backgroundColor = "yellow";
        }

        duyet.addEventListener('click', () => {
            // Hiển thị hoặc ẩn bảng chi tiết khi click
            if (bangchitietvanchuyen.style.display === 'none' || bangchitietvanchuyen.style.display === '') {
                bangchitietvanchuyen.style.display = 'block';
                nenlammo.style.display = "block";
            }
        });
    });

    closechitietvanchuyen.onclick = function () {
        bangchitietvanchuyen.style.display = "none"; // Sử dụng bangchitietvanchuyen
        nenlammo.style.display = "none";
    };
});
