let timeout;

function resetInactivityTimer() {
    clearTimeout(timeout);

    timeout = setTimeout(() => {
        Swal.fire({
            icon: 'warning',
            title: 'Tự động đăng xuất',
            text: 'Bạn đã không hoạt động trong 30 phút. Hệ thống sẽ đăng xuất!',
            confirmButtonText: 'OK',
            allowOutsideClick: false,
            allowEscapeKey: false
        }).then(() => {
            fetch("/Dangxuat/dangxuat", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                }
            }).then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    console.error("Đã xảy ra lỗi khi đăng xuất");
                }
            });
        });
    }, 60 * 60 * 1000);
}

["click", "mousemove", "keypress", "scroll", "touchstart"].forEach(event => {
    document.addEventListener(event, resetInactivityTimer);
});

resetInactivityTimer();
