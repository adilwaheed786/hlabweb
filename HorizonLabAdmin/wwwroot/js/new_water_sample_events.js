$(function () {
    //$("#test_date").datepicker();    
    //$("#report_date").datepicker();    

    //$('#collect_date').datetimepicker({ dateFormat: 'dd-mm-yyy HH:MM' });
    //$('#submitt_date').datetimepicker();
    //$('#receive_date').datetimepicker();

    $("#change_customer_btn").click(function () {
        window.location = "/Customer/ViewCustomerListPage?status=true";
    });

    $("#save_water_chemical").click(function () {
        var proceed = true;
        prepare_waterchemistry_datefields();
        var datereceived = $("#datereceived").val();
        var datesampled = $("#datesampled").val();
        if (datesampled == "") {
            proceed = false;
            alert("Please provide a Date and Time Sampled");
        }

        if (datereceived == "") {
            proceed = false;
            alert("Please provide a Date and Time Received");
        }

        if (proceed) {
            $("#water_chemical_form").submit();
        }
    });

    $("#payment_option").change(function () {
        $("#transaction_type").val($(this).val());
    });

    $("#transaction_type").change(function () {
        $("#payment_option").val($(this).val());
    });

    $('.create_sample_date').focusout(function () {
        var month = $("#sample_month").val();
        var day = $("#sample_day").val();
        var year = $("#sample_year").val();
        var hour = $("#sample_hh").val();
        var minute = $("#sample_mm").val();

        $("#datesampled").val(year + "-" + month + "-" + day + " " + hour + ":" + minute + ":00.000");
    });

    $('.create_receive_date').focusout(function () {
        var month = $("#receive_month").val();
        var day = $("#receive_day").val();
        var year = $("#receive_year").val();
        var hour = $("#receive_hh").val();
        var minute = $("#receive_mm").val();

        $("#datereceived").val(year + "-" + month + "-" + day + " " + hour + ":" + minute + ":00.000");
    });

    $('#input_collecttime').focusout(function () {
        //**$("#input_submitdate").datetextentry('set_date', $("#input_collectdate").val());
        //**$("#input_receiveddate").datetextentry('set_date', $("#input_collectdate").val());        

        //$("#input_submittime").val($("#input_collecttime").val());
        //$("#input_receivedtime").val($("#input_collecttime").val());

        //**var input_submitdate = $("#input_submitdate").val();
        //**var input_receiveddate = $("#input_receiveddate").val();
        //var input_submittime = $("#input_submittime").val();
        //var input_receivedtime = $("#input_receivedtime").val();

        //**$("#submitdate").val(input_submitdate + " " + input_submittime + ":00.000");
        //$("#receivedate").val(input_receiveddate + " " + input_receivedtime + ":00.000");
    });

    $('#input_submittime').focusout(function () {
        //$("#input_receiveddate").datetextentry('set_date', $("#input_submitdate").val());
        //$("#input_testdate").datetextentry('set_date', $("#input_submitdate").val());
        $("#input_receivedtime").val($("#input_submittime").val());

        //var input_receiveddate = $("#input_receiveddate").val();
        var input_receivedtime = $("#input_receivedtime").val();
        //var input_testdate = $("#input_testdate").val();
        //$("#testdate").val(input_testdate + " " + "00:00:00.000");
        $("#receivedate").val(input_receiveddate + " " + input_receivedtime + ":00.000");
    });

    $('#input_receivedtime').focusout(function () {
        //$("#input_testdate").datetextentry('set_date', $("#input_receiveddate").val());
        var input_testdate = $("#input_testdate").val();
        $("#testdate").val(input_testdate + " " + "00:00:00.000");
    });

    $("#addTown").click(function () {
        var city = prompt("Please enter new town:", "");
        if (city != null) {
            var api_url = "/hlab_customer_api/addnewcity?newcity=" + city;
            GetApiData(api_url, AppendCityTownList);
        }
    });

    $("#addMunicipality").click(function () {
        var newmuncipality = prompt("Please enter new municipality:", "");
        if (newmuncipality != null) {
            var api_url = "/hlab_customer_api/addnewmuncipality?newmuncipality=" + newmuncipality;
            GetApiData(api_url, AppendMunicipalityList);
        }
    });

    //$("#select_town").change(function () {
    //    select_town = $(this).val();
    //    $("#form_town").val(select_town);
    //});

    function AppendCityTownList(data) {
        $('#select_town').append(`<option value="${data.city}" selected> ${data.city}</option>`);
        $('#form_town').append(`<option value="${data.city}" selected> ${data.city}</option>`);
        //$("#form_town").val(data.city);
    }

    function AppendMunicipalityList(data) {
        $('#form_municipality').append(`<option value="${data.id}" selected> ${data.rural_municipality}</option>`);
        //$("#form_town").val(data.city);
    }

    function prepare_waterchemistry_datefields() {
        var input_datereceived = $("#input_datereceived").val();
        var input_sampleddate = $("#input_sampleddate").val();
        
        var input_sampledtime = $("#input_sampledtime").val();
        var input_timereceived = $("#input_timereceived").val();

        if (input_sampleddate != "") {
            if (input_sampledtime == "") { input_sampledtime = "00:00"; }
            $("#datesampled").val(input_sampleddate + " " + input_sampledtime + ":00.000");
        }

        if (input_datereceived != "") {
            if (input_timereceived == "") { input_timereceived = "00:00"; }
            $("#datereceived").val(input_datereceived + " " + input_timereceived + ":00.000");
        }
    }

});