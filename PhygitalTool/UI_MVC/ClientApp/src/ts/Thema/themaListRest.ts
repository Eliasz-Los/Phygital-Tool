export async function tableData(): Promise<any> {
    const response = await fetch(`/api/Themas/subthemas`, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    });
        if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
}

export async function deleteSubtheme(idTheme: string): Promise<void> {
    try {
        const response = await fetch("/api/Themas/deleteSubTheme/" + idTheme, {
            method: 'DELETE'
        });

        // Check if deletion was successful
        if (!response.ok) {
            throw Error('Unable to DELETE the theme: ' + response.status + ' ' + response.statusText);
        }

        console.log("Deletion successful");
    } catch (error) {
        console.error("Error deleting theme:", error);
        throw error;
    }
}
