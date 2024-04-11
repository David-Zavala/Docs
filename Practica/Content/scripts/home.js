var validations = { Name: false, Email: false, Year: false, Month: false, Day: false, EducationLevel: false, EducationProgress: false, Doc: false };
var DocForm = {
    Name: "",
    Email: "",
    Year: -1,
    Month: -1,
    Day: -1,
    EducationLevel: "0",
    EducationProgress: "0",
    Doc: null,
    FileExtension: ""
}
function changeValidationColor(item, validation) {
    if (validation) {
        item.css('border-color', 'green');
    }
    else {
        item.css('border-color', 'red');
    }
}
function checkRegisterButton() {
    if (validations.Name && validations.Email && validations.Year && validations.Month && validations.Day && validations.Doc && validations.EducationLevel && validations.EducationProgress)
        $('#RegisterDocButton').prop('disabled', false);
    else
        $('#RegisterDocButton').prop('disabled', true);
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
function validateNumber(number, min, max, item) {
    numberObj = { Number: number, Min: min, Max: max };
    $.ajax({
        url: '/FormValidations/CheckNumber',
        type: 'GET',
        data: { numberObj: numberObj },
        success: function (valid) {
            switch (item) {
                case "Y":
                    if (valid) {
                        validations.Year = true;
                        DocForm.Year = number;
                    }
                    else {
                        validations.Year = false;
                        DocForm.Year = -1;
                    }
                    break;
                case "M":
                    if (valid) {
                        validations.Month = true;
                        DocForm.Month = number;
                    }
                    else {
                        validations.Month = false;
                        DocForm.Month = -1;
                    }
                    break;
                case "D":
                    if (valid) {
                        validations.Day = true;
                        DocForm.Day = number;
                    }
                    else {
                        validations.Day = false;
                        DocForm.Day = -1;
                    }
                    break;
            }
            
        }
    });
}
function validateFocusChange(input, nextInput, number, maxLength, max, min) {
    if (number.toString().length >= maxLength) {
        if (number > max)
            input.val(max);
        if (number < min)
            input.val(min);
        if (nextInput != null)
            nextInput.focus();
    }
}
function validateEducationLevel(selectedValue) {
    if (selectedValue == "0") {
        validations.EducationLevel = false;
        DocForm.EducationLevel = "0";
    }
    else {
        validations.EducationLevel = true;
        DocForm.EducationLevel = selectedValue;
    }
}
async function validateEdLevelOtherInput(edLevel) {
    await $.ajax({
        url: '/FormValidations/CheckAlphabeticInput',
        type: 'GET',
        data: { str: edLevel },
        success: function (valid) {
            if (valid == 1) {
                validations.EducationLevel = true;
                DocForm.EducationLevel = edLevel;
            }
            else {
                validations.EducationLevel = false;
                DocForm.EducationLevel = "0";
            }
            changeValidationColor($('#EducationLevel'), validations.EducationLevel);
            changeValidationColor($('#OtherOption'), validations.EducationLevel);
        }
    });
}
function validateEducationProgress(selectedValue) {
    if (selectedValue == "0") {
        validations.EducationProgress = false;
        DocForm.EducationProgress = "0";
    }
    else {
        validations.EducationProgress = true;
        DocForm.EducationProgress = selectedValue;
    }
}
async function validateDoc(doc) {
    if (doc != undefined) {
        var formData = new FormData();
        formData.append('doc', doc);

        await $.ajax({
            url: '/FormValidations/CheckDoc',
            type: 'GET',
            data: formData,
            processData: false,
            contentType: false,
            success: function (valid) {
                if (valid) {
                    validations.Doc = true;
                    DocForm.Doc = doc;
                }
                else {
                    validations.Doc = false;
                    DocForm.Doc = null;
                }
            }
        });
    }
    else {
        validations.Doc = false;
        DocForm.Doc = null;
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
    $('#Day').on('input', function () {
        var day = $(this).val();
        var actualDay = new Date().getDay();

        if ($('#Year').val() == new Date().getFullYear() && $('#Month').val() == new Date().getMonth())
            validateFocusChange($(this), $('#Month'), day, 2, actualDay, 1)
        else
            validateFocusChange($(this), $('#Month'), day, 2, 31, 1)

        var day = $(this).val();
        validateNumber(day, 1, 31, "D");

        checkRegisterButton();
    });
    $('#Day').on('blur', function () {
        if ($('#Year').val() != "" && $('#Month').val() != "") {
            changeValidationColor($('#Day'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Month'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Year'), (validations.Year && validations.Month && validations.Day));
        }
    }); 
    $('#Month').on('input', function () {
        var month = $(this).val();
        var actualMonth = new Date().getMonth() + 1;

        if ($('#Year').val() == new Date().getFullYear())
            validateFocusChange($(this), $('#Year'), month, 2, actualMonth, 1)
        else
            validateFocusChange($(this), $('#Year'), month, 2, 12, 1)

        var month = $(this).val();
        validateNumber(month,1,12,"M");

        checkRegisterButton();
    });
    $('#Month').on('blur', function () {
        if ($('#Year').val() != "" && $('#Day').val() != "") {
            changeValidationColor($('#Day'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Month'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Year'), (validations.Year && validations.Month && validations.Day));
        }
    });
    $('#Year').on('input blur', function () {
        var year = $(this).val();
        actualYear = new Date().getFullYear();

        validateFocusChange($(this), null, year, 4, actualYear, 1000)

        var year = $(this).val();
        validateNumber(year, 1, actualYear, "Y");

        checkRegisterButton();
    });
    $('#Year').on('blur', function () {
        if ($('#Month').val() != "" && $('#Day').val() != "") {
            changeValidationColor($('#Day'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Month'), (validations.Year && validations.Month && validations.Day));
            changeValidationColor($('#Year'), (validations.Year && validations.Month && validations.Day));
        }
    });
    $('#EducationLevel').on('change', function () {
        var edLevel = $(this).val();
        if (edLevel != "AddOther") {
            $('#OtherOption').hide();
            validateEducationLevel(edLevel);
            checkRegisterButton();
            changeValidationColor($('#EducationLevel'), validations.EducationLevel);
        }
        else {
            $('#OtherOption').show();
        }
    });
    $('#OtherOption').on('input', function () {
        var option = $(this).val();
        edLevelControl(option);
    });
    $('#OtherOption').on('blur', function () {
        changeValidationColor($('#EducationLevel'), validations.EducationLevel);
        changeValidationColor($('#OtherOption'), validations.EducationLevel);
    });
    $('#EducationProgress').on('change', function () {
        var edProgress = $(this).val();
        validateEducationProgress(edProgress);
        checkRegisterButton();
        changeValidationColor($('#EducationProgress'), validations.EducationProgress);
    });
    $('#Doc').on('change', function () {
        var doc = this.files[0];
        DocForm.FileExtension = $(this).val().split('\\').pop().split('.').pop();
        docControl(doc);
    });
    $('#DeleteDoc').on('click', function () {
        var fileInput = $('#Doc');
        fileInput.val('');
        validations.Doc = false;
        DocForm.Doc = null;
        DocForm.FileExtension = "";
        checkRegisterButton();
    });
    $('#RegisterDocButton').on('click', function () {
        $('#RegisterDocButton').prop('disabled', true);
        sendRegisterRequest();
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
async function edLevelControl(edLevel) {
    await validateEdLevelOtherInput(edLevel);
    checkRegisterButton();
}
async function docControl(doc) {
    await validateDoc(doc);
    checkRegisterButton();
    changeValidationColor($('#Doc'), validations.Doc);
}
function sendRegisterRequest() {
    var registerMessage = "";

    var docData = new FormData();
    docData.append('Name', DocForm.Name);
    docData.append('Email', DocForm.Email);
    docData.append('Year', DocForm.Year);
    docData.append('Month', DocForm.Month);
    docData.append('Day', DocForm.Day);
    docData.append('EducationLevel', DocForm.EducationLevel);
    docData.append('EducationProgress', DocForm.EducationProgress);
    docData.append('FileExtension', DocForm.FileExtension);
    docData.append('Doc', DocForm.Doc)

    $.ajax({
        url: '/Doc/Register',
        type: 'POST',
        data: docData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success == false) {
                console.log("Error en el registro del documento: ", reponse.message);
                $('.Register-message').show();
                $('.Register-message').text(response.message);
            }

            if (response.success == true) {
                $('.Register-message').show();
                $('.Register-message').text(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error en la solicitud de registro:', error);
            $('.Register-message').show();
            $('.Register-message').text(error);
        }
    });
}