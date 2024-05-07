function fillSubthemesSelect() {
    fetch(`/api/Themas/subthemas`, //flow/subthemas bestaat niet, we gaan jonas zijn api gebruiken waar da wel is
        {
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
                        <option value="${subThema.title}" data-description="${subThema.description}">${subThema.title}</option>
                        `
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error)
        });
}

function addFlow() {
    var selectedType = document.getElementById('TypeSelect').value;
    // TODO ID VAN THEMA MEEKRIJGEN
    var selectedTheme = document.getElementById('ThemaSelect').value;
    var isActive = document.getElementById('ActiveCheckbox').checked;

    var data = {
        selectedType: selectedType,
        selectedTheme: parseInt(selectedTheme),
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