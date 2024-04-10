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

function createProject() {
    const answers  = getAnswers();
    const answerObject = answers.map(answer =>({
        Flow: {Id: flowId}, // Send Flow as an object with an Id property
        subTheme: {Title: "test"},  // Send SubTheme as an object with a Title property
        chosenOptions: answer.chosenOptions.map(option => ({OptionText: option})),   // Send each option as an object with an OptionText property
        chosenAnswer: answer.openAnswer
    }));

    fetch(`/api/projects/AddProject`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(answerObject)
    })
        .then(response => {
            console.log("response: ", response )
            console.log("answerObject: ", answerObject)
            if (response.ok) {
                console.log("Project werd gecreeerd: ", response)
            } else{
                alert("Problem with commiting answers: " + JSON.stringify(answerObject))
            }
        })
        .catch(error => {
            console.log("problem with fetching: ", error)
        });
}

fillSubthemesSelect();
fillSubthemesTable();