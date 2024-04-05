

function getSubthemesData() {
    fetch(`/api/Projects/SubThemas`,
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
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `<tr>
                            <td>${subThema.text}</td>
                            <td>
                                <input type="checkbox">
                            </td>
                        </tr>`
            }
            questionsElement.innerHTML += bodyData
        })
        .catch(error => {
            console.log(error)
        });

}