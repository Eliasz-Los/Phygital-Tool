// Import Chart from the chart.js package
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables)

// Define the flowId
let flowId = 1; // Replace this with the actual flowId

// Call the API endpoint to get the participation counts by time spent categories
fetch(`/api/GetParticipationCountsByTimeSpentCategories?flowId=${flowId}`)
    .then(response => response.json())
    .then(participationCountsByTimeSpentCategories => {
        // Get the context of the canvas element
        let ctx = (document.getElementById('participationChart') as HTMLCanvasElement).getContext('2d');

        if(!ctx) {
            console.log('Canvas context not found: ' + 'participationChart');
            return;
        }
        // Create the chart
        let chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: Object.keys(participationCountsByTimeSpentCategories),
                datasets: [{
                    label: 'Number of Participations',
                    data: Object.values(participationCountsByTimeSpentCategories),
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
    });