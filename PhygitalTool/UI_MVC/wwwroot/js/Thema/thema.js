const addButton = document.getElementById("submitThema");
function addThema() {
    const themaName = document.getElementById("ThemaInput").value;
    const themaDesc = document.getElementById("DescriptionArea").value;

    const themaObject = {
        title: themaName,
        description: themaDesc
    };

    fetch(`/api/projects/subthemas`,
        {
            method: "POST",
            body: JSON.stringify(themaObject),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.status === 201 || response.status === 400) {
                return response.json();
            } else {
                alert("Something went wrong");
            }
        })
        .then(data => {
            console.log(data);
        })
        .catch(error => {
            console.log(error);
        });
}
addButton.addEventListener("click", addThema);
