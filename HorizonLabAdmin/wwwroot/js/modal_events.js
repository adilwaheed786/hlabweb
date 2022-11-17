$(".open_modal").click(function () {
    var modal_id = $(this).val();
    $("#" + modal_id).show();
    
});

$(".close_modal").click(function () {
    var modal_id = $(this).val();
    $("#" + modal_id).hide();
});

/*
$(window).click(function (event) {
    alert(event.target.id); 
    alert(event.target.className); 
});
*/