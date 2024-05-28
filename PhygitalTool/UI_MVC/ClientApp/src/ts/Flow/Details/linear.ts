import {Carousel, Modal} from "bootstrap";
import {
    getSingleChoiceQuestionData, getOpenQuestionsData, getRangeQuestionsData, getMultipleChoiceQuestionsData,
    getTextData, getImageData, getVideoData, commitAnswers, updatePorgressBar
} from "./details";

const userCountModalElement = document.getElementById('userCountModal');
const submitButtonElement= userCountModalElement?.querySelector('.btn-warning');
const userCountDisplayElement = document.getElementById('userCountDisplay');

const addButton: HTMLElement | null = document.getElementById("answerFlow") as HTMLButtonElement;
const btnNext: HTMLElement | null = document.getElementById("nextBtn");
const btnPrev: HTMLElement | null = document.getElementById("prevBtn");
let numberOfPeople: number = 1;

if (btnPrev) (btnPrev as HTMLInputElement).disabled = true;

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


// Get the modal, submit button elements
if (userCountModalElement && submitButtonElement) {
    const userCountModal = new Modal(userCountModalElement, {
        backdrop: 'static'
    });
    submitButtonElement.addEventListener('click', function (e) {
        e.stopPropagation() // zodat die nie zomaar dicht gaat
        //om nummer in een variable te steken
        const userCountRange = document.getElementById('userCountRange') as HTMLInputElement;
        numberOfPeople = parseInt(userCountRange.value);
        userCountModal.hide();
    });

    userCountModalElement.addEventListener('hidden.bs.modal', function () {
        const userCountRange = document.getElementById('userCountRange') as HTMLInputElement;
       console.log(userCountRange.value);   //checken of de waarde goed is
        InitializeFlow();
        const modalBackdrop = document.querySelector('.modal-backdrop');
        if (modalBackdrop) {
            modalBackdrop.remove();
        }
    });
    userCountModal.show();
}


// Display the modal when the Linear page is loaded
window.addEventListener('DOMContentLoaded', (event) => {
    const userCountModalElement = document.getElementById('userCountModal');
    if (userCountModalElement) {
        const userCountModal = new Modal(userCountModalElement);
        userCountModal.show();
        window.addEventListener("keydown", function (e: KeyboardEvent) {
            let rangeInput: HTMLInputElement | null = userCountModalElement.querySelector('input[type="range"]');
            switch (e.code) {
                case 'KeyA':
                    rangeInput = userCountModalElement.querySelector('input[type="range"]');
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) - 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                        userCountDisplayElement!.textContent = rangeInput.value;
                    }
                    break;
                case 'KeyS':
                    rangeInput = userCountModalElement.querySelector('input[type="range"]');
                    if (rangeInput) {
                        rangeInput.value = (parseInt(rangeInput.value) + 1).toString();
                        rangeInput.dispatchEvent(new Event('input'));
                        userCountDisplayElement!.textContent = rangeInput.value;
                    }
                    break;
                default:
                    break;
            }
            //??? why is this here
            console.log(window.currentQuestionNumber)
        });

        document.addEventListener('click', function (e) {
            if (e.button === 0) {
                (submitButtonElement as HTMLInputElement).click();
            }
        });
    }
});

// BOVENSTAANDE CODE (DE MODAL) laat de radio en checkboxen niet werken...
// voor radio & checkboxen werken ze tenzij ze na OPENQUESTIONS komen dan ist fucked up
// en links klikken ga nie door de modal... zal zo kijken

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
                let openInput: HTMLInputElement | null;
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



getTextData();
getImageData();
getVideoData();
addButton.addEventListener("click", commitAnswers);