const addButton = document.getElementById("delete");
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
                                <td><a class="bi bi-pencil-square" href=""></a></td>
                                <td><i class="bi bi-trash" id="delete"></td>
                            </tr>`;
                console.log(subThema);
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error)
        });
}

function deleteSubtheme() {
    return fetch("/api/projects/subthemas" + '/' + idTheme, {
        method: 'DELETE'
    })
        .then(function (response) {
            if (!response.ok) {
                throw Error(
                    'Unable to DELETE the theme: ' +
                    response.status +
                    ' ' +
                    response.statusText
                );
            }
            return response.json();
        })
        .catch(function (e) {
            console.error(e);
            throw e;
        });
}
addButton.addEventListener("click", deleteSubtheme);
fillSubthemesTable();
