/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!*********************************************!*\
  !*** ./src/ts/Organisation/organisation.ts ***!
  \*********************************************/

function addOrganisatie() {
    const orgName = document.getElementById('orgName');
    const orgDescription = document.getElementById('orgDescription');
    const orgObject = {
        name: orgName.value,
        description: orgDescription.value
    };
    fetch('/api/Organisatie/AddOrganisatie', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orgObject)
    })
        .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
        .then(data => {
        console.log('Success:', data);
    })
        .catch((error) => {
        console.error('Error:', error);
    });
}
// Add event listener to the submit button
const submitButton = document.getElementById('submitOrg');
if (submitButton) {
    submitButton.addEventListener('click', addOrganisatie);
}

/******/ })()
;
//# sourceMappingURL=organisation.entry.js.map