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
                        borderWidth: 1,
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
                        borderWidth: 1,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 1,
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
                        borderWidth: 1,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 1,
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
                        borderWidth: 1,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor3,
                        borderColor: bordercolor3,
                        fill: datasetFill

                    },

                    {
                        label: label4,
                        data: data4,
                        borderWidth: 1,
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
                        borderWidth: 1,
                        backgroundColor: backgroundcolor1,
                        borderColor: bordercolor1,
                        fill: datasetFill

                    },

                    {
                        label: label2,
                        data: data2,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor2,
                        borderColor: bordercolor2,
                        fill: datasetFill

                    },
                    {
                        label: label3,
                        data: data3,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor3,
                        borderColor: bordercolor3,
                        fill: datasetFill

                    },

                    {
                        label: label4,
                        data: data4,
                        borderWidth: 1,
                        backgroundColor: backgroundcolor4,
                        borderColor: bordercolor4,
                        fill: datasetFill

                    },
                    {
                        label: label5,
                        data: data5,
                        borderWidth: 1,
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
                    beginAtZero: true,                   
                    autoSkip: false
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




    //var grafiekNieuw = new Chart(ctx, {
    //    options: grafiekopties,
    //    type: grafiektype,
    //    data: grafiekdata
    //});
 //if (window.bar !== undefined) {
    //    window.bar.destroy();
    //window.bar
    //}

    var ctx = $("canvas#" + id);
   

    new Chart(ctx, {
        options: grafiekopties,
        type: grafiektype,
        data: grafiekdata
    });
}

