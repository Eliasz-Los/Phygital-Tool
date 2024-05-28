/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Flow/Details/detailsRest.ts":
/*!********************************************!*\
  !*** ./src/ts/Flow/Details/detailsRest.ts ***!
  \********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   readImageData: () => (/* binding */ readImageData),
/* harmony export */   readMultipleChoiceQuestionsData: () => (/* binding */ readMultipleChoiceQuestionsData),
/* harmony export */   readOpenQuestionsData: () => (/* binding */ readOpenQuestionsData),
/* harmony export */   readRangeQuestionsData: () => (/* binding */ readRangeQuestionsData),
/* harmony export */   readSingleChoiceQuestionData: () => (/* binding */ readSingleChoiceQuestionData),
/* harmony export */   readTextData: () => (/* binding */ readTextData),
/* harmony export */   readVideoData: () => (/* binding */ readVideoData),
/* harmony export */   sendAnswers: () => (/* binding */ sendAnswers)
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
function readSingleChoiceQuestionData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/SingleChoiceQuestions`);
        if (response.ok) {
            return response.json();
        }
        else {
            throw new Error("Error fetching single choice questions");
        }
    });
}
function readOpenQuestionsData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/OpenQuestions`);
        if (!response.ok) {
            throw new Error("Error fetching open questions");
        }
        return yield response.json();
    });
}
function readRangeQuestionsData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/RangeQuestions`);
        if (!response.ok) {
            throw new Error("Error fetching range questions");
        }
        return yield response.json();
    });
}
function readMultipleChoiceQuestionsData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/MultipleChoiceQuestions`);
        if (!response.ok) {
            throw new Error("Error fetching multiple choice questions");
        }
        return yield response.json();
    });
}
function readTextData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/TextInfos`);
        if (!response.ok) {
            throw new Error("Error fetching text infos");
        }
        return yield response.json();
    });
}
function readImageData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/ImageInfos`);
        if (!response.ok) {
            throw new Error("Error fetching image infos");
        }
        return yield response.json();
    });
}
function readVideoData(flowId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/VideoInfos`);
        if (!response.ok) {
            throw new Error("Error fetching video infos");
        }
        return yield response.json();
    });
}
//TODO: fix AnswerObject type
function sendAnswers(flowId, answerObject) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/flows/${flowId}/AddAnswers`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(answerObject)
        });
        if (!response.ok) {
            throw new Error("Error committing answers");
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
/*!****************************************!*\
  !*** ./src/ts/Flow/Details/details.ts ***!
  \****************************************/
__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   commitAnswers: () => (/* binding */ commitAnswers),
/* harmony export */   getAnswers: () => (/* binding */ getAnswers),
/* harmony export */   getImageData: () => (/* binding */ getImageData),
/* harmony export */   getMultipleChoiceQuestionsData: () => (/* binding */ getMultipleChoiceQuestionsData),
/* harmony export */   getOpenQuestionsData: () => (/* binding */ getOpenQuestionsData),
/* harmony export */   getRangeQuestionsData: () => (/* binding */ getRangeQuestionsData),
/* harmony export */   getSingleChoiceQuestionData: () => (/* binding */ getSingleChoiceQuestionData),
/* harmony export */   getTextData: () => (/* binding */ getTextData),
/* harmony export */   getVideoData: () => (/* binding */ getVideoData),
/* harmony export */   handleScrollForVideoPlayback: () => (/* binding */ handleScrollForVideoPlayback),
/* harmony export */   updatePorgressBar: () => (/* binding */ updatePorgressBar)
/* harmony export */ });
/* harmony import */ var _detailsRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./detailsRest */ "./src/ts/Flow/Details/detailsRest.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
//Importeren van de functies

