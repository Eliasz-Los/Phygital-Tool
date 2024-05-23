/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/ts/Feedback/feedbackRest.ts":
/*!*****************************************!*\
  !*** ./src/ts/Feedback/feedbackRest.ts ***!
  \*****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   createDislikePost: () => (/* binding */ createDislikePost),
/* harmony export */   createLikePost: () => (/* binding */ createLikePost),
/* harmony export */   createReaction: () => (/* binding */ createReaction),
/* harmony export */   deleteReaction: () => (/* binding */ deleteReaction),
/* harmony export */   readReactions: () => (/* binding */ readReactions)
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
function readReactions(postId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/feedbacks/${postId}/Reactions`);
        if (!response.ok) {
            throw new Error("Error fetching reactions");
        }
        return yield response.json();
    });
}
function createReaction(postId, reaction) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/feedbacks/${postId}/AddReaction`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(reaction)
        });
        if (!response.ok) {
            const error = yield response.json();
            throw { status: response.status, message: error };
        }
        return yield response.json();
    });
}
function deleteReaction(postId, reactionId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/feedbacks/${postId}/DeleteReaction/${reactionId}`, {
            method: 'POST'
        });
        if (!response.ok) {
            const error = yield response.text();
            throw { status: response.status, message: error.toString() };
        }
    });
}
function createLikePost(postId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/feedbacks/${postId}/LikePost`, {
            method: 'POST'
        });
        if (!response.ok) {
            throw new Error("Error liking post");
        }
        return yield response.json();
    });
}
function createDislikePost(postId) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield fetch(`/api/feedbacks/${postId}/DislikePost`, {
            method: 'POST'
        });
        if (!response.ok) {
            throw new Error("Error disliking post");
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
/*!*************************************!*\
  !*** ./src/ts/Feedback/feedback.ts ***!
  \*************************************/
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _feedbackRest__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./feedbackRest */ "./src/ts/Feedback/feedbackRest.ts");
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};

