$(document).ready(function () {
    $(".datepicker_default").datepicker({ dateFormat: 'dd/mm/yy' });
    $('.input_collecttime').timetator({ useCap: true });
    $('#time_control').timetator({ useCap: true });
    $('.input_collectdate').datetextentry({ field_order: 'DMY', /*year_id: 'collectdate_year'*/ });    

    $('.fromatted_date').datetextentry({field_order: 'DMY',});

    var sample_description_list = [
        "Basement Tap",
        "Bathroom",
        "Kitchen",
        "Outdoor Tap",
        "Well"
    ];
    $(".sample_description_field").autocomplete({
        source: sample_description_list
    });

    $('#collect_date_control').datetextentry({
        field_order: 'DMY',
        year_id: 'date_year_control',
        on_change: function (datestr) {
            modify_selected_date_fields(datestr, "collect_date_");
        }
    });

    $('#submitted_date_control').datetextentry({
        field_order: 'DMY',
        year_id: 'date_year_control',
        on_change: function (datestr) {
            modify_selected_date_fields(datestr, "submitted_date_");
        }
    });    

    $('#rcv_date_control').datetextentry({
        field_order: 'DMY',
        year_id: 'date_year_control',
        on_change: function (datestr) {
            modify_selected_date_fields(datestr, "rcv_date_");
        }
    });

    $('#test_date_control').datetextentry({
        field_order: 'DMY',
        year_id: 'date_year_control',
        on_change: function (datestr) {
            modify_selected_date_fields(datestr, "test_date_");
        }
    });

    $("#collect_time_control").focusout(function () {
        var formatted_time = foramtTime($(this).val());
        modify_selected_time_fields(formatted_time, "collect_time_");
    });

    $("#rcv_time_control").focusout(function () {
        var formatted_time = foramtTime($(this).val());
        modify_selected_time_fields(formatted_time, "rcv_time_");
    });

    $("#submitted_time_control").focusout(function () {
        var formatted_time = foramtTime($(this).val());
        modify_selected_time_fields(formatted_time, "submitted_time_");
    });

    $("#test_time_control").focusout(function () {
        var formatted_time = foramtTime($(this).val());
        modify_selected_time_fields(formatted_time, "test_time_");
    });

    $("#temp_control").focusout(function () {
        var temp = $(this).val();
        $(".modify_value").each(function () {
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                $("#temp_" + req_id).val(temp);
            }
        });
    });

    $("#submitted_by_controller").focusout(function () {
        var submitted_by = $(this).val();
        $(".modify_value").each(function () {
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                $("#submitted_by_" + req_id).val(submitted_by);
            }
        });
    });

    function modify_selected_date_fields(str_date, classid_suffix) {
        $(".modify_value").each(function () {
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                $("#" + classid_suffix + req_id).datetextentry('set_date', str_date);
            }
        });
    }

    function modify_selected_time_fields(time, classid_suffix) {
        $(".modify_value").each(function () {
            console.log(classid_suffix);
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                $("#" + classid_suffix + req_id).val(time);
                console.log(time);
            }
        });
    }

    $(".get_customer_info").change(function () {
        var searchvalue = $(this).val();
        var searchfilter = "ci";
        var index = $(this).attr("index");
        //console.log($(this).attr("index"));
        if (searchvalue != "" && searchfilter != "") {
            $.ajax({
                url: "/hlab_order_api/getcustomerlist?searchvalue=" + searchvalue + "&searchfilter=" + searchfilter,
                method: "GET",
                success: function (data) {
                    $("#fname_" + index).val(data[0].first_name);
                    $("#lname_" + index).val(data[0].last_name);
                    //console.log(data[0]);                        
                }
            });
        }
    });

    $("#search_request_btn").click(function () {
        $("#record_start").val(0);
        $("#record_end").val(100);
        $("#request_records_form").submit();
    });

    $("#update_requests").click(function () {
        consolidate_collect_date_time();
        consolidate_received_date_time();
        consolidate_test_date_time();
        consolidate_submitted_date_time();
        $("#bulk_request_form").attr("target", "_self");
        $("#bulk_request_form").submit();
    });

    $("#apply_proj_form_changes").click(function () {

        var datein = $("#inc_date_in").val();
        var timein = $("#inc_time_in").val();
        //console.log(date + " " + time);
        $("#inc_datetime_in").val(datein + " " + timein);

        var dateout = $("#inc_date_out").val();
        var timeout = $("#inc_time_out").val();
        //console.log(dateout + " " + timeout);
        $("#inc_datetime_out").val(dateout + " " + timeout);

        $("#update_proj_form").submit();        
    });

    function consolidate_collect_date_time() {
        $(".collect_datetime_fields").each(function () {
            var index = $(this).attr("index");
            var date = $("#collect_date_" + index).val();
            var time = $("#collect_time_" + index).val();

            //console.log(date + " " + time);
            $(this).val(date + " " + time);
        });
    }

    function consolidate_submitted_date_time() {
        $(".submitted_datetime_fields").each(function () {
            var index = $(this).attr("index");
            var date = $("#submitted_date_" + index).val();
            var time = $("#submitted_time_" + index).val();

            //console.log(date + " " + time);
            $(this).val(date + " " + time);
        });
    }

    function consolidate_received_date_time() {
        $(".received_datetime_fields").each(function () {
            var index = $(this).attr("index");
            var date = $("#rcv_date_" + index).val();
            var time = $("#rcv_time_" + index).val();

            //console.log(date + " " + time);
            $(this).val(date + " " + time);
        });
    }

    function consolidate_test_date_time() {
        $(".test_datetime_fields").each(function () {
            var index = $(this).attr("index");
            var date = $("#test_date_" + index).val();
            var time = $("#test_time_" + index).val();

            //console.log(date + " " + time);
            $(this).val(date + " " + time);
        });
    }

    $("#create_project_request_form").click(function () {
        var proceed = false;
        var form_id = "0";
        var inc_date_in = "";
        var inc_date_out = "";
        var inc_time_in = "";
        var inc_time_out = "";

        proceed = IsCheckBoxesChecked("modify_value");
        if (!proceed) {
            alert("Please select requests to include in the form.");
            return;
        }

        proceed = IsCheckBoxesChecked("supply_ids");
        if (!proceed) {
            alert("Please select test supply for the form.");
            return;
        }

        form_id = $("#form_id").val();
        if (form_id == "0") {
            alert("Please select a form.");
            return;
        }

        inc_date_in = $("#inc_date_in").val();
        inc_date_out = $("#inc_date_out").val();
        inc_time_in = $("#inc_time_in").val();
        inc_time_out = $("#inc_time_out").val();

        console.log(inc_date_in);
        if (inc_date_in != "") {
            $("#IncubationIn").val(inc_date_in + " " + inc_time_in);
        }

        console.log(inc_date_out);
        if (inc_time_out != "") {
            $("#IncubationOut").val(inc_date_out + " " + inc_time_out);
        }

        consolidate_collect_date_time();
        consolidate_received_date_time();
        consolidate_test_date_time();
        consolidate_submitted_date_time()
        $("#bulk_request_form").attr("action", "/ProjectRequestPosts/CreateProjectForm");
        $("#bulk_request_form").attr("target", "_blank");
        $("#bulk_request_form").submit();
    });

    $("#add_new_project").click(function () {
        var project_name = prompt("Please enter new project:", "");
        if (project_name != null && project_name != "") {
            $.ajax({
                url: "/hlab_test_project/addnewproject",
                type: "POST",
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({ project: project_name }),
                success: function () {
                    location.reload();
                }
            });
        }
    });

    //$(".delete_request").click(function () {
    //    var request_id = $(this).val();
    //    change_accessibility("customer_id_" + request_id);
    //    change_accessibility("fname_" + request_id);
    //    change_accessibility("lname_" + request_id);
    //    change_accessibility("test_pkg_id_" + request_id);
    //    change_accessibility("payment_" + request_id);
    //    change_accessibility("sample_desc_" + request_id);
    //    change_accessibility("collect_date_" + request_id);
    //    change_accessibility("collect_time_" + request_id);
    //});

    $("#receiver_controller").change(function () {
        var receiver_id = $(this).val();
        //console.log(selected_test_pkg_id);
        $(".modify_value").each(function () {
            //console.log($(this).is(':checked'));
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                //console.log(req_id);
                $("#received_by_id_" + req_id).val(receiver_id);
            }
        });
    });

    $("#change_test_pkg").change(function () {
        var selected_test_pkg_id = $(this).val();
        //console.log(selected_test_pkg_id);
        $(".modify_value").each(function () {
            //console.log($(this).is(':checked'));
            if ($(this).is(':checked')) {                
                var req_id = $(this).val();
                //console.log(req_id);
                $("#test_pkg_id_" + req_id).val(selected_test_pkg_id);
            }
        });
    });

    $("#change_payment").change(function () {
        var selected_payment_id = $(this).val();
        //console.log(selected_payment_id);
        $(".modify_value").each(function () {
            //console.log($(this).is(':checked'));
            if ($(this).is(':checked')) {
                var req_id = $(this).val();
                //console.log(req_id);
                $("#payment_" + req_id).val(selected_payment_id);
            }
        });
    });

    $("#checkbox_control").click(function () {
        var check = false;
        if ($(this).is(":checked")) {
            check = true;
        }

        $(".modify_value").each(function () {
            $(this).prop('checked', check);
        });
    });

    $(".view_settings").click(function () {
        var form_id = $(this).val();
        $("#selected_project_form_id").val(form_id);
        $("#search_record_form").submit();
    });

    $("#delete_request").click(function () {
        var ans = confirm("Are you sure you want to delete the selected request(s)?")
        if (!ans) return;
        proceed = IsCheckBoxesChecked("modify_value");
        if (!proceed) {
            alert("Please select request(s) to delete.");
            return;
        }

        $("#bulk_request_form").attr("action", "/ProjectRequestPosts/DeleteProjectTempRequest");
        $("#bulk_request_form").submit();
    });

});

function change_accessibility(element_id) {
    if ($("#" + element_id).prop("disabled")) {
        $("#" + element_id).prop("disabled", false);
    }
    else {
        $("#" + element_id).prop("disabled", true);
    }
}