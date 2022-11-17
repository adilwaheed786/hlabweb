////////////////////////////////// START GET FUNCTIONS //////////////////////////////////////////////////////
function GetRequestRecords(customer_id, ProcessJasonDataFunction) {
    if (customer_id != "") {
        GetApiData("/hlab_order_api/getcustomertestrequests?customerid=" + customer_id, ProcessJasonDataFunction);
    }
    else {
        //alert("No customer was provided!");
    }
}

function GetTestRequestDetail(requestid, ProcessJasonDataFunction) {
    if (requestid != "") {
        GetApiData("/hlab_order_api/getcustomertestrequestdetails?requestid=" + requestid, ProcessJasonDataFunction);
    }
    else {
        alert("No test request was provided!");
    }
}

function GetTestRequestItems(requestid, ProcessJasonDataFunction) {
    if (requestid != "") {
        GetApiData("/hlab_order_api/getorderitemdetails?orderid=" + requestid, ProcessJasonDataFunction);
    }
    else {
        alert("No test transaction was provided!");
    }
}


function GetWaterBacteriaDetails(transid, ProcessJasonDataFunction) {
    if (transid != "") {
        GetApiData("/hlab_order_api/getwaterbacteriatestresults?transid=" + transid, ProcessJasonDataFunction);
    }
    else {
        alert("No test transaction was provided!");
    }
}

function GetHiddenRequestIdField() {
    return $("#hidden_requestid_field").val();
}

function GetHiddenRequestItemIdField() {
    return $("#hidden_requestitemid_field").val();
}

function ExtractTransactionIdFromString(string_value) {
    var arr_value = string_value.toString().split('_');
    return arr_value[0];
}

function ExtractRequestItemIdFromString(string_value) {
    var arr_value = string_value.toString().split('_');
    return arr_value[1];
}
////////////////////////////////// END GET FUNCTIONS //////////////////////////////////////////////////////

////////////////////////////////// START GENERATE FUNCTIONS //////////////////////////////////////////////////////
function GenerateTestTransactionJsonObject() {
    var hlab_test_transactions = JSON.stringify({
        "trans_id": $("#transactionid").val(),
        "test_pkg_id": $("#database_test_pkg_id").val(),
        "price": $("#database_price").val(),
        "is_paid": $("#transactionIsPaid").val(),
        "collect_datetime": $("#collectdate").val(),
        "submtd_by": $("#form_submittedby").val(),
        "submtd_datetime": $("#submitdate").val(),
        "rcv_by_id": $("#database_rcv_by_id").val(),
        "rcv_date": $("#receivedate").val(),
        "test_date": $("#testdate").val(),
        "is_flood_sample": $("#database_is_flood_sample").val(),
        "customer_id": $("#customer_select").val(),
        "notes": $("#database_notes").val(),
        "assigned_coupon": $("#database_assigned_coupon").val(),
        "date_entered": $("#database_date_entered").val(),
        "work_date": $("#reportdate").val(),
        "transaction_type_id": $("#database_transaction_type_id").val(),
        "report_type_id": $("#form_reporttype").val(),
        "temp": $("#form_temperature").val(),
        "project_id": $("#database_project_id").val(),
        "idnty_name": $("#database_idnty_name").val(),
        "idnty_location": $("#form_sampleid").val(),
        "sample_type_id": $("#form_sampletype").val(),
        "sample_legal_loc": $("#form_sample_legal_loc").val(),
        "town": $("#form_town").val(),
        "rm_id": $("#form_municipality").val(),
        "latitude": $("#database_latitude").val(),
        "longitude": $("#database_longitude").val(),
        "utm_x": $("#database_utm_x").val(),
        "utm_y": $("#database_utm_y").val(),
        "zone": $("#database_zone").val(),
        "status": "true",
        "existence": $("#database_existence").val(),
        "is_good_condition": $("#database_is_good_condition").val()
    });

    return hlab_test_transactions;
}

function GenerateRequestJsonObject() {
    var total = ComputeTotalRequestAmount();
    total = total.toFixed(2);

    var hlab_order_logs = {
        customer_id: $("#hidden_customerid_field").val(),
        order_date: GetCurrentDate(),
        received_by: GetCurrentUser(),
        proc_status: true,
        total_amount: total,
        record_status: true
    };

    return hlab_order_logs;
}

function GenerateRequestItemJsonObject() {
    var counter = 0;
    var arr_hlab_order_items = new Array();

    $.each($(".hidden_pkgid"), function (index) {
        package_id = $(this).val();
        price = document.getElementsByClassName('hidden_raw_price')[counter].value;
        coupon = document.getElementsByClassName('assign_coupon')[counter].value;        
        var item  = {
            test_pkg_id: package_id,
            amount: price,
            trans_id: 0,
            coupon: coupon
        };
        arr_hlab_order_items.push(item);
        //console.log(arr_hlab_order_items[counter]);
        counter++;
    });
    return arr_hlab_order_items;
}
////////////////////////////////// END GENERATE FUNCTIONS //////////////////////////////////////////////////////

////////////////////////////////// START CLEAR FUNCTIONS //////////////////////////////////////////////////////
function ClearTestRequestRecordsPanel() {
    $("#request_list").empty();
}

function ClearTestRequestDetailPanel() {
    $("#requestid_val").html("");
    $("#receivedby_val").html("");
    $("#customer_val").html("");
    $("#date_val").html("");
}

function ClearTestRequestItemsPanel() {
    $("#request_item_list").empty();
}

function ClearSelectedTestRequestId() {
    $("#hidden_requestid_field").val("");
}

function ClearSelectedTestTransactionId() {
    $("#hidden_testtransactioid_field").val("");
}

function ClearSelectedRequestItemId() {
    $("#hidden_requestitemid_field").val("");
}

function ClearTestDetailIframe() {
    $("#test_detail_frame").attr('src', "");
}

function ClearNewTestRequestForm() {

}

function ClearPackageSelectList() {
    $("#pkg_select").empty();
}

function ClearCategorySelectList() {
    $("#category_select").empty();
}

function ClearPaymentSection() {
    var cash_payment_type = "1";
    $(".payment_type").val(cash_payment_type);
    $("#total_price").html("0.00")
    $(".payment_amount").val("");
}

function ClearTestPackageDetails() {
    $("#pkg_labcode").html("");
    $("#pkg_price").html("");
    $("#pkg_analysis").html("");
    $("#pkg_desc").html("");
}

function RemoveItemFromSelectList(selectlist_id, opttion_value) {
    $("#" + selectlist_id + " option[value='" + opttion_value + "']").remove();
}
////////////////////////////////// END CLEAR FUNCTIONS //////////////////////////////////////////////////////

////////////////////////////////// START SET FUNCTIONS //////////////////////////////////////////////////////
function SetSelectedTestRequestId(requestid) {
    ClearSelectedTestRequestId();
    $("#hidden_requestid_field").val(requestid);
}

function SetSelectedTestTransactionItemId(transactionid) {
    ClearSelectedTestTransactionId();
    $("#hidden_testtransactioid_field").val(transactionid);
}

function SetSelectedRequestItemId(requestitemid) {
    ClearSelectedRequestItemId();
    $("#hidden_requestitemid_field").val(requestitemid);
}

function SetRequestHTMLValue(testrequest) {
    $("#requestid_val").html(testrequest.order_id);
    $("#receivedby_val").html(testrequest.received_by);
    $("#customer_val").html(testrequest.first_name + " " + testrequest.last_name);
    $("#date_val").html(formatDate(testrequest.order_date));
}
////////////////////////////////// END SET FUNCTIONS //////////////////////////////////////////////////////

////////////////////////////////// START RELOAD FUNCTIONS //////////////////////////////////////////////////////
function AppendRequestHtmlSelectList(requestid) {
    var customer_id = $("#customer_list").val();
    CloseModalDialogBox("processing_modaldialogbox");
    LoadRequestSelectList(customer_id);

    ClearTestRequestItemsPanel();
    ClearTestRequestDetailPanel();
    ClearTestDetailIframe();
    ClearTableRows("order_table");
    ClearPaymentSection();
    ClearPackageSelectList();
    ClearTestPackageDetails();

    CloseModalDialogBox("add_test_request");
}

function RemoveCustomerRequestFromSelectList() {
    var selected_request_id = GetHiddenRequestIdField();
    RemoveItemFromSelectList("request_list",selected_request_id),
    ClearSelectedTestRequestId();
    ClearTestRequestItemsPanel();
    ClearTestDetailIframe();
}

function ReloadRequestHTMLDropDown(request) {
    $("#request_list").empty();
    $.each(request, function (key, value) {
        $("#request_list").append("<option value='" + value.order_id + "'> Request ID: " + value.order_id + " </option>");
    });
}

function ReloadRequestItemHTMLTable(requestitems) {
    var hidden_pkg_id = 3;
    var misc_pkg_id = 2;
    ClearTestRequestItemsPanel();
    $.each(requestitems, function (key, value) {
        var disabled = "";
        if (value.pkg_class_id == hidden_pkg_id || value.pkg_class_id == misc_pkg_id)
        {
            disabled = "disabled";
        }

        if (IsTransactionIdEmpty(value.trans_id)) {
            value.trans_id = 0;
        }
        //console.log(value.lab_code + ":" + value.pkg_class_id  + ":" + disabled);
        $("#request_item_list").append("<option value='" + value.trans_id + "_" + value.order_item_id + "' " + disabled + ">" + value.lab_code + " (" + USDCurrencyFormatter.format(value.amount) + ")</option>");
    });
}

function LoadRequestSelectList(customer_id) {
    SetSelectedCustomerId(customer_id);
    ClearSelectedTestRequestId();
    ClearSelectedTestTransactionId();

    ClearTestRequestDetailPanel();
    ClearTestRequestItemsPanel();
    ClearTestDetailIframe();
    GetRequestRecords(customer_id, ReloadRequestHTMLDropDown);
}

function LoadRequestItemSelectList(requestid) {
    SetSelectedTestRequestId(requestid);
    ClearSelectedTestTransactionId();
    ClearTestDetailIframe();
    GetTestRequestDetail(requestid, SetRequestHTMLValue);
    GetTestRequestItems(requestid, ReloadRequestItemHTMLTable);
}

function ReloadTestRequestPage() {
    var customerid = $("#hidden_customerid_field").val();
    var fname = $().val("#hidden_customerfname_field");
    var lname = $().val("#hidden_customerlname_field");
    var requestid = $().val("#hidden_requestid_field");
    var transactionid = $("#hidden_testtransactioid_field").val();

    window.location.href = "/Order/TestRequestPage?customerid=" + customerid + "&fname=" + fname + "&lname=" + lname + "&requestid=" + requestid + "&transactionid=" + transactionid;
}
////////////////////////////////// END RELOAD FUNCTIONS //////////////////////////////////////////////////////

////////////////////////////////// START BOOLEAN FUNCTIONS //////////////////////////////////////////////////////
function IsRequestHasTransactionCreated(request) {
    if (request.trans_id > 0) {
        return true;
    }
    return false;
}

function IsTestRequestHasBeenSelected() {
    if (
        $("#hidden_requestid_field").val() != "" ||
        $("#hidden_requestid_field").val() != " " ||
        $("#hidden_requestid_field").val() != "0" ||
        $("#hidden_requestid_field").val() != 0) {
        return true;
    }
    return false;
}

function IsClientHasBeenSelected() {
    if (
        $("#hidden_customerid_field").val() != "" &&
        $("#hidden_customerid_field").val() != " " &&
        $("#hidden_customerid_field").val() != "0" &&
        $("#hidden_customerid_field").val() != null &&
        $("#hidden_customerid_field").val() != 0) {
        return true;
    }
    return false;
}

function IsTestRequestItemTablePopulated() {
    if ($(".hidden_pkgid").length > 0) {
        return true;
    }
    return false;
}

function IsTransactionIdEmpty(trans_id) {
    if (trans_id == "" || trans_id == " " || trans_id == null) {
        return true;
    }
    return false;
}

function IsRequestItemIdValid(str_value, request_Item_id) {
    if (str_value != "" && request_Item_id == 0) {
        return false;
    }
    return true;
}
////////////////////////////////// END BOOLEAN FUNCTIONS //////////////////////////////////////////////////////