// elementen ophalen
const flowIdElement = document.getElementById("flowId");
const flowId = flowIdElement ? parseInt(flowIdElement.innerText) : 0;
const questionsElement = document.getElementById("questions");
const infoElements = document.getElementById("infoAccordion");
const keys = ['Key1', 'Key2', 'Key3', 'Key4']; // voor keydown event
window.updateLabel = function (input, labelId) {
    let label = document.getElementById(labelId);
    if (label) {
        label.textContent = input.getAttribute(`data-option-${input.value}`);
    }
};
window.totalQuestions = 0;
let firstQuestion = true;
let totalInformations = 0;
window.currentQuestionNumber = 1;
function getSingleChoiceQuestionData(numberOfPeople) {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readSingleChoiceQuestionData)(flowId)
            .then(singleChoiceQuestions => {
            let bodyData = ``;
            for (let i = 0; i < numberOfPeople; i++) {
                for (let j = 0; j < singleChoiceQuestions.length; j++) {
                    const singleChoiceQuestion = singleChoiceQuestions[j];
                    window.totalQuestions += 1;
                    const isActive = firstQuestion ? 'active' : '';
                    if (firstQuestion)
                        firstQuestion = false;
                    bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${singleChoiceQuestion.sequenceNumber}" data-card-id="${singleChoiceQuestion.id}">
            <div class="card-body">
                <h5 class="card-title">${singleChoiceQuestion.text}</h5>
                ${singleChoiceQuestion.options.map((option, index) => `<div class="form-check">
                    <input class="form-check-input" type="radio" name="option${singleChoiceQuestion.text}" id="option${singleChoiceQuestion.text}_${index}" data-key-index="${keys[index]}" value="${option}">
                    <label class="form-check-label" for="option${singleChoiceQuestion.text}_${index}" data-key-index="${keys[index]}">
                        ${option}
                    </label>
                </div>`).join('')}
            </div>
        </div>`;
                }
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "questions" not found');
            }
        }).catch(error => {
            console.error(error);
        });
    });
}
function getOpenQuestionsData(numberOfPeople) {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readOpenQuestionsData)(flowId)
            .then(openQuestions => {
            let bodyData = ``;
            for (let i = 0; i < numberOfPeople; i++) {
                for (let j = 0; j < openQuestions.length; j++) {
                    const openQuestion = openQuestions[j];
                    window.totalQuestions += 1;
                    const isActive = firstQuestion ? 'active' : '';
                    if (firstQuestion)
                        firstQuestion = false;
                    bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${openQuestion.sequenceNumber}" data-card-id="${openQuestion.id}">
                        <div class="card-body">
                            <h5 class="card-title">${openQuestion.text}</h5>
                            <div class="form-group">
                                <textarea type="text" class="form-control" id="openQuestion${openQuestion.text}" rows="3"></textarea>
                            </div>
                        </div>
                    </div>`;
                }
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "questions" not found');
            }
        }).catch(error => {
            console.error(error);
        });
    });
}
function getRangeQuestionsData(numberOfPeople) {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readRangeQuestionsData)(flowId)
            .then(rangeQuestions => {
            let bodyData = ``;
            for (let i = 0; i < numberOfPeople; i++) {
                for (let i = 0; i < rangeQuestions.length; i++) {
                    const rangeQuestion = rangeQuestions[i];
                    window.totalQuestions += 1;
                    const isActive = firstQuestion ? 'active' : '';
                    if (firstQuestion)
                        firstQuestion = false;
                    let options = rangeQuestion.options.map((option, index) => `data-option-${index}="${option}"`).join('');
                    bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${rangeQuestion.sequenceNumber}" data-card-id="${rangeQuestion.id}">
                        <div class="card-body">
                            <h5 class="card-title">${rangeQuestion.text}</h5>
                            <div class="form-group">
                                <input type="range" class="form-control-range" id="formControlRange${i}" min="0" max="${rangeQuestion.options.length - 1}" 
                                        ${options} oninput="updateLabel(this, 'rangeLabel${i}')"> <!--oninput="updateLabel(this, 'rangeLabel${i}')"-->
                                <label id="rangeLabel${i}" for="formControlRange${i}"></label>
                            </div>
                        </div>
                    </div>`;
                }
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "questions" not found');
            }
        })
            .catch(error => {
            console.log(error);
        });
    });
}
function getMultipleChoiceQuestionsData(numberOfPeople) {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readMultipleChoiceQuestionsData)(flowId)
            .then(multipleChoiceQuestions => {
            let bodyData = ``;
            for (let i = 0; i < numberOfPeople; i++) {
                for (const multipleChoiceQuestion of multipleChoiceQuestions) {
                    window.totalQuestions += 1;
                    const isActive = firstQuestion ? 'active' : '';
                    if (firstQuestion)
                        firstQuestion = false;
                    bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${multipleChoiceQuestion.sequenceNumber}" data-card-id="${multipleChoiceQuestion.id}">
                        <div class="card-body">
                            <h5 class="card-title">${multipleChoiceQuestion.text}</h5>
                            ${multipleChoiceQuestion.options.map((option, index) => `<div class="form-check">
                                <input class="form-check-input" type="checkbox" name="${multipleChoiceQuestion.text}" id="${option}" data-key-index="${keys[index]}">
                                <label class="form-check-label" for="${option}" data-key-index="${keys[index]}">
                                    ${option}
                                </label>
                            </div>`).join('')}
                        </div>
                    </div>`;
                }
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "questions" not found');
            }
        })
            .catch(error => {
            console.log(error);
        });
    });
}
function getTextData() {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readTextData)(flowId)
            .then(textInfos => {
            let bodyData = ``;
            for (let i = 0; i < textInfos.length; i++) {
                totalInformations++;
                bodyData += `
                    <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="true" aria-controls="collapse${totalInformations}">
                            ${textInfos[i].title}
                        </button>
                    </h2>
                    <div id="collapse${totalInformations}" class="accordion-collapse " aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                            ${textInfos[i].content}
                        </div>
                    </div></div>`;
            }
            if (infoElements) {
                infoElements.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "infoAccordion" not found');
            }
        })
            .catch(error => {
            console.log(error);
        });
    });
}
function getImageData() {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readImageData)(flowId)
            .then(imageInfos => {
            let bodyData = ``;
            for (let i = 0; i < imageInfos.length; i++) {
                totalInformations++;
                bodyData += `
                        <div class="accordion-item">
                        <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button " type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="true" aria-controls="collapse${totalInformations}">
                            ${imageInfos[i].title}
                        </button>
                    </h2>
                   
                    <div id="collapse${totalInformations}" class="accordion-collapse " aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                         <img src="${imageInfos[i].url.replace('~', '')}" class="d-block w-100" alt="${imageInfos[i].altText}">
                            ${imageInfos[i].altText}
                        </div>
                    </div>
                    </div>`;
            }
            if (infoElements) {
                infoElements.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "infoAccordion" not found');
            }
        })
            .catch(error => {
            console.log(error);
        });
    });
}
function getVideoData() {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.readVideoData)(flowId)
            .then(videos => {
            let bodyData = ``;
            for (let i = 0; i < videos.length; i++) {
                totalInformations++;
                bodyData += `
                        <div class="accordion-item">
                        <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="true" aria-controls="collapse${totalInformations}">
                            ${videos[i].title}
                        </button>
                    </h2>
                   
                    <div id="collapse${totalInformations}" class="accordion-collapse" aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                         <iframe width="560" height="315" src="https://www.youtube.com/embed/${videos[i].url}" title="${videos[i].title}" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                            <div class="spacing-top">${videos[i].description}</div>
                        </div>
                    </div>
            </div>`;
            }
            if (infoElements) {
                infoElements.innerHTML += bodyData;
            }
            else {
                console.error('Element with id "infoAccordion" not found');
            }
        })
            .catch(error => {
            console.log(error);
        });
    });
}
function getAnswers() {
    const answers = [];
    const carouselItems = document.querySelectorAll('.carousel-item');
    carouselItems.forEach((item, index) => {
        var _a;
        const questionText = ((_a = item.querySelector('.card-title')) === null || _a === void 0 ? void 0 : _a.textContent) || '';
        const questionId = Number(item.getAttribute('data-card-id') || '');
        const answer = { question: questionText, chosenOptions: [], openAnswer: '', id: questionId };
        const checkboxes = item.querySelectorAll('input[type="checkbox"]:checked');
        checkboxes.forEach(checkbox => {
            answer.chosenOptions.push(checkbox.id);
        });
        const textarea = item.querySelector('textarea');
        if (textarea) {
            answer.openAnswer = textarea.value;
        }
        const radioButtons = item.querySelectorAll('input[type="radio"]:checked');
        radioButtons.forEach(radioButton => {
            if (radioButton.checked) {
                answer.chosenOptions.push(radioButton.value);
            }
        });
        const rangeInput = item.querySelector('input[type="range"]');
        if (rangeInput) {
            let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
            if (optionText) {
                answer.chosenOptions.push(optionText);
            }
        }
        answers.push(answer);
    });
    return answers;
}
function commitAnswers() {
    return __awaiter(this, void 0, void 0, function* () {
        const answers = getAnswers();
        const answerObject = answers.map(answer => ({
            chosenOptions: answer.chosenOptions.map(option => ({ OptionText: option })),
            chosenAnswer: answer.openAnswer,
            questionId: answer.id
        }));
        yield (0,_detailsRest__WEBPACK_IMPORTED_MODULE_0__.sendAnswers)(flowId, answerObject)
            .then(response => {
            console.log(response);
        })
            .catch(error => {
            console.error(error);
        });
    });
}
function updatePorgressBar() {
    let progressPerc = 100 * (window.currentQuestionNumber / window.totalQuestions);
    let progressBar = document.getElementById("progressBar");
    progressBar.style.width = progressPerc + "%";
    progressBar.setAttribute("aria-valuenow", progressPerc.toString());
    console.log("progressbarPerc: ", progressPerc);
}
//Werkt nog niet, mag eventueel weg maar dan hebben we geen manier om video te spelen 🥲
function handleScrollForVideoPlayback() {
    let videos = document.querySelectorAll("iframe[id^='video']");
    videos.forEach((video) => {
        var _a, _b;
        let top_of_element = video.offsetTop;
        let bottom_of_element = top_of_element + video.offsetHeight;
        let bottom_of_screen = window.pageYOffset + window.innerHeight;
        let top_of_screen = window.pageYOffset;
        if ((bottom_of_screen > top_of_element) && (top_of_screen < bottom_of_element)) {
            // The element is visible, play the video
            (_a = video.contentWindow) === null || _a === void 0 ? void 0 : _a.postMessage('{"event":"command","func":"' + 'playVideo' + '","args":""}', '*');
        }
        else {
            // The element is not visible, pause the video
            (_b = video.contentWindow) === null || _b === void 0 ? void 0 : _b.postMessage('{"event":"command","func":"' + 'pauseVideo' + '","args":""}', '*');
        }
    });
}

})();

/******/ })()
;
//# sourceMappingURL=details.entry.js.map