import { getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData, getAnswers, commitAnswer, updateProgressBar } from './details.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");

/*let singleChoiceQuestions = getSingleChoiceQuestionData();
let openQuestions = getOpenQuestionsData();
let rangeQuestions = getRangeQuestionsData();
let multipleChoiceQuestions = getMultipleChoiceQuestionsData();
//effe alles combineren in een array
let allQuestions = singleChoiceQuestions.concat(openQuestions, rangeQuestions, multipleChoiceQuestions);
//nu sorteren op sequenceNumber
allQuestions.sort((a, b) => a.sequenceNumber - b.sequenceNumber);*/

function InitializeFlow() {
    Promise.all([
        getSingleChoiceQuestionData(),
        getOpenQuestionsData(),
        getRangeQuestionsData(), 
        getMultipleChoiceQuestionsData(),
    ]).then(() => {
        let carousel = new bootstrap.Carousel(document.getElementById('linearFlow'), {
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
    
}
/*// Fetch the questions
const questions = await fetchQuestions();

// Sort the questions by sequence number
questions.sort((a, b) => a.sequenceNumber - b.sequenceNumber);

// Add the questions to the DOM
questions.forEach(question => {
    addQuestionToDOM(question);
});*/

InitializeFlow();
await getTextData();
await getImageData();
await getVideoData();
getAnswers();
addButton.addEventListener("click", await commitAnswer);
