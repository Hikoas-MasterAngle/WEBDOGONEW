document.addEventListener("DOMContentLoaded", function() {
    const duyetlist = document.querySelectorAll('.duyet');
    const vanchuyenlist = document.querySelectorAll('.vanchuyen');
    const tiendosanxuatlist = document.querySelectorAll('.tiendosanxuat');
    const chitietsanxuat = document.querySelector('.chitietsanxuat');
    const closechitietsanxuat = document.querySelector('.closechitietsanxuat');
    const chitietvanchuyen = document.querySelector('.chitietvanchuyen');
    const closechitietvanchuyen = document.querySelector('.closechitietvanchuyen');
    const nenlammo = document.querySelector('.overlay');

    duyetlist.forEach(duyet => {
        if (duyet && duyet.innerHTML.toLowerCase() === "duyệt") {
            duyet.style.backgroundColor = "green";
        }
        else {
            duyet.style.backgroundColor = "red";
        }
    })

    vanchuyenlist.forEach(vanchuyen => {
        if (vanchuyen && vanchuyen.innerHTML === "Hoàn thành") {
            vanchuyen.style.backgroundColor = "green";
        }
        else {
            vanchuyen.style.backgroundColor = "orange";
        }
    })

    tiendosanxuatlist.forEach(tiendo => {
        if (tiendo && tiendo.innerHTML === "Hoàn thành") {
            tiendo.style.backgroundColor = "green";
        }
        else {
            tiendo.style.backgroundColor = "orange";
        }
    })

 
    tiendosanxuatlist.forEach(tiendo => {
        tiendo.onclick = function() {
            chitietsanxuat.style.display = "block";
                    nenlammo.style.display = "block"
        }
    })

    vanchuyenlist.forEach(vanchuyen => {
        vanchuyen.onclick = function() {
            chitietvanchuyen.style.display = "block";
                    nenlammo.style.display = "block"
        }
    })

    closechitietvanchuyen.onclick = function() {
        chitietvanchuyen.style.display = "none";
        nenlammo.style.display = "none"
    }

    
    closechitietsanxuat.onclick = function() {
        chitietsanxuat.style.display = "none";
        nenlammo.style.display = "none"
    }

});

