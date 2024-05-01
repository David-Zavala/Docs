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
    $('#closeModal').on('click', function () {
        $("#account-modal").addClass('hidden');
    });
    $("#account-modal").on('click', function (e) {
        if (e.target === this) {
            $("#account-modal").addClass('hidden');
        }
    });
    $("#LoginButton").on('click', function () {
        if ($('#LoginButton').prop('disabled') == false) {
            sendLoginRequest();
        }
    });
});
async function emailControl(email) {
    await validateEmail(email);
    checkLoginButton();
}
async function sendLoginRequest() {
    $("#charging-icon").removeClass('hidden');

    var loginData = new FormData();
    loginData.append('Email',$('#Email').val());
    loginData.append('Password',$('#Password').val());

    await $.ajax({
        url: '/Account/Login',
        type: 'POST',
        data: loginData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success == false) {
                console.log("Error en login: ", response.message);
                $("#account-modal").removeClass('hidden');
            }
            else {
                location.reload();
            }
        },
        error: function (xhr, status, error) {
            console.error('Error en el login:', error);
            $('.Register-message').show();
            $('.Register-message').text(error);
        }
    });

    $("#charging-icon").addClass('hidden');
}