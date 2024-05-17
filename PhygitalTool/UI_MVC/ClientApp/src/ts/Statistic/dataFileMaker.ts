import * as XLSX from 'xlsx';

const csv: HTMLElement | null = document.getElementById('download-csv');
const excel: HTMLElement | null = document.getElementById('download-excel');
/*npm install --save-dev @types/xlsx OM specifieke types van library te kunnen gebruiken in typescript*/
function csvFileMaker(): void {
    const tables: NodeListOf<HTMLTableElement> = document.querySelectorAll('table[id^="table-"]');
    let csv: string[] = [];
    tables.forEach((table: HTMLTableElement) => {
        //                                                         .innerText
        const questionText: string = table.previousElementSibling?.textContent || '';
        csv.push(questionText);

        const rows: NodeListOf<HTMLTableRowElement> = table.querySelectorAll('tr');
        rows.forEach((row: HTMLTableRowElement) => {
            const cols: NodeListOf<HTMLTableCellElement> = row.querySelectorAll('td, th');
            csv.push(Array.from(cols, col => col.innerText).join(','));
        });
        csv.push('\n');
    });
    const csvString: string = csv.join('\n');
    const blob: Blob = new Blob([csvString], { type: 'text/csv;charset=utf-8;' });
    const url: string = URL.createObjectURL(blob);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = 'data.csv';
    link.click();
}

function sanitizeSheetName(name: string): string {
    const maxLength: number = 31;
    const illegalCharacters: RegExp = /[:\\/?*[\]]/g;

    let sanitized: string = name.replace(illegalCharacters, '');

    if (sanitized.length > maxLength) {
        sanitized = sanitized.substring(0, maxLength);
    }

    return sanitized;
}

function excelFileMaker(): void {
    const tables: NodeListOf<HTMLTableElement> = document.querySelectorAll('table[id^="table-"]');
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    tables.forEach((table: HTMLTableElement) => {
        const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(table);
        const sanitizedSheetName: string = sanitizeSheetName(table.id);
        XLSX.utils.book_append_sheet(wb, ws, sanitizedSheetName);
    });
    XLSX.writeFile(wb, 'data.xlsx');
}

if (csv) {
    csv.addEventListener('click', csvFileMaker);
}

if (excel) {
    excel.addEventListener('click', excelFileMaker);
}