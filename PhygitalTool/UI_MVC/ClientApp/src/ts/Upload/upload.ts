const formElement = document.getElementById("upload-form") as HTMLFormElement;
const uploadButton = document.getElementById("upload-button") as HTMLButtonElement;
const imageElement = document.getElementById("uploaded-image") as HTMLImageElement;

async function uploadFile(): Promise<void> {
    if (!formElement) {
        return;
    }

    const resultElement = formElement.elements.namedItem("result") as HTMLOutputElement;
    const formData = new FormData(formElement);

    try {
        const response = await fetch("/api/files", {
            method: "POST",
            body: formData
        });

        if (response.ok) {
            alert("Upload succeeded!");
        }

        resultElement.value = `Result: ${response.status} ${response.statusText}.`;

        if (response.status === 200) {
            const responseObject = await response.json() as { url: string };
            imageElement.src = responseObject.url;
            console.log(`Here is the image in the cloud: ${responseObject.url}`);
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

uploadButton.addEventListener("click", uploadFile);