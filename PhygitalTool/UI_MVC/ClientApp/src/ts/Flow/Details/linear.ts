import {Carousel} from "bootstrap";
import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getRangeQuestionsData, getMultipleChoiceQuestionsData,
    getTextData, getImageData, getVideoData, commitAnswers, updatePorgressBar
} from "./details";

const addButton: HTMLElement | null = document.getElementById("answerFlow") as HTMLButtonElement;
const btnNext: HTMLElement | null = document.getElementById("nextBtn");
const btnPrev: HTMLElement | null = document.getElementById("prevBtn");
if (btnPrev) (btnPrev as HTMLInputElement).disabled = true;

/*let currentQuestionNumber: number = 1;
let totalQuestions: number = 0;*/

let checkboxToToggle = null;
let radiobuttonToToggle = null;

function updateButton(): void {
    if (window.currentQuestionNumber < 2) {
        (btnPrev as HTMLInputElement).disabled = true;
    } else if (btnPrev) {
        (btnPrev as HTMLInputElement).disabled = false;
    }

    if (window.currentQuestionNumber === window.totalQuestions) {
        (btnNext as HTMLInputElement).disabled = true;
    } else if (btnNext) {
        (btnNext as HTMLInputElement).disabled = false;
    }
}

// TODO: visible & invisible van antwoorden voor kiezen gebruikers
// Dit was test code voor het verbergen van de userCountSection en het tonen van de linearFlowSection maar voorlopig niet werkend
function visibleF() {
    const submitUserCount: HTMLElement | null = document.getElementById('submitUserCount');
    const userCountSection: HTMLElement | null = document.getElementById('userCountSection');
    const linearFlowSection: HTMLElement | null = document.getElementById('linearFlow');

    if (submitUserCount) {
        submitUserCount.addEventListener('click', function () {
            if (userCountSection && linearFlowSection) {
                // Use Bootstrap classes to hide and show elements
                userCountSection.classList.remove('visible ');
                userCountSection.classList.add('invisible');

                linearFlowSection.classList.remove('invisible');
                linearFlowSection.classList.add('visible');
            }
        });
    }
}


function InitializeFlow(): void {
    Promise.all([
        getSingleChoiceQuestionData(),
        getOpenQuestionsData(), //Maybe with QR CODE? //TODO: Add open questions
        getRangeQuestionsData(),
        getMultipleChoiceQuestionsData()
    ]).then(() => {

            let carousel: bootstrap.Carousel = new Carousel(document.getElementById('linearFlow') as HTMLElement, {
                interval: false,
                wrap: true
            });

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
                    case 'ArrowRight':
                        if (window.currentQuestionNumber < window.totalQuestions) {
                            window.currentQuestionNumber++;
                            updateButton();
                            carousel.next();
                        }
                        updatePorgressBar();
                        break;

                    case 'ArrowLeft':
                        if (window.currentQuestionNumber > 1) {
                            window.currentQuestionNumber--;
                            updateButton();
                            carousel.prev();
                        }
                        updatePorgressBar();
                        break;

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
                console.log(window.currentQuestionNumber)
            });

            document.addEventListener('click', function (e) {
                if (e.button === 0) {
                    (addButton as HTMLInputElement).click();
                }
            });
        }
    );
}

visibleF();
InitializeFlow();
getTextData();
getImageData();
getVideoData();

addButton.addEventListener("click", commitAnswers);