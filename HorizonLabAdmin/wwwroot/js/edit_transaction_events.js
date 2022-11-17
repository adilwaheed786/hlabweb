$(function () {
    //$("#transReceiveDate").datepicker();
    //$("#transReportDate").datepicker();
    //$("#transTestDate").datepicker();
    //$("#editAnalysisDate").datepicker();
   //$("#add_analysisdate").datepicker();
   //$(".editAnalysisDate").datepicker();

    $(".editresultbtn").click(function () {
        var result_id = $(this).val();
        var paramid = $("#paramid_" + result_id).val();
        var unitid = $("#unit_" + result_id).val();
        var note = $("#note_" + result_id).text();
        var result = $("#result_" + result_id).text();
        var passfail = $("#passfail_" + result_id).html();
        var transactionid = $("#transactionid").val();
        
        $("#editResultId").val(result_id);
        $("#editNote").val(note);
        $("#editResult").val(result);
        $("#editParam").val(paramid);
        $("#editUnit").val(unitid);
        $("#editTransId").val(transactionid);
        $("#editPassFail").val(passfail.toLowerCase());     
    });

    $(".openeditform").click(function () {
        $('#edittestresult').show();
    });

    $(".closeditform").click(function () {
        $('#edittestresult').hide();
    });

    $("#save_test_details_btn").click(function () {        
        SaveTransactionChanges();
    });

    $("#save_new_test").click(function () {
        prepare_date_time_fields();
        OpenModalDialogBox("processing_modaldialogbox");
        $("#water_test_form").submit();
    });

    $("#save_watersample_to_all").click(function () {
        prepare_date_time_fields();
        OpenModalDialogBox("processing_modaldialogbox");
        $('#water_test_form').attr('action', '/WaterBacteriaPostTransactions/SaveToAllWaterSample');
        $("#water_test_form").submit();
    });    

    $("#save_test_result_btn").click(function () {
        var customerid = $("#customer_select").val();
        var test_pkg_id = $("#test_package_id").val();
        $("#edit_transaction").submit();
    });

    $("#add_test_result_btn").click(function () {
        $("#add_test_result_form").submit();
    });

    $(".delete_result").click(function () {
        var value = $(this).val();
        var details = value.split('_');
        window.location = "/WaterBacteriaPostTransactions/DeleteTestResult?r=" + details[0] + "&t=" + details[1];
    });   

    $("#payment_option").change(function () {
        $("#transaction_type").val($(this).val());
    });

    $("#transaction_type").change(function () {
        $("#payment_option").val($(this).val());
    });

    $("#addUnit").click(function () {
        var unit = prompt("Please enter new unit of measurement:", "");
        var unit_id = 0;
        if (unit != null) {
            //do something here
            $.ajax({
                url: "/hlab_test_transaction/addnewunitofmeasurement?new_unit=" + unit,
                method: "GET",
                success: function (data) {
                    console.log("new unit id: " + data);
                    location.reload();
                }
            });
        }
    });

    $("#deleted_attachment").click(function () {
        var id = $(this).val();
        var ans = confirm("Are you sure you want to remove attachment?");        
        if (ans) {
            $.ajax({
                url: "/hlab_test_transaction/deletesubsidyformimage?id=" + id,
                method: "GET",
                success: function (data) {
                    $("#attachment").html("");
                    $("#scan_image_hidden_value").val("");
                }
            });
        }
    });

    $(".result_value").focusout(function () {
        result_id = event.target.id.split("_");
        result = $(this).val();
        if (result == "" || result == "0" || result.toLowerCase() == "absent") {            
            $("#isFail_" + result_id[1]).val("true").change;            
        }
        else {
            $("#isFail_" + result_id[1]).val("false").change;
        }
    });

    //START TABBING
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

    $("#form_receivedby").focusout(function () {
        $('#input_receiveddate').datetextentry('focus');
    });

    $("#form_temperature").focusout(function () {
        $('#input_testdate').datetextentry('focus');
    });

    $("#testdate_year").focusout(function () {
        //$('#input_reportdate').datetextentry('focus');
    }); 

    $("#form_reporttype").focusout(function () {
        //$("#database_test_pkg_id").focus() 
    });

    $("#form_submittedby").focusout(function () {
        $('#input_submitdate').datetextentry('focus');
    });
    //END TABBING

    function forced_generate_coupon(customerid = 0, transactionid=0) {
        $.ajax({
            url: "/hlab_customer_api/forced_generatecoupon?customerid=" + customerid + "&transid=" + transactionid,
            method: "GET",
            success: function (data) {
                console.log("result: " + data);
                if ($.isNumeric(data)) {
                    $("#generated_coupon").val(data);
                    $("#generate_coupon").css({ "display": "none" });
                    $("#delete_coupon").css({ "display": "block" });;
                }
                else {
                    alert(data);
                }
            }
        });
    }


    $("#generate_coupon").click(function () {
        var customerid = $("#customer_select").val();
        var transactionid = $("#transactionid").val();
        
        if (customerid != "" && transactionid!="") {
            //do something here
            $.ajax({
                url: "/hlab_customer_api/generatecoupon?customerid=" + customerid + "&transid=" + transactionid,
                method: "GET",
                success: function (data) {
                    console.log("result: " + data);
                    if ($.isNumeric(data)) {
                        $("#generated_coupon").val(data);
                        $("#generate_coupon").css({ "display": "none" });
                        $("#delete_coupon").css({ "display": "block" });;
                    }
                    else {
                        var ans = confirm(data);
                        if (ans) {
                            forced_generate_coupon(customerid, transactionid);
                        }
                    }                   
                }
            });
        }
        else {
            alert("Missing parameters (customer id or transaction id)!");
        }
    });

    $("#delete_coupon").click(function () {
        var customerid = $("#customer_select").val();
        var generated_coupon = $("#generated_coupon").val();

        if (customerid != "" && generated_coupon != "") {
            //do something here
            $.ajax({
                url: "/hlab_customer_api/deletecoupon?customerid=" + customerid + "&coupon=" + generated_coupon,
                method: "GET",
                success: function (data) {
                    console.log("result: " + data);
                    if ($.isNumeric(data)) {
                        $("#generated_coupon").val("0");
                        $("#generate_coupon").css({ "display": "block" });
                        $("#delete_coupon").css({ "display": "none" });
                    }
                    else {
                        alert(data);
                    }
                }
            });
        }
        else {
            alert("Missing parameters (customer id or coupon)!");
        }
    });
});

function prepare_date_time_fields() {
    var input_collectdate = $("#input_collectdate").val();
    var input_submitdate = $("#input_submitdate").val();
    var input_receiveddate = $("#input_receiveddate").val();
    var input_testdate = $("#input_testdate").val();
    var input_reportdate = $("#input_reportdate").val();

    var input_collecttime = $("#input_collecttime").val();
    var input_submittime = $("#input_submittime").val();
    var input_receivedtime = $("#input_receivedtime").val();

    if (input_collectdate != "") {
        if (input_collecttime == "") { input_collecttime = "00:00"; }
        $("#collectdate").val(input_collectdate + " " + input_collecttime + ":00.000");
    }

    if (input_submitdate != "") {
        if (input_submittime == "") { input_submittime = "00:00"; }
        $("#submitdate").val(input_submitdate + " " + input_submittime + ":00.000");
    }

    if (input_receiveddate != "") {
        if (input_receivedtime == "") { input_receivedtime = "00:00"; }
        $("#receivedate").val(input_receiveddate + " " + input_receivedtime + ":00.000");
    }

    if (input_testdate != "") {
        $("#testdate").val(input_testdate + " " + "00:00:00.000");
    }

    if (input_reportdate != "") {
        $("#reportdate").val(input_reportdate + " " + "00:00:00.000");
    }
}

function SaveTransactionChanges() {
    var resultid = "";
    var param_id = "";
    var test_note = "";
    var result = "";
    var unit_id = "";
    var is_failed = "";
    var transactionid = $("#transactionid").val();

    $('.result_ids').each(function (index, value) {
        resultid = $(this).val();
        param_id = $("#paramid_" + resultid).val();
        test_note = $("#testnote_" + resultid).val();
        result = $("#result_" + resultid).val();
        unit_id = $("#unit_id_" + resultid).val();
        is_failed = $("#isFail_" + resultid).val();

        var js_hlab_test_results = JSON.stringify({
            "result_id": parseInt(resultid),
            "param_id": parseInt(param_id),
            "result": result,
            "unit_id": parseInt(unit_id),
            "test_note": test_note,
            "is_failed": is_failed,
            "trans_id": parseInt(transactionid)
        });
        console.log(js_hlab_test_results);

        $.ajax({
            type: "post",
            url: "/hlab_test_transaction/savetestresultchanges",
            data: js_hlab_test_results,
            xhrFields: {
                withCredentials: true
            },
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data, text) {
                console.log(resultid + " - post requested. ");
            },
            error: function (request, status, error) {
                console.log("One of more test result failed to save changes: " + request.status + " | " + request.statusText);
            }
        });
    });

    OpenModalDialogBox("processing_modaldialogbox");
    prepare_date_time_fields();
    $("#edit_transaction_info").submit();
}