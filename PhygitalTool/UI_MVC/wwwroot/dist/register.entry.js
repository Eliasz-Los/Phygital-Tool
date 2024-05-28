/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!************************************!*\
  !*** ./src/ts/Account/register.ts ***!
  \************************************/

// Purpose: Contains functions for the end page.
const registerSubmit = document.getElementById("registerSubmit");
function InitializeRegister() {
    let formElement = document.getElementById("registerForm");
    let openInput;
    if (formElement) {
        openInput = formElement.querySelector('input[type="email"]');
        if (openInput) {
            openInput.focus();
        }
    }
    document.addEventListener('click', function (e) {
        if (e.button === 0) {
            registerSubmit.click();
        }
    });
}
InitializeRegister();

/******/ })()
;
//# sourceMappingURL=register.entry.js.map