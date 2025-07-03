const passwordold = document.getElementById("passwordold");
const passwordnew = document.getElementById("passwordnew");
const replaypasswordnew = document.getElementById("replaypasswordnew");
const formdoimk = document.getElementById("formdoimk");

formdoimk.addEventListener('submit', function (e) {
    passwordold.value = chongXSS(passwordold.value);
    passwordnew.value = chongXSS(passwordnew.value);
    replaypasswordnew.value = chongXSS(replaypasswordnew.value);
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