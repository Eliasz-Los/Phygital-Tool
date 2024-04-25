const csv = document.getElementById('download-csv');
const excel = document.getElementById('download-excel');
function createChart(tableId, chartId, questionText) {
    var table = document.getElementById(tableId);
    if (!table) {
        console.log('Table not found: ' + tableId);
        return;
    }
    var rows = table.querySelectorAll('tbody tr');

    var labels = [];
    var data = [];

    rows.forEach(function(row) {
        var cells = row.querySelectorAll('td');
        labels.push(cells[0].textContent);
        data.push(parseInt(cells[1].textContent));
    });

    var ctx = document.getElementById(chartId).getContext('2d');
    var chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: questionText,
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    return chart;
}

$(document).ready(function() {
    var tables = document.querySelectorAll('table[id^="table-"]');
    tables.forEach(function(table) {
        var safeId = table.id.replace('table-', '');
        createChart('table-' + safeId, 'chart-' + safeId, safeId);
    });
});

function csvFileMaker(){
    var tables = document.querySelectorAll('table[id^="table-"]');
    var csv = [];
    tables.forEach(function(table) {
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