$(".goto_newuseraccountform").click(function(){
    window.location.href = "/UserAccount/UserAccountForm";
});

$(".delete_user").click(function () {
    var userid = $(this).val();
    var ans = confirm("Are you sure you want to delete the selected user?");
    if (ans)
    {
        window.location.href = "/UserAccount/DelteUserAccount?userId=" + userid;
    }    
});

$(".activate_user").click(function () {
    var userid = $(this).val();
    var ans = confirm("Activate selected user account?");
    if (ans) {
        window.location.href = "/UserAccount/ActivateAccount?userId=" + userid;
    }
});

$("#save_new_user").click(function(){
    var fname = $("#fname").val();
    var lname = $("#lname").val();
    var email = $("#email").val();
    var username = $("#username").val();
    var password = $("#password").val();
    var re_password = $("#repassword").val();
    var submit = true;
  
    if (re_password != password) {
        submit = false;
        alert("Form can't be submitted, Something is wrong with the Password!");
    }

    if (password=="") {
        submit = false;
        alert("Please provide a Password!");
    }

    if (email == "") {
        submit = false;
        alert("Please provide your Email address!");
    }

    if (fname == "") {
        submit = false;
        alert("Please provide your First Name!");
    }

    if (lname == "") {
        submit = false;
        alert("Please provide your Last Name!");
    }

    if (username == "") {
        submit = false;
        alert("Please provide your UserName!");
    }

    if (submit) {
        var ans = confirm("Save user account?");
        if (ans) {
            $("#new_user_form").submit();
        }
    }
});
