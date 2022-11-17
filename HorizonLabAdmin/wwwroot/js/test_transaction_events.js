$(function () {
    $("#searchSubmtDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchSubmtDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchRcvdDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchRcvdDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchTestDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchTestDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchRequestDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchRequstDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchProjectDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#searchProjectDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });
    var _batchRecCount = 100;
    $("#next_btn").click(function () {
        var start = parseInt($("#record_start").val());
        var end = parseInt($("#record_end").val());
        var rec_count = parseInt($("#rec_count").val());
        var batchEndCount = end + _batchRecCount;
        if (batchEndCount > rec_count) { batchEndCount = rec_count; }

        if (end < (rec_count) && start >= 0) {
            $("#record_start").val(start + _batchRecCount);
            $("#record_end").val(batchEndCount);
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
            end = rec_count;
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
        $("#record_end").val(rec_count);
        if (rec_count >= (_batchRecCount)) {            
            $("#record_end").val(_batchRecCount - 1);
        }
        $("#test_transaction_nav").submit();
    });

    $("#submit_search_btn").click(function () {

    });

    $(".deactivate_test_record").click(function () {     
        var ans = confirm("Are you sure you want to deactivate this record?");
        if (ans) {
            value = $(this).val();
            window.location = "/WaterBacteriaPostTransactions/DeactivateTestTransactionRecord?transid=" + value;
        }
    });

    $("#checkbox_controller").click(function () {
        $('.cert_checkbox').not(this).prop('checked', this.checked);
    });

    $("#multi_report").click(function () {
        //alert("Submit");
        $("#multi_report_form").attr("action", "/Certificate/MultipleB1nsReport").submit();
    });

    $("#download_multi_report").click(function () {
        //$('input:checkbox.cert_checkbox').each(function () {
            //var transid = $(this).val();
            //if (this.checked) {
                //window.open("/Certificate/DownloadB1NSPdfReport?transid=" + $(this).val(),"_blank");
                //window.location = "/Certificate/DownloadB1NSPdfReport?transid=" + $(this).val();
                //$("#download_cert_" + transid).submit();
            //}                       
        //});        
        $("#multi_report_form").attr('target', '_blank');
        $("#multi_report_form").attr("action", "/Certificate/DownloadMultiple1NSPdfReport").submit();
    });

    $("#email_documents").click(function () {
        $("#multi_report_form").attr('target', '_self');
        $("#multi_report_form").attr("action", "/Certificate/SendEmailDocuments").submit();     
    });

    $("#sort_customer").click(function () {        
        var sortByOption = $("#sortByOption").val();
        $("#sortByString").val("first_name");
        if (sortByOption == "asc") {
            $("#sortByOption").val("desc");
        }
        else {
            $("#sortByOption").val("asc");
        }
        $("#test_transaction_search").submit();
    });

    $("#sort_submitteddate").click(function () {
        var sortByOption = $("#sortByOption").val();
        $("#sortByString").val("submtd_datetime");
        if (sortByOption == "asc") {
            $("#sortByOption").val("desc");
        }
        else {
            $("#sortByOption").val("asc");
        }
        $("#test_transaction_search").submit();
    });

    $("#sort_receivedate").click(function () {
        var sortByOption = $("#sortByOption").val();
        $("#sortByString").val("rcv_date");
        if (sortByOption == "asc") {
            $("#sortByOption").val("desc");
        }
        else {
            $("#sortByOption").val("asc");
        }
        $("#test_transaction_search").submit();
    });

    $("#sort_testdate").click(function () {
        var sortByOption = $("#sortByOption").val();
        $("#sortByString").val("test_date");
        if (sortByOption == "asc") {
            $("#sortByOption").val("desc");
        }
        else {
            $("#sortByOption").val("asc");
        }
        $("#test_transaction_search").submit();
    });

    $('#template_controller').change(function () {
        $('.emailtemplate').val($(this).val());
    });

    $(".edit_transaction").click(function () {
        $(".edit_transaction").each(function () {
            $(this).css("background-color","#EDEFEF");
        });
        $(this).css("background-color", "#BDFFD2");
        OpenModalDialogBox("loading_modaldialogbox");
        var framelocation = $(this).val();
        $("#iframe_transaction_page").attr("src", framelocation);
        $("#save_sample_changes").css("display","block");
    });

    $('#save_sample_changes').click(function () {
        //var child_page = document.getElementById("iframe_transaction_page");
        //var iframe = (child_page.contentWindow || child_page.contentDocument);
        //if (iframe.document) iframe = iframe.document;
        //iframe.getElementById("edit_transaction_info").submit();
        OpenModalDialogBox("loading_modaldialogbox");
        var iframe = $('#iframe_transaction_page');        
        var button = iframe.contents().find('#save_test_details_btn');     
        button.trigger("click");

        setTimeout(function () {
            location.reload()
        }, 4000);
    });

});