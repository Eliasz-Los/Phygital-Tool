const flowElementBody = document.getElementById("flowElementId")
const addButton = document.getElementById("answerQuestion")
const flowIdElement = document.getElementById("flowId")
const flowId = parseInt(flowIdElement.innerText)

const openFlowElement = document.getElementById("openFlowElementId")
const subThemasFlowElement = document.getElementById("subThemasFlowElementId")
const rangeQuestionsElement = document.getElementById("rangeQuestions")
const multipleChoiceQuestionsElement = document.getElementById("multipleChoiceQuestions")

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
                alert("Something went wrong with placing or reading the single choice questions.")
            }
        })
        .then(singleChoiceQuestions => {
            var selectedOption = null;
            let bodyData = `<div id="questionCarousel" class="carousel slide" data-bs-ride="carousel">
        
            <div class="carousel-inner">`;
            for (const [index, singleChoiceQuestion] of singleChoiceQuestions.entries()) {
                bodyData += `<div class="carousel-item ${index === 0 ? 'active' : ''}">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">${singleChoiceQuestion.text}</h5>
                    
                    ${singleChoiceQuestion.options.map((option, index) => `<div class="form-check">
                        <input class="form-check-input" type="radio" name="option${singleChoiceQuestion.id}" id="option${index}" value="${option}">
                        <label class="form-check-label" for="option${index}">
                            ${option}
                        </label>
                    </div>`).join('')}
                </div>
            </div>
        </div>`
            }
            
            bodyData += `</div class="p-4">
         <button id="prevBtn" class="btn btn-warning" >Previous</button>
          <button id="nextBtn" class="btn btn-warning">Next</button>
        </div>`;
            
            flowElementBody.innerHTML = bodyData
            //listeners
            // document.getElementById("prevBtn").addEventListener("click", () => {
            //     var carousel = document.getElementById("questionCarousel");
            //     var bsCarousel = new bootstrap.Carousel(carousel, {
            //         interval: false
            //
            //     });
            //     bsCarousel.prev();
            // });
            //
            // document.getElementById("nextBtn").addEventListener("click", () => {
            //     var carousel = document.getElementById("questionCarousel");
            //     var bsCarousel = new bootstrap.Carousel(carousel, {
            //         interval: false
            //     });
            //     bsCarousel.next();
            // });
           /* singleChoiceQuestions.forEach((singleChoiceQuestion, questionIndex) => {
                singleChoiceQuestion.options.forEach((option, optionIndex) => {
                    document.getElementById(`option${questionIndex}_${optionIndex}`).addEventListener('change', (event) => {
                        selectedOption = event.target.value;
                    });
                });
            });*/
            
            var carousel = document.getElementById("questionCarousel");
            var bsCarousel = new bootstrap.Carousel(carousel, {
                interval: false
            });
            
            document.getElementById("prevBtn").addEventListener("click", () => {
                bsCarousel.prev();
                console.log("selctedOption: ", selectedOption)
            });
            document.getElementById("nextBtn").addEventListener("click", () => {
                bsCarousel.next();
                console.log("selctedOption: ", selectedOption)

            });
            
            
            
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
                alert("Something went wrong with themes, check the console for more details!")
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
                alert("Something went wrong in the backend openquestions, check the console for more details!")
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
                alert("Something went wrong with the rangequestions, check the console for more details!")
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
                    <input type="range" class="form-control-range" id="formControlRange${i}" min="0" max="${rangeQuestion.options.length -1}" 
                            ${options} oninput="updateLabel(this, 'rangeLabel${i}')">
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

function getMultipleChoiceQuestionsData() {
    fetch(`/api/flows/${flowId}/MultipleChoiceQuestions`,
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
                alert("Something went wrong with multiple choice questions.")
            }
        })
        .then(multipleChoiceQuestions => {
            let bodyData = ``;
            for (const multipleChoiceQuestion of multipleChoiceQuestions) {
                bodyData += `<div class="card">
            <div class="card-body">
                <h5 class="card-title">${multipleChoiceQuestion.text}</h5>
                ${multipleChoiceQuestion.options.map(option => `<div class="form-check">
                    <input class="form-check-input" type="checkbox" value="" id="${option}">
                    <label class="form-check-label" for="${option}">
                        ${option}
                    </label>
                </div>`).join('')}
            </div>
        </div>`
            }
            multipleChoiceQuestionsElement.innerHTML = bodyData
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
getMultipleChoiceQuestionsData();
getSubThemasData();
addButton.addEventListener("click", commitAnswer);