const addButton = document.getElementById("submitThema");

function addThema() {

    const themaName = document.getElementById("ThemaInput").value;
    const themaDesc = document.getElementById("DescriptionArea").value;
    
    const themaObject = {
        title: themaName,
        description: themaDesc
    };

    fetch(`/api/Themas/AddSubThemas`, {
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
                throw new Error("Something went wrong.");
            }
        })
        .then(data => {
            console.log(data); // Log the parsed JSON data
            // Optionally, perform further actions (e.g., display a success message)
        })
        .catch(error => {
            console.error(error);
            // Optionally, handle the error (e.g., display an error message)
        });
}

document.getElementById("submitThema").addEventListener("click", addThema);


