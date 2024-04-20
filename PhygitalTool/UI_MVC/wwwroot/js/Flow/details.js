// Purpose: Fetch data from the backend and display it in the flow details page.
const flowIdElement = document.getElementById("flowId")
const flowId = parseInt(flowIdElement.innerText)
const subThemasFlowElement = document.getElementById("subThemasFlowElementId")
const questionsElement = document.getElementById("questions")
const infoElements = document.getElementById("infoAccordion")

//export let totalQuestions = 0;
window.totalQuestions = 0;
let firstQuestion = true;
let totalInformations = 0;
//export let currentQuestionNumber = 1; // null was juist te kort omdat we beginnen met 1ste vraag waardoor er 1 te kort vr progressbar
window.currentQuestionNumber = 1;

export function getSingleChoiceQuestionData() {
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
            for (let i = 0; i < singleChoiceQuestions.length; i++) {
                const singleChoiceQuestion = singleChoiceQuestions[i];
                totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${singleChoiceQuestion.sequenceNumber}" data-card-id="${singleChoiceQuestion.id}">
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

export function getOpenQuestionsData() {
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
            for (let i = 0; i < openQuestions.length; i++) {
                const openQuestion = openQuestions[i];
                totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${openQuestion.sequenceNumber}" data-card-id="${openQuestion.id}">
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
/*function updateLabel(rangeInput, labelId) {
    console.log("rangeinput",rangeInput.value);
    console.log("labelId",labelId);
    let label = document.getElementById(labelId);
    let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
    console.log("optionText",optionText);
    console.log("label",label);
    label.textContent = optionText;
    console.log("labelcontent",label.textContent);
}*/

//Zo is de functie ook beschikbaar in de window object en beschikbaar over verschillende files, dus global gezet, 
// anders werkte het niet bij flow.js & circular.js waar ik het nodig had
window.updateLabel = function (rangeInput, labelId) {
    let label = document.getElementById(labelId);
    let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
    label.textContent = optionText;
}

export function getRangeQuestionsData() {
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
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${rangeQuestion.sequenceNumber}" data-card-id="${rangeQuestion.id}">
            <div class="card-body">
                <h5 class="card-title">${rangeQuestion.text}</h5>
                <div class="form-group">
                    <input type="range" class="form-control-range" id="formControlRange${i}" min="0" max="${rangeQuestion.options.length -1}" 
                            ${options} oninput="window.updateLabel(this, 'rangeLabel${i}')"> <!--oninput="updateLabel(this, 'rangeLabel${i}')"-->
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



export function getMultipleChoiceQuestionsData() {
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
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${multipleChoiceQuestion.sequenceNumber}" data-card-id="${multipleChoiceQuestion.id}">
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
export function getThemasData() {
    fetch(`/api/flows/${flowId}/SubThemas`,
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
        .then(themes => {
            let bodyData = ``;
            for (const theme of themes) {
                bodyData += `<div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">${theme.title}</h5>
                                    <p class="card-text">${theme.description}</p>
                                </div>
                             </div>`;
            }
            subThemasFlowElement.innerHTML = bodyData
        })
}
export function getInfoData(){
    fetch(`/api/flows/${flowId}/TextInfos`,
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
                alert("Something went wrong with textinfos, check the console for more details!")
            }
        })
        .then(texts => {
            let bodyData = ` `;
            for (let i = 0; i < texts.length; i++) {
                totalInformations++;
                bodyData += `
                    <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="false" aria-controls="collapse${totalInformations}">
                            ${texts[i].title}
                        </button>
                    </h2>
                    <div id="collapse${totalInformations}" class="accordion-collapse collapse" aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                            ${texts[i].content}
                        </div>
                    </div></div>`;
            }
            infoElements.innerHTML += bodyData
        })
        .catch(error => {
            console.log(error)
        });
}
export function getImageData(){
    fetch(`/api/flows/${flowId}/ImageInfos`,
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
                alert("Something went wrong with images, check the console for more details!")
            }
        })
        .then(images => {
            let bodyData = ``;
            for (let i = 0; i < images.length; i++) {
                totalInformations++;
                bodyData += `
                        <div class="accordion-item">
                        <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="false" aria-controls="collapse${totalInformations}">
                            ${images[i].title}
                        </button>
                    </h2>
                   
                    <div id="collapse${totalInformations}" class="accordion-collapse collapse" aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                         <img src="${images[i].url.replace('~', '')}" class="d-block w-100" alt="${images[i].altText}">
                            ${images[i].altText}
                        </div>
                    </div>
                    </div>`;
            }
            infoElements.innerHTML += bodyData
        })
        .catch(error => {
            console.log(error)
        });

}

export function getVideoData(){
    fetch(`/api/flows/${flowId}/VideoInfos`,
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
                alert("Something went wrong with videos, check the console for more details!")
            }
        })
        .then(videos => {
            let bodyData = ``;
            for (let i = 0; i < videos.length; i++) {
                totalInformations++;
                bodyData += `
                        <div class="accordion-item">
                        <h2 class="accordion-header" id="heading${totalInformations}">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${totalInformations}" aria-expanded="false" aria-controls="collapse${totalInformations}">
                            ${videos[i].title}
                        </button>
                    </h2>
                   
                    <div id="collapse${totalInformations}" class="accordion-collapse collapse" aria-labelledby="heading${totalInformations}" data-bs-parent="#infoAccordion">
                        <div class="accordion-body">
                         <iframe width="560" height="315" src="https://www.youtube.com/embed/${videos[i].url}" title="${videos[i].title}" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                            <div class="spacing-top">${videos[i].description}</div>
                        </div>
                    </div>
            </div>`;
            }
            infoElements.innerHTML += bodyData
        })
        .catch(error => {
            console.log(error)
        });

}

export function getAnswers() {
    const answers = [];
    const carouselItems = document.querySelectorAll('.carousel-item');

    carouselItems.forEach((item, index) => {
        const questionText = item.querySelector('.card-title').textContent;
        const questionId = item.getAttribute('data-card-id');
        const answer = { question: questionText, chosenOptions: [], openAnswer: '', id : questionId};

        const checkboxes = item.querySelectorAll('input[type="checkbox"]:checked');
        if (checkboxes.length > 0) {
            checkboxes.forEach(checkbox => {
                answer.chosenOptions.push(checkbox.id);
            });
        }

        const textarea = item.querySelector('textarea');
        if (textarea) {
            answer.openAnswer = textarea.value;
        }

        const radioButtons = item.querySelectorAll('input[type="radio"]:checked');
        if (radioButtons.length > 0) {
            radioButtons.forEach(radioButton => {
                if (radioButton.checked) {
                    answer.chosenOptions.push(radioButton.value);
                }
            });
        }

        const rangeInput = item.querySelector('input[type="range"]');
        if (rangeInput) {
            let optionText = rangeInput.getAttribute(`data-option-${rangeInput.value}`);
            answer.chosenOptions.push(optionText);
        }
        console.log('answer: ', answer);
        answers.push(answer);
    });

    return answers;
}

export function commitAnswer() {
    const answers  = getAnswers();
    const answerObject = answers.map(answer =>({
        Flow: {Id: flowId}, // Send Flow as an object with an Id property, ik gebruik id om dan die flow uit te krijgen
        subTheme: {Title: "test"},  // Send SubTheme as an object with a Title property, gebruik ik nie echt
        chosenOptions: answer.chosenOptions.map(option => ({OptionText: option})),   // Send each option as an object with an OptionText property
        chosenAnswer: answer.openAnswer,
        questionId: answer.id
    }));

    fetch(`/api/flows/${flowId}/AddAnswers`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(answerObject)
    })
        .then(response => {
            if (response.ok) {
                console.log("answers objecten werden gecreeerd: \n", response)
            } else{
                alert("Problem with commiting answers: \n" + JSON.stringify(answerObject))
            }
        })
        .catch(error => {
            console.log("problem with fetching answers: ", error)
        });
}
export function updateProgressBar() {
    let progressPerc = 100 * (currentQuestionNumber / totalQuestions) ;
    let progressBar = document.getElementById("progressBar");

    progressBar.style.width = progressPerc + "%";
    progressBar.setAttribute("aria-valuenow", progressPerc);
    console.log("progressbarPerc: ", progressPerc);
}