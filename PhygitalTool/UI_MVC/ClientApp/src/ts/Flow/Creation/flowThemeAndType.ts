interface SubTheme {
    id: number;
    title: string;
    description: string;
}

interface FlowData {
    FlowType: string;
    IsOpen: boolean;
    ThemeId: number;
}

function fillSubthemesSelect(): void {
    fetch(`/api/Themas/subthemas`, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => {
            if (response.status === 200) {
                return response.json() as Promise<SubTheme[]>;
            } else {
                alert("Something went wrong in the backend subthemas, check the console for more details!");
                return Promise.reject("Failed to fetch subthemas");
            }
        })
        .then(subThemas => {
            const output = document.getElementById("ThemaSelect") as HTMLSelectElement;
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `
                <option value="${subThema.id}" data-description="${subThema.description}">${subThema.title}</option>
            `;
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error);
        });
}

function addFlow(): void {
    const selectedType = (document.getElementById('TypeSelect') as HTMLSelectElement).value;
    const selectedTheme = document.getElementById('ThemaSelect') as HTMLSelectElement;
    const selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    const isActive = (document.getElementById('ActiveCheckbox') as HTMLInputElement).checked;

    const data: FlowData = {
        FlowType: selectedType,
        IsOpen: isActive,
        ThemeId: parseInt(selectedThemeId)
    };

    // Send POST request to the server
    fetch('/api/Flows/AddFlow', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (response.ok) {
                // Handle success response
                console.log('Flow added successfully');
                // Redirect to the Index action with a query parameter to indicate refresh
                window.location.href = '/Flow/Index?refresh=true';
            } else {
                // Handle error response
                console.error('Failed to add flow');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

document.getElementById("submitFlow")?.addEventListener("click", addFlow);
fillSubthemesSelect();