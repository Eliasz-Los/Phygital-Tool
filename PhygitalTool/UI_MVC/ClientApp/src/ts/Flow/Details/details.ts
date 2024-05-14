//TODO: Refactoren met rest functies
const flowIdElement: HTMLElement | null = document.getElementById("flowId");
const flowId: number = flowIdElement ? parseInt(flowIdElement.innerText) : 0;
const subThemasFlowElement: HTMLElement | null  = document.getElementById("subThemasFlowElementId");
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

window.totalQuestions = 0;
let firstQuestion: boolean = true;
let totalInformations: number = 0;
window.currentQuestionNumber = 1;
//typescript bs
let totalQuestions: number = 0;
let currentQuestion: number = 1;

//Importeren van de functies
import { readSingleChoiceQuestionData, readOpenQuestionsData, readRangeQuestionsData, readMultipleChoiceQuestionsData,
readTextData, readImageData, readVideoData, commitAnswer} from './detailsRest';

//Ophalen van de data

export async function getSingleChoiceQuestionData() {
     await readSingleChoiceQuestionData(flowId)
        .then(singleChoiceQuestions => {
            let bodyData = ``;
            for (let i = 0; i < singleChoiceQuestions.length; i++) {
                const singleChoiceQuestion = singleChoiceQuestions[i];
                totalQuestions += 1;
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