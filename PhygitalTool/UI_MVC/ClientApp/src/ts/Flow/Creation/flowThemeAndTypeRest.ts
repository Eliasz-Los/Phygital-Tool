import {flowObject} from "./flowThemeAndType";


export async function fillSubthemesSelect(): Promise<any> {
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

export async function addFlow(flowObject: flowObject): Promise<void> {
    const response = await fetch('/api/Flows/AddFlow', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(flowObject)
    });

    if (response.ok) {
        // Handle success response
        console.log('Flow added successfully');
    } else {
        // Handle error response
        console.error('Failed to add flow');
    }
}

