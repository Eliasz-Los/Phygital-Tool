const flowElementBody = document.getElementById("flowElementId")
const addButton = document.getElementById("answerQuestion")

function getSingleChoiceQuestionData() {
    fetch(`http://localhost:5000/api/flow/${flowId}/SingleChoiceQuestions`,
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
                alert("Something went wrong ;(")
            }
        })
        .then(singleChoiceQuestions => {
            let bodyData = ``;
            for (const singleChoiceQuestion of singleChoiceQuestions) {
                bodyData += `<tr><td>${singleChoiceQuestion.text}</td><td>${singleChoiceQuestion.options}</td></tr>`
            }
            flowElementBody.innerHTML = bodyData
        })
        .catch(error => {
            console.log(error)
        });
}


function commitAnswer() {
    // TODO : save answer & go to next flowElement
}

getSingleChoiceQuestionData();

addButton.addEventListener("click", commitAnswer);