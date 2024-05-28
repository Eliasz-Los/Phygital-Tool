import {Carousel} from 'bootstrap';
import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getMultipleChoiceQuestionsData, getRangeQuestionsData,
    getTextData, getImageData, getVideoData,  commitAnswers, updatePorgressBar
} from './details';

const addButton: HTMLElement | null = document.getElementById("answerFlow") as HTMLButtonElement;
const endbox: HTMLElement | null = document.getElementById('end-box');

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
        getSingleChoiceQuestionData(),
        getRangeQuestionsData(),
        getMultipleChoiceQuestionsData(),
        getOpenQuestionsData()
    ]).then(() => {
        
/*        const carousel: Carousel = new Carousel(document.getElementById('circularFlow') as HTMLElement, {
            interval: false,
            wrap: true
        });*/
        
        window.addEventListener("keydown", function (e: KeyboardEvent) {
            let checkboxToToggle: HTMLInputElement | null;
            let radiobuttonToToggle: HTMLInputElement | null;
            let activeCarouselItem: Element = document.querySelector('.carousel-item.active')!;
            let rangeInput: HTMLInputElement | null = activeCarouselItem.querySelector('input[type="range"]');
            switch (e.code) {
                case 'ArrowLeft':
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) + 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                    }
                    break;
                case 'ArrowRight':
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) - 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                    }
                    break;
                case 'KeyW':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key1"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key1"]');
                    break;
                case 'KeyS':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key2"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key2"]');
                    break;
                case 'KeyF':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key3"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key3"]');
                    break;
                case 'KeyG':
                    checkboxToToggle = activeCarouselItem.querySelector('input[type="checkbox"][data-key-index="Key4"]');
                    radiobuttonToToggle = activeCarouselItem.querySelector('input[type="radio"][data-key-index="Key4"]');
                    break;
                case 'Space':
                    // @ts-ignore
                    addButton.click();
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
        startTimer();
    });
}

InitializeFlow();
getTextData();
getImageData();
getVideoData();


addButton.addEventListener("click", commitAnswers);