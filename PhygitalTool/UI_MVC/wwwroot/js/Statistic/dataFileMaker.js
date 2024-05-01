const csv = document.getElementById('download-csv');
const excel = document.getElementById('download-excel');
function csvFileMaker(){
    var tables = document.querySelectorAll('table[id^="table-"]');
    var csv = [];
    tables.forEach(function(table) {
        var questionText = table.previousElementSibling.innerText;
        csv.push(questionText);
        
        var rows = table.querySelectorAll('tr');
        rows.forEach(function(row) {
            var cols = row.querySelectorAll('td, th');
            csv.push(Array.from(cols, col => col.innerText).join(','));
        });
        csv.push('\n');
    });
    var csvString = csv.join('\n');
    var blob = new Blob([csvString], { type: 'text/csv;charset=utf-8;' });
    var url = URL.createObjectURL(blob);
    var link = document.createElement('a');
    link.href = url;
    link.download = 'data.csv';
    link.click();
}

//Vragen zijn heel lang en max is 31 chars waardoor we de naam van de sheet moeten inkorten en sanitzen van speciale tekens. :, , /, ?, *, [, or ]
function sanitizeSheetName(name) {
    var maxLength = 31;
    var illegalCharacters = /[:\\/?*[\]]/g;

    // Remove illegal characters
    var sanitized = name.replace(illegalCharacters, '');

    // Trim to max length
    if (sanitized.length > maxLength) {
        sanitized = sanitized.substring(0, maxLength);
    }

    return sanitized;
}

function excelFileMaker(){
    var tables = document.querySelectorAll('table[id^="table-"]');
    var wb = XLSX.utils.book_new();
    tables.forEach(function(table) {
        var ws = XLSX.utils.table_to_sheet(table);
        var sanitedSheetName = sanitizeSheetName(table.id);
        XLSX.utils.book_append_sheet(wb, ws, sanitedSheetName);
    });
    XLSX.writeFile(wb, 'data.xlsx');
}

csv.addEventListener('click', csvFileMaker);
excel.addEventListener('click', excelFileMaker);