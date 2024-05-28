export async function addOrganisationData(organisationObject: Organisation): Promise<Organisation> {
        const response = await fetch(`/api/Organisations/AddOrganisation`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(organisationObject)
        });

        if (!response.ok) {
            const error = await response.json();
            throw {status: response.status, message: error};
        }
        return await response.json();
}