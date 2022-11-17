$(function () {
    $(".number_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
    });

    $(".month_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
        if (this.value > 12) {
            $(this).val("");
        }
    });

    $(".input_time").focus(function (e) {
        $(this).select();
    });

    $(".day_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }

        if (parseInt(this.value) > 31) {           
            $(this).val("");
        }
    });

    $(".hour_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
        if (this.value > 23) {
            $(this).val("");
        }
    });

    $(".minute_only").keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
        if (this.value > 59) {
            $(this).val("");
        }
    });

    $('.decimal_only').keyup(function () {
        var val = $(this).val();
        if (isNaN(val)) {
            val = val.replace(/[^0-9\.]/g, '');
            if (val.split('.').length > 2)
                val = val.replace(/\.+$/, "");
        }
        $(this).val(val);
    });

    $(".date_only").keyup(function (e) {
        var timestamp = Date.parse($(this).val())
        if (isNaN(timestamp) == false) {
            alert("Invalid Date Format!");
            $(this).val("");
        }
    });

    $("#water_chem_upload").change(function (evt) {
        var f = evt.target.files[0];
        var output = [];
        $("#upload_result").html("");
        $("#upload_details").html("");
        if (f) {
            var file_name_details = f.name.split(".");
            if (file_name_details[1].toLowerCase() == "csv") {
                var r = new FileReader();
                r.onload = function (e) {
                    var contents = e.target.result;
                    $("#upload_details").html("<b>File Name:</b> " + f.name + "<br />" + "<b>File Type:</b> " + f.type + "<br />" + "<b>File Size:</b> " + f.size + " bytes <br />");

                    var lines = contents.split("\n");
                    for (var i = 0; i < lines.length; i++) {
                        output.push("<tr><td>" + lines[i].split(",").join("</td><td>") + "</td></tr>");
                    }
                    output = "<table class='table table-striped' style='width:100%;'>" + output.join("") + "</table>";
                    $("#upload_result").html(output);
                }
                r.readAsText(f);
                $("#upload_result").html(output);
            }
            else {
                $("#upload_details").html("<span style='color:red;font-weight:bold;'>File " + f.name +" is not in csv format!</span>");
            }

        } else {
            alert("Failed to load file");
        }
    });

    //$(".hlab_date_picker").datepicker();

    $(".clear_button").click(function () {
        $(".clear_field").val("");
    });

    $(".validatecoupon").focusout(function () {
        var coupon = $(this).val();
        var customerid = $("#customer_select").val();       
        if (customerid != "") {
            if (coupon != "") {
                //do something here
                $.ajax({
                    url: "/hlab_customer_api/validatecoupon?coupon=" + coupon + "&customerid=" + customerid,
                    method: "GET",
                    success: function (data) {
                        console.log("result: " + data);
                        if (!data) {
                            alert("Invalid Coupon Number!");
                            $(this).val("");
                        }
                    }
                });
            }
        }
        else {
            alert("Please select a customer!");
        }
    });   
});

function isStringValueNotEmpty(value) {
    if (value == " " || value == "") {
        return false;
    }
    return true;
}

function ClearTableRows(tableid) {
    $("#" + tableid + " > tbody").html("");
}

function GetCurrentUser() {
    var current_user = $("#current_user").html();
    //RefreshPageiFNoLoginUser();
    return current_user;
}

function GetCurrentDate() {
    return new Date().toLocaleDateString();
}

function RefreshPageiFNoLoginUser() {
    var current_user = $("#current_user").html();
    if (current_user == "" || current_user == " ") {
        location.reload();
    }
}

function formatDate(input_date) {
    var this_date = new Date(input_date);
    var day = this_date.getDate();
    var month = this_date.getMonth();
    month += 1;  // JavaScript months are 0-11
    var year = this_date.getFullYear();
    return month + " / " + day + " / " + year;
}

function CloseModalDialogBox(modal_id) {
    $("#" + modal_id).hide();
}

function OpenModalDialogBox(modal_id) {
    $("#" + modal_id).show();   
}

function IsCheckBoxesChecked(classname = "") {
    var proceed = false;
    $("." + classname).each(function () {
        if ($(this).is(':checked')) {
            proceed = true;
        }
    });
    return proceed;
}

function foramtTime(time, seperator, useSeconds, useCap) {
    useSeconds = useSeconds === undefined ? false : useSeconds;
    useCap = useCap === undefined ? true : useCap;
    seperator = seperator === undefined ? ':' : seperator;
    if (time !== '') {
        time = time.toString();
        time = time.replace(/\D/g, '');
        if (useCap) {
            time = time.substring(0, useSeconds ? 6 : 4);
        }

        switch (time.length) {
            case 0:
                time = '0000' + (useSeconds ? '00' : '');
                break;
            case 1:
                time = '0' + time + '00' + (useSeconds ? '00' : '');
                break;
            case 2:
                time = time + '00' + (useSeconds ? '00' : '');
                break;
            case 3:
                time = '0' + time + (useSeconds ? '00' : '');
                break;
            case 4:
                time = time + (useSeconds ? '00' : '');
                break;
            case 5:
                time = useCap ? '0' + time : time;
                break;
            case 6:
                break;
        }
        if (!useSeconds) {
            time = ((parseInt(time.substring(0, time.length - 2), 10) > 23 && useCap) ? '23' : time.substring(0, time.length - 2)) + (parseInt(time.substring(time.length - 2, time.length), 10) > 59 ? '59' : time.substring(time.length - 2, time.length));
        } else {
            time = ((parseInt(time.substring(0, time.length - 4), 10) > 23 && useCap) ? '23' : time.substring(0, time.length - 4)) + (parseInt(time.substring(time.length - 4, time.length - 2), 10) > 59 ? '59' : time.substring(time.length - 4, time.length - 2)) + (parseInt(time.substring(time.length - 2, time.length), 10) > 59 ? '59' : time.substring(time.length - 2, time.length));
        }
        if (!useSeconds) {
            time = time.substring(0, time.length - 2) + seperator + time.substring(time.length - 2, time.length);
        } else {
            time = time.substring(0, time.length - 4) + seperator + time.substring(time.length - 4, time.length - 2) + seperator + time.substring(time.length - 2, time.length);
        }
    }
    return time;
}

function toStandardTime(militaryTime) {
    militaryTime = militaryTime.split(':');
    if (militaryTime[0].charAt(0) == 1 && militaryTime[0].charAt(1) > 2) {
        //console.log((militaryTime[0] - 12) + ':' + militaryTime[1] + ':' + militaryTime[2] + ' P.M.');
        return (militaryTime[0] - 12) + ':' + militaryTime[1] + ':' + militaryTime[2] + ' P.M.';
    } else {
        //console.log(militaryTime.join(':') + ' A.M.');
        return militaryTime.join(':') + ' A.M.';
    }
}

function checkIframeLoaded(iframe_id, afterLoading) {
    // Get a handle to the iframe element
    var iframe = document.getElementById(iframe_id);
    var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;

    // Check if loading is complete
    if (iframeDoc.readyState == 'complete') {
        //iframe.contentWindow.alert("Hello");
        iframe.contentWindow.onload = function () {
            alert("I am loaded");
        };
        // The loading is complete, call the function we want executed once the iframe is loaded

        alert("Done loading..");
        afterLoading;
        return;
    }

    // If we are here, it is not loaded. Set things up so we check   the status again in 100 milliseconds
    window.setTimeout(checkIframeLoaded, 100);
}

var USDCurrencyFormatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',

    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
});




