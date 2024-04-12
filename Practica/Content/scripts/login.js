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
            if (valid == 1) {
                validations.Email = true;
            }
            else {
                validations.Email = false;
            }
            changeValidationColor($('#Email'), validations.Email);
        }
    });
}
function validatePassword(password) {
    if (password != "") {
        validations.Password = true;
        changeValidationColor($('#Password'), validations.Password);
    }
    else {
        validations.Password = false;
        changeValidationColor($('#Password'), validations.Password);
    }
}
function changeValidationColor(item, validation) {
    if (validation) {
        item.css('border-color', 'green');
    }
    else {
        item.css('border-color', 'red');
    }
}
$(document).ready(function () {
    $('#Email').on('input', function () {
        var email = $(this).val();
        emailControl(email);
    });
    $('#Email').on('blur', function () {
        changeValidationColor($('#Email'), validations.Email);
    });
    $('#Password').on('input', function () {
        var password = $(this).val();
        validatePassword(password);
        checkLoginButton();
    });
    $('#Password').on('blur', function () {
        changeValidationColor($('#Password'), validations.Password);
    });
    $('#toRegisterButton').on('click', function () {
        $.ajax({
            url: 'Login/Register',
            type: 'GET'
        });
    });
});
async function emailControl(email) {
    await validateEmail(email);
    checkRegisterButton();
}