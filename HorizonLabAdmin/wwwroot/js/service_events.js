
$("#save_service_header_button").click(function () {
    var line_with_errors = "";
    $(".line").each(function () {
        if ($(this).val().length > 155) {
            line_with_errors = line_with_errors + " " + this.id;
        }
    });

    if (line_with_errors != "") {
        alert("The following lines have more than 155 characters: " + line_with_errors);
    }
    else {
        $("#service_header_form").submit();
    }
});

$(".line").change(function () {
    var input_id = this.id;     
    if ($(this).val().length > 155) {
        $("#error_" + input_id).show();
        $("#ok_" + input_id).hide();
    }
    else {
        $("#error_" + input_id).hide();
        $("#ok_" + input_id).show();
    }
});

$(".goto_detail").click(function () {
    var detail = $(this).text();
    var raw = $(this).attr("id").split("_");
    var service = raw[0];
    var id = raw[1];

    $("#service_id").val("");
    $("#detail").val(detail);
    $("#detail_id").val(id);
    $("#service").html(service);

    $("#save_new_service_detail").hide();
    $("#save_service_detail_changes").show();
});

$(".goto_addnew_detail").click(function () {
    var raw = $(this).attr("id").split("_");
    var service = raw[1];
    var service_id = raw[0];

    $("#detail").val("");
    $("#detail_id").val("");
    $("#service_id").val(service_id);
    $("#service").html(service);

    $("#save_new_service_detail").show();
    $("#save_service_detail_changes").hide();
});

$(".goto_service").click(function () {
    var service = $(this).text();
    var id = this.id;
    $("#modal_service_name").val(service);
    $("#modal_service_id").val(id);
});

$("#save_new_service").click(function () {
    var service = $("#new_service_name").val();
    if (service == "") {
        alert("Please provide a service name!");
    }
    else{
        $("#add_service_form").submit();
    }
});

$("#save_service_btn").click(function() {
    if ($("#modal_service_name").val() == "") {
        alert("Can't save service name without a value!");
    }
    else if ($("#modal_service_id").val() == "") {
        alert("Record ID is missing, please contact administrator!");
    }
    else {
        $("#service_form").submit();
    }
});

$("#delete_service").click(function () {
    var service_name = $("#modal_service_name").val();
    $("#service_form").attr("action", "/Services/DeleteService");
    $("#service_form").submit();
});

$("#delete_service_detail").click(function () {
    var detail_name = $("#detail").val();
    var ans = confirm("Are you sure you want to delete " + detail_name + "?");
    if (ans) {
        $("#service_detail_form").attr("action", "/Services/DeleteServiceDetail");
        $("#service_detail_form").submit();
    }
});

$("#save_service_detail_changes").click(function () {
    var detail_name = $("#detail").val();
    if (detail_name == "") {
        alert("Can't save changes with no value!")
    }
    else {
        $("#service_detail_form").attr("action", "/Services/SaveServiceDetailChanges");
        $("#service_detail_form").submit();
    }
});

$("#save_new_service_detail").click(function () {
    var detail_name = $("#detail").val();
    if (detail_name == "") {
        alert("Can't save changes with no value!")
    }
    else {
        $("#service_detail_form").attr("action", "/Services/AddNewServiceDetail");
        $("#service_detail_form").submit();
    }
});