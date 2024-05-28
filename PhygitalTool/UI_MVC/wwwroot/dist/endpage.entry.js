/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!***********************************!*\
  !*** ./src/ts/Endpage/endpage.ts ***!
  \***********************************/

// Purpose: Contains functions for the end page.
const btnNo = document.getElementById("btnNo");
const btnYes = document.getElementById("btnYes");
function Initialize() {
    window.addEventListener("keydown", function (e) {
        switch (e.code) {
            case 'KeyD':
                btnYes.click();
                break;
            case 'KeyA':
                btnNo.click();
                break;
        }
    });
}
Initialize();

/******/ })()
;
//# sourceMappingURL=endpage.entry.js.map