$(document).ready(function () {
 
    
});

//dont use all years. Too much data
function AllYearsData() {

    $.ajax({
        type: 'POST',
        url: 'App%20Code/DataService.asmx/getAllYears',
        data: {},
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: onAYDSuccess,
        error: OnError
    });
}

function onAYDSuccess(data) {
    console.log(data.d);

}

function execCodeBehind(id) {
    console.log(id);
    var hv = document.getElementById("spanID").value = id;
    //hv.val(id);
    var lb = document.getElementById("linkbutton");
    lb.textContent = id;
    lb.click();
}
function OnError(err) {
    console.log(JSON.stringify(err));
}
var submit = 0;
function CheckDouble() {
    if (++submit > 1) {
        alert('Loading...');
        return false;
    }
}

