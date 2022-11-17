$(function () {
    $("#search_customer").click(function () {
        ClearSelectedCustomerId();
        ClearSelectedTestRequestId();
        ClearSelectedTestTransactionId();

        ClearTestRequestRecordsPanel();
        ClearTestRequestDetailPanel();
        ClearTestRequestItemsPanel();
        ClearTestDetailIframe();

        var searchfilter = $("#form_searchfilter").val();
        var searchvalue = $("#form_searchvalue").val();
        GetCustomersRecords(searchfilter, searchvalue, ReloadCustomerRecordsDropDown);
    });

    $("#customer_list").click(function (e) {
        var customer_id = $(this).val();
        LoadRequestSelectList(customer_id);
    });

    $("#request_list").click(function () {
        var requestid = $(this).val();
        LoadRequestItemSelectList(requestid);
    });

    $("#reload_request_items").click(function () {
        var requestid = GetHiddenRequestIdField();
        if (requestid != 0 || requestid != "") {
            LoadRequestItemSelectList(requestid);
        }
    });

    $("#request_item_list").click(function () {        
        var str_value = $(this).val();
        var request_item_id = 0;
        var transid = 0;
        var request_id = GetHiddenRequestIdField();        
        var frame_url = "";
        
        if (str_value != 0 || str_value != "") {
            transid = ExtractTransactionIdFromString(str_value);
            request_item_id = ExtractRequestItemIdFromString(str_value);
        }

        if (request_id == 0) {
            alert("No Request ID found, please contact administrator.");
        }

        if (!IsRequestItemIdValid(str_value, request_item_id)) {
            alert("Invalid Request item found, please contact administrator.");
        }

        SetSelectedRequestItemId(request_item_id);
        OpenModalDialogBox("loading_modaldialogbox");
        
        if (transid == 0 || transid == "") {            
            frame_url = "/WaterBacteriaTransaction/NoTestRecordFramePage?url_requestid=" + request_id + "&url_requestitemid=" + request_item_id;
        }
        else {            
            SetSelectedTestTransactionItemId(transid);
            frame_url = "/WaterBacteriaTransaction/EditTestTransactionFrame?transId=" + transid;
        }

        $('#test_detail_frame').attr('src', frame_url);
    });

    $("#save_test_transaction_btn").click(function () {
        var url = "/hlab_test_transaction/updatewatertesttransaction";
        var hlab_test_transactions = GenerateTestTransactionJsonObject();
        ExecutePostRequest(url, hlab_test_transactions, alert("Done"));
    });

    $("#submit_new_customer").click(function () {

        var input_firstname = $("#input_firstname").val();
        var input_lastname = $("#input_lastname").val();
        var proceed = true;
        var duplicatemessage = "";

        //if (input_firstname == "") { alert("Please provide customer's First Name"); proceed = false; }
        if (input_lastname == "") { alert("Please provide customer's Last Name"); proceed = false; }

        //proceed = ValidateEmailEntries();
        var api_url = "/hlab_customer_api/getcustomernameduplicates?firstname=" + input_firstname + "&lastname=" + input_lastname;        

        if (proceed) {
            GetApiData(api_url, CheckCustomerNameDuplicate);
        }
    });

    $("#test_request_button").click(function () {
        if (IsClientHasBeenSelected()) {
            PopulateCustomerInfoForTestRequest();
            OpenModalDialogBox("add_test_request");
        }
        else {
            alert("Please select a customer before creating a test request.");
        }
    });

    $("#submit_request_btn").click(function () {    
        if (!IsTestRequestItemTablePopulated()) {
            alert("Please place a test request before submitting.");
            return null;
        }

        var url = "/hlab_order_api/savenewtestrequest";
        var items = GenerateRequestItemJsonObject();
        var payments = GeneratePaymentsJsonObject();
        var request = GenerateRequestJsonObject();

        var test_request_parameter = JSON.stringify({
            hlab_order_logs: request,
            hlab_order_items: items,
            hlab_test_payments: payments
        });
        console.log("ITEMS:" + JSON.stringify(items));
        OpenModalDialogBox("processing_modaldialogbox");
        ExecutePostRequest(url, test_request_parameter, AppendRequestHtmlSelectList);               
    });

    $("#close_customer_form").click(function () {
        ClearNewCustomerForm();
        CloseModalDialogBox("add_new_customer");
    });

    $("#delete_request").click(function () {
        var requestid = GetHiddenRequestIdField();
        if (requestid == "" || requestid == 0) {
            alert("Please select a test request to delete.");
            return false;
        }

        var url = "/hlab_order_api/delete_customer_request_items_transactions?requestid=" + requestid;
        GetApiData(url, RemoveCustomerRequestFromSelectList);
    });
     
});


