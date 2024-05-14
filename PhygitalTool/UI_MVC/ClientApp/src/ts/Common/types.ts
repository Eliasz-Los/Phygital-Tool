//TODO: alle types in een file zetten zodat ze overal gebruikt kunnen worden en
// typescript die kan gebruiken zodat we properties kunnen gebruiken

//details.ts
type Keys = 'Key1' | 'Key2' | 'Key3' | 'Key4';

interface SingleChoiceQuestion {
    sequenceNumber: number;
    id: string;
    text: string;
    options: string[];
}

interface OpenQuestion {
    sequenceNumber: number;
    id: string;
    text: string;
}

interface RangeQuestion {
    sequenceNumber: number;
    id: string;
    text: string;
    options: string[];
}

interface MultipleChoiceQuestion {
    sequenceNumber: number;
    id: string;
    text: string;
    options: string[];
}

interface Theme {
    title: string;
    description: string;
}

interface TextInfo {
    title: string;
    content: string;
}

interface ImageInfo {
    title: string;
    url: string;
    altText: string;
}

interface VideoInfo {
    title: string;
    url: string;
    description: string;
}

interface Answer {
    question: string;
    chosenOptions: string[];
    openAnswer: string;
    id: string;
}

interface AnswerObject {
    chosenOptions: { OptionText: string }[];
    chosenAnswer: string;
    questionId: string;
}