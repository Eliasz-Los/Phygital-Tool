import {addFlowData} from "./flowThemeAndTypeRest";
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


export async function addFlow(): Promise<void> {
    const selectedType: HTMLInputElement = document.getElementById('TypeSelect') as HTMLInputElement;
    const selectedTheme: HTMLSelectElement = document.getElementById('themeSelect') as HTMLSelectElement;
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive: HTMLInputElement = document.getElementById('ActiveCheckbox') as HTMLInputElement;

    var data: flowObject = {
        FlowType: selectedType.value,
        IsOpen: isActive.checked,
        ThemeId: parseInt(selectedThemeId)
    };
    
    await addFlowData(data)
        .then(response => {
            console.log(response);
        })
        .catch(error => {
            console.error(error);
        });
}


fillSubthemesSelect()
    .then(response => {
        console.log(response);
    })
    .catch(error => {
        console.error(error);
    });

if (addButton) {
    addButton.addEventListener("click", addFlow);
}
if (addQuestion) {
    addQuestion.addEventListener("click", addFlow);
}
