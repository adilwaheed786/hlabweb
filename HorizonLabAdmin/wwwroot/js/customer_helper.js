function GetCustomersRecords(searchfilter, searchvalue, ProcessJasonDataFunction) {
    if (searchvalue != "" && searchfilter != "") {
        GetApiData("/hlab_order_api/getcustomerlist?searchvalue=" + searchvalue + "&searchfilter=" + searchfilter, ProcessJasonDataFunction);
    }
    else {
        alert("Please specify a value or search filter when searching a customer!");
    }
}

function PopulateCustomerInfoForTestRequest(data) {
    $("#customer_info").html("");
    $("#customer_info").html(" (for " + GetSelectedCustomerFirstAndLastName() + ")");
}

function GetSelectedCustomerFirstAndLastName() {
    return $("#customer_list").find(":selected").text();
}

function GetSelectedCustomerId() {
    return $("#hidden_customerid_field").val();
}

function CheckCustomerNameDuplicate(customer_api_data) {
    var duplicatemessage = "";
    var proceed = true;
    $.each(customer_api_data, function (key, value) {
        duplicatemessage = duplicatemessage + "Customer ID: " + value.customer_id + " | Customer Name: " + value.first_name + " " + value.last_name + " | Addess: " + value.street + " " + value.city + "\n";
    });

    if (duplicatemessage != "") {
        proceed = confirm("New customer has similar name(s) in our records: \n\n" + duplicatemessage + "\n\n Do you still wish to proceed?");
    }

    if (proceed) {
        var ans = confirm("Save New Customer?");
        if (ans) {
            var url = "/hlab_customer_api/addnewcustomer";
            var customer_parameter = JSON.stringify({
                "hlab_customers": GenerateCustomerObject(),
                "phone_list": GeneratePhoneListObject(),
                "email_list": GenerateEmailListObject()
            });
            ExecutePostRequest(url, customer_parameter, UpdateCustomerSelectList);
        }
    }
}

function GenerateCustomerObject() {
    var hlab_customers = {
        producer_number: $("#input_producer_number").val(),
        company_name: $("#input_compname").val(),
        first_name: $("#input_firstname").val(),
        last_name: $("#input_lastname").val(),
        street: $("#input_street").val(),
        city_id: $("#select_city").val(),
        province_id: $("#input_province").val(),
        postal_code: $("#input_postal_code").val(),
        fax: $("#input_fax").val(),
        is_public: $("#input_ispublic").val(),
        status: true,
        is_semi_public: $("#input_issemipublic").val(),
        is_approve_financing: $("#input_isapproveforfinancing").val(),
        env_distr: $("#input_environment_district").val(),
        dw_officer: $("#input_dwofficer").val(),
        dw_phone: $("#input_dwphone").val(),
        com_code: $("#input_com_code").val(),
        date_registered: GetCurrentDate(),
        //coupon: $("#").val(),
        //gst_number: $("#").val(),
        is_realestate: $("#input_isrealestate").val()
    };
    return hlab_customers;
}

function GeneratePhoneListObject() {
    var counter = 0;
    var arr_hlab_customer_phone = [];
    $.each($(".input_phone"), function () {
        if (isStringValueNotEmpty($(this).val())) {
            arr_hlab_customer_phone[counter] = { phone: $(this).val() };
            counter++;
        }
    });
    return arr_hlab_customer_phone;
}

function GenerateEmailListObject() {
    var counter = 0;
    var arr_hlab_customer_email = []
    $.each($(".customer_email"), function () {
        var isprimary = false;
        if (isStringValueNotEmpty($(this).val())) {
            if (counter == 0) { isprimary = true }
            arr_hlab_customer_email[counter] = { email: $(this).val(), is_primary: isprimary };
            counter++;
        }
    });
    return arr_hlab_customer_email;
}

function UpdateCustomerSelectList(customer_data) {
    $("#customer_list").append("<option value='" + customer_data.customer_id + "'>" + customer_data.first_name + " " + customer_data.last_name + "</option>");
    SetSelectedCustomerId("");
    //SetSelectedCustomerFname(customer_data.first_name);
    //SetSelectedCustomerLname(customer_data.last_name);

    CloseModalDialogBox("add_new_customer");
    ClearNewCustomerForm();
}

function ClearNewCustomerForm() {
    $("#input_producer_number").val("");
    $("#input_compname").val("");
    $("#input_firstname").val("");
    $("#input_lastname").val("");
    $("#input_street").val("");
    $("#select_city").val("");
    $("#input_province").val("");
    $("#input_postal_code").val("");
    $("#input_fax").val("");
    $("#input_ispublic").val("");
    $("#input_issemipublic").val("");
    $("#input_isapproveforfinancing").val("");
    $("#input_environment_district").val("");
    $("#input_dwofficer").val("");
    $("#input_dwphone").val("");
    $("#input_com_code").val("");
    $("#input_primary_email").val("");
    $("#input_primary_phone").val("");
}

function ClearSelectedCustomerId() {
    $("#hidden_customerid_field").val("");
}

function SetSelectedCustomerId(customer_id) {
    $("#hidden_customerid_field").val();
    $("#hidden_customerid_field").val(customer_id);
}

function SetSelectedCustomerFname(customer_first_name) {
    $("#hidden_customerfname_field").val();
    $("#hidden_customerfname_field").val(customer_first_name);
}

function SetSelectedCustomerLname(customer_last_name) {
    $("#hidden_customerlname_field").val();
    $("#hidden_customerlname_field").val(customer_last_name);
}

function ReloadCustomerRecordsDropDown(customer_data) {
    $("#customer_list").empty();
    $.each(customer_data, function (key, value) {
        $("#customer_list").append("<option value='" + value.customer_id + "'>" + value.first_name + " " + value.last_name + " (" + value.city + ")</option>")
    });
}