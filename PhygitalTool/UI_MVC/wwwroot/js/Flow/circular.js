import { getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData, getAnswers, commitAnswer, updateProgressBar } from './details.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");
const endbox = document.getElementById('end-box');

function resetCarouselInputs() {
    // Select all input and select elements in the carousel
    const inputs = document.querySelectorAll('#circularFlow .carousel-item input, #circularFlow .carousel-item select, #circularFlow .carousel-item textarea');

    // Loop through each element and reset its value
    inputs.forEach(input => {
        switch (input.type) {
            case 'checkbox':
            case 'radio':
                input.checked = false;
                break;
            case 'select-one':
            case 'select-multiple':
                input.selectedIndex = -1;
                break;
            default:
                input.value = '';
                break;
        }
    });
}

// the timer values
let timerId = null;
let timeLeftInSeconds = 10;
const timeBeginQuestion = 10;
function startTimer() {
    // Get the timer element
    const timerElement = document.getElementById('timer');
    const carousel = new bootstrap.Carousel(document.getElementById('circularFlow'), {
        interval: false,
        wrap: true
    });

    // Clear the existing timer
    if (timerId) {
        clearInterval(timerId);
    }

    // Update the timer element
    timerElement.textContent = timeLeftInSeconds;

    // Start the timer
    timerId = setInterval(() => {
        timeLeftInSeconds--;
        timerElement.textContent = timeLeftInSeconds;

        // When the timer reaches 0, stop the timer
        if (timeLeftInSeconds <= 0 ) {
            clearInterval(timerId);
            // You can add code here to handle what happens when the timer reaches 0
            if (currentQuestionNumber === totalQuestions) {
                endbox.innerText = "Proficiat, je hebt alle vragen afgerond!\nJe kan nu antwoorden indienen oftewel wordt de flow gerest naar het begin.";
                currentQuestionNumber = 1;
                /*updateProgressBar();
                endbox.innerText = "";*/
                timeLeftInSeconds = 10;
                startTimer();
                setTimeout(resetCarouselInputs,timeLeftInSeconds*1000);
            } else {
                endbox.innerText = "";
                carousel.next();
                currentQuestionNumber++;
                updateProgressBar()
                timeLeftInSeconds = timeBeginQuestion;
                startTimer();
            }

        }
    }, 1000);
}

function InitializeFlow() {
    Promise.all([
        getSingleChoiceQuestionData(),
        getOpenQuestionsData(),
        getRangeQuestionsData(),
        getMultipleChoiceQuestionsData()
    ]).then(() => {
        var carousel = new bootstrap.Carousel(document.getElementById('circularFlow'), {
            interval: false,
            wrap: true
        });

        btnNext.addEventListener("click", function() {
            currentQuestionNumber++;
            updateProgressBar();
            // reset the timer
            clearInterval(timerId);
            timeLeftInSeconds = timeBeginQuestion;
            startTimer();
           
        });

        btnPrev.addEventListener("click", function() {
            if (currentQuestionNumber > 0) {
                currentQuestionNumber--;
            }
            updateProgressBar();

            // Reset the timer
            clearInterval(timerId);
            timeLeftInSeconds = timeBeginQuestion;
            startTimer();
        });

        startTimer();
    });
}

InitializeFlow();
getTextData();
getImageData();
getVideoData();
addButton.addEventListener("click", commitAnswer);
