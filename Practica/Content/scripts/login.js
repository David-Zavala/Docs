var validations = { Email:false , Password:false };

function checkLoginButton() {
    if (validations.Email && validations.Password)
        $('#LoginButton').prop('disabled', false);
    else
        $('#LoginButton').prop('disabled', true);
}

function validateEmail(email) {
    $.ajax({
        url: '/FormValidations/CheckEmail',
        type: 'GET',
        data: { email: email },
        success: function (valid) {
            if (valid) {
                validations.Email = true;
            }
            else {
                validations.Email = false;
            }
        }
    });
}
function validatePassword(password) {
    if (password != "") {
        validations.Password = true;
    }
    else {
        validations.Password = false;
    }
}
function showOrHideMessage(item,validation,message) {
    if (validation) {
        item.text(null);
        item.hide();
        validations.Password = true;
    }
    else {
        item.text(message);
        item.show();
        validations.Password = false;
    }
}
$(document).ready(function () {
    $('#Email').on('input', function () {
        var email = $(this).val();
        validateEmail(email);
        checkLoginButton();
    });
    $('#Email').on('blur', function () {
        showOrHideMessage($('.Email-error-message'), validations.Email, "El correo electrónico no es válido");
    });
    $('#Password').on('input', function () {
        var password = $(this).val();
        validatePassword(password);
        checkLoginButton();
    });
    $('#Password').on('blur', function () {
        showOrHideMessage($('.Password-error-message'), validations.Password, "Debes ingresar la contraseña");
    });
    $('#toRegisterButton').on('click', function () {
        $.ajax({
            url: 'Login/Register',
            type: 'GET'
        });
    });
});