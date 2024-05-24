import {fillSubthemesSelect} from "./questionsRest";
import {sendOptions} from "./questionsRest";
import {addQuestion} from "./questionsRest";

export interface Question {
    Text: string;
    isActive: boolean;
    SubTheme: number;
    Type: string;
}


const optionButton: HTMLElement | null = document.getElementById("OptionButton");
const submitQuestion: HTMLElement | null = document.getElementById("submitQuestion");


export async function addOption(): Promise<void> {
    const input = document.getElementById('OptionTitle') as HTMLInputElement;
    const optionValue = input.value.trim();
    const optionList = document.getElementById('optionList') as HTMLUListElement;

    if (optionValue && optionList.childElementCount < 4) {
        if (optionValue.length > 30) {
            alert('Option cannot exceed 30 characters.');
            return;
        }

        const listItem = document.createElement('li');
        listItem.className = 'list-group-item d-flex justify-content-between align-items-center';
        listItem.textContent = optionValue;

        const removeButton = document.createElement('button');
        removeButton.className = 'btn btn-danger btn-sm';
        removeButton.textContent = 'Remove';
        removeButton.addEventListener('click', () => {
            optionList.removeChild(listItem);
            (document.getElementById('OptionButton') as HTMLButtonElement).disabled = false;
        });

        listItem.appendChild(removeButton);
        optionList.appendChild(listItem);

        input.value = ''; // Clear the input field after adding

        // Disable the button if there are 4 items in the list
        if (optionList.childElementCount >= 4) {
            (document.getElementById('OptionButton') as HTMLButtonElement).disabled = true;
        }
    }
}

export async function collectOptions(): Promise<void> {
    const optionList = document.getElementById('optionList') as HTMLUListElement;
    const options: string[] = [];

    optionList.querySelectorAll('li').forEach((listItem) => {
        const optionText = (listItem.firstChild as Text).textContent?.trim();
        if (optionText) {
            options.push(optionText);
        }
    });
    await sendOptions(options);
}


export async function addQuestionData(): Promise<void> {
    await collectOptions();

    const questionTitle = (document.getElementById('QuestionTitle') as HTMLInputElement).value;
    const selectedTheme = document.getElementById('ThemaSelect') as HTMLSelectElement;
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive = (document.getElementById('ActiveCheckbox') as HTMLInputElement).checked;
    const selectedType = (document.getElementById('TypeSelect') as HTMLSelectElement).value;

    const data = {
        Text: questionTitle,
        isActive: isActive,
        SubTheme: parseInt(selectedThemeId, 10),
        Type: selectedType
    };
    
    await addQuestion(data);
}

if (optionButton) {
    optionButton.addEventListener("click", addOption);
}

if (submitQuestion) {
    submitQuestion.addEventListener("click", addQuestionData);
}
fillSubthemesSelect();

