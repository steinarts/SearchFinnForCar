  const dateOptions = {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  };

  let myBarChart = null;

  async function loadJSONFile() {
    // Hent valgt fil fra select-elementet
    const selectElement = document.getElementById("fileSelect");
    const selectedFile = selectElement.value;
    // Kall plotChart med valgt fil
    const data = await readJSONFile(selectedFile);
     // Ødelegg eksisterende Chart før du oppretter et nytt
     destroyChart();
    
     // Opprett et nytt Chart med dataene
    plotChart(data);
  }

  function destroyChart() {
    if (myBarChart) { 
      myBarChart.destroy();
    }
  }


  async function readJSONFile(fileName) {
      // Send en forespørsel til serveren for å hente JSON-filen
      const response = await fetch(`http://localhost:8080/${fileName}`);
      // Parse svaret som JSON
      const data = await response.json();
      return data;
  }
  //async function readJSONFile() {
  //  // Send a request to the server to get the JSON file
  //    const response = await fetch('http://localhost:8080/cars.json');
  //    // Parse the response as JSON
  //    const data = await response.json();
      // Use the data to plot the chart
  //    plotChart(data);
  //}
  
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
  myBarChart = new Chart(ctx, {
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
     
}

window.addEventListener('DOMContentLoaded', loadJSONFile);

    
