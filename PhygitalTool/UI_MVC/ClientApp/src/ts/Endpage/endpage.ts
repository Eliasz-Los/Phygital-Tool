// Purpose: Contains functions for the end page.
const btnNo: HTMLElement | null = document.getElementById("btnNo");
const btnYes: HTMLElement | null = document.getElementById("btnYes");

function InitializeEndpage(): void {
    window.addEventListener("keydown", function (e: KeyboardEvent) {
        switch (e.code) {
            case 'ArrowRight':
                btnYes?.click();
                break;
            case 'ArrowLeft':
                btnNo?.click();
                break;
        }
    });
}

InitializeEndpage();