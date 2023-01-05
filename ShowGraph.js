const dateOptions = {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  };


async function readJSONFile() {
    // Send a request to the server to get the JSON file
    const response = await fetch('http://localhost:8080/cars.json');
    // Parse the response as JSON
    const data = await response.json();
    // Use the data to plot the chart
    plotChart(data);
  }
  
function plotChart(data) {
    // Extract the time, high, low, and average values from the data
    const xDato = data.map(d => new Date(d.DateTime).toLocaleDateString('nb-NO', dateOptions))

    const dataInput =          {
        datasets: [
          {
            label: "HighV",
            type: "bar",
            data: data.map(d => d.HigestPrice),
            color: "red",
            fill: false,
            yAxisID: 'y'
          },
          {
            label: "LowV",
            type: "bar",
            data: data.map(d => d.LowestPrice),
            color: "green",
            fill: false,
            yAxisID: 'y'
          },
          {
            label: "Gjennomsnitt",
            type: "line",
            data: data.map(d => d.MeanValue),
            borderColor: "blue",
            fill: false,
            yAxisID: 'y'
          },
          {
            label: "Antall",
            type: "line",
            data: data.map(d => d.NoOfPoints),
            borderColor: "orange",
            fill: false,
            yAxisID: 'y_right'
          }

        ],
        labels: xDato
        
      }

    const ctx = document.getElementById('myChart').getContext('2d');
    const myBarChart = new Chart(ctx, {
        data: dataInput,
        options: {  legend: {display: false},
                    scales: {
                        y:  {
                            position: 'left',
                            ticks: {
                                callback: function(value, index, values)  {
                                    return `${value} kr`;
                                }
                            }
                        },
                        y_right: {
                            position: 'right',
                            grid: {
                                drawOnChartArea: false
                            }
                        }

                    }

        }
      });
     

         // Create the chart
    /*
    const ctx = document.getElementById('myChart').getContext('2d');
    const chart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: time,
        datasets: [{
        label: 'Middelvalue',
        data: data.map(d => {
            return {
            t: d.DateTime,
            y: d.MiddelValue,
            };
        }),
        borderColor: 'rgb(255,255, 132)',
        }, {
        type: 'line',
        label: 'Average',
        data: data.map(d => {
            return {
            t: d.DateTime,
            y: d.MeanValue,
            };
        }),
        borderColor: 'rgb(255, 99, 132)',
        }, {
        type: 'line',
        label: 'High',
        data: data.map(d => {
            return {
            t: d.DateTime,
            y: d.HigestPrice,
        
            };
        }),
        borderColor: 'rgb(255, 99, 0)',
        }, {
            type: 'bar',
            label: 'High/Low',
            data: data.map(d => {
                return {
                t: d.DateTime,
                o: d.LowestPrice,
                c: d.HigstPrice,
                s: [d.LowestPrice,d.HigestPrice]
                };
            }),
            borderColor: 'rgb(255, 26, 104)',
            }

    
        ]
    },
    options: {
        parsing: {
            yAxes:'s'
        },
        scales: {
        yAxes: [{
            ticks: {
            beginAtZero: true
            }
        }]
        }
    }
    });
    */

}

readJSONFile();
    
/*
    new Chart("myChart", {
    type: "line",
    data: {
        labels: data.map(d => d.DateTime),
        datasets: [{ 
        data: data.map(d => d.HigestPrice),
        borderColor: "red",
        fill: false,
        yAxisID: 'y'
        }, { 
        data: data.map(d => d.LowestPrice),
        borderColor: "green",
        fill: false,
        yAxisID: 'y'
        }, { 
        data: data.map(d => d.MeanValue),
        borderColor: "blue",
        fill: false,
        yAxisID: 'y'
        }, { 
        data: data.map(d => d.NoOfPoints),
        borderColor: "yellow",
        fill: false,
        yAxisID: 'y-axis-2'
        }]
    },
    options: {
        legend: {display: false},
        scales: {
            yAxes: [{
                id:"y",
                position:'left',
                type: 'linear'
            },{
                id:"y-axis-2",
                position:'right',
                type: 'linear',
                gridLines: {
                    drawOnChartArea: false}
            }]
        }
    }
    });

*/

/**
 fetch('http://localhost:8080/cars.json')
    .then(response => response.json())
    .then(data => {
        debugger;
        // data is the JSON data from your file
        // Create a new line chart
        const time = data.map(d => new Date(d.DateTime));
        const price = data.map(d => d.HigestPrice);
    
        // Create the chart
        const ctx = document.getElementById('myChart').getContext('2d');
        const chart = new Chart(ctx, {
          type: 'line',
          data: {
            labels: time,
            datasets: [{
              label: 'Price',
              data: price
            }]
          },
          options: {
            scales: {
              xAxes: [{
                type: 'time'
              }]
            }
          }
        });
      });

 */