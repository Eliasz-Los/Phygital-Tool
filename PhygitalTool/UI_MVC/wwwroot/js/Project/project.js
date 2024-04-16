function fillSubthemesTable() {
    fetch(`/api/projects/subthemas`,
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
            let output = document.querySelector("#SubthemaTable");
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `<tr>
                            <td>${subThema.title}</td>
                            <td><input type="checkbox"></td>
                        </tr>`
                console.log(subThema);
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error)
        });
}

function fillSubthemesSelect() {
    fetch(`/api/projects/subthemas`,
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
            let output = document.querySelector("#ThemaSelect");
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `
                        <option value="${subThema.id}">${subThema.title}</option>
                        `
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error)
        });
}



// Function to retrieve selected themes when the button is clicked
function getSelectedThemes() {
    const selectElement = document.querySelector("#SubthemaTable");
    let selectedThemes = [];
    
    selectElement.querySelectorAll('option').forEach(option => {
        if (option.selected) {
            selectedThemes.push(option.value);
        }
    });
    
    console.log("Selected Themes:", selectedThemes);
    return selectedThemes;
}


function getName() {
    const nameBox = document.querySelector("#nameBox");
    const name = nameBox.value;
    return name;
}

function getMainTheme() {
    const themeBox = document.querySelector("#ThemaSelect");
    const mainTheme = themeBox.value;
    return themeBox;
}


function createProject() {
    const projectModel = {
        name: getName(), // retrieve name
        mainTheme: getMainTheme(), // retrieve main theme 
        subThemes: getSelectedThemes()   // retrieve selected themes
    };

    fetch(`/api/projects/AddProject`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(projectModel)
    })
        .then(response => {
            console.log("response: ", response )
            console.log("answerObject: ", projectModel)
            if (response.ok) {
                console.log("Project werd gecreeerd: ", response)
            } else{
                alert("Problem with creating project: " + JSON.stringify(projectModel))
            }
        })
        .catch(error => {
            console.log("problem with fetching: ", error)
        });
}

fillSubthemesSelect();
fillSubthemesTable();