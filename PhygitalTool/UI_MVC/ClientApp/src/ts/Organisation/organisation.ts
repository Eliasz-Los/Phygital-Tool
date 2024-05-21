function addOrganisatie() {
    const orgName = document.getElementById('orgName') as HTMLInputElement;
    const orgDescription = document.getElementById('orgDescription') as HTMLInputElement;

    const orgObject = {
        name: orgName.value,
        description: orgDescription.value
    };

    fetch('/api/Organisatie/AddOrganisatie', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orgObject)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Success:', data);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

// Add event listener to the submit button
const submitButton = document.getElementById('submitOrg');
if (submitButton) {
    submitButton.addEventListener('click', addOrganisatie);
}