//function grafiekVerwijderen(event) {
//    var grafiekVerwijderen = confirm("Ben je zeker dat je de grafiek wilt verwijderen?");
//    if (grafiekVerwijderen === true) {
//        event.data.teVerwijderenGrafiek.destroy();
//    }
//}

//function grafiekVerwijderen(teVerwijderenGrafiek) {
//    var grafiekVerwijderen = confirm("Ben je zeker dat je de grafiek wilt verwijderen?");
//    if (grafiekVerwijderen === true) {
//        teVerwijderenGrafiek.destroy();
//    }
//}


function grafiekWijzigen(teWijzigenGrafiek) {
    var xAs = $("#inputXas").val();
    var titel = $("#inputTitel").val();

    if (xAs === "data1") {
        teWijzigenGrafiek.data.labels = ["Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag"];
        teWijzigenGrafiek.data.datasets[0].data = [1, 2, 3, 4, 5, 6, 7];
        //myHorizontalBarChart.data.datasets[0].data[0] = 90;
    } else if (xAs === "data2") {
        teWijzigenGrafiek.data.labels = ["Week 1", "Week 2", "Week3", "Week4"];
        teWijzigenGrafiek.data.datasets[0].data = [5, 15, 25, 35];
    } else if (xAs === "data3") {
        //teWijzigenGrafiek.data.labels = ["test1", "test2"];
        //teWijzigenGrafiek.data.datasets[0].data = [50, 50];
        teWijzigenGrafiek.data.labels = ["test1"];
        teWijzigenGrafiek.data.datasets[0].data = [50];
    }

    teWijzigenGrafiek.options.title.text = titel;
    teWijzigenGrafiek.update();
}



function GrafiekOpbouwen1Dataset(id, labels, label1, data1, backgroundcolor1, bordercolor1, titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, xAsMaxRotatie = 90, xAsMinRotatie = 0, toonXAs = true, toonYAs = true) {
    var ctx = $("#" + id);
    //var tData = $.getValues("/Grafiek/" + data);
    var myChart = new Chart(ctx, {

        options: {

            title: {
                display: true,
                text: titel,
                fontSize: 18
            },

            legend: {
                display: toonLegende
            },

            scales: {
                xAxes: [{
                    display: toonXAs,
                    ticks: {
                        beginAtZero: xAsNul,
                        maxRotation: xAsMaxRotatie,
                        minRotation: xAsMinRotatie
                    }
                }],

                yAxes: [{
                    display: toonYAs,
                    ticks: {
                        beginAtZero: yAsNul
                    }
                }]
            }
        },
        type: grafiektype,
        //data: tData

        data: {
            labels: labels,
            datasets: [
                {
                    label: label1,
                    data: data1,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor1,
                    borderColor: bordercolor1
                    //backgroundColor: ["#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60"],
                    //borderColor: ["#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60"]
                }
            ]
        },

    }

    );
}


function GrafiekOpbouwen2Datasets(id, labels,
    label1, data1, backgroundcolor1, bordercolor1,
    label2, data2, backgroundcolor2, bordercolor2,
    titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, xAsMaxRotatie = 90, xAsMinRotatie = 0, toonXAs = true, toonYAs = true) {
    var ctx = $("#" + id);
    //var tData = $.getValues("/Grafiek/" + data);
    var myChart = new Chart(ctx, {

        options: {

            title: {
                display: true,
                text: titel,
                fontSize: 18
            },

            legend: {
                display: toonLegende
            },

            scales: {
                xAxes: [{
                    display: toonXAs,
                    ticks: {
                        beginAtZero: xAsNul,
                        maxRotation: xAsMaxRotatie,
                        minRotation: xAsMinRotatie
                    }
                }],

                yAxes: [{
                    display: toonYAs,
                    ticks: {
                        beginAtZero: yAsNul
                    }
                }]
            }
        },
        type: grafiektype,
        //data: tData

        data: {
            labels: labels,
            datasets: [
                {
                    label: label1,
                    data: data1,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor1,
                    borderColor: bordercolor1
                },

                {
                    label: label2,
                    data: data2,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor2,
                    borderColor: bordercolor2
                }
            ]
        },

    }

    );

}


function GrafiekOpbouwen3Datasets(id, labels,
    label1, data1, backgroundcolor1, bordercolor1,
    label2, data2, backgroundcolor2, bordercolor2,
    label3, data3, backgroundcolor3, bordercolor3,
    titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, xAsMaxRotatie = 90, xAsMinRotatie = 0, toonXAs = true, toonYAs = true) {
    var ctx = $("#" + id);
    //var tData = $.getValues("/Grafiek/" + data);
    var myChart = new Chart(ctx, {

        options: {

            title: {
                display: true,
                text: titel,
                fontSize: 18
            },

            legend: {
                display: toonLegende
            },

            scales: {
                xAxes: [{
                    display: toonXAs,
                    ticks: {
                        beginAtZero: xAsNul,
                        maxRotation: xAsMaxRotatie,
                        minRotation: xAsMinRotatie
                    }
                }],

                yAxes: [{
                    display: toonYAs,
                    ticks: {
                        beginAtZero: yAsNul
                    }
                }]
            }
        },
        type: grafiektype,
        //data: tData

        data: {
            labels: labels,
            datasets: [
                {
                    label: label1,
                    data: data1,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor1,
                    borderColor: bordercolor1
                },

                {
                    label: label2,
                    data: data2,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor2,
                    borderColor: bordercolor2
                },

                {
                    label: label3,
                    data: data3,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor3,
                    borderColor: bordercolor3
                }
            ]
        },

    }

    );

}

function GrafiekOpbouwen4Datasets(id, labels,
    label1, data1, backgroundcolor1, bordercolor1,
    label2, data2, backgroundcolor2, bordercolor2,
    label3, data3, backgroundcolor3, bordercolor3,
    label4, data4, backgroundcolor4, bordercolor4,
    titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, xAsMaxRotatie = 90, xAsMinRotatie = 0, toonXAs = true, toonYAs = true) {
    var ctx = $("#" + id);
    //var tData = $.getValues("/Grafiek/" + data);
    var myChart = new Chart(ctx, {

        options: {

            title: {
                display: true,
                text: titel,
                fontSize: 18
            },

            legend: {
                display: toonLegende
            },

            scales: {
                xAxes: [{
                    display: toonXAs,
                    ticks: {
                        beginAtZero: xAsNul,
                        maxRotation: xAsMaxRotatie,
                        minRotation: xAsMinRotatie
                    }
                }],

                yAxes: [{
                    display: toonYAs,
                    ticks: {
                        beginAtZero: yAsNul
                    }
                }]
            }
        },
        type: grafiektype,
        //data: tData

        data: {
            labels: labels,
            datasets: [
                {
                    label: label1,
                    data: data1,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor1,
                    borderColor: bordercolor1
                },

                {
                    label: label2,
                    data: data2,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor2,
                    borderColor: bordercolor2
                },

                {
                    label: label3,
                    data: data3,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor3,
                    borderColor: bordercolor3
                },

                {
                    label: label4,
                    data: data4,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor4,
                    borderColor: bordercolor4
                }
            ]
        },

    }

    );

}



function GrafiekOpbouwen5Datasets(id, labels,
    label1, data1, backgroundcolor1, bordercolor1,
    label2, data2, backgroundcolor2, bordercolor2,
    label3, data3, backgroundcolor3, bordercolor3,
    label4, data4, backgroundcolor4, bordercolor4,
    label5, data5, backgroundcolor5, bordercolor5,
    titel, grafiektype, toonLegende = true, xAsNul = true, yAsNul = true, xAsMaxRotatie = 90, xAsMinRotatie = 0, toonXAs = true, toonYAs = true) {
    var ctx = $("#" + id);
    //var tData = $.getValues("/Grafiek/" + data);
    var myChart = new Chart(ctx, {

        options: {

            title: {
                display: true,
                text: titel,
                fontSize: 18
            },

            legend: {
                display: toonLegende
            },

            scales: {
                xAxes: [{
                    display: toonXAs,
                    ticks: {
                        beginAtZero: xAsNul,
                        maxRotation: xAsMaxRotatie,
                        minRotation: xAsMinRotatie
                    }
                }],

                yAxes: [{
                    display: toonYAs,
                    ticks: {
                        beginAtZero: yAsNul
                    }
                }]
            }
        },
        type: grafiektype,
        //data: tData

        data: {
            labels: labels,
            datasets: [
                {
                    label: label1,
                    data: data1,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor1,
                    borderColor: bordercolor1
                },

                {
                    label: label2,
                    data: data2,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor2,
                    borderColor: bordercolor2
                },

                {
                    label: label3,
                    data: data3,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor3,
                    borderColor: bordercolor3
                },

                {
                    label: label4,
                    data: data4,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor4,
                    borderColor: bordercolor4
                },

                {
                    label: label5,
                    data: data5,
                    borderWidth: 4,
                    backgroundColor: backgroundcolor5,
                    borderColor: bordercolor5
                }
            ]
        },

    }

    );

}




