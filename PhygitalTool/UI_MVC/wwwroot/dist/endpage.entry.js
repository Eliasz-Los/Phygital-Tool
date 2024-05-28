/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!***********************************!*\
  !*** ./src/ts/Endpage/endpage.ts ***!
  \***********************************/

// Purpose: Contains functions for the end page.
const btnNo = document.getElementById("btnNo");
const btnYes = document.getElementById("btnYes");
function InitializeEndpage() {
    window.addEventListener("keydown", function (e) {
        switch (e.code) {
            case 'ArrowRight':
                btnYes === null || btnYes === void 0 ? void 0 : btnYes.click();
                break;
            case 'ArrowLeft':
                btnNo === null || btnNo === void 0 ? void 0 : btnNo.click();
                break;
        }
    });
}
InitializeEndpage();

/******/ })()
;
//# sourceMappingURL=endpage.entry.js.map