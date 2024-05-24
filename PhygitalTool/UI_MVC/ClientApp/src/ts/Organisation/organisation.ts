import {addOrganisationData} from "./organisationRest";

const addButton: HTMLElement | null = document.getElementById('submitOrg');

interface Organisation {
    name: string;
    description: string;
}

function addOrganisatie() {
    const orgNameInput: HTMLInputElement | null = document.getElementById('orgName') as HTMLInputElement;
    const orgDescriptionInput : HTMLInputElement | null = document.getElementById('orgDescription') as HTMLInputElement;

    if (orgNameInput && orgDescriptionInput) {
        const orgName: string = orgNameInput.value;
        const orgDescription: string = orgDescriptionInput.value;

        const orgObject: Organisation = {
            name: orgName,
            description: orgDescription
        };

        addOrganisationData(orgObject)
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