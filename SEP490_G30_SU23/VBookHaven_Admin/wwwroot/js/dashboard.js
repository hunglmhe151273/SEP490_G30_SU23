function defaultChartInYear(year) {
    let api = `https://localhost:7123/Admin/Home/Chart/?year=${year}`;
    callApiChartWithYear(api);
}
var chart;

function showYearChart(Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec) {
    // chart 1
    var options = {
        series: [{
            name: 'Income',
            data: [Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec]
        }],
        chart: {
            foreColor: '#9ba7b2',
            type: 'area',
            height: 340,
            toolbar: {
                show: false
            },
            zoom: {
                enabled: false
            },
            dropShadow: {
                enabled: false,
                top: 3,
                left: 14,
                blur: 4,
                opacity: 0.10,
            }
        },
        legend: {
            position: 'top',
            horizontalAlign: 'left',
            offsetX: -25
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            show: true,
            width: 3,
            curve: 'smooth'
        },
        tooltip: {
            theme: 'dark',
            y: {
                formatter: function(val) {
                    return "" + val + " VND"
                }
            }
        },
        fill: {
            type: 'gradient',
            gradient: {
                shade: 'light',
                gradientToColors: ['#377dff', '#00c9db'],
                shadeIntensity: 1,
                type: 'vertical',
                inverseColors: false,
                opacityFrom: 0.4,
                opacityTo: 0.1,
                //stops: [0, 50, 65, 91]
            },
        },
        grid: {
            show: true,
            borderColor: '#f8f8f8',
            strokeDashArray: 5,
        },
        colors: ["#377dff"],
        yaxis: {
            labels: {
                formatter: function(value) {
                    return value + " VND";
                }
            },
        },
        xaxis: {
            categories: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
        }
    };
    var chart = new ApexCharts(document.querySelector("#chart1"), options);
    chart.render();
}

$("#Year").change(() => {
    //set paramenter to api url
    console.log("set paramenter:" + $("#Year").val());
    callApiChartWithYear("https://localhost:7123/Admin/Home/Chart/?year=" + $("#Year").val());
});

function callApiChartWithYear(api) {
    $.ajax({
        url: api,
        method: "GET",
        dataType: "json",
        success: function(data) {
            //Gọi hàm chart cùng tham số chuyền vào
            clearChart();
            showYearChart(data.jan, data.feb, data.mar, data.apr, data.may, data.jun, data.jul, data.aug, data.sep, data.oct, data.nov, data.dec);
            console.log(data);
        },
        error: function(error) {
            console.error('Error:', error);
        }
    });
}

function clearChart() {
    // if (chart) {
    //     chart.destroy();
    // }
    document.querySelector("#chart1").innerHTML = '';
}

//-------------------------------Daily tracking report-----------------------------
function defaultDailyReport(selectValue) {
    //to fix
    let api = `https://localhost:7123/Admin/Home/DailyReport`;
    callApiDailyReport(api, selectValue);
}

$("#DailyReport").change(() => {
    //set paramenter to api url
    console.log("Set paramenter dayly report:" + $("#DailyReport").val());
    callApiDailyReport("https://localhost:7123/Admin/Home/DailyReport", $("#DailyReport").val());
});


function callApiDailyReport(api, selectedValue) {
    $.ajax({
        url: api,
        method: "POST",
        data: { selectValue: selectedValue },
        dataType: "json",
        success: function(data) {
            //Gọi hàm set value
            SetDailyReportValue(data.revenue, data.newOrder, data.doneOrder, data.cancelledOrder);
            console.log("DailyReport revenue" + data.revenue);
        },
        error: function(error) {
            console.error('Error:', error);
        }
    });
}


function SetDailyReportValue(revenue, newOrder, doneOrder, cancelledOrder) {
    $("#revenue").text(revenue + "VND");
    $("#newOrder").text(newOrder);
    $("#doneOrder").text(doneOrder);
    $("#cancelledOrder").text(cancelledOrder);
}