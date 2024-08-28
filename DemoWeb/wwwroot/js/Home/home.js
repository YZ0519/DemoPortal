var hiddenDescription = JSON.parse(document.getElementById("HiddenDescription").value)
$(document).ready(function () {
    AssignDescription();
    DashboardConfiguration();
});
function AssignDescription() {
    for (let i = 0; i < hiddenDescription.length; i++) {
        var des = "historyDetails" + i.toString();
        quillDescription = new Quill('#' + des, {
            modules: {
                toolbar: false
            },
            readOnly: true
        });
        var jsonDescription = JSON.parse(hiddenDescription[i].description)
        quillDescription.setContents(jsonDescription);
    }
}

/*
const company = ['EHUB - May 2022', 'AGTIV - May 2023', 'AGTIV - August 2023', 'AGTIV - February 2023']
const salary = [2400, 3000, 3200, 3700]
const percentage = [0, 25, 6.67, 15.63]
*/
function DashboardConfiguration() {
    const company = JSON.parse(document.getElementById("companyValue").value)
    const salary = JSON.parse(document.getElementById("salaryValue").value)
    const percentage = JSON.parse(document.getElementById("percentageValue").value)
    new Chart(document.querySelector('#salaryHistory'), {
        type: 'bar',
        data: {
            datasets: [{
                label: 'Salary',
                data: salary,
                yAxisID: 'y',
                backgroundColor: 'rgba(30, 144, 255, 0.2)',
                borderColor: 'rgb(30, 144, 255)',
                barPercentage: 0.5,
                order: 2
            }, {
                label: 'Percentage',
                data: percentage,
                yAxisID: 'percentage',
                borderColor: 'rgb(50, 205, 50)',
                lineTension: 0.2,
                type: 'line',
                // this dataset is drawn on top
                order: 1
            }],
            labels: company
        },
        options: {
            scales: {
                y: {
                    type: 'linear',
                    position: 'left',
                    beginAtZero: true,
                },
                percentage: {
                    type: 'linear',
                    position: 'right',
                    beginAtZero: true,
                    suggestedMax: 100,
                    title: {
                        display: true,
                        text: 'Percentage'
                    },
                    grid: {
                        drawOnChartArea: false
                    },
                    ticks: {
                        callback: function (value, index, values) {
                            return `${value}%`;
                        }
                    }
                }
            }
        }
    });
}
