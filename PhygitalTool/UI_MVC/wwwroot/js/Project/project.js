

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
            // totalQuestions += openQuestions.length;
            for (let i = 0; i < subThemas.length; i++) {
                const subThema = subThemas[i];
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