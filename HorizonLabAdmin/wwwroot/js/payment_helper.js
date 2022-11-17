function GeneratePaymentsJsonObject() {
    var counter = 0;
    var arr_hlab_payments = [];

    $.each($(".payment_amount"), function (index) {        
        payment_type_id = document.getElementsByClassName('payment_type')[counter].value;
        payment_amount = $(this).val();

        if (payment_amount != "") {
            arr_hlab_payments[counter] = {
                payment_type_id: payment_type_id,
                paid_amount: payment_amount,
                payment_date: GetCurrentDate()
            };
        }

        counter++;
    });
    return arr_hlab_payments;
}

function ComputeTotalRequestAmount() {
    var total = 0;
    var tax = $("#input_tax").val();
    if ($('.hidden_price').length > 0) {
        $(".hidden_price").each(function (index, value) {
            if ($(this).val() != "") {
                total = total + parseFloat($(this).val());
            }
        });
        total = total + (total * tax);
    }
    return total;
}