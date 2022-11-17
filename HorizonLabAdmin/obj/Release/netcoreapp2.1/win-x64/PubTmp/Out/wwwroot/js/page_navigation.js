$(document).ready(function () {
    var _batchRecCount = 100;

    $("#next_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());
        var batchEndCount = end + _batchRecCount;
        var form = $(this).attr("page_form");

        if (batchEndCount > rec_count) { batchEndCount = rec_count; }
       
        if (end < (rec_count) && start >= 0) {
            $("#record_start").val(start + _batchRecCount);
            $("#record_end").val(batchEndCount);
            $("#" + form).submit();
        }
    });

    $("#back_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#$("#record_start")$("#record_start")$("#record_start")").val());
        var form = $(this).attr("page_form");

        if (start <= 0 && end <= (_batchRecCount - 1)) {
            return;
        }

        $("#record_start").val(start - _batchRecCount);
        //$("#record_end").val(end - _batchRecCount);
        $("#record_end").val(((Math.ceil(end / _batchRecCount) * _batchRecCount) - _batchRecCount)-1);

        start = $("#record_start").val();
        if (start == 0) {
            $("#record_end").val(_batchRecCount);
        }
        $("#" + form).submit();
    });

    $("#last_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());
        var form = $(this).attr("page_form");

        if ((end < (rec_count)) && start >= 0) {
            start = ((Math.ceil(rec_count / _batchRecCount) * _batchRecCount) - _batchRecCount) - 1 //_batchRecCount = 100 / round off to the nearest 100
            end = rec_count;
            $("#record_start").val(start);
            $("#record_end").val(end);
            $("#" + form).submit();
        }
    });

    $("#first_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());
        var form = $(this).attr("page_form");

        $("#record_start").val(0);
        $("#record_end").val(rec_count);
        if (rec_count >= (_batchRecCount)) {
            $("#record_end").val(_batchRecCount - 1);
        }
        $("#" + form).submit();
    });
});
