// Purpose: Contains functions for the end page.
const registerSubmit: HTMLElement | null = document.getElementById("registerSubmit");

function InitializeRegister(): void {

    let element: Element = document.querySelector('.active')!;
    let openInput: HTMLInputElement | null;

    openInput = element.querySelector('input[type="text"]');
    if (openInput) {
        openInput.focus();
    }


    document.addEventListener('click', function (e) {
        if (e.button === 0) {
            (registerSubmit as HTMLInputElement).click();
        }
    });
}
InitializeRegister();