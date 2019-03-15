

$("#dtDOB").change(function () {
    var dtEntered = $("#dtDOB").val();
    var dt = new Date(dtEntered);
    var ret = dt.getDate() + "/" + dt.getMonth() + "/" + dt.getFullYear();
    $("#dtDOB").val(ret);
});
