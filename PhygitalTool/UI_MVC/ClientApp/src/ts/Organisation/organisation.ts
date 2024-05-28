import {addOrganisationData} from "./organisationRest";

const addButton: HTMLElement | null = document.getElementById('submitOrg');

interface Organisation {
    name: string;
    description: string;
}

async function addOrganisatie() {
    const orgNameInput: HTMLInputElement | null = document.getElementById('orgNameInput') as HTMLInputElement;
    const orgDescriptionInput : HTMLInputElement | null = document.getElementById('orgDescriptionInput') as HTMLInputElement;

    if (orgNameInput && orgDescriptionInput) {
        const orgName: string = orgNameInput.value;
        const orgDescription: string = orgDescriptionInput.value;

        const orgObject: Organisation = {
            name: orgName,
            description: orgDescription
        };
        console.log("Adding organisation: " + orgName + " with description: " + orgDescription + " to the database.)");

        await addOrganisationData(orgObject)
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            });
    }
}

if (addButton) {
    addButton.addEventListener("click", addOrganisatie);
}