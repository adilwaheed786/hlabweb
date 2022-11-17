function LoadItemstoPackageSelectList(data) {
    $("#pkg_select").empty();
    $.each(data, function (key, value) {
        $("#pkg_select").append("<option value='" + value.id + "'>" + value.lab_code + "</option>")
    });
}

function LoadTestPackageInfo(data) {
    $("#pkg_labcode").html(" " + data.lab_code);
    $("#pkg_price").html(" $" + data.price);
    $("#pkg_analysis").html(" " + data.analysis);
    $("#pkg_desc").html(" " + data.description);
}

function LoadDataToItemTable(data) {
    $("#label_customer").html("");
    $("#label_dateorder").html("");
    $("#label_receivedby").html("");
    $("#label_orderid").html("");
    $("#item_table > tbody").empty();

    $("#label_orderid").html(data[0].order_id);
    $("#label_dateorder").html(formatDate(data[0].order_date));
    $("#label_receivedby").html(data[0].received_by);
    $("#label_customer").html(data[0].first_name + " " + data[0].last_name);

    var total = 0;
    $.each(data, function (key, value) {       
        total = total + parseFloat(value.amount);
        placeorderbtn = "";
        if (IsRequestItemWaterBacteriaNoTransaction(value.pkg_class_id, value.trans_id)) { 
            placeorderbtn = CreateHtmlButtonForGeneratingWaterBacteriaRecord(value.order_id, value.order_item_id);
        }

        if (IsRequestItemWaterChemNoTransaction(value.pkg_class_id, value.trans_id)) {
            placeorderbtn = CreateHtmlButtonForGeneratingWaterChemRecord(value.order_id, value.order_item_id);
        }

        if (IsRequestItemNotMiscWithTransaction(value.pkg_class_id, value.trans_id)) {
            if (!isNaN(parseInt(value.trans_id))) {
                if (IsRequestWaterChem(value.pkg_class_id)) {
                    placeorderbtn = CreateWaterChemLink(value.trans_id);
                }
                else if (IsRequestWaterBacteria(value.pkg_class_id)) {
                    placeorderbtn = CreateWaterBacteriaLink(value.trans_id);
                }
            }
        }
        AppendItemTable(placeorderbtn, value.pkg_class, value.lab_code, value.amount)
    });

    if (total > 0) {
        gst = total * 0.05;
        total = total + gst;
        AddGstRowItemTable(gst)
        AddTotalRowItemTable(total)
    }
}

function IsRequestItemWaterBacteriaNoTransaction(pkg_class_id, trans_id) {
    if (pkg_class_id == "1" && pkg_class_id != "2" && trans_id == "0") { //if class is 1(Water Test Sample) and not 2(Misc) but transaction id is 0
        return true;
    }
    return false;
}

function CreateHtmlButtonForGeneratingWaterBacteriaRecord(request_id, request_item_id) {
    return "<a \
            type = 'button'\
            class='btn btn-sm btn-outline-success'\
            target='_blank'\
            href='/WaterBacteriaTransaction/NewWaterSampleForm?oid=" + request_id + "&oiid=" + request_item_id + "&frame=no' data-toggle='tooltip' title = 'add test sample info' >\
            <span class='glyphicon glyphicon-open-file' style = 'font-size:1.3rem;' ></span ></a > ";
        //onclick = 'window.location=\"/WaterBacteriaTransaction/NewWaterSampleForm?oid=" + value.order_id + "&oiid=" + value.order_item_id + "\"' data - toggle='tooltip' title = 'add test sample info' >\
}

function IsRequestItemWaterChemNoTransaction(pkg_class_id, trans_id) {    
    if (pkg_class_id == "4" && pkg_class_id != "2" && trans_id == "0") { //if class is 4(Water Chemical) and not 2(Misc) but transaction id is 0
        return true;
    }
    return false;
}

function CreateHtmlButtonForGeneratingWaterChemRecord(request_id, request_item_id) {
    return "<a \
            type = 'button'\
            class='btn btn-sm btn-outline-success'\
            target='_blank'\
            href='/WaterChemicalTransaction/NewWaterChemForm?oid=" + request_id + "&oiid=" + request_item_id + "' data-toggle='tooltip' title='add test sample info'>\
            <span class='glyphicon glyphicon-open-file' style = 'font-size:1.3rem;' ></span ></a > ";
//onclick = 'window.location=\"/WaterChemicalTransaction/NewWaterChemForm?oid=" + value.order_id + "&oiid=" + value.order_item_id + "\"' data-toggle='tooltip' title='add test sample info'>\
}

function IsRequestItemNotMiscWithTransaction(pkg_class_id, trans_id) {
    if (pkg_class_id != "2" && trans_id != "0") { //if package class id is not 2(Misc) but transaction id is not equal to 0
        return true;
    }
    return false;
}

function IsRequestWaterChem(pkg_class_id) {
    if (pkg_class_id == "4") {
        return true;
    }
    return false;
}

function IsRequestWaterBacteria(pkg_class_id) {
    if (pkg_class_id == "1") {
        return true;
    }
    return false;
}

function CreateWaterChemLink(trans_id) {
    return "Trans ID: <a href='/WaterChemicalTransaction/ViewWaterChemForm?transId=" + trans_id + "' target='_blank'>" + trans_id + "</a>";
}

function CreateWaterBacteriaLink(trans_id) {
    return "Trans ID: <a href='/WaterBacteriaTransaction/EditTestTransactionPage?transId=" + trans_id + "' target='_blank'>" + trans_id + "</a>";
}

function AppendItemTable(placeorderbtn, pkg_class, lab_code, amount) {
    $("#item_table > tbody").append("<tr> \
                            <td>" + placeorderbtn + "</td>\
                            <td>1</td> \
                            <td>" + pkg_class + "</td> \
                            <td>" + lab_code + "</td> \
                            <td>" + USDCurrencyFormatter.format(amount) + "</td></tr> ");
}

function AddGstRowItemTable(gst) {
    $("#item_table > tbody").append("<tr> \
                            <td></td> \
                            <td></td> \
                            <td>GST</td> \
                            <td></td> \
                            <td>" + USDCurrencyFormatter.format(gst) + "</td></tr> ");
}

function AddTotalRowItemTable(total) {
    $("#item_table > tbody").append("<tr> \
                            <td></td> \
                            <td></td> \
                            <td><b>TOTAL</b></td> \
                            <td></td> \
                            <td>" + USDCurrencyFormatter.format(total) + "</td></tr> ");
}

function validatecoupon(coupon) {
    var customerid = $("#hidden_customerid_field").val();
    if (customerid != "") {
        if (coupon != "") {
            //do something here
            OpenModalDialogBox("processing_modaldialogbox");
            $.ajax({
                url: "/hlab_customer_api/validatecoupon?coupon=" + coupon.value + "&customerid=" + customerid,
                method: "GET",
                success: function (data) {
                    console.log("result: " + data);
                    if (!data) {
                        alert("Invalid Coupon Number!");
                        CloseModalDialogBox("processing_modaldialogbox");
                        coupon.value="";
                    }
                    else {
                        CloseModalDialogBox("processing_modaldialogbox");
                    }
                }
            });
        }
    }
    else {
        alert("Please select a customer!");
    }
}

function PopulateHTMLRequestTable(data) {
    var orderitemid = "";
    var tax = $("#input_tax").val();
    orderitemid = $(".orderitem").length + 1;

    var formatted_price = "";
    var formatted_amount = "";

    //add rows to table (quantity, labcode, description, price)
    formatted_price = USDCurrencyFormatter.format(data.price);
    formatted_amount = USDCurrencyFormatter.format(data.price + (data.price * (tax / 100)));
    var raw_amount = data.price + (data.price * (tax / 100));
    if (data.analysis == "null") data.analysis = "";
    $("#order_table  > tbody").append("<tr id='" + orderitemid + "' class='orderitem'> \
                        <td>\
                            <button \
                                type='button' \
                                class='btn btn-sm btn-outline-danger removeorderitem' \
                                value='" + orderitemid + "' \
                                onclick='$(\"#" + orderitemid + "\").remove();$(\"#total_price\").html(\"0.00\");'\
                                title = 'remove'> \
                            <span class= 'glyphicon glyphicon-remove' style = 'font-size:1.3rem;'></span>\
                            </button >\
                            <input type='hidden' class='hidden_pkgid' value='" + data.id + "' id=''>\
                            <input type='hidden' class='hidden_raw_price' value='" + data.price + "' id='hidden_price_" + orderitemid + "'>\
                            <input type='hidden' class='hidden_price' value='" + raw_amount + "' id='hidden_taxpriceamount_" + orderitemid + "'>\
                        </td > \
                        <td>1</td> \
                        <td>" + data.lab_code + "</td> \
                        <td>" + data.analysis + "</td> \
                        <td><input type='text' value='' class='assign_coupon form-control form-control-sm' onfocusout='validatecoupon(this);'/></td> \
                        <td>" + formatted_price + "</td>  \
                        <td><input type='number' value='" + tax + "' onchange='changeamount(\"" + orderitemid + "\")' id='tax_" + orderitemid + "' class='number_only' style='width:50px;'/></td>  \
                        <td><span id='amt_" + orderitemid + "'>" + formatted_amount + "</span></td></tr>");
    $("#form_pkgid").val(""); //set form field of form_pkgid to blank
    $("#pkg_select").prop("selectedIndex", -1); //unselect pkg_select field
    computetotal();
}

function PopulateGSTfield(data) {
    if (data.gst_number == "0") {
        data.gst_number = "n/a"
    }
    $("#gst_number").html(data.gst_number);
}

function AppendPackageSelectList(data) {
    $("#pkg_select").empty();
    $.each(data, function (key, value) {
        $("#pkg_select").append("<option value='" + value.id + "'>" + value.lab_code + "</option>")
    });
}

function computetotal() {
    //alert("compute!");
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 2
    })

    var tax = $("#input_tax").val();

    if (tax != "" && tax != 0) {
        tax = tax / 100;
    }
    var total = 0;
    if ($('.hidden_price').length > 0) {
        $(".hidden_price").each(function (index, value) {
            if ($(this).val() != "") {
                total = total + parseFloat($(this).val());
            }
        });

        //if ($("#form_gst").val()=="0")
        //{
        //    $("#form_gst").val("1");
        //    //$("#order_table  > tbody").append("<tr id='gst'>\
        //    //    <td>\
        //    //        <button \
        //    //            type='button' \
        //    //            class='btn btn-sm btn-outline-danger' \
        //    //            onclick='$(\"#gst\").remove();$(\"#form_gst\").val(\"0\");$(\"#total_price\").html(\"0.00\");'> \
        //    //        <span class= 'glyphicon glyphicon-remove' style = 'font-size:1.3rem;'></span>\
        //    //        </button >\
        //    //    </td >\
        //    //    <td></td>\
        //    //    <td></td>\
        //    //    <td>GST Tax</td>\
        //    //    <td>" + formatter.format(total * 0.05) + "</td>\
        //    //    </tr>");
        //    $("#gst").remove();
        //    $("#order_table  > tbody").append("<tr id='gst'>\
        //        <td></td >\
        //        <td></td>\
        //        <td></td>\
        //        <td>GST Tax</td>\
        //        <td>" + formatter.format(total * 0.05) + "</td>\
        //        </tr>");
        //}

        $("#gst").remove();
        //$("#order_table  > tbody").append("<tr id='gst'>\
        //        <td></td >\
        //        <td></td>\
        //        <td></td>\
        //        <td></td>\
        //        <td></td>\
        //        <td></td>\
        //        <td>GST Tax</td>\
        //        <td>" + formatter.format(total * tax) + "</td>\
        //        </tr>");
        //total = total + (total * tax);
    }
    $("#total_price").html(formatter.format(total));
    $("#autofill_payment").val(total.toFixed(2));
}

function searchcustomer(searchfilter, searchvalue) {
    //var searchfilter = $("#form_searchfilter").val();
    //var searchvalue = $("#form_searchvalue").val();
    if (searchvalue != "" && searchfilter != "") {
        $.ajax({
            url: "/hlab_order_api/getcustomerlist?searchvalue=" + searchvalue + "&searchfilter=" + searchfilter,
            method: "GET",
            success: function (data) {
                //alert(data[0].id);
                $("#customer_select").empty();

                $.each(data, function (key, value) {
                    $("#customer_select").append("<option value='" + value.customer_id + "'>" + value.first_name + " " + value.last_name + "</option>")
                });
            }
        });
    }
    else {
        alert("Please specify a value or search filter when searching a customer!");
    }
}

function submit_order_form() {
    var selectedpkgid = "";
    var coupon = "";
    var proceed = true;
    var customerid = $("#form_customerid").val();
    console.log($(".hidden_pkgid").length);

    if ($(".hidden_pkgid").length > 0) {
        var packageList = document.getElementsByClassName('hidden_pkgid');
        var couponList = document.getElementsByClassName('assign_coupon');
        var amountList = document.getElementsByClassName('hidden_price'); //amount

        for (var i = 0; i < packageList.length; i++) {
            if (packageList[i].value != "") {
                document.getElementsByClassName('form_pkg_id_group')[i].value = packageList[i].value;
            }
            if (couponList[i] != "") {
                document.getElementsByClassName('form_coupon_group')[i].value = couponList[i].value;
            }
            if (amountList[i] != "") {
                document.getElementsByClassName('form_amount_group')[i].value = amountList[i].value;
            }
        }
    }
    else {
        alert("Can't submit this form, no items found!");
        proceed = false;
    }

    if (customerid == "") {
        proceed = false;
        alert("Please select a customer for this order!");
    }

    if (proceed) {
        var total = ComputeTotalRequestAmount();
        $("#form_totalamount").val(total.toFixed(2));
        $("#order_page_form").submit();
    }
    else {
        $(".form_pkg_id_group").val("");
    }
}
