var validations = { Email:false , Password:false };

function checkLoginButton() {
    if (validations.Email && validations.Password)
        $('#LoginButton').prop('disabled', false);
    else
        $('#LoginButton').prop('disabled', true);
}

function validateEmailResponse(response) {
    if (response.isValid) {
        $('.Email-error-message').text("");
        $('.Email-error-message').hide();
        validations.Email = true;
    }
    else {
        $('.Email-error-message').text(response.errorMessage);
        $('.Email-error-message').show();
        validations.Email = false;
    }
}

function validatePassword(password) {
    if (password != "") {
        $('.Password-error-message').text(null);
        $('.Password-error-message').hide();
        validations.Password = true;
    }
    else {
        $('.Password-error-message').text("Debes ingresar la contraseña");
        $('.Password-error-message').show();
        validations.Password = false;
    }
}

$(document).ready(function () {
    $('#Email').blur(function () {
        var email = $(this).val();
        
        $.ajax({
            url: '/Account/CheckEmail',
            type: 'GET',
            data: { email: email },
            success: function (response) {
                validateEmailResponse(response);
                checkLoginButton();
            }
        });
    });
    $('#Password').blur(function () {
        var password = $(this).val();
        validatePassword(password);
        checkLoginButton();
    });
    $('#Email').on("input", function() {
        checkLoginButton();
    });
    $('#Password').on("input", function() {
        checkLoginButton();
    });
});