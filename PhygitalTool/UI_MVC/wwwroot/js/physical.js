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


        window.addEventListener('keydown', function(event) {
            switch (event.key) {
                case 'q':
                    btnNext.addEventListener("click", function() {
                        currentQuestionNumber++;
                        updateProgressBar();
                    });
                    break;
                case 'd':
                    btnPrev.addEventListener("click", function() {
                        if (currentQuestionNumber > 0) {
                            currentQuestionNumber--;
                        }
                        updateProgressBar();
                    });
                    break;
                case 'z':
                    // Code to send it in
                    // For example, you might do something like this:
                    commitAnswer();
                    break;
            }
        });



    });
}


InitializeFlow();
getTextData();
getImageData();
getVideoData();
getAnswers();
addButton.addEventListener("click", commitAnswer);
