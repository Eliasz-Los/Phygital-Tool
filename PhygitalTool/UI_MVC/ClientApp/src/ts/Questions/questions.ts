import {fillSubthemesSelect} from "./questionsRest";
import {sendOptions} from "./questionsRest";
import {addQuestion} from "./questionsRest";
import {SubTheme} from "../Flow/Creation/flowThemeAndType";

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

async function populateSubthemesSelect() {
    try {
        const subThemas: SubTheme[] = await fillSubthemesSelect();

        const output = document.getElementById("ThemaSelect") as HTMLSelectElement;
        if (!output) {
            throw new Error("ThemaSelect element not found");
        }

        let bodyData = ``;
        for (const subThema of subThemas) {
            bodyData += `
                <option value="${subThema.id}" data-description="${subThema.description}">${subThema.title}</option>
            `;
        }
        output.innerHTML += bodyData;

    } catch (error) {
        console.error('Error:', error);
        alert("Something went wrong while fetching subthemes. Check the console for more details.");
    }
}


export async function addQuestionData(): Promise<void> {
    await collectOptions();
    const questionTitle = (document.getElementById('QuestionTitle') as HTMLInputElement).value;
    const selectedTheme = document.getElementById('ThemaSelect') as HTMLSelectElement;
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive = (document.getElementById('ActiveCheckbox') as HTMLInputElement).checked;
    const selectedType = (document.getElementById('TypeSelect') as HTMLSelectElement).value;

    const data: Question = {
        Text: questionTitle,
        isActive: isActive,
        SubTheme: parseInt(selectedThemeId),
        Type: selectedType
    };

    await addQuestion(data)
        .then(response => {
        alert(response);
    })
        .catch(error => {
            alert(error);
        });
}

populateSubthemesSelect();

if (optionButton) {
    optionButton.addEventListener("click", addOption);
}

if (submitQuestion) {
    submitQuestion.addEventListener("click", addQuestionData);
}


