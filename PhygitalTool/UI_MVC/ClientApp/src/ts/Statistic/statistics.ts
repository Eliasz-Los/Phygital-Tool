import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

function createBarChart(chartId: string, labels: string[], data: number[]): void {
    const canvas = document.getElementById(chartId) as HTMLCanvasElement;
    if (!canvas) {
        console.log('Canvas not found: ' + chartId);
        return;
    }

    const ctx = canvas.getContext('2d');
    if (!ctx) {
        console.log('Canvas context not found: ' + chartId);
        return;
    }

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Number of Participations',
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
}

// Get the flowId from the data-flow-id attribute of the body tag
let flowIdElement = document.querySelector('body');
let flowId = flowIdElement ? (flowIdElement.getAttribute('data-flow-id')) : 1;

// Call the API endpoint to get the participation counts by time spent categories
fetch(`/api/GetParticipationCountsByTimeSpentCategories?flowId=${flowId}`)
    .then(response => response.json())
    .then(participationCountsByTimeSpentCategories => {
        const labels = Object.keys(participationCountsByTimeSpentCategories);
        const data = Object.values(participationCountsByTimeSpentCategories).map(Number);
        createBarChart('participationChart', labels, data);
    })
    .catch(e => {
        console.log('There was a problem with the fetch operation: ' + e.message);
    });