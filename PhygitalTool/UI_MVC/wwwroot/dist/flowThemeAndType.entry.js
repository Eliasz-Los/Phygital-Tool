/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Flow/Creation/flowThemeAndTypeRest.ts":
/*!******************************************************!*\
  !*** ./src/ts/Flow/Creation/flowThemeAndTypeRest.ts ***!
  \******************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   addFlow: () => (/* binding */ addFlow),
/* harmony export */   fillSubthemesSelect: () => (/* binding */ fillSubthemesSelect)
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
function fillSubthemesSelect() {
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
function addFlow(flowObject) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch('/api/Flows/AddFlow', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(flowObject)
        });
        if (response.ok) {
            // Handle success response
            console.log('Flow added successfully');
        }
        else {
            // Handle error response
            console.error('Failed to add flow');
        }
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
/*!**************************************************!*\
  !*** ./src/ts/Flow/Creation/flowThemeAndType.ts ***!
  \**************************************************/
__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   addFlowData: () => (/* binding */ addFlowData)
/* harmony export */ });
/* harmony import */ var _flowThemeAndTypeRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./flowThemeAndTypeRest */ "./src/ts/Flow/Creation/flowThemeAndTypeRest.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};


const addButton = document.getElementById("submitFlow");
const addQuestion = document.getElementById("getquestion");
function addFlowData() {
    return __awaiter(this, void 0, void 0, function* () {
        const selectedType = document.getElementById('TypeSelect');
        const selectedTheme = document.getElementById('ThemaSelect');
        const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
        const isActive = document.getElementById('ActiveCheckbox');
        const data = {
            FlowType: selectedType.value,
            IsOpen: isActive.checked,
            ThemeId: parseInt(selectedThemeId)
        };
        yield (0,_flowThemeAndTypeRest__WEBPACK_IMPORTED_MODULE_0__.addFlow)(data)
            .then(response => {
            console.log(response);
        })
            .catch(error => {
            console.error(error);
        });
    });
}
function populateSubthemesSelect() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const subThemas = yield (0,_flowThemeAndTypeRest__WEBPACK_IMPORTED_MODULE_0__.fillSubthemesSelect)();
            const output = document.getElementById("ThemaSelect");
            if (!output) {
                throw new Error("ThemaSelect element not found");
            }
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `
                <option value="${subThema.id}" data-description="${subThema.description}">${subThema.title}</option>
            `;
            }
            output.innerHTML += bodyData;
        }
        catch (error) {
            console.error('Error:', error);
            alert("Something went wrong while fetching subthemes. Check the console for more details.");
        }
    });
}
populateSubthemesSelect();
if (addButton) {
    addButton.addEventListener("click", addFlowData);
}
if (addQuestion) {
    addQuestion.addEventListener("click", addFlowData);
}

})();

/******/ })()
;
//# sourceMappingURL=flowThemeAndType.entry.js.map