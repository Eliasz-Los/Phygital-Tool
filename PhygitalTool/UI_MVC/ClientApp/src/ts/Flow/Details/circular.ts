import {Carousel} from 'bootstrap';
import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData,  commitAnswers, updatePorgressBar
} from './details';

const addButton: HTMLElement | null = document.getElementById("answerFlow");
const endbox: HTMLElement | null = document.getElementById('end-box');

let numberOfPeople: number = 1;

function resetCarouselInputs(): void {
    const inputs: NodeListOf<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement> = document.querySelectorAll('#circularFlow .carousel-item input, #circularFlow .carousel-item select, #circularFlow .carousel-item textarea');

    inputs.forEach(input => {
        switch (input.type) {
            case 'checkbox':
            case 'radio':
                (input as HTMLInputElement).checked = false;
                break;
            case 'select-one':
            case 'select-multiple':
                (input as HTMLSelectElement).selectedIndex = -1;
                break;
            default:
                input.value = '';
                break;
        }
    });
}

let timerId: ReturnType<typeof setInterval> | null = null;
let timeLeftInSeconds: number = 10;
const timeBeginQuestion: number = 10;

function startTimer(): void {
    const timerElement: HTMLElement | null = document.getElementById('timer');
    const carousel: Carousel = new Carousel(document.getElementById('circularFlow') as HTMLElement, {
        interval: false,
        wrap: true
    });

    if (timerId) {
        clearInterval(timerId);
    }

    if (timerElement) {
        timerElement.textContent = timeLeftInSeconds.toString();
    }

    timerId = setInterval(() => {
        timeLeftInSeconds--;
        if (timerElement) {
            timerElement.textContent = timeLeftInSeconds.toString();
        }

        if (timeLeftInSeconds <= 0) {
            
            if(timerId !== null){
                clearInterval(timerId);
                timerId = null;
            } 

            carousel.next();
            if (window.currentQuestionNumber === window.totalQuestions) {
                if (endbox) {
                    endbox.innerText = "Proficiat, je hebt alle vragen afgerond!\nJe kan nu antwoorden indienen oftewel wordt de flow gerest naar het begin.";
                }
                window.currentQuestionNumber = 1;
                timeLeftInSeconds = 20;
                startTimer();
                setTimeout(resetCarouselInputs, timeLeftInSeconds * 1000);
                
                //updateButton();
            } else {
                if (endbox) {
                    endbox.innerText = "";
                }
                window.currentQuestionNumber++;
                updatePorgressBar();
                timeLeftInSeconds = timeBeginQuestion;
                startTimer();
            }
        }
    }, 1000);
}

function InitializeFlow(): void {
    Promise.all([
        getSingleChoiceQuestionData(numberOfPeople),
        getRangeQuestionsData(numberOfPeople),
        getMultipleChoiceQuestionsData(numberOfPeople),
        getOpenQuestionsData(numberOfPeople)
    ]).then(() => {
        window.addEventListener("keydown", function (e: KeyboardEvent) {
            let checkboxToToggle: HTMLInputElement | null;
            let radiobuttonToToggle: HTMLInputElement | null;
            let activeCarouselItem: Element = document.querySelector('.carousel-item.active')!;
            let rangeInput: HTMLInputElement | null = activeCarouselItem.querySelector('input[type="range"]');
            let openInput: HTMLInputElement | null ;
            openInput = activeCarouselItem.querySelector('textarea[type="text"]');
            if (openInput) {
                openInput.focus();
            }

            switch (e.code) {
                case 'KeyA':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key1"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key1"]');
                    rangeInput = activeCarouselItem.querySelector('input[type="range"]');
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) - 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                    }
                    break;
                case 'KeyS':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key2"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key2"]');
                    rangeInput = activeCarouselItem.querySelector('input[type="range"]');
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) + 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                    }
                    break;
                case 'KeyD':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key3"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key3"]');
                    break;
                case 'KeyF':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key4"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key4"]');
                    break;
                default:
                    break;
            }
            // @ts-ignore
            if (checkboxToToggle) {
                checkboxToToggle.checked = !checkboxToToggle.checked;
            }
            // @ts-ignore
            if (radiobuttonToToggle) {
                radiobuttonToToggle.checked = !radiobuttonToToggle.checked;
            }
        });
        document.addEventListener('click', function (e) {
            if (e.button === 0) {
                (addButton as HTMLInputElement).click();
            }
        });
        startTimer();
    });
}

InitializeFlow();
getTextData();
getImageData();
getVideoData();

if (addButton) {
    addButton.addEventListener("click", commitAnswers);
}