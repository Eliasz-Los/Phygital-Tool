// Purpose: Contains functions for the end page.
const btnNo = document.getElementById("btnNo") as HTMLButtonElement;
const btnYes = document.getElementById("btnYes") as HTMLButtonElement;

function Initialize() {
    window.addEventListener("keydown", function (e) {
        switch (e.code) {
            case 'ArrowRight':
                btnYes.click();
                break;
            case 'ArrowLeft':
                btnNo.click();
                break;
        }});}
Initialize();