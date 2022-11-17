$(function () {
    $('#input_collectdate').datetextentry({
        field_order: 'DMY',
        year_id: 'collectdate_year',
        on_change: function (datestr) {
            //$('#input_submitdate').datetextentry('set_date', datestr);
            //$('#input_receiveddate').datetextentry('set_date', datestr);
            //$('#input_testdate').datetextentry('set_date', datestr);
        }
    });

    $('#input_submitdate').datetextentry({
        field_order: 'DMY',
        year_id: 'submitdate_year',
        on_change: function (datestr) {
            //$('#input_receiveddate').datetextentry('set_date', datestr);
            //$('#input_testdate').datetextentry('set_date', datestr);
        }
    });

    $('#input_receiveddate').datetextentry({
        field_order: 'DMY',
        year_id: 'receiveddate_year',
        on_change: function (datestr) {
            $('#input_testdate').datetextentry('set_date', datestr);
        }
    });

    $('#input_testdate').datetextentry({
        field_order: 'DMY',
        year_id: 'testdate_year',
        on_blur: function () {

        }
    });
    $('#input_reportdate').datetextentry({ field_order: 'DMY', year_id: 'reportdate_year' });
    $('#input_datereceived').datetextentry({ field_order: 'DMY' });
    $('#input_sampleddate').datetextentry({ field_order: 'DMY' });

    $('#input_collecttime').timetator({ useCap: true });
    $('#input_submittime').timetator({ useCap: true });
    $('#input_receivedtime').timetator({ useCap: true });
    $('#input_sampledtime').timetator({ useCap: true });
    $('#input_timereceived').timetator({ useCap: true });

    $(".jq-dte-year").focusout(function () {
        if ($(this).val() == "") {
            var year = new Date().getFullYear();
            $(this).val(year);
        }
    });

    $("#form_sampleid").focusout(function () {
        var isDisabled = $('#form_sampletype').prop('disabled');
        if (isDisabled) {
            $('#input_collectdate').datetextentry('focus');
        }
    });

    $("#form_municipality").focusout(function () {
        $('#input_collectdate').datetextentry('focus');
    });

    $("#input_collecttime").focusout(function () {
        $('#form_submittedby').focus();
    });

    $("#input_submittime").focusout(function () {
        $('#form_receivedby').focus();
    });

    $("#form_reporttype").focusout(function () {
        $('#input_reportdate').datetextentry('focus');
    });

    $("#collectdate_year").focusout(function () {
        $('#input_collecttime').focus();
    });

    $("#submitdate_year").focusout(function () {
        $('#input_submittime').focus();
    });

    $("#receiveddate_year").focusout(function () {
        $('#input_receivedtime').focus();
    });

    $("#testdate_year").focusout(function () {
        //$('#form_temperature').focus();
    });

    $("#form_receivedby").focusout(function () {
        $('#input_receiveddate').datetextentry('focus');
    });

    $("#input_receivedtime").focusout(function () {
        //$('#input_testdate').datetextentry('focus');
        $('#form_temperature').focus();
    });

    $("#form_reporttype").focusout(function () {
        $('#database_test_pkg_id').focus();
    });

    $("#form_submittedby").focusout(function () {
        $('#input_submitdate').datetextentry('focus');
    });

    $("#reportdate_year").focusout(function () {
        $('#form_reporttype').focus();
    });

    $("#database_project_id").focusout(function () {
        $('#form_sampleid').focus();
    });
});