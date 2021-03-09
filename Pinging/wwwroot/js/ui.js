class UI {

    constructor() {
       this.tracertButtons = document.querySelectorAll("a[aria-disabled]");
    }

    tracertDisable() {
       
        console.log(this.tracertButtons);
        this.tracertButtons.forEach(element =>
            element.className = "btn btn-secondary disabled")
    }

    tracertEnable() {

        this.tracertButtons.forEach(element =>
            element.className = "btn btn-secondary");
    }
}