$(function () {
    $(".status_checkbox").click(function () {
        var id = parseInt($(this).val());
        var status = $(this).prop('checked');

        $.ajax({
            url: "/hlab_settings_api/updateemailtemplate?templateid=" + id + "&status=" + status,
            method: "GET",
            success: function (data) {
                if (status) {
                    $("#status_label_" + id).html("Enabled");
                }
                else {
                    $("#status_label_" + id).html("Disabled");
                }
            }
        });
    });
});