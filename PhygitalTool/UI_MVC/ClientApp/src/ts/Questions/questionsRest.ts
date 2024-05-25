import {Question} from "./questions";


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

export async function sendOptions(options: string[]): Promise<void> {
    try {
        const response = await fetch('/api/Questions/SaveOptions', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(options)
        });

        if (response.ok) {
            console.log('Options sent successfully');
        } else {
            console.error('Failed to send options');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

export async function addQuestion(data: Question): Promise<void> {
    try {
        const response = await fetch('/api/Questions/AddQuestion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            alert('Question added successfully');
        } else {
            const errorMessage = await response.text();
            alert(`Failed to add question. Server response: ${errorMessage}`);
        }
    } catch (error) {
        console.error('Error adding question:', error);
    }
}

