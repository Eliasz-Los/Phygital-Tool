import {addFlow} from "./flowThemeAndTypeRest";
import {fillSubthemesSelect} from "./flowThemeAndTypeRest";

export interface SubTheme {
    id: number;
    title: string;
    description: string;
}

export interface flowObject {
    FlowType: string;
    IsOpen: boolean;
    ThemeId: number;
}

const addButton: HTMLElement | null = document.getElementById("submitFlow");
const addQuestion: HTMLElement | null = document.getElementById("getquestion");


export async function addFlowData(): Promise<void> {
    const selectedType: HTMLInputElement = document.getElementById('TypeSelect') as HTMLInputElement;
    const selectedTheme: HTMLSelectElement = document.getElementById('ThemaSelect') as HTMLSelectElement;
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive: HTMLInputElement = document.getElementById('ActiveCheckbox') as HTMLInputElement;

    const data: flowObject = {
        FlowType: selectedType.value,
        IsOpen: isActive.checked,
        ThemeId: parseInt(selectedThemeId)
    };
    
    await addFlow(data)
        .then(response => {
            console.log(response);
        })
        .catch(error => {
            console.error(error);
        });
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

populateSubthemesSelect();


if (addButton) {
    addButton.addEventListener("click", addFlowData);
}

if (addQuestion) {
    addQuestion.addEventListener("click", addFlowData);
}