let posts = document.querySelectorAll('.post');
const likeButtons = document.querySelectorAll('.bi-hand-thumbs-up');
const dislikeButtons = document.querySelectorAll('.bi-hand-thumbs-down');
function getReactionsOfPost(postId) {
    return __awaiter(this, void 0, void 0, function* () {
        yield (0,_feedbackRest__WEBPACK_IMPORTED_MODULE_0__.readReactions)(postId)
            .then(reactions => {
            let bodyData = ``;
            reactions.forEach(reaction => {
                bodyData += `
                <div class="row d-flex spacing-top form-control">
                    <div class="input-group gap-2">
                        <div class="col-2 d-flex justify-content-start">
                            <p class="form-control">${reaction.accountName}</p>
                        </div>
                        <div class="col-8 d-flex justify-content-between">
                            <p class="form-control">${reaction.content}</p>
                        </div>
                        <div class="col ">
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-danger bi bi-trash" id="${reaction.id}"></button>
                            </div>
                        </div>
                    </div>
                </div>`;
            });
            let reactionList = document.getElementById(`listReactions_${postId}`);
            if (reactionList) {
                reactionList.innerHTML = bodyData;
            }
            else {
                console.log(`reactionList for post ${postId} is null`);
            }
            removeReaction(postId, reactions);
        })
            .catch(error => {
            console.error(error);
        });
    });
}
function getReactions() {
    return __awaiter(this, void 0, void 0, function* () {
        posts.forEach(post => {
            let postId = post.getAttribute('data-post-id');
            if (postId !== null) {
                getReactionsOfPost(parseInt(postId));
            }
        });
    });
}
function sendReaction(postId) {
    return __awaiter(this, void 0, void 0, function* () {
        let content = document.querySelector(`input[name="content_${postId}"]`);
        const reactionObject = {
            content: content.value
        };
        console.log(reactionObject);
        yield (0,_feedbackRest__WEBPACK_IMPORTED_MODULE_0__.createReaction)(postId, reactionObject)
            .then(reaction => {
            console.log(reaction);
            let reactionCountElement = document.getElementById(`reactionCount_${postId}`);
            if (reactionCountElement !== null) {
                let currentCount = parseInt(reactionCountElement.innerText);
                console.log(currentCount);
                reactionCountElement.innerText = (currentCount + 1).toString();
            }
        })
            .catch(error => {
            if (error.status === 400) {
                alert(error.message.errors.Content[0]);
            }
            else {
                console.error(error);
            }
        });
        content.value = '';
    });
}
function removeReaction(postId, reactions) {
    return __awaiter(this, void 0, void 0, function* () {
        reactions.forEach(reaction => {
            const deleteButton = document.getElementById(`${reaction.id}`);
            if (deleteButton) {
                deleteButton.addEventListener('click', (event) => __awaiter(this, void 0, void 0, function* () {
                    event.preventDefault();
                    yield (0,_feedbackRest__WEBPACK_IMPORTED_MODULE_0__.deleteReaction)(postId, reaction.id)
                        .then(() => {
                        console.log(`Reaction ${reaction.id} deleted`);
                        //todo: update reaction count in UI realtime => shouldn't go under 0
                        let reactionCountElement = document.getElementById(`reactionCount_${postId}`);
                        if (reactionCountElement !== null) {
                            let currentCount = parseInt(reactionCountElement.innerText);
                            console.log(currentCount);
                            reactionCountElement.innerText = (currentCount - 1).toString();
                        }
                        getReactionsOfPost(postId);
                    })
                        .catch(error => {
                        if (error.status === 403) {
                            alert('Je mag niet andermans reacties verwijderen');
                        }
                        else {
                            console.error(error);
                        }
                    });
                }));
            }
        });
    });
}
function handlePostLikes() {
    return __awaiter(this, void 0, void 0, function* () {
        likeButtons.forEach(button => {
            button.addEventListener('click', (event) => __awaiter(this, void 0, void 0, function* () {
                const postId = button.getAttribute('data-post-id');
                event.preventDefault();
                if (postId !== null) {
                    button.disabled = true;
                    yield (0,_feedbackRest__WEBPACK_IMPORTED_MODULE_0__.createLikePost)(parseInt(postId))
                        .then(result => {
                        console.log(`Post ${postId} liked`);
                        if (typeof result === 'object' && result !== null) {
                            const { likeCount, dislikeCount } = result;
                            const likeCountElement = document.getElementById(`likeCount_${postId}`);
                            const dislikeCountElement = document.getElementById(`dislikeCount_${postId}`);
                            if (likeCountElement !== null && dislikeCountElement !== null) {
                                likeCountElement.innerText = likeCount.toString();
                                dislikeCountElement.innerText = dislikeCount.toString();
                            }
                        }
                    })
                        .catch(error => {
                        console.error(error);
                    });
                    button.disabled = false;
                }
            }));
        });
    });
}
function handlePostDislikes() {
    return __awaiter(this, void 0, void 0, function* () {
        dislikeButtons.forEach(button => {
            button.addEventListener('click', (event) => __awaiter(this, void 0, void 0, function* () {
                const postId = button.getAttribute('data-post-id');
                event.preventDefault();
                if (postId !== null) {
                    button.disabled = true;
                    yield (0,_feedbackRest__WEBPACK_IMPORTED_MODULE_0__.createDislikePost)(parseInt(postId))
                        .then(result => {
                        console.log(`Post ${postId} disliked`);
                        if (typeof result === 'object' && result !== null) {
                            const { dislikeCount, likeCount } = result;
                            const likeCountElement = document.getElementById(`likeCount_${postId}`);
                            const dislikeCountElement = document.getElementById(`dislikeCount_${postId}`);
                            if (dislikeCountElement !== null && likeCountElement !== null) {
                                likeCountElement.innerText = likeCount.toString();
                                dislikeCountElement.innerText = dislikeCount.toString();
                            }
                        }
                    }).catch(error => {
                        console.error(error);
                    });
                    button.disabled = false;
                }
            }));
        });
    });
}
getReactions();
handlePostLikes();
handlePostDislikes();
posts.forEach(post => {
    let postId = post.getAttribute('data-post-id');
    if (postId !== null) {
        let reactionForm = document.getElementById(`reactionForm_${postId}`);
        reactionForm.addEventListener('submit', (event) => __awaiter(void 0, void 0, void 0, function* () {
            event.preventDefault();
            if (postId !== null) {
                yield sendReaction(parseInt(postId));
                yield getReactionsOfPost(parseInt(postId));
            }
        }));
    }
});

})();

/******/ })()
;
//# sourceMappingURL=feedback.entry.js.map