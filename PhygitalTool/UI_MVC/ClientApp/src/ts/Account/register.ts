// Purpose: Contains functions for the end page.
const registerSubmit: HTMLElement | null = document.getElementById("registerSubmit");

function InitializeRegister(): void {

    let formElement: HTMLElement | null = document.getElementById("registerForm");
    let openInput: HTMLInputElement | null;

    if (formElement) {
        openInput = formElement.querySelector('input[type="email"]');
        if (openInput) {
            openInput.focus();
        }
    }


    document.addEventListener('click', function (e) {
        if (e.button === 0) {
            (registerSubmit as HTMLInputElement).click();
        }
    });
}
InitializeRegister();