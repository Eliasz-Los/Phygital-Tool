export async function addOrganisationData(organisationObject: Organisation): Promise<Organisation> {
        const response = await fetch(`/api/Organisations/AddOrganisation`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(organisationObject)
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
}