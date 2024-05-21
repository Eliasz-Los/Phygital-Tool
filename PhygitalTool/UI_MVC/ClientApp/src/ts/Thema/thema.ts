import {addThemaData} from "./themaRest";

const addButton: HTMLElement | null = document.getElementById("submitThema");

interface Thema {
    title: string;
    description: string;
}

async function addThema(): Promise<void> {
    const themaNameInput: HTMLInputElement | null = document.getElementById("ThemaInput") as HTMLInputElement;
    const themaDescInput: HTMLInputElement | null = document.getElementById("DescriptionArea") as HTMLInputElement;

    if (themaNameInput && themaDescInput) {
        const themaName: string = themaNameInput.value;
        const themaDesc: string = themaDescInput.value;

        const themaObject: Thema = {
            title: themaName,
            description: themaDesc
        };

        await addThemaData(themaObject)
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            });
    }
}

if (addButton) {
    addButton.addEventListener("click", addThema);
}