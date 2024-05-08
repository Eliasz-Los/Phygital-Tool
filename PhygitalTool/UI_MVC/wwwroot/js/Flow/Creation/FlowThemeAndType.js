function fillSubthemesSelect() {
    fetch(`/api/Themas/subthemas`, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => {
            if (response.status === 200) {
                return response.json()
            } else {
                alert("Something went wrong in the backend subthemas, check the console for more details!")
            }
        })
        .then(subThemas => {
            let output = document.getElementById("ThemaSelect");
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


function addFlow() {
    var selectedType = document.getElementById('TypeSelect').value;
    var selectedTheme = document.getElementById('ThemaSelect');
    var selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    var isActive = document.getElementById('ActiveCheckbox').checked;

    var data = {
        selectedType: selectedType,
        selectedTheme: parseInt(selectedThemeId),
        isActive: isActive
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
            } else {
                // Handle error response
                console.error('Failed to add flow');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

document.getElementById("submitFlow").addEventListener("click", addFlow);
fillSubthemesSelect();