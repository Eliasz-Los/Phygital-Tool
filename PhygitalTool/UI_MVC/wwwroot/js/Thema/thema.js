const addButton = document.getElementById("submitThema");

function addThema() {
    const themaName = document.getElementById("ThemaInput").value;
    const themaDesc = document.getElementById("DescriptionArea").value;

    const themaObject = {
        title: themaName,
        description: themaDesc
    };
    console.log(themaObject);

    fetch(`/api/Themas/AddSubThemas`,
        {
            method: "POST",
            body: JSON.stringify(themaObject),
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                alert("Something went wrong");
            }
        })
        .then(data => {
            console.log(data); // Log the parsed JSON data
        })
        .catch(error => {
            console.log(error);
        });
}

addButton.addEventListener("click", addThema);



