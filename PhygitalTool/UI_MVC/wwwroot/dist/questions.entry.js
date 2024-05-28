/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Questions/questionsRest.ts":
/*!*******************************************!*\
  !*** ./src/ts/Questions/questionsRest.ts ***!
  \*******************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   addQuestion: () => (/* binding */ addQuestion),
/* harmony export */   fillSubthemesSelect: () => (/* binding */ fillSubthemesSelect),
/* harmony export */   sendOptions: () => (/* binding */ sendOptions)
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
function sendOptions(options) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield fetch('/api/Questions/SaveOptions', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(options)
            });
            if (response.ok) {
                console.log('Options sent successfully');
            }
            else {
                console.error('Failed to send options');
            }
        }
        catch (error) {
            console.error('Error:', error);
        }
    });
}
function addQuestion(data) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield fetch('/api/Questions/AddQuestion', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (response.ok) {
                alert('Question added successfully');
            }
            else {
                const errorMessage = yield response.text();
                alert(`Failed to add question. Server response: ${errorMessage}`);
            }
        }
        catch (error) {
            console.error('Error adding question:', error);
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
/*!***************************************!*\
  !*** ./src/ts/Questions/questions.ts ***!
  \***************************************/
__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   addOption: () => (/* binding */ addOption),
/* harmony export */   addQuestionData: () => (/* binding */ addQuestionData),
/* harmony export */   collectOptions: () => (/* binding */ collectOptions)
/* harmony export */ });
/* harmony import */ var _questionsRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./questionsRest */ "./src/ts/Questions/questionsRest.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};



const optionButton = document.getElementById("OptionButton");
const submitQuestion = document.getElementById("submitQuestion");
function addOption() {
    return __awaiter(this, void 0, void 0, function* () {
        const input = document.getElementById('OptionTitle');
        const optionValue = input.value.trim();
        const optionList = document.getElementById('optionList');
        if (optionValue && optionList.childElementCount < 4) {
            if (optionValue.length > 30) {
                alert('Option cannot exceed 30 characters.');
                return;
            }
            const listItem = document.createElement('li');
            listItem.className = 'list-group-item d-flex justify-content-between align-items-center';
            listItem.textContent = optionValue;
            const removeButton = document.createElement('button');
            removeButton.className = 'btn btn-danger btn-sm';
            removeButton.textContent = 'Remove';
            removeButton.addEventListener('click', () => {
                optionList.removeChild(listItem);
                document.getElementById('OptionButton').disabled = false;
            });
            listItem.appendChild(removeButton);
            optionList.appendChild(listItem);
            input.value = ''; // Clear the input field after adding
            // Disable the button if there are 4 items in the list
            if (optionList.childElementCount >= 4) {
                document.getElementById('OptionButton').disabled = true;
            }
        }
    });
}
function collectOptions() {
    return __awaiter(this, void 0, void 0, function* () {
        const optionList = document.getElementById('optionList');
        const options = [];
        optionList.querySelectorAll('li').forEach((listItem) => {
            var _a;
            const optionText = (_a = listItem.firstChild.textContent) === null || _a === void 0 ? void 0 : _a.trim();
            if (optionText) {
                options.push(optionText);
            }
        });
        yield (0,_questionsRest__WEBPACK_IMPORTED_MODULE_0__.sendOptions)(options);
    });
}
function populateSubthemesSelect() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const subThemas = yield (0,_questionsRest__WEBPACK_IMPORTED_MODULE_0__.fillSubthemesSelect)();
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
function addQuestionData() {
    return __awaiter(this, void 0, void 0, function* () {
        yield collectOptions();
        const questionTitle = document.getElementById('QuestionTitle').value;
        const selectedTheme = document.getElementById('ThemaSelect');
        const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
        const isActive = document.getElementById('ActiveCheckbox').checked;
        const selectedType = document.getElementById('TypeSelect').value;
        const flowElement = document.getElementById('FlowId');
        let flowid = 0; // Default value
        if (flowElement) {
            const textContent = flowElement.textContent;
            if (textContent) {
                // TODO wack manier maar werkt
                flowid = parseInt(textContent, 10);
            }
        }
        const data = {
            Text: questionTitle,
            isActive: isActive,
            SubTheme: parseInt(selectedThemeId),
            Type: selectedType,
            FlowId: flowid
        };
        yield (0,_questionsRest__WEBPACK_IMPORTED_MODULE_0__.addQuestion)(data)
            .then(response => {
            alert(response);
        })
            .catch(error => {
            alert(error);
        });
    });
}
populateSubthemesSelect();
if (optionButton) {
    optionButton.addEventListener("click", addOption);
}
if (submitQuestion) {
    submitQuestion.addEventListener("click", addQuestionData);
}

})();

/******/ })()
;
//# sourceMappingURL=questions.entry.js.map