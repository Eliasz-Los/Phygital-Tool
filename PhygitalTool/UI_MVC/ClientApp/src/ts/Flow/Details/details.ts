//Importeren van de functies
import {
    readImageData,
    readMultipleChoiceQuestionsData,
    readOpenQuestionsData,
    readRangeQuestionsData,
    readSingleChoiceQuestionData,
    readTextData,
    readVideoData,
    sendAnswers
} from './detailsRest';



// elementen ophalen
const flowIdElement: HTMLElement | null = document.getElementById("flowId");
const flowId: number = flowIdElement ? parseInt(flowIdElement.innerText) : 0;
const questionsElement: HTMLElement | null  = document.getElementById("questions");
const infoElements: HTMLElement | null  = document.getElementById("infoAccordion");
const keys : string[] = ['Key1', 'Key2', 'Key3', 'Key4']; // voor keydown event

//Globale functies
declare global {
    interface Window {
        totalQuestions: number;
        currentQuestionNumber: number;
    }
}

(window as any).updateLabel = function (input: HTMLInputElement, labelId: any) {
    let label = document.getElementById(labelId);
    if (label) {
        label.textContent = input.getAttribute(`data-option-${input.value}`);
    }
}


window.totalQuestions = 0;
let firstQuestion: boolean = true;
let totalInformations: number = 0;
window.currentQuestionNumber = 1;
//typescript bs
/*let totalQuestions: number = 0;
let currentQuestion: number = 1;*/

//Functies

// export function setUpQrCode(): void {
//     const uriElement = document.getElementById("qrCodeData");
//     const uri: string | null = uriElement ? uriElement.getAttribute('data-url') : null;
//     const qrCode = new QRCodeStyling({
//         width: 400,
//         height: 400,
//         type: "svg",
//         data: uri,
//         dotsOptions: {
//             color: "#000000",
//             type: "rounded"
//         },
//         backgroundOptions: {
//             color: "#e9ebee",
//         },
//         imageOptions: {
//             crossOrigin: "anonymous",
//             imageSize: 1,
//             hideBackgroundDots: false,
//             margin: 2
//         }
//     });
//
//     const qrCodeElement = document.getElementById("qrCode");
//     if (qrCodeElement) {
//         qrCode.append(qrCodeElement);
//     }
// }




export async function getSingleChoiceQuestionData() {
     await readSingleChoiceQuestionData(flowId)
        .then(singleChoiceQuestions => {
            let bodyData = ``;
            for (let i = 0; i < singleChoiceQuestions.length; i++) {
                const singleChoiceQuestion = singleChoiceQuestions[i];
                window.totalQuestions += 1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
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
        </div>`
            }
              if (questionsElement) {
                    questionsElement.innerHTML += bodyData;
                } else {
                    console.error('Element with id "questions" not found');
            }
        }).catch(error => {
            console.error(error);
        });
}

export async function getOpenQuestionsData() {
    await readOpenQuestionsData(flowId)
        .then(openQuestions => {
            let bodyData = ``;
            for (let i = 0; i < openQuestions.length; i++) {
                const openQuestion = openQuestions[i];
                window.totalQuestions += 1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${openQuestion.sequenceNumber}" data-card-id="${openQuestion.id}">
            <div class="card-body">
                <h5 class="card-title">${openQuestion.text}</h5>
                <div class="form-group">
                    <textarea class="form-control" id="openQuestion${openQuestion.text}" rows="3"></textarea>
                </div>
            </div>
        </div>`
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            } else {
                console.error('Element with id "questions" not found');
            }
        }).catch(error => {
            console.error(error);
        });
}
export async function getRangeQuestionsData() {
    await readRangeQuestionsData(flowId)
        .then(rangeQuestions => {
            let bodyData = ``;
            for (let i = 0; i < rangeQuestions.length; i++) {
                const rangeQuestion = rangeQuestions[i];
                window.totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;

                let options = rangeQuestion.options.map((option,index) => `data-option-${index}="${option}"`).join('')
                bodyData += `<div class="carousel-item ${isActive}" data-sequence-number="${rangeQuestion.sequenceNumber}" data-card-id="${rangeQuestion.id}">
            <div class="card-body">
                <h5 class="card-title">${rangeQuestion.text}</h5>
                <div class="form-group">
                    <input type="range" class="form-control-range" id="formControlRange${i}" min="0" max="${rangeQuestion.options.length -1}" 
                            ${options} oninput="updateLabel(this, 'rangeLabel${i}')"> <!--oninput="updateLabel(this, 'rangeLabel${i}')"-->
                    <label id="rangeLabel${i}" for="formControlRange${i}"></label>
                </div>
            </div>
        </div>`
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            } else {
                console.error('Element with id "questions" not found');
            }
        })
        .catch(error => {
            console.log(error)
        });
}

export async function getMultipleChoiceQuestionsData() {
    await readMultipleChoiceQuestionsData(flowId)
        .then(multipleChoiceQuestions => {
            let bodyData = ``;
            for (const multipleChoiceQuestion of multipleChoiceQuestions) {
                window.totalQuestions +=1;
                const isActive = firstQuestion ? 'active' : '';
                if (firstQuestion) firstQuestion = false;
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
        </div>`
            }
            if (questionsElement) {
                questionsElement.innerHTML += bodyData;
            } else {
                console.error('Element with id "questions" not found');
            }
        })
        .catch(error => {
            console.log(error)
        });
}

export async function getTextData() {
    await readTextData(flowId)
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
            } else {
                console.error('Element with id "infoAccordion" not found');
            }
        })
        .catch(error => {
            console.log(error)
        });
}

export async function getImageData() {
    await readImageData(flowId)
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
            } else {
                console.error('Element with id "infoAccordion" not found');
            }
        })
        .catch(error => {
            console.log(error)
        });
}

export async function getVideoData() {
    await readVideoData(flowId)
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
        } else {
            console.error('Element with id "infoAccordion" not found');
        }
    })
        .catch(error => {
            console.log(error)
        });
}



export function getAnswers(): Answer[] {
    const answers: Answer[] = [];
    const carouselItems = document.querySelectorAll('.carousel-item');

    carouselItems.forEach((item, index) => {
        const questionText = item.querySelector('.card-title')?.textContent || '';
        const questionId: number = Number(item.getAttribute('data-card-id') || '');
        const answer: Answer = { question: questionText, chosenOptions: [], openAnswer: '', id: questionId };

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
            if ((radioButton as HTMLInputElement).checked) {
                answer.chosenOptions.push((radioButton as HTMLInputElement).value);
            }
        });

        const rangeInput = item.querySelector('input[type="range"]');
        if (rangeInput) {
            let optionText = rangeInput.getAttribute(`data-option-${(rangeInput as HTMLInputElement).value}`);
            if (optionText) {
                answer.chosenOptions.push(optionText);
            }
        }

        answers.push(answer);
    });

    return answers;
}

export async function commitAnswers() {
    const answers = getAnswers();
    const answerObject: AnswerObject[] = answers.map(answer => ({
        chosenOptions: answer.chosenOptions.map(option => ({ OptionText: option })),
        chosenAnswer: answer.openAnswer,
        questionId: answer.id
    }));
    await sendAnswers(flowId, answerObject)
        .then(response => {
            console.log(response);
        })
        .catch(error => {
            console.error(error);
        });
}

export function updatePorgressBar() {
    let progressPerc = 100 * (window.currentQuestionNumber / window.totalQuestions);
    let progressBar = document.getElementById("progressBar");

    (progressBar as HTMLInputElement).style.width = progressPerc + "%";
    (progressBar as HTMLInputElement).setAttribute("aria-valuenow", progressPerc.toString());
    console.log("progressbarPerc: ", progressPerc);
}


