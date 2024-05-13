import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData, getAnswers, commitAnswer, updateProgressBar
} from './cookingDetails.js';

const addButton = document.getElementById("answerFlow")
const btnNext = document.getElementById("nextBtn");
const btnPrev = document.getElementById("prevBtn");

function InitializeFlow() {
    Promise.all([
        getSingleChoiceQuestionData(),
        //getOpenQuestionsData(), //Maybe with QR CODE? //TODO: Add open questions
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
                let activeCarouselItem = document.querySelector('.carousel-item.active');
                let rangeInput = activeCarouselItem.querySelector('input[type="range"]');
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
                    case 'ArrowRight':
                        rangeInput.value = parseInt(rangeInput.value) + 1;
                        rangeInput.dispatchEvent(new Event('input'));
                        break;
                    case 'ArrowLeft':
                        rangeInput.value = parseInt(rangeInput.value) - 1;
                        rangeInput.dispatchEvent(new Event('input'));
                        break;

                    case 'KeyW':
                        checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][id="Key1"]');
                        radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][id="Key1"]');
                        break;
                    case 'KeyS':
                        checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][id="Key2"]');
                        radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][id="Key2"]');
                        break;
                    case 'KeyF':
                        checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][id="Key3"]');
                        radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][id="Key3"]');
                        break;
                    case 'KeyG':
                        checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][id="Key4"]');
                        radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][id="Key4"]');
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
            });
        }
    );
}


InitializeFlow();
getTextData();
getImageData();
getVideoData();
getAnswers();


