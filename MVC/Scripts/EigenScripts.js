function grafiekWijzigen(teWijzigenGrafiek) {
    var titel = $("#inputTitel").val();


    //if (document.getElementById('chkAssenWisselen').checked) {
    //    teWijzigenGrafiek.destroy();
    //    teWijzigenGrafiek = new Chart(ctx, {
    //        type: 'horizontalBar',
    //        data: chartData,
    //        options: chartOptions
    //    });
    //} else {
    //    teWijzigenGrafiek.destroy();
    //    teWijzigenGrafiek = new Chart(ctx, {
    //        type: 'bar',
    //        data: chartData,
    //        options: chartOptions
    //    });
    //}


    //if (xAs === "data1") {
    //    teWijzigenGrafiek.data.labels = ["Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag"];
    //    teWijzigenGrafiek.data.datasets[0].data = [1, 2, 3, 4, 5, 6, 7];
    //    //myHorizontalBarChart.data.datasets[0].data[0] = 90;
    //} else if (xAs === "data2") {
    //    teWijzigenGrafiek.data.labels = ["Week 1", "Week 2", "Week3", "Week4"];
    //    teWijzigenGrafiek.data.datasets[0].data = [5, 15, 25, 35];
    //} else if (xAs === "data3") {
    //    //teWijzigenGrafiek.data.labels = ["test1", "test2"];
    //    //teWijzigenGrafiek.data.datasets[0].data = [50, 50];
    //    teWijzigenGrafiek.data.labels = ["test1"];
    //    teWijzigenGrafiek.data.datasets[0].data = [50];
    //}

   

    teWijzigenGrafiek.options.title.text = titel;

    teWijzigenGrafiek.update();
}



function GrafiekOpbouwen(id, titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, toonXAs = true, toonYAs = true, datasetFill, lijnweergave,
    xAsMaxRotatie = 90, xAsMinRotatie = 0, xTitel, yTitel,
    labels, label1 = null, label2 = null, label3 = null, label4 = null, label5 = null,
    data1, data2 = null, data3 = null, data4 = null, data5 = null,
    backgroundcolor1 = null, backgroundcolor2 = null, backgroundcolor3 = null, backgroundcolor4 = null, backgroundcolor5 = null,
    bordercolor1 = null, bordercolor2 = null, bordercolor3 = null, bordercolor4 = null, bordercolor5 = null) {


    var grafiekdata;
    var grafiekopties;
    var aantalDatasets;

    if (data1 !== null && data2 === null && data3 === null && data4 === null && data5 === null) {
        aantalDatasets = 1;
    } else if (data1 !== null && data2 !== null && data3 === null && data4 === null && data5 === null) {
        aantalDatasets = 2;
    } else if (data1 !== null && data2 !== null && data3 !== null && data4 === null && data5 === null) {
        aantalDatasets = 3;
    } else if (data1 !== null && data2 !== null && data3 !== null && data4 !== null && data5 === null) {
        aantalDatasets = 4;
    } else if (data1 !== null && data2 !== null && data3 !== null && data4 !== null && data5 !== null) {
        aantalDatasets = 5;
    }

    switch (aantalDatasets) {
        case 1:
            grafiekdata = {
                labels: labels,
                datasets: [
                    {
                        label: label1,
                        data: data1,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    }
                ]
            }
            break;

        case 2:
            grafiekdata = {
                labels: labels,
                datasets: [
                    {
                        label: label1,
                        data: data1,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                ]
            }
            break;

        case 3:
            grafiekdata = {
                labels: labels,
                datasets: [
                    {
                        label: label1,
                        data: data1,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor3,
                        borderColor: bordercolor3,
                        fill: datasetFill

                    }
                ]
            }
            break;

        case 4:
            grafiekdata = {
                labels: labels,
                datasets: [
                    {
                        label: label1,
                        data: data1,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor3,
                        borderColor: bordercolor3,
                        fill: datasetFill

                    },

                    {
                        label: label4,
                        data: data4,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor4,
                        borderColor: bordercolor4,
                        fill: datasetFill

                    }
                ]
            }
            break;

        case 5:
            grafiekdata = {
                labels: labels,
                datasets: [
                    {
                        label: label1,
                        data: data1,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor3,
                        borderColor: bordercolor3,
                        fill: datasetFill

                    },

                    {
                        label: label4,
                        data: data4,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor4,
                        borderColor: bordercolor4,
                        fill: datasetFill

                    },
                    {
                        label: label5,
                        data: data5,
                        borderWidth: 4,
                        backgroundColor: backgroundcolor5,
                        borderColor: bordercolor5,
                        fill: datasetFill

                    }
                ]
            }
            break;
    }

    grafiekopties = {

        title: {
            display: true,
            text: titel,
            fontSize: 18
        },

        legend: {
            display: toonLegende,
            labels: {
                useLineStyle: lijnweergave
            }
        },

        scales: {
            xAxes: [{
                display: toonXAs,
                ticks: {
                    beginAtZero: xAsNul,
                    maxRotation: xAsMaxRotatie,
                    minRotation: xAsMinRotatie
                },
                scaleLabel: {
                    display: true,
                    labelString: xTitel
                }
            }],

            yAxes: [{
                display: toonYAs,
                ticks: {
                    beginAtZero: yAsNul
                },
                scaleLabel: {
                    display: true,
                    labelString: yTitel
                }
            }]
        }
    };


    var ctx = $("#" + id);


    //var grafiekNieuw = new Chart(ctx, {
    //    options: grafiekopties,
    //    type: grafiektype,
    //    data: grafiekdata
    //});

    new Chart(ctx, {
        options: grafiekopties,
        type: grafiektype,
        data: grafiekdata
    });
}


