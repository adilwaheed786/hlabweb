
function GetApiData(url, ProcessJasonDataFunction) {
    //console.log("URL:" +url );
    if (url != "") {
        $.ajax({
            url: url,
            method: "GET",
            success: ProcessJasonDataFunction
        });
    }
}

function ExecutePostRequest(url_input, post_json_data_input, ProcessJasonDataFunction) {
    $.ajax({
        type: "post",
        url: url_input,
        data: post_json_data_input,
        xhrFields: {
            withCredentials: true
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: ProcessJasonDataFunction,
        error: function (request, status, error) {
            alert("Failed submitting request, please check console for details.");
            console.log("STATUS: " + request.status);
            console.log("RESPONSE: " + request.responseText);
            console.log("ERROR: " + request.statusText);
        }
    });    
}
