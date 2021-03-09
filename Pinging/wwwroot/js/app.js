
const tracertButton = document.getElementById("example");

let ui = new UI();

eventListeners();

function eventListeners() {
    tracertButton.addEventListener("click", ipTracert);
}

function ipTracert(e) {
    if (e.target.textContent === "Tracert") {
        const hostName = e.target.getAttribute("id");
        //Button clasını pasif ediyoruz
        console.log("test");
        ui.tracertDisable();

        var host = {
            "HostName": hostName
        };

        let html = "";
        Request.PostTracert("Tracert", host)
            .then(data => {
                console.log(data);
                for (const [k, v] of data.entries()) {
                    html += `<p>${k + 1} - ${v}</p>`
                };

                Swal.fire({
                    html: html
                });
                //Button clasını tekrar aktif ediyoruz
                ui.tracertEnable();
            });
    }
}