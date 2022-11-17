$(function () {
    $("#searchOrderDateEnd").datepicker({ dateFormat: 'dd/mm/yy' });    
    $("#searchOrderDateStart").datepicker({ dateFormat: 'dd/mm/yy' });
    $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
    $('.time_formatter').timetator({ useCap: true });

    $('[data-toggle="tooltip"]').tooltip();
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 2
    })

    $("#pkg_class_select").click(function (e) {
        var class_id = $(this).val();
        $("#form_classid").val(class_id);
        $("#form_pkgid").val("");
        var url = "/hlab_order_api/gettestpackages?classid=" + class_id;
        GetApiData(url, LoadItemstoPackageSelectList);
    });

    $("#pkg_select").click(function (e) {
        var pkgid = $(this).val();
        var class_id = $("#form_classid").val();
        $("#form_pkgid").val(pkgid);
        url = "/hlab_order_api/getpackagedetails?classid=" + class_id + "&pkgid=" + pkgid;
        GetApiData(url, LoadTestPackageInfo);
    });

    $("#category_select").click(function (e) {
        var categoryid = $(this).val();
        if (categoryid != "") {
            var url = "/hlab_order_api/gettestpackagesbycategory?categoryid=" + categoryid;
            $("#form_pkgid").val("");
            GetApiData(url, AppendPackageSelectList);
        }

    });

    $("#place_order_btn").click(function () {    
        var pkgid = $("#form_pkgid").val();   
        if (pkgid != "") {
            GetApiData("/hlab_order_api/getpackagedetails?pkgid=" + pkgid, PopulateHTMLRequestTable);
        }
        else {
            alert("Please select a package!");
        }
    });

    $("#submit_order").click(function () {        
        $("#save_proceed").val("false");
        submit_order_form();
    });

    $("#submit_order_proceed").click(function () {
        $("#save_proceed").val("true");
        submit_order_form();
    });

    $("#compute_total").click(function () {
        computetotal();
    });

    $("#input_tax").keyup(function (e) {
        //computetotal();
    });

    $("#customer_select").click(function (e) {
        var customer_id = $(this).val();
        $("#form_customerid").val(customer_id);
        if (customer_id != "") {
            var url = "/hlab_customer_api/getcustomerinfo?customerid=" + customer_id;
            GetApiData(url, PopulateGSTfield);
        }
    });

    $("#search_customer").click(function () {
        var searchfilter = $("#form_searchfilter").val();
        var searchvalue = $("#form_searchvalue").val();
        searchcustomer(searchfilter, searchvalue);
    });

    $(document).on('keypress', function (e) {
        if (e.which == 13) {
            var searchfilter = $("#form_searchfilter").val();
            var searchvalue = $("#form_searchvalue").val();
            searchcustomer(searchfilter, searchvalue);
        }
    });
    
    $(".view_order").click(function () {
        var total = 0;
        var gst = 0;
        var order_id = $(this).val();
        var placeorderbtn = "";

        if (order_id != "") {
            url = "/hlab_order_api/getorderitemdetails?orderid=" + order_id;
            GetApiData(url, LoadDataToItemTable);
        }
        else {
            alert("No Order ID was found!");
        }
    });

    $("#checkbox_control").click(function () {
        if ($(this).is(':checked'))
            $(".close_order").prop("checked", true);
        else
            $(".close_order").prop("checked", false);      
    });

    $("#colse_order_btn").click(function () {    
        var checked_count = $('.close_order:checkbox:checked').length;
        if (checked_count == 0) {
            alert("Please select some orders to close!");
        }
        else {
            $("#order_list_form").submit();         
        }
    });    

    $(".remove_order_btn").click(function () {
        var orderid = $(this).val();
        window.location = "/OrderPostTransactions/DeleteOrder?orderid=" + orderid;
    });

    $(".clear_payment_form").click(function () {

    });

    $("#payment_mode").change(function () {
        var val = $(this).val();
        if (val == 3) {
            $(".assign_coupon").css({ "display": "block" });
        }
        else {
            $(".assign_coupon").css({ "display": "none" });
        }
    });

    $("#payment_mode_2").change(function () {
        var val = $(this).val();
        if (val == 3) {
            $showcouponfield("2");
        }
        else {
            hidecouponfield("2");
        }
    });

    $("#payment_mode_3").change(function () {
        var val = $(this).val();
        if (val == 3) {
            showcouponfield("3");
        }
        else {
            hidecouponfield("3");
        }
    });

    $("#payment_mode_4").change(function () {
        var val = $(this).val();
        if (val == 3) {
            showcouponfield("4");
        }
        else {
            hidecouponfield("4");
        }
    });

    $("#payment_mode_5").change(function () {
        var val = $(this).val();
        if (val == 3) {
            showcouponfield("5");
        }
        else {
            hidecouponfield("5");
        }
    });

    $("#save_request_form_details").click(function () {
        var date_in = $("#date_in").val();
        var time_in = $("#time_in").val();
        if (date_in != "" && date_in != " " && time_in != "" && time_in != " ") {
            PrepareIncubationDates();
        }
        $("#request_details_form").submit();
    });

    $(".supply_settings_button").click(function () {
        //OpenModalDialogBox("supply_settings_modal");
        var input = $(this).val();
        var fields = input.split('-');
        var trans_id = fields[0];
        var pkg_id = fields[1];

        $("#selected_transaction_id").val(trans_id);
        $("#selected_package_id").val(pkg_id);

        if (date_in != "" && date_in != " " && time_in != "" && time_in != " ") {           
            PrepareIncubationDates();
        }
        
        $('#request_details_form').prop("action", "/OrderPostTransactions/SaveRequestIncubationDetails");
        $('#request_details_form').submit();

        //window.location = "\OrderListPage?req_id=" + request_id + "&trans_id=" + trans_id + "&pkgid=" + pkg_id;
        
    });

    $(".uid").focusout(function () {
        var uid = $(this).val();
        var request_id = $(this).attr('id');
        var url = "/hlab_order_api/updaterequestitemuid";

        console.log("UID: " + uid);
        console.log("request id: " + request_id);

        var test_request_parameter = JSON.stringify({
            hlab_order_item: { id: request_id, hl_code: uid}
        });

        ExecutePostRequest(url, test_request_parameter, null); 
    });

    $(".set_rush_condition").change(function () {        
        $(".wb_form_link").each(function () {
            var href = $(this).attr("href");
            var url_values = href.split("?");
            var values = url_values[1].split("&");
            var requestid = values[0].split("="); 
            var formid = values[1].split("="); 
            var transactionid = values[2].split("="); 
            var rush = $("#set_rush").val();
            var condition = $("#set_condition_met").val();

            if (rush.toLowerCase() == "true") {
                rush = "1";
            }
            else {
                rush = "0";
            }

            if (condition.toLowerCase() == "true") {
                condition = "1";
            }
            else {
                condition = "0";
            }
            $(this).attr("href", url_values[0] + "?rid=" + requestid[1] + "&fid=" + formid[1] + "&transid=" + transactionid[1] + "&rush=" + rush + "&condition=" + condition);
            //alert("Forms/F054?rid=" + requestid + "&fid=" + formid + "&transid=" + transactionid + "&rush=" + rush + "&condition=" + condition);
        });        
    });

    function IsIncubationInfoNotEmpty(date_in, time_in, date_out, time_out, incubation_temp) {
        if (date_in != "") { return true;}
        if (time_in != "") { return true;}
        if (date_out != "") { return true;}
        if (time_out != "") { return true;}
        if (incubation_temp != "") { return true; }
        return false;
    }

    function PrepareIncubationDates() {
        var date_in = $("#date_in").val();
        var date_out = $("#date_out").val();
        var time_in = $("#time_in").val();
        var time_out = $("#time_out").val();

        if (time_in == "") { time_in = "00:00"; }
        if (time_out == "") { time_out = "00:00"; }

        $("#incubation_date_time_in").val("");
        $("#incubation_date_time_in").val(date_in + " " + time_in + ":00.000");

        $("#incubation_date_time_out").val("");
        $("#incubation_date_time_out").val(date_out + " " + time_out + ":00.000");
                     
    }
});