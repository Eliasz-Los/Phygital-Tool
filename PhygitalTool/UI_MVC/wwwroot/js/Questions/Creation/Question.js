function fillSubthemesSelect() {
    fetch(`/api/Themas/subthemas`, {
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
            let output = document.getElementById("ThemaSelect");
            let bodyData = ``;
            for (const subThema of subThemas) {
                bodyData += `
                <option value="${subThema.id}" data-description="${subThema.description}">${subThema.title}</option>
            `;
            }
            output.innerHTML += bodyData;
        })
        .catch(error => {
            console.log(error);
        });
}

function addOption() {
    var input = document.getElementById('OptionTitle');
    var optionValue = input.value.trim();
    var optionList = document.getElementById('optionList');

    if (optionValue && optionList.childElementCount < 4) {
        var listItem = document.createElement('li');
        listItem.className = 'list-group-item d-flex justify-content-between align-items-center';
        listItem.textContent = optionValue;

        if (optionValue.length > 30) {
            alert('Option cannot exceed 30 characters.');
            return;
        }
        
        var removeButton = document.createElement('button');
        removeButton.className = 'btn btn-danger btn-sm';
        removeButton.textContent = 'Remove';
        removeButton.addEventListener('click', function() {
            optionList.removeChild(listItem);
            document.getElementById('OptionButton').disabled = false;
        });

        listItem.appendChild(removeButton);
        optionList.appendChild(listItem);

        input.value = ''; // Clear the input field after adding

        // Disable the button if there are 4 items in the list
        if (optionList.childElementCount >= 4) {
            document.getElementById('OptionButton').disabled = true;
        }
    }
}

function addQuestion() {
    var questionTitle = document.getElementById('QuestionTitle').value;
    var selectedTheme = document.getElementById('ThemaSelect');
    var selectedThemeId = selectedTheme.options[selectedTheme.selectedIndex].value;
    var isActive = document.getElementById('ActiveCheckbox').checked;
    var selectedType = document.getElementById('TypeSelect');
    selectedType = selectedType.value

    var data = {
        Text: questionTitle,
        isActive: isActive,
        SubTheme: parseInt(selectedThemeId),
        Type: selectedType
    };

    // Send POST request to the server
    fetch('/api/Questions/AddQuestion', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (response.ok) {
                // Handle success response
                console.log('Question added successfully');
            } else {
                // Handle error response
                console.error('Failed to add Question');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

document.getElementById('OptionButton').addEventListener("click", addOption);
document.getElementById("submitQuestion").addEventListener("click", addQuestion);
fillSubthemesSelect();