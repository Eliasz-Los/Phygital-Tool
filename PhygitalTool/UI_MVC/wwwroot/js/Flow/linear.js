import { getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData, getAnswers, commitAnswer, updateProgressBar } from './details.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");


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


InitializeFlow();
getTextData();
getImageData();
getVideoData();
getAnswers();
addButton.addEventListener("click", commitAnswer);
