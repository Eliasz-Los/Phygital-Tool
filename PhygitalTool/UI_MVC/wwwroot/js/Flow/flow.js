import { getSingleChoiceQuestionData, getOpenQuestionsData, getRangeQuestionsData, getMultipleChoiceQuestionsData,
    getInfoData, getImageData, getVideoData, getAnswers, commitAnswer} from './details.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");

// In zodat de progressbar werkt
let currentQuestionNumber = 1; // null was juist te kort omdat we beginnen met 1ste vraag waardoor er 1 te kort vr progressbar

function updateProgressBar() {
    let progressPerc = 100 * (currentQuestionNumber / totalQuestions) ;
    let progressBar = document.getElementById("progressBar");

    progressBar.style.width = progressPerc + "%";
    progressBar.setAttribute("aria-valuenow", progressPerc);
}

function InitializeFlow() {
    Promise.all([
        getSingleChoiceQuestionData(),
        getOpenQuestionsData(),
        getRangeQuestionsData(),
        getMultipleChoiceQuestionsData()
    ]).then(() => {
        let carousel = new bootstrap.Carousel(document.getElementById('carouselExampleControls'), {
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
getInfoData();
getImageData();
getVideoData();
getAnswers();
addButton.addEventListener("click", commitAnswer);
