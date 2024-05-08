import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData, getAnswers, commitAnswer, updateProgressBar
} from './cookingDetails.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");


function resetCarouselInputs() {
    // Select all input and select elements in the carousel
    const inputs = document.querySelectorAll('#physicalLinearFlow .carousel-item input, #physicalLinearFlow .carousel-item select, #physicalLinearFlow .carousel-item textarea');

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



function InitializeFlow() {
    Promise.all([
        getSingleChoiceQuestionData(),
        getOpenQuestionsData(),
        getRangeQuestionsData(),
        getMultipleChoiceQuestionsData(),
    ]).then(() => {
        let carousel = new bootstrap.Carousel(document.getElementById('physicalLinearFlow'), {
            interval: false,
            wrap: true
        });
        window.addEventListener("keydown", function (e) {
                let checkboxToToggle;
                let radiobuttonToToggle;
                switch (e.code) {
                    case 'KeyD':
                        btnNext.click();
                        currentQuestionNumber++;
                        updateProgressBar();
                        break;
                    case 'KeyA':
                        btnPrev.click();
                        if (currentQuestionNumber > 0) {
                            currentQuestionNumber--;
                        }
                        updateProgressBar();
                        break;
                    case 'KeyW':
                        checkboxToToggle = document.querySelector('input[type="checkbox"][id="Key1"]');
                        radiobuttonToToggle = document.querySelector('input[type="radio"][id="Key1"]');
                        break;
                    case 'KeyS':
                        checkboxToToggle = document.querySelector('input[type="checkbox"][id="Key2"]');
                        radiobuttonToToggle = document.querySelector('input[type="radio"][id="Key2"]');
                        break;
                    case 'KeyF':
                        checkboxToToggle = document.querySelector('input[type="checkbox"][id="Key3"]');
                        radiobuttonToToggle = document.querySelector('input[type="radio"][id="Key3"]');
                        break;
                    case 'KeyG':
                        checkboxToToggle = document.querySelector('input[type="checkbox"][id="Key4"]');
                        radiobuttonToToggle = document.querySelector('input[type="radio"][id="Key4"]');
                        break;

                    case 'Space':
                        addButton.click();
                        break;
                    default:
                        break;
                }
                if (checkboxToToggle) {
                    checkboxToToggle.checked = !checkboxToToggle.checked;
                }
                if (radiobuttonToToggle) {
                    radiobuttonToToggle.checked = !radiobuttonToToggle.checked;
                }
            }
        )
        ;
    });
}


InitializeFlow();
getTextData();
getImageData();
getVideoData();
getAnswers();
resetCarouselInputs();


