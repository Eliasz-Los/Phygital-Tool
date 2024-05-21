async function addThemaData(themaObject: Thema): Promise<any> {
        const response = await fetch(`/api/Themas/AddSubThemas`, {
            method: "POST",
            body: JSON.stringify(themaObject),
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
}

//TODO: Het werkt nog niet ...