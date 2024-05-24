export async function addOrganisationData(organisationObject: Organisation): Promise<any> {
        const response = await fetch(`/api/Organisations/AddOrganisation`, {
            method: "POST",
            body: JSON.stringify(organisationObject),
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
}