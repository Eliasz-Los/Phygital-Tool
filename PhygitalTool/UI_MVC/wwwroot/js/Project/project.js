const addButton = document.getElementById("addButton");

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
                bodyData += `<tr data-description="${subThema.description}">
                                <td>${subThema.title}</td>
                                <td><input type="checkbox"></td>
                            </tr>`;
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
                        <option value="${subThema.title}" data-description="${subThema.description}">${subThema.title}</option>
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
    const checkboxes = document.querySelectorAll("#SubthemaTable input[type='checkbox']:checked");
    let selectedThemes = [];

    checkboxes.forEach(checkbox => {
        const themeRow = checkbox.closest('tr');
        const themeTitle = themeRow.querySelector('td:first-child').textContent;
        const themeDescription = themeRow.dataset.description; // Access the data-description attribute
        const themeObject = {
            title: themeTitle,
            description: themeDescription
        };
        selectedThemes.push(themeObject);
    });

    console.log("Selected Themes:", selectedThemes);
    return selectedThemes;
}


function getName() {
    const nameBox = document.querySelector("#nameBox");
    return nameBox.value;
}

function getMainTheme() {
    const selectElement = document.querySelector("#ThemaSelect");
    const selectedIndex = selectElement.selectedIndex;

    if (selectedIndex !== -1) {
        const selectedOption = selectElement.options[selectedIndex];
        const selectedThemeTitle = selectedOption.value;
        const selectedThemeDescription = selectedOption.dataset.description; 
        console.log("Selected Theme Title:", selectedThemeTitle);
        console.log("Selected Theme Description:", selectedThemeDescription);

        // Return an object containing both title and description
        return {
            title: selectedThemeTitle,
            description: selectedThemeDescription
        };
    } else {
        console.log("No theme selected");
        return null; // or any default value indicating no selection
    }
}



function createProject() {
    const projectModel = {
        name: getName(), // retrieve name
        mainTheme: getMainTheme(), // retrieve main theme 
        themas: getSelectedThemes()   // retrieve selected themes
    };
    console.log(projectModel);

    fetch(`/api/Projects/AddProject`,
        {
            method: "POST",
            body: JSON.stringify(projectModel),
            headers: {
                "Content-Type": "application/json",
            }
        })
        .then(response => {
            if (response.ok) {
                console.log("Project gecreeerd: ", response)
            }else{
                throw new Error("Problem with creating project");
            }
        })
        .then(data => {
            console.log("Server response:", data);
            alert("Project created successfully!");
        })
        .catch(error => {
            console.error("Error:", error.message);
            alert("Problem with creating project: " + error.message);

        });
}


fillSubthemesSelect();
fillSubthemesTable();
addButton.addEventListener("click", createProject);