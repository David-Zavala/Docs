var validations = { Name: false, Email: false, Password: false, ConfirmPassword: false, AdminRole: false };
var DocForm = {
    Name: "",
    Email: "",
    Password: "",
    ConfirmPassword: "",
    AdminRole: false,
}
function checkRegisterButton() {
    if (validations.Name && validations.Email && validations.Password && validations.ConfirmPassword && validations.AdminRole)
        $('#RegisterButton').prop('disabled', false);
    else
        $('#RegisterButton').prop('disabled', true);
}

async function validateName(name) {
    await $.ajax({
        url: '/FormValidations/CheckAlphabeticInput',
        type: 'GET',
        data: { str: name },
        success: function (valid) {
            if (valid == 1) {
                validations.Name = true;
                DocForm.Name = name;
            }
            else {
                validations.Name = false;
                DocForm.Name = "";
            }
            changeValidationColor($('#Name'), validations.Name);
        }
    });
}
function validateEmail(email) {
    $.ajax({
        url: '/FormValidations/CheckEmail',
        type: 'GET',
        data: { email: email },
        success: function (valid) {
            if (valid == 1) {
                validations.Email = true;
                DocForm.Email = email;
            }
            else {
                validations.Email = false;
                DocForm.Email = "";
            }
            changeValidationColor($('#Email'), validations.Email);
        }
    });
}
function validatePassword(password) {
    $.ajax({
        url: '/FormValidations/CheckPassword',
        type: 'GET',
        data: { password: password },
        success: function (valid) {
            if (valid == 1) {
                validations.Password = true;
                DocForm.Password = password;
            }
            else {
                validations.Password = false;
                DocForm.Password = "";
            }
            changeValidationColor($('#Password'), validations.Password);
        }
    });
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
    $('#Name').on('input', function () {
        var name = $(this).val();
        nameControl(name);
    });
    $('#Name').on('blur', function () {
        changeValidationColor($('#Name'), validations.Name);
    });
    $('#Email').on('input', function () {
        var email = $(this).val();
        emailControl(email);
    });
    $('#Email').on('blur', function () {
        changeValidationColor($('#Email'), validations.Email);
    });
    $('#Password').on('input', function () {
        var password = $(this).val();
        passwordControl(password);
    });
    $('#Password').on('blur', function () {
        changeValidationColor($('#Password'), validations.Password);
    });
    $('#ConfirmPassword').on('input', function () {
        var originalPassword = $("#Password").val();
        var confirmPassword = $(this).val();
        if (originalPassword == confirmPassword) validations.ConfirmPassword = true;
        changeValidationColor($('#ConfirmPassword'), validations.ConfirmPassword);
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
    $("#AdminRole").on('change', function () {
        console.log($(this).prop('checked'));
    });
});
async function nameControl(name) {
    await validateName(name);
    checkRegisterButton();
}
async function emailControl(email) {
    await validateEmail(email);
    checkRegisterButton();
}
async function passwordControl(password) {
    await validatePassword(password);
    checkLoginButton();
}
async function sendRegisterRequest() {
    $("#charging-icon").removeClass('hidden');

    var loginData = new FormData();
    loginData.append('Email', $('#Email').val());
    loginData.append('Password', $('#Password').val());

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