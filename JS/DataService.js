$(document).ready(function() {
    getRecent();
    getFiles();
    ipCheck();
   
});

function getFiles() {
    //check that this is set correctly.
    var jsonData = JSON.stringify({
        MaxDate: maxDate
    });

    $.ajax({
        type: 'POST',
        url: 'App%20Code/DataService.asmx/getFiles',
        data: jsonData,
        contentType:'application/json; charset=utf-8',
        dataType: 'json',
        success: onGFSuccess,
        error: OnError
    });
}
function ipCheck() {

    $.ajax({
        type: 'POST',
        url: 'App%20Code/DataService.asmx/checkIPs',
        data: {},
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {

            var jdata = JSON.parse(response.d);
            console.log(jdata);
             //var array = response.split("-");

            //console.log(array);
            $('#modemSpinner').hide();
            $('#sensorSpinner').hide();
            $('#wakeupSpinner').hide();

          
            var statusText = 'Online';
            var statusCSS = 'badge badge-pill badge-success';
            var failText = 'Offline';
            var failCSS = 'badge badge-pill badge-danger';
            //Ports: 2332, 3389, 2322
            if (jdata[0].connStatus == true) {
                $("#lbl_modemStatus").text(statusText).addClass(statusCSS);
            }
            else if (jdata[0].connStatus == false) {
                $("#lbl_modemStatus").text(failText).addClass(failCSS);
            }
            if (jdata[1].connStatus == true) {
                $("#lbl_sensorStatus").text(statusText).addClass(statusCSS);
            }
            else if (jdata[1].connStatus == false) {
                $("#lbl_sensorStatus").text(failText).addClass(failCSS);
            }
            if (jdata[2].connStatus == true) {
                $("#lbl_ftpPort").text(statusText).addClass(statusCSS);
            }
            else if (jdata[2].connStatus == false) {
                $("#lbl_ftpPort").text(failText).addClass(failCSS);
            }
       
        },
        error: OnError
    });
}
function onGFSuccess(data) {
    var jdata = JSON.parse(data.d);
    console.log(jdata);
    var monthArr = [" ", "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"];

    $('#elevLastDB').text(jdata.elevation);
    $('#downLastDB').text(jdata.downstream);
    $('#upLastDB').text(jdata.upstream);
    $('#dateLastDB').text(jdata.maxDate);
    $('#fileLastSync').text('C:\\SouthForkData\\Data\\Elevation' + '\\' + jdata.year + '\\' + monthArr[jdata.measMonth] + '\\data.csv');
}

function getRecent() {

    $.ajax({
        type: 'POST',
        url: 'App%20Code/DataService.asmx/getRecent',
        data: {},
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: onGRSuccess,
        error: OnError
    });
}
function onGRSuccess(data) {
    console.log('this is the getRecent Data');
    console.log(data);
    //DataOrder: [data time am/pm]
    var dataStr = data.d;
    dataArr = dataStr.split(" ");
    for (d in dataArr) {
        console.log(dataArr[d]);
    }

    $('#dateLastSync').text(dataArr[0]);
    $('#timeLastSync').text(dataArr[1] + dataArr[2]);

}

function OnError(response) {
    console.log(response);
}
var maxDate;
var minDate;
