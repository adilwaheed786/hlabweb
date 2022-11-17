$(function () {

    $(".category_status_button").click(function () {        
        var statusid = $(this).val();
        var status = $("#" + statusid).val();
        console.log(status);
        if (status.toLowerCase() == "false") {
            status = "True";
            $(this).css("color","#30B136");
            $(this).css("border-color","#30B136");
        }
        else {
            status = "False";
            $(this).css("color", "#CCCECC");
            $(this).css("border-color", "#CCCECC");
        }

        $("#" + statusid).val(status);
    });

    $("#save_new_category").click(function () {
        var category_name = $("#category_name").val();
        if (category_name != "") {
            $("#new_cateogry_form").submit();
        }
        else {
            alert("Please specify a category name.");
        }
    });

    $("#save_new_package").click(function () {
        var pkg_name = $("#pkg_name").val();
        if (pkg_name != "") {
            $("#new_package_form").submit();
        }
        else {
            alert("Please specify atleast a test package name.");
        }
    });

    $("#select_category").change(function(){
        $("#select_category_form").submit();
    });

    $("#select_class").change(function(){
        $("#select_category_form").submit();
    });

    $("#addpackage").click(function(){
        var selected = $("#add_package_id").val();
        if(selected=="")
        {
            alert("Please select a test package to add.");
        }
        else    
        {
            $("#addcategorizationform").submit();
        }        
    });

    $("#deletepackage").click(function(){
        var selected = $("#delete_package_id").val();
        if(selected=="")
        {
            alert("Please select a test package to delete.");
        }
        else    
        {
            var ans = confirm("Are you sure you want to delete the selected package?");
            if (ans) {
                $("#deletecategorizationform").submit();
            }
            else {
                alert("Please select at least one parameter to proceed.");
            }
        }  
    });

    $("#submit_param").click(function () {
        if ($('.defparam:checked').length > 0)
        {
            $("#addnewparamform").submit();            
        }
    });

    $(".delete_defaultparameter").click(function () {
        var ans = confirm("Are you sure you want to delete the selected parameter?");
        if (ans) {            
            window.location.href=$(this).val();
        }  
    });
});