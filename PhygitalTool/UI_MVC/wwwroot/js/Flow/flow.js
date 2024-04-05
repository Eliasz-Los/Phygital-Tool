const addButton = document.getElementById("answerFlow")
const flowIdElement = document.getElementById("flowId")
const flowId = parseInt(flowIdElement.innerText)
const subThemasFlowElement = document.getElementById("subThemasFlowElementId")
const questionsElement = document.getElementById("questions")

let currentQuestionNumber = 0;
let totalQuestions = 0;
let firstQuestion = true;

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
            let bodyData = ``;
            //totalQuestions += singleChoiceQuestions.length;
            for (let i = 0; i < singleChoiceQuestions.length; i++) {
                const singleChoiceQuestion = singleChoiceQuestions[i];
              totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}">
            <div class="card-body">
                <h5 class="card-title">${singleChoiceQuestion.text}</h5>
                ${singleChoiceQuestion.options.map((option, index) => `<div class="form-check">
                    <input class="form-check-input" type="radio" name="option${singleChoiceQuestion.text}" id="option${singleChoiceQuestion.text}_${index}" value="${option}">
                    <label class="form-check-label" for="option${singleChoiceQuestion.text}_${index}">
                        ${option}
                    </label>
                </div>`).join('')}
            </div>
        </div>`
            }
            questionsElement.innerHTML += bodyData
        })
        .catch(error => {
            console.log(error)
        });
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
           // totalQuestions += openQuestions.length;
            for (let i = 0; i < openQuestions.length; i++) {
                const openQuestion = openQuestions[i];
             totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}">
            <div class="card-body">
                <h5 class="card-title">${openQuestion.text}</h5>
                <div class="form-group">
                    <textarea class="form-control" id="exampleFormControlTextarea${i}" rows="3"></textarea>
                </div>
            </div>
            </div>`
            }
            questionsElement.innerHTML += bodyData
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
                totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                
                let options = rangeQuestion.options.map((option,index) => `data-option-${index}="${option}"`).join('')
                bodyData += `<div class="carousel-item ${isActive}">
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
            questionsElement.innerHTML += bodyData
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
                totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}">
            <div class="card-body">
                <h5 class="card-title">${multipleChoiceQuestion.text}</h5>
                ${multipleChoiceQuestion.options.map(option => `<div class="form-check">
                    <input class="form-check-input" type="checkbox" name="${multipleChoiceQuestion.text}" id="${option}">
                    <label class="form-check-label" for="${option}">
                        ${option}
                    </label>
                </div>`).join('')}
            </div>
        </div>`
            }
            questionsElement.innerHTML += bodyData
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
function updateLabel(rangeInput, labelId) {
    let label = document.getElementById(labelId);
    let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
    label.textContent = optionText;
}

const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");
function updateProgressBar() {
    let progressPerc = 100 * (currentQuestionNumber / totalQuestions) ;
    let progressBar = document.getElementById("progressBar");
    
    progressBar.style.width = progressPerc + "%";
    progressBar.setAttribute("aria-valuenow", progressPerc);
}

function getAnswers() {
    const answers = [];
    const carouselItems = document.querySelectorAll('.carousel-item');

    carouselItems.forEach((item, index) => {
        const questionText = item.querySelector('.card-title').textContent;
        const answer = { question: questionText, chosenOptions: [], openAnswer: '' };

        // Check if it's a multiple choice question
        const checkboxes = item.querySelectorAll('input[type="checkbox"]:checked');
        console.log('checkboxes: ', checkboxes);
        if (checkboxes.length > 0) {
            checkboxes.forEach(checkbox => {
                answer.chosenOptions.push(checkbox.id);
            });
        }

        // Check if it's an open question
        const textarea = item.querySelector('textarea');
        console.log('textarea: ', textarea);
        if (textarea) {
            answer.openAnswer = textarea.value;
        }

        // Check if it's a single choice question
        const radioButtons = item.querySelectorAll('input[type="radio"]:checked');
        console.log('radioButtons: ', radioButtons);
        if (radioButtons.length > 0) {
            radioButtons.forEach(radioButton => {
                if (radioButton.checked) {
                    answer.chosenOptions.push(radioButton.value);
                }
            });
        }

        // Check if it's a range question
        const rangeInput = item.querySelector('input[type="range"]');
        console.log('rangeInput: ', rangeInput);
        if (rangeInput) {
            answer.chosenOptions.push(rangeInput.value);
        }
        console.log('answer: ', answer);
        answers.push(answer);
    });

    return answers;
}
function commitAnswer() {
    const answers  = getAnswers();
    const answerObject = answers.map(answer =>({
        Flow: {Id: flowId}, // Send Flow as an object with an Id property
        subTheme: {Title: "test"},  // Send SubTheme as an object with a Title property
        chosenOptions: answer.chosenOptions.map(option => ({OptionText: option})),   // Send each option as an object with an OptionText property
        chosenAnswer: answer.openAnswer
    }));
    
    fetch(`/api/flows/${flowId}/AddAnswers`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(answerObject)
    })
        .then(response => {
            console.log("response: ", response )
            console.log("answerObject: ", answerObject)
            if (response.ok) {
                console.log("Objecten answers gecreeerd: ", response)
            } else{
                alert("Problem with commiting answers: " + JSON.stringify(answerObject))                
            }
        })
        .catch(error => {
            console.log("problem with fetching answers: ", error)
        });
}


/*zo ladt openquestions eerst omdat er minder data is dan bij singel dus ik heb get wat ander gedaan*/
//tis hier ook zo zodat we die carousel code nie 4x moeten schrijven per question type
Promise.all([
    getSingleChoiceQuestionData(),
    getOpenQuestionsData(),
    getRangeQuestionsData(),
    getMultipleChoiceQuestionsData()
]).then(() => {
    // Initialize the carousel after all questions have been loaded
    var carousel = new bootstrap.Carousel(document.getElementById('carouselExampleControls'), {
        interval: false,
        wrap: true
    });
    
    btnNext.addEventListener("click", function() {
        currentQuestionNumber++;
        updateProgressBar();
    });

    btnPrev.addEventListener("click", function() {
        if (currentQuestionNumber > 0) {
            currentQuestionNumber--;
        }
        updateProgressBar();
    });
});

getSubThemasData();
addButton.addEventListener("click", commitAnswer);
