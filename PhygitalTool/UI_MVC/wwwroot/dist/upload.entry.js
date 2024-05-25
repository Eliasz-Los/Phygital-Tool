/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Upload/upload.ts":
/*!*********************************!*\
  !*** ./src/ts/Upload/upload.ts ***!
  \*********************************/
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
const formElement = document.getElementById("upload-form");
const uploadButton = document.getElementById("upload-button");
const imageElement = document.getElementById("uploaded-image");
function uploadFile() {
    return __awaiter(this, void 0, void 0, function* () {
        if (!formElement) {
            return;
        }
        const resultElement = formElement.elements.namedItem("result");
        const formData = new FormData(formElement);
        try {
            const response = yield fetch("/api/files", {
                method: "POST",
                body: formData
            });
            if (response.ok) {
                alert("Upload succeeded!");
            }
            resultElement.value = `Result: ${response.status} ${response.statusText}.`;
            if (response.status === 200) {
                const responseObject = yield response.json();
                imageElement.src = responseObject.url;
                console.log(`Here is the image in the cloud: ${responseObject.url}`);
            }
        }
        catch (error) {
            console.error("Error:", error);
        }
    });
}
uploadButton.addEventListener("click", uploadFile);


/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/ts/Upload/upload.ts"]();
/******/ 	
/******/ })()
;
//# sourceMappingURL=upload.entry.js.map