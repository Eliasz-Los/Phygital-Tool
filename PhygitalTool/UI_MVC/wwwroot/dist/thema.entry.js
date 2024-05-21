/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Thema/thema.ts":
/*!*******************************!*\
  !*** ./src/ts/Thema/thema.ts ***!
  \*******************************/
/***/ (function() {


var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const addButton = document.getElementById("submitThema");
function addThema() {
    return __awaiter(this, void 0, void 0, function* () {
        const themaNameInput = document.getElementById("ThemaInput");
        const themaDescInput = document.getElementById("DescriptionArea");
        if (themaNameInput && themaDescInput) {
            const themaName = themaNameInput.value;
            const themaDesc = themaDescInput.value;
            const themaObject = {
                title: themaName,
                description: themaDesc
            };
            yield addThemaData(themaObject)
                .then(response => {
                console.log(response);
            })
                .catch(error => {
                console.error(error);
            });
        }
    });
}
if (addButton) {
    addButton.addEventListener("click", addThema);
}


/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/ts/Thema/thema.ts"]();
/******/ 	
/******/ })()
;
//# sourceMappingURL=thema.entry.js.map