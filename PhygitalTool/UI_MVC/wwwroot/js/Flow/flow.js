const flowElementBody = document.getElementById("flowElementId")
const addButton = document.getElementById("answerQuestion")
const flowIdElement = document.getElementById("flowId")
const flowId = parseInt(flowIdElement.innerText)

const openFlowElement = document.getElementById("openFlowElementId")
const subThemasFlowElement = document.getElementById("subThemasFlowElementId")
const rangeQuestionsElement = document.getElementById("rangeQuestions")
function getSingleChoiceQuestionData() {
    fetch(`/api/flows/${flowId}/SingleChoiceQuestions`,
        {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.status === 200) {
                return response.json()
            } else {
                alert("Something went wrong ;(")
            }
        })
        .then(singleChoiceQuestions => {
            let bodyData = ``;
            for (const singleChoiceQuestion of singleChoiceQuestions) {
                bodyData += `<div class="card">
            <div class="card-body">
                <h5 class="card-title">${singleChoiceQuestion.text}</h5>
                ${singleChoiceQuestion.options.map(option => `<div class="form-check">
                    <input class="form-check-input" type="checkbox" value="" id="${option}">
                    <label class="form-check-label" for="${option}">
                        ${option}
                    </label>
                </div>`).join('')}
            </div>
        </div>`
            }
            flowElementBody.innerHTML = bodyData
        })
        .catch(error => {
            console.log(error)
        });
}

function getSubThemasData() {
    fetch(`http://localhost:5000/api/flows/${flowId}/SubThemas`,
        {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.status === 200) {
                return response.json()
            } else {
                alert("Something went wrong ;(")
            }
        })
        .then(subThemas => {
            let bodyData = ``;
            for (const subTheme of subThemas) {
                bodyData += `<div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">${subTheme.title}</h5>
                                    <p class="card-text">${subTheme.description}</p>
                                </div>
                             </div>`;
            }
            subThemasFlowElement.innerHTML = bodyData
        })
}


function getOpenQuestionsData() {
    fetch(`/api/flows/${flowId}/OpenQuestions`,
        {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.status === 200) {
                return response.json()
            } else {
                alert("Something went wrong in the backend, check the console for more details!")
            }
        })
        .then(openQuestions => {
            let bodyData = ``;
            for (const openQuestion of openQuestions) {
                bodyData += `<div class="card">
            <div class="card-body">
                <h5 class="card-title">${openQuestion.text}</h5>
                <div class="form-group">
                    <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
                </div>
            </div>
        </div>`
            }
            openFlowElement.innerHTML = bodyData
        })
        .catch(error => {
            console.log(error)
        });

}

function getRangeQuestionsData() {
    fetch(`/api/flows/${flowId}/RangeQuestions`,
        {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.status === 200) {
                return response.json()
            } else {
                alert("Something went wrong in the backend, check the console for more details!")
            }
        })
        .then(rangeQuestions => {
            let bodyData = ``;
            for (let i = 0; i < rangeQuestions.length; i++) {
                const rangeQuestion = rangeQuestions[i];
                console.log(rangeQuestion.options)
                //TODO: Fix volgorde van de options dat getoond wordt
                let options = rangeQuestion.options.map((option,index) => `data-option-${index}="${option}"`).join('')
                bodyData += `<div class="card">
            <div class="card-body">
                <h5 class="card-title">${rangeQuestion.text}</h5>
                <div class="form-group">
                    <input type="range" class="form-control-range" id="formControlRange${i}" min="0" max="${rangeQuestion.options.length -1}" ${options} oninput="updateLabel(this, 'rangeLabel${i}')">
                    <label id="rangeLabel${i}" for="formControlRange${i}"></label>
                </div>
            </div>
        </div>`
            }
            rangeQuestionsElement.innerHTML = bodyData
        })
        .catch(error => {
            console.log(error)
        });

}
function updateLabel(rangeInput, labelId) {
    let label = document.getElementById(labelId);
    let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
    label.textContent = optionText;
}

function commitAnswer() {
    // TODO : save answer & go to next flowElement
}

getSingleChoiceQuestionData();
getOpenQuestionsData();
getRangeQuestionsData();
getSubThemasData();
addButton.addEventListener("click", commitAnswer);