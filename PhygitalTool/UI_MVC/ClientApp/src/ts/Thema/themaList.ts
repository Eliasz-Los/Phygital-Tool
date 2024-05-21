type SubTheme = {
    description: string;
    title: string;
    id: string;
};

async function fillSubthemesTable(): Promise<void> {
    try {
        const response = await fetch(`/api/Themas/subthemas`, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });

        if (response.status !== 200) {
            throw new Error("Something went wrong in the backend subthemas, check the console for more details!");
        }

        const subThemas: SubTheme[] = await response.json();
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

        if (output) {
            output.innerHTML += bodyData;
        }

        // Select all delete icons
        const deleteIcons = document.querySelectorAll(".deleteIcon");

        // Iterate over delete icons and attach event listeners
        deleteIcons.forEach(function(this: HTMLElement) {
            this.addEventListener("click", function() {
                let subThemaId = this.getAttribute("data-id");
                if (subThemaId) {
                    deleteSubtheme(subThemaId);
                }
            });
        });
    } catch (error) {
        console.error(error);
    }
}

async function deleteSubtheme(idTheme: string): Promise<void> {
    try {
        const response = await fetch("/api/Themas/deleteSubTheme/" + idTheme, {
            method: 'DELETE'
        });

        // Check if deletion was successful
        if (!response.ok) {
            throw Error('Unable to DELETE the theme: ' + response.status + ' ' + response.statusText);
        }

        location.reload();
        console.log("Deletion successful");
    } catch (error) {
        console.error("Error deleting theme:", error);
        throw error;
    }
}

fillSubthemesTable();