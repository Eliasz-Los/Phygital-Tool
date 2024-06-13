/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Thema/themaList.ts":
/*!***********************************!*\
  !*** ./src/ts/Thema/themaList.ts ***!
  \***********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   fillSubthemesTable: () => (/* binding */ fillSubthemesTable)
/* harmony export */ });
/* harmony import */ var _themaListRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./themaListRest */ "./src/ts/Thema/themaListRest.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};

function fillSubthemesTable() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const subThemas = yield (0,_themaListRest__WEBPACK_IMPORTED_MODULE_0__.tableData)();
            let output = document.querySelector("#SubthemaTable");
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `<tr data-description="${subThema.description}">
                        <td>${subThema.title}</td>
                        <td><a class="bi bi-pencil-square" href=""></a></td>
                        <td><i class="bi bi-trash deleteIcon" data-id="${subThema.id}"></i></td>
                    </tr>`;
                console.log(subThema);
            }
            if (output) {
                output.innerHTML += bodyData;
            }
            const deleteIcons = document.querySelectorAll(".deleteIcon");
            deleteIcons.forEach(function () {
                this.addEventListener("click", function () {
                    let subThemaId = this.getAttribute("data-id");
                    if (subThemaId) {
                        (0,_themaListRest__WEBPACK_IMPORTED_MODULE_0__.deleteSubtheme)(subThemaId);
                    }
                });
            });
        }
        catch (error) {
            console.error(error);
        }
    });
}
fillSubthemesTable();


/***/ }),

/***/ "./src/ts/Thema/themaListRest.ts":
/*!***************************************!*\
  !*** ./src/ts/Thema/themaListRest.ts ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   deleteSubtheme: () => (/* binding */ deleteSubtheme),
/* harmony export */   tableData: () => (/* binding */ tableData)
/* harmony export */ });
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
function tableData() {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/Themas/subthemas`, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return yield response.json();
    });
}
function deleteSubtheme(idTheme) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield fetch("/api/Themas/deleteSubTheme/" + idTheme, {
                method: 'DELETE'
            });
            // Check if deletion was successful
            if (!response.ok) {
                throw Error('Unable to DELETE the theme: ' + response.status + ' ' + response.statusText);
            }
            console.log("Deletion successful");
        }
        catch (error) {
            console.error("Error deleting theme:", error);
            throw error;
        }
    });
}


/***/ }),

/***/ "./src/ts/Thema/themaRest.ts":
/*!***********************************!*\
  !*** ./src/ts/Thema/themaRest.ts ***!
  \***********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   addThemaData: () => (/* binding */ addThemaData)
/* harmony export */ });
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
function addThemaData(themaObject) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/Themas/AddSubThemas`, {
            method: "POST",
            body: JSON.stringify(themaObject),
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return yield response.json();
    });
}


/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => (Object.prototype.hasOwnProperty.call(obj, prop))
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// This entry need to be wrapped in an IIFE because it need to be isolated against other modules in the chunk.
(() => {
/*!*******************************!*\
  !*** ./src/ts/Thema/thema.ts ***!
  \*******************************/
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _themaRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./themaRest */ "./src/ts/Thema/themaRest.ts");
/* harmony import */ var _themaList__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./themaList */ "./src/ts/Thema/themaList.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
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
            yield (0,_themaRest__WEBPACK_IMPORTED_MODULE_0__.addThemaData)(themaObject)
                .then(response => {
                console.log(response);
            })
                .catch(error => {
                console.error(error);
            });
            (0,_themaList__WEBPACK_IMPORTED_MODULE_1__.fillSubthemesTable)();
        }
    });
}
if (addButton) {
    addButton.addEventListener("click", addThema);
}

})();

/******/ })()
;
//# sourceMappingURL=thema.entry.js.map