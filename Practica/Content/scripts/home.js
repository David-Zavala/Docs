var validations = { Name: false, Email: false, Year: false, Month: false, Day: false, EducationLevel: false, EducationProgress: false, Doc: false };
function showOrHideMessage(item, validation, message) {
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
function checkRegisterButton() {
    if (validations.Name && validations.Email && validations.Year && validations.Month && validations.Day && validations.EducationLevel && validations.EducationProgress && validations.Doc)
        $('#RegisterDocButton').prop('disabled', false);
    else
        $('#RegisterDocButton').prop('disabled', true);
} 
function validateName(name) {
    if (name != "") {
        validations.Name = true;
    }
    else {
        validations.Name = false;
    }
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
function validateNumber(number, min, max) {
    numberObj = { Number: number, Min: min, Max: max };
    $.ajax({
        url: '/FormValidations/CheckNumber',
        tyoe: 'GET',
        data: { numberObj: numberObj },
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
$(document).ready(function () {
    $('#Name').on('input', function () {
        var name = $(this).val();
        validateName(name);
        checkRegisterButton();
    });
    $('#Name').on('blur', function () {
        showOrHideMessage($('.Name-error-message'), validations.Name, "Debes ingresar un nombre");
    });
    $('#Email').on('input', function () {
        var email = $(this).val();
        console.log(email)
        validateEmail(email);
        checkRegisterButton();
    });
    $('#Email').on('blur', function () {
        showOrHideMessage($('.Email-error-message'), validations.Email, "El correo electrónico no es válido");
    });
    $('#Year').on('input', function () {
        var year = $(this).val();
        validateNumber(year,1,2024);
        checkRegisterButton();
    });
    //$('#Year').on('blur', function () {
    //    showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    //});
    $('#Month').on('input', function () {
        var month = $(this).val();
        validateName(month,1,12);
        checkRegisterButton();
    });
    //$('#Month').on('blur', function () {
    //    showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    //});
    $('#Day').on('input', function () {
        var day = $(this).val();
        validateName(day,1,31);
        checkRegisterButton();
    });
    $('#Day').on('blur', function () {
        showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    });
});