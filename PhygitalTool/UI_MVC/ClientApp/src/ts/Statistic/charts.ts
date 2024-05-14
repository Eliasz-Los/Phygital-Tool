function createDoughnutChart(tableId: string, chartId: string, questionText: string): any
{
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

    var ctx = document.getElementById(chartId).getElementsByClassName('2d');
    var chart = new chart(ctx, {
        type: 'doughnut', // Change this line
        data: {
            labels: labels,
            datasets: [{
                label: questionText,
                data: data,
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)',
                    'rgb(75,192,91)',
                ],
                //backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                },
            }
        }
    });
    return chart;
}

function createCharts(): any{
    var tables = document.querySelectorAll('table[id^="table-"]');
    tables.forEach(function(table) {
        var safeId = table.id.replace('table-', '');
        createDoughnutChart('table-' + safeId, 'chart-' + safeId, safeId);
    });
}
   
createCharts();