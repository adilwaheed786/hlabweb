$(function () {
    var _batchRecCount = 100;
    $("#next_btn").click(function () {
        var start = parseInt($("#customer_rec_start").val());
        var end = parseInt($("#customer_rec_end").val());
        var rec_count = parseInt($("#customer_rec_count").val());
        var batchEndCount = end + _batchRecCount;
        if (batchEndCount > rec_count) { batchEndCount = rec_count;}

        if (end < (rec_count) && start >= 0) {
            $("#customer_rec_start").val(start + _batchRecCount);
            $("#customer_rec_end").val(batchEndCount);
            $("#customer_nav").submit();
        }
    });

    $("#back_btn").click(function () {
        var start = parseInt($("#customer_rec_start").val());
        var end = parseInt($("#customer_rec_end").val());

        if (start > 0 && end > (_batchRecCount - 1)) {
            $("#customer_rec_start").val(start - _batchRecCount);
            $("#customer_rec_end").val(end - _batchRecCount);
            $("#customer_nav").submit();
        }
    });

    $("#last_btn").click(function () {
        var start = parseInt($("#customer_rec_start").val());
        var end = parseInt($("#customer_rec_end").val());
        var rec_count = parseInt($("#customer_rec_count").val());

        if ((end < (rec_count)) && start >= 0) {
            start = ((Math.ceil(rec_count / _batchRecCount) * _batchRecCount) - _batchRecCount) - 1 //_batchRecCount = 100 / round off to the nearest 100
            end = rec_count;
            $("#customer_rec_start").val(start);
            $("#customer_rec_end").val(end);
            $("#customer_nav").submit();
        }
    });

    $("#first_btn").click(function () {
        var start = parseInt($("#customer_rec_start").val());
        var end = parseInt($("#customer_rec_end").val());
        var rec_count = parseInt($("#rec_count").val());

        $("#customer_rec_start").val(0);
        if (rec_count <= (_batchRecCount)) {
            $("#customer_rec_end").val(rec_count - 1);
        }
        else {
            $("#customer_rec_end").val(_batchRecCount - 1);
        }
        $("#customer_nav").submit();
    });

    $(".goto_placeorderform").click(function () {
        var customerid = $(this).val();
        window.location.href = "/WaterBacteriaTransaction/NewWaterSampleForm?customerid=" + customerid;
    });

    $(".view_customer").click(function () {
        var customerid = $(this).val();
        window.location.href = "/Customer/ViewCustomerDetails?customerid=" + customerid;
    });

    $(".edit_customer").click(function () {
        var customerid = $(this).val();
        window.location.href = "/Customer/EditCustomerForm?cid=" + customerid;
    });

    $("#testpackagecategoryid").change(function () {
        $("#set_package_info").submit();
    });

    $("#testpackageid").change(function () {
        $("#set_package_info").submit();
    });

    $("#submit_btn").click(function () {
        submit_customer_form_with_full_validation();
    });  

    $("#submit_proceed_btn").click(function () {
        $("#save_proceed").val("true");
        submit_customer_form_with_full_validation();
    });  

    $("#collect_date").datepicker();

    $(".deactivate_customer").click(function () {
        var customerid = $(this).val();
        var ans = confirm("Are you sure you want to deactivate the selected customer id:" + customerid + "?");
        if (ans) {
            window.location = "/Customer/DeactivateCustomer?customerid=" + customerid;
        }
    });

    $("input[type=checkbox]").change(function () {
        $("input[type=checkbox]").prop('checked', false);
        $(this).prop("checked", true);               
    });

    $("[tabindex]").addClass("TabOnEnter");
    $(document).on("keypress", ".TabOnEnter", function (e) {
        //Only do something when the user presses enter
        if (e.keyCode == 13) {
            var nextElement = $('[tabindex="' + (this.tabIndex + 1) + '"]');
            console.log(this, nextElement);
            if (nextElement.length)
                nextElement.focus()
            else
                $('[tabindex="1"]').focus();
        }
    });

    $("#addcity").click(function () {
        var city = prompt("Please enter new town:", "");                
        if (city != null) {
            var api_url = "/hlab_customer_api/addnewcity?newcity=" + city;
            GetApiData(api_url, AppendCitySelectList);            
        }
    });

    $("#input_compname").focusout(function () {
        $("input[tabindex='0']").select();
    });

    $(".mask_phone").keyup(function (e) {
        //$(this).mask("999-999-9999");
        //this.value = this.value.replace(/(\d{3})\-?/g, '$1-');
        if (this.value.trim().length <= 8) {
            if ((e.keyCode > 47 && e.keyCode < 58) || (e.keyCode < 106 && e.keyCode > 95)) {
                this.value = this.value.replace(/(\d{3})\-?/g, '$1-');
            }            
        }
    });
});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function AppendCitySelectList(data) {  
    $('#select_city').append(`<option value="${data.city_id}" selected> ${data.city}</option>`);
}

function submit_customer_form_with_full_validation() {
    var input_compname = $("#input_compname").val();
    var input_street = $("#input_street").val();
    var input_postal_code = $("#input_postal_code").val();
    var form_action = $("#form_action").val();
    var primary_email = $(".primary_email").val();
    var input_firstname = $("#input_firstname").val();
    var input_lastname = $("#input_lastname").val();
    var proceed = true;


    //if (input_firstname == "") { alert("Please provide customer's First Name"); proceed = false; }
    if (input_lastname == "") { alert("Please provide customer's Last Name"); proceed = false; }
    //if (input_street == "") { alert("Please provide customer's Street Address"); proceed = false; }
    //if (input_postal_code == "") { alert("Please provide customer's Postal Code"); proceed = false; }
    //if (primary_email == "") { alert("Please provide customer's Primary Email Address"); proceed = false; }
    //proceed = CheckPostlCodeFormat(input_postal_code);
    //proceed = ValidateEmailEntries();

    if (proceed) {
        var api_url = "/hlab_customer_api/getcustomernameduplicates?firstname=" + input_firstname + "&lastname=" + input_lastname;
        GetApiData(api_url, SubmitCustomerForm)
    }
}

function SubmitCustomerForm(customerdata) {
    var duplicatemessage = "";
    $.each(customerdata, function (key, value) {
        duplicatemessage = duplicatemessage + "Customer ID: " + value.customer_id + " | Customer Name: " + value.first_name + " " + value.last_name + " | Addess: " + value.street + " " + value.city + "\n";
    });

    if (duplicatemessage != "") {
        var ans = confirm("New customer has similar name(s) in our records: \n\n" + duplicatemessage + "\n\n Do you still wish to proceed?");
    }
    else {
        var ans = confirm("Do you wish to submit this form?");
    }

    if (ans) {
        $("#customer_form").submit();
    }
}

function ValidateEmailEntries() {
    primary_email = $("#input_primary_email").val();
    var proceed = true;
    if (primary_email == "" || primary_email == " ") {
        alert("Please provide a primary email address!");
        proceed = false;
    }
    return proceed;
}

function CheckPostlCodeFormat(input_postal_code) {
    var postalcode_pattern = /[ABCEFGHJKLMNPRSTVXY][0-9][ABCEFGHJKLMNPRSTVWXYZ][0-9][ABCEFGHJKLMNPRSTVWXYZ][0-9]/
    
    if (!input_postal_code.match(postalcode_pattern)) {
        alert("Invalid Postal Code format!");
        proceed = false;
    } else {
        proceed = true;
    }
    return proceed;
}

window.onload = function () {
    document.getElementById("input_compname").focus();
};