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
                        <option value="${subThema.title}">${subThema.title}</option>
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
        const themeTitle = checkbox.closest('tr').querySelector('td:first-child').textContent;
        selectedThemes.push(themeTitle);
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
        const selectedTheme = selectedOption.value;
        console.log("Selected Theme:", selectedTheme);
        return selectedTheme;
    } else {
        console.log("No theme selected");
        return null; // or any default value indicating no selection
    }
}


function createProject() {
    const projectModel = {
        name: getName(), // retrieve name
        mainTheme: getMainTheme(), // retrieve main theme 
        subThemes: getSelectedThemes()   // retrieve selected themes
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
                return response.json(); // Parse response JSON
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