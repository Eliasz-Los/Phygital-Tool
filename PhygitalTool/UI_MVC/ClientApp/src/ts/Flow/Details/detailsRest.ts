//TODO: maak de rest vna apis aan
// This file contains functions that make HTTP requests to the server to get data for the flow details page.
//Promise<SingleChoiceQuestion[]> miss types opmaken
export async function readSingleChoiceQuestionData(flowId: number): Promise<SingleChoiceQuestion[]> {
    const response = await fetch(`/api/flows/${flowId}/SingleChoiceQuestions`);
    if (response.ok) {
        return response.json();
    } else {
        throw new Error("Error fetching single choice questions");
    }
}

export async function readOpenQuestionsData(flowId: number) :Promise<OpenQuestion[]> {
    const response = await fetch(`/api/flows/${flowId}/OpenQuestions`);
    if (!response.ok) {
        throw new Error("Error fetching open questions");
    }
    return await response.json();
}

export async function readRangeQuestionsData(flowId: number): Promise<RangeQuestion[]> {
    const response = await fetch(`/api/flows/${flowId}/RangeQuestions`);
    if (!response.ok) {
        throw new Error("Error fetching range questions");
    }
    return await response.json();
}

export async function readMultipleChoiceQuestionsData(flowId: number):Promise<MultipleChoiceQuestion[]> {
    const response = await fetch(`/api/flows/${flowId}/MultipleChoiceQuestions`);
    if (!response.ok) {
        throw new Error("Error fetching multiple choice questions");
    }
    return await response.json();
}

export async function readTextData(flowId: number): Promise<TextInfo[]> {
    const response = await fetch(`/api/flows/${flowId}/TextInfos`);
    if (!response.ok) {
        throw new Error("Error fetching text infos");
    }
    return await response.json();
}

export async function readImageData(flowId: number): Promise<ImageInfo[]>{
    const response = await fetch(`/api/flows/${flowId}/ImageInfos`);
    if (!response.ok) {
        throw new Error("Error fetching image infos");
    }
    return await response.json();
}

export async function readVideoData(flowId: number): Promise<VideoInfo[]>{
    const response = await fetch(`/api/flows/${flowId}/VideoInfos`);
    if (!response.ok) {
        throw new Error("Error fetching video infos");
    }
    return await response.json();
}

//TODO: fix AnswerObject type
export async function commitAnswer(flowId: number, answerObject: any): Promise<any>{
    const response = await fetch(`/api/flows/${flowId}/AddAnswers`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(answerObject)
    });
    if (!response.ok) {
        throw new Error("Error committing answers");
    }
    return await response.json();
}