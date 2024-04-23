function fillSubthemesTable() {
    fetch(`/api/Themas/subthemas`, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => {
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error("Something went wrong in the backend subthemas, check the console for more details!");
            }
        })
        .then(subThemas => {
            let output = document.querySelector("#SubthemaTable");
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `<tr data-description="${subThema.description}">
                            <td>${subThema.title}</td>
                            <td><a class="bi bi-pencil-square" href=""></a></td>
                            <td><i class="bi bi-trash deleteIcon" data-id="${subThema.id}"></i></td>
                        </tr>`;
                console.log(subThema);
            }
            output.innerHTML += bodyData;
            // Select all delete icons
            const deleteIcons = document.querySelectorAll(".deleteIcon");

            // Iterate over delete icons and attach event listeners
            deleteIcons.forEach(icon => {
                icon.addEventListener("click", function () {
                    let subThemaId = this.getAttribute("data-id");
                    deleteSubtheme(subThemaId);
                });
            });
        })
        .catch(error => {
            console.error(error);
            // Handle error
        });
}


function deleteSubtheme(idTheme) {
    return fetch("/api/Themas/deleteSubTheme/" + idTheme, {
        method: 'DELETE'
    })
        .then(function (response) {
            // Check if deletion was successful
            if (!response.ok) {
                throw Error('Unable to DELETE the theme: ' + response.status + ' ' + response.statusText);
            }
            location.reload();
        })
        .then(function () {
            console.log("Deletion successful");
        })
        .catch(function (error) {
            console.error("Error deleting theme:", error);
            throw error;
        });
}




fillSubthemesTable();
