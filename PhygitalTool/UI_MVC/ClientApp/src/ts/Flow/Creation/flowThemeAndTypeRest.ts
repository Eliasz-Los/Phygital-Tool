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

export async function addFlowData(flowObject: flowObject): Promise<void> {
    const response = await fetch('/api/Flows/AddFlow', {
        headers: {
            "Content-Type": "application/json"
        }
    });
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
}


