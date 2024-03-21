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
function validateNumber(number, min, max, item) {
    numberObj = { Number: number, Min: min, Max: max };
    console.log(numberObj);
    console.log("ASDASDASDASDASD");
    $.ajax({
        url: '/FormValidations/CheckNumber',
        type: 'GET',
        data: { numberObj: numberObj },
        success: function (valid) {
            switch (item) {
                case "Y":
                    if (valid) {
                        validations.Year = true;
                    }
                    else {
                        validations.Year = false;
                    }
                case "M":
                    if (valid) {
                        validations.Month = true;
                    }
                    else {
                        validations.Month = false;
                    }
                case "D":
                    if (valid) {
                        validations.Day = true;
                    }
                    else {
                        validations.Day = false;
                    }
            }
            
        }
    });
}
function validateNumberInput(input, nextInput, number, maxLength, max, min) {
    if (number.toString().length == maxLength) {
        if (number > max)
            input.val(max);
        if (number < min)
            input.val(min);
        nextInput.focus()
    }
}
function validateEducationLevel(selectedValue) {
    if (selectedValue == "0")
        validations.EducationLevel = false
    else
        validations.EducationLevel = true
}
function validateEducationProgress(selectedValue) {
    if (selectedValue == "0")
        validations.EducationProgress = false
    else
        validations.EducationProgress = true
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
        validateEmail(email);
        checkRegisterButton();
    });
    $('#Email').on('blur', function () {
        showOrHideMessage($('.Email-error-message'), validations.Email, "El correo electrónico no es válido");
    });
    $('#Year').on('input', function () {
        var year = $(this).val();
        actualYear = new Date().getFullYear();

        validateNumberInput($(this), $('#Month'), year, 4, actualYear, 1000)

        var year = $(this).val();
        validateNumber(year, 1, actualYear, "Y");

        checkRegisterButton();
    });
    $('#Year').on('blur', function () {
        if ($('#Month').val != "" && $('#Day').val != "")
            showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    });
    $('#Month').on('input', function () {
        var month = $(this).val();
        var actualMonth = new Date().getMonth() + 1;

        if ($('#Year').val() == getFullYear())
            validateNumberInput($(this), $('#Day'), month, 2, actualMonth, 1)
        else
            validateNumberInput($(this), $('#Day'), month, 2, 12, 1)

        var month = $(this).val();
        validateNumber(month,1,12,"M");

        checkRegisterButton();
    });
    $('#Month').on('blur', function () {
        if ($('#Year').val != "" && $('#Day').val != "")
            showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    });
    $('#Day').on('input', function () {
        var day = $(this).val();
        var actualDay = new Date().getDay();

        if ($('#Year').val() == new Date().getFullYear() && $('#Month').val() == new Date().getMonth())
            validateNumberInput($(this), $('#Day'), day, 2, actualDay, 1)
        else
            validateNumberInput($(this), $('#Day'), day, 2, 31, 1)

        var day = $(this).val();
        validateNumber(day, 1, 31, "D");

        checkRegisterButton();
    });
    $('#Day').on('blur', function () {
        showOrHideMessage($('.BirthDate-error-message'), validations.Email, "La fecha de nacimiento parece estar mal");
    });
    $('#EducationLevel').on('blur', function () {
        var edLevel = $(this).val();
        validateEducationLevel(edLevel);
        checkRegisterButton();
    });
    $('#EducationProgress').on('blur', function () {
        var edProgress = $(this).val();
        validateEducationLevel(edProgress);
        checkRegisterButton();
    });
    $('#Doc').on('change', function () {

    });
});