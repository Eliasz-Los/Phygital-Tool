const themeElement = document.getElementById("themeId");
const themeId = parseInt(themeElement.innerText)
console.log(themeId);
function fillQuestionsTable() {
    fetch(`/api/Flows/${themeId}/OpenQuestionsOfTheme`,
        {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            console.log(response); // Log the entire response
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error("Something went wrong in the backend subthemes, check the console for more details!");
            }
        })
        .then(questions => {
            let output = document.querySelector("#QuestionTable");
            let bodyData = ``;
            for (const question of questions) {
                bodyData += `<tr data-description="test">
                                <td>${question.text}</td>
                                <td><input type="checkbox"></td>
                            </tr>`;
                console.log(question);
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.error(error);
        });
}

fillQuestionsTable();
