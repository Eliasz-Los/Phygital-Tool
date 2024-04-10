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

fillSubthemesSelect();
fillSubthemesTable();