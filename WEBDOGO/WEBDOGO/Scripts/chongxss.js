function chongXSS(input) {
    if (!input) return '';

    input = input.replace(/<script.*?>.*?<\/script>/gi, '');

    input = input.replace(/<.*?>/g, '');

    const div = document.createElement('div');
    div.appendChild(document.createTextNode(input));
    return div.innerHTML;
}

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formTimKiem");
    const input = document.getElementById("tenSanPham");

    if (form && input) {
        form.addEventListener("submit", function (e) {
            input.value = chongXSS(input.value);
        });
    } else {
        console.warn("Không tìm thấy form hoặc input.");
    }
});

document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('#formDangNhap'); 
    if (!form) return;

    form.addEventListener('submit', function (e) {
        const username = document.querySelector('input[name="taikhoan"]');
        const password = document.querySelector('input[name="matkhau"]');

        const cleanUsername = chongXSS(username.value);
        const cleanPassword = chongXSS(password.value);

        username.value = cleanUsername;
        password.value = cleanPassword;
    });
});


const form = document.querySelector('#formDangKy');
const email = document.getElementById("email");
const usernamedangky = document.querySelector("input[name='TENDANGNHAP']");
const passworddangky = document.getElementById("matkhau");
const replaypassword = document.getElementById("matkhaunhaplai");

//if (!form) return;

form.addEventListener('submit', function (e) {
    email.value = chongXSS(email.value);
    usernamedangky.value = chongXSS(usernamedangky.value);
    passworddangky.value = chongXSS(passworddangky.value);
    replaypassword.value = chongXSS(replaypassword.value);

    if (!email.value || !usernamedangky.value || !passworddangky.value || !replaypassword.value) {
        e.preventDefault();

        Swal.fire({
            icon: 'warning',
            title: 'Lỗi',
            text: 'Bạn đang nhập thông tin không hợp lệ!',
            confirmButtonText: 'OK',
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    }
});


