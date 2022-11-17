$(function () {
    $(".expiry_date").datepicker({ dateFormat: 'dd/mm/yy' });

    $("#new_supply_button").click(function () {
        OpenModalDialogBox("add_new_supply");
        var selected_pkg_id = $("#selected_package_id").val()
        $("#modal_pkg_id").val(selected_pkg_id);
    });

    $("#close_new_supply_box").click(function () {
        CloseModalDialogBox("add_new_supply");
        $("#modal_pkg_id").val("");
        $("#modal_lot").val("");
        $("#modal_status").val("false");
        $("#modal_name").val("");
        $("#modal_expiry_date").val("");
    });

    $("#save_supply_button").click(function () {
        if (IsNewSupplyFieldsValid()) {
            $("#new_supply_form").submit();
        }
        else {
            alert("Please complete new supply information before submitting.");
        }
    });

    $("#test_package_list").click(function () {
        var pkgid = $(this).val();
        location.href = "/Supply/MainPage?pkgid=" + pkgid;
    });

    $(".supply_status").change(function () {
        var value = $(this).val();
        if (value.toLowerCase() == "true") {
            $(".supply_status").val("false");
        }
        $(this).val("true");
    });

    $("#save_supply_changes").click(function () {
        $("#supply_list").submit();
    }); 

    $("#delete_supply").click(function () {
        var supply_id = $("#supply_list input[type='radio']:checked").val();
        if (typeof supply_id === 'undefined') {
            alert("Please select a supply to delete.");
        }
        else {
            //window.location = "/Supply/DeleteSupply?sid=" + supply_id;
        }
    }); 
});

function IsTestPackageSelected() {
    if (typeof $("#selected_package_id").val() === 'undefined') { return false; }
    if ($("#selected_package_id").val() == "") { return false; }
    if ($("#selected_package_id").val() == " ") { return false; }
    if ($("#selected_package_id").val() == "0") { return false; }
    if ($("#selected_package_id").val() == 0) { return false; }
    return true;
}

function IsNewSupplyFieldsValid() {
    var isNameOk = true;
    var isLotOk = true;
    var isExpiryDateOk = true;
    var isPkgIdOk = true;

    if (
        $("#modal_pkg_id").val() == "" ||
        $("#modal_pkg_id").val() == " " ||
        $("#modal_pkg_id").val() == "0" ||
        $("#modal_pkg_id").val() == 0) {
        isPkgIdOk = false;
    }

    if (
        $("#modal_name").val() == "" ||
        $("#modal_name").val() == " ") {
        isNameOk = false;
    }

    if (
        $("#modal_lot").val() == "" ||
        $("#modal_lot").val() == " ") {
        isLotOk = false;
    }

    if (
        $("#modal_expiry_date").val() == "" ||
        $("#modal_expiry_date").val() == " ") {
        isExpiryDateOk = false;
    }

    if (isNameOk && isLotOk && isExpiryDateOk && isPkgIdOk) {
        return true;      
    }

    return false;
}