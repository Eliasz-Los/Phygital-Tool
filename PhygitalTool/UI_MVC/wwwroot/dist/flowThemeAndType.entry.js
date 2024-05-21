/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!**************************************************!*\
  !*** ./src/ts/Flow/Creation/flowThemeAndType.ts ***!
  \**************************************************/

var _a;
function fillSubthemesSelect() {
    fetch(`/api/Themas/subthemas`, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => {
        if (response.status === 200) {
            return response.json();
        }
        else {
            alert("Something went wrong in the backend subthemas, check the console for more details!");
            return Promise.reject("Failed to fetch subthemas");
        }
    })
        .then(subThemas => {
        const output = document.getElementById("ThemaSelect");
        let bodyData = ``;
        for (const subThema of subThemas) {
            bodyData += `
                <option value="${subThema.id}" data-description="${subThema.description}">${subThema.title}</option>
            `;
        }
        output.innerHTML += bodyData;
    })
        .catch(error => {
        console.log(error);
    });
}
function addFlow() {
    const selectedType = document.getElementById('TypeSelect').value;
    const selectedTheme = document.getElementById('ThemaSelect');
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive = document.getElementById('ActiveCheckbox').checked;
    const data = {
        FlowType: selectedType,
        IsOpen: isActive,
        ThemeId: parseInt(selectedThemeId)
    };
    // Send POST request to the server
    fetch('/api/Flows/AddFlow', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
        if (response.ok) {
            // Handle success response
            console.log('Flow added successfully');
            // Redirect to the Index action with a query parameter to indicate refresh
            window.location.href = '/Flow/Index?refresh=true';
        }
        else {
            // Handle error response
            console.error('Failed to add flow');
        }
    })
        .catch(error => {
        console.error('Error:', error);
    });
}
(_a = document.getElementById("submitFlow")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", addFlow);
fillSubthemesSelect();

/******/ })()
;
//# sourceMappingURL=flowThemeAndType.entry.js.map