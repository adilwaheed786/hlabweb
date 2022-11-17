$(function () {
    $("#searchSubmtDateStart").datepicker();
    $("#searchSubmtDateEnd").datepicker();
    $("#searchRcvdDateStart").datepicker();
    $("#searchRcvdDateEnd").datepicker();
    $("#searchTestDateStart").datepicker();
    $("#searchTestDateEnd").datepicker();

    var _batchRecCount = 100;
    $("#next_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());
        if (end < (rec_count) && start >= 0) {
            $("#record_start").val(start + _batchRecCount);
            $("#record_end").val(end + _batchRecCount);
            $("#test_transaction_nav").submit();
        }
    });

    $("#back_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());

        if (start > 0 && end > (_batchRecCount - 1)) {
            $("#record_start").val(start - _batchRecCount);
            $("#record_end").val(end - _batchRecCount);
            $("#test_transaction_nav").submit();
        }
    });

    $("#last_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());

        if ((end < (rec_count)) && start >= 0) {
            start = ((Math.ceil(rec_count / _batchRecCount) * _batchRecCount) - _batchRecCount) - 1 //_batchRecCount = 100 / round off to the nearest 100
            end = rec_count - 1;
            $("#record_start").val(start);
            $("#record_end").val(end);
            $("#test_transaction_nav").submit();
        }
    });

    $("#first_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());

        $("#record_start").val(0);
        if (rec_count <= (_batchRecCount)) {
            $("#record_end").val(rec_count - 1);
        }
        else {
            $("#record_end").val(_batchRecCount - 1);
        }
        $("#test_transaction_nav").submit();
    });

    $("#submit_search_btn").click(function () {

    });

    $(".number_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
    });

    $(".date_only").keyup(function (e) {
        var timestamp = Date.parse($(this).val()) 
        if (isNaN(timestamp) == false) {
            alert("Invalid Date Format!");
            $(this).val("");
        } 
    });
});