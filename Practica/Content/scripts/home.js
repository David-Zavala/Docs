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
    FileName: ""
}
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
        DocForm.Name = name;
    }
    else {
        validations.Name = false;
        DocForm.Name = "";
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
                DocForm.Email = email;
            }
            else {
                validations.Email = false;
                DocForm.Email = "";
            }
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
    if (number.toString().length == maxLength) {
        if (number > max)
            input.val(max);
        if (number < min)
            input.val(min);
        nextInput.focus()
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
async function validateDoc(doc, fileName) {
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
                    DocForm.FileName = fileName;
                }
                else {
                    validations.Doc = false;
                    DocForm.Doc = null;
                    DocForm.FileName = "";
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

        validateFocusChange($(this), $('#Month'), year, 4, actualYear, 1000)

        var year = $(this).val();
        validateNumber(year, 1, actualYear, "Y");

        checkRegisterButton();
    });
    $('#Year').on('blur', function () {
        if ($('#Month').val() != "" && $('#Day').val() != "")
            showOrHideMessage($('.BirthDate-error-message'), (validations.Year && validations.Month && validations.Day), "La fecha de nacimiento parece estar mal");
    });
    $('#Month').on('input', function () {
        var month = $(this).val();
        var actualMonth = new Date().getMonth() + 1;

        if ($('#Year').val() == new Date().getFullYear())
            validateFocusChange($(this), $('#Day'), month, 2, actualMonth, 1)
        else
            validateFocusChange($(this), $('#Day'), month, 2, 12, 1)

        var month = $(this).val();
        validateNumber(month,1,12,"M");

        checkRegisterButton();
    });
    $('#Month').on('blur', function () {
        if ($('#Year').val() != "" && $('#Day').val() != "")
            showOrHideMessage($('.BirthDate-error-message'), (validations.Year && validations.Month && validations.Day), "La fecha de nacimiento parece estar mal");
    });
    $('#Day').on('input', function () {
        var day = $(this).val();
        var actualDay = new Date().getDay();

        if ($('#Year').val() == new Date().getFullYear() && $('#Month').val() == new Date().getMonth())
            validateFocusChange($(this), $('#Day'), day, 2, actualDay, 1)
        else
            validateFocusChange($(this), $('#Day'), day, 2, 31, 1)

        var day = $(this).val();
        validateNumber(day, 1, 31, "D");

        checkRegisterButton();
    });
    $('#Day').on('blur', function () {
        if ($('#Year').val()  != "" && $('#Month').val() != "")
            showOrHideMessage($('.BirthDate-error-message'), (validations.Year && validations.Month && validations.Day), "La fecha de nacimiento parece estar mal");
    }); 
    $('#EducationLevel').on('change', function () {
        var edLevel = $(this).val();
        validateEducationLevel(edLevel);
        checkRegisterButton();
        showOrHideMessage($('.EducationLevel-error-message'), validations.EducationLevel, "Debe seleccionar una opción");
    });
    $('#EducationProgress').on('change', function () {
        var edProgress = $(this).val();
        validateEducationProgress(edProgress);
        checkRegisterButton();
        showOrHideMessage($('.EducationProgress-error-message'), validations.EducationProgress, "Debe seleccionar una opción");
    });
    $('#Doc').on('change', function () {
        var doc = this.files[0];
        var fileName = $(this).val().split('\\').pop();
        docControl(doc, fileName); // Esto es necesario ya que si no ValidateDoc se terminaba de ejecutar hasta el final.
    });
    $('#RegisterDocButton').on('click', function () {
        sendRegisterRequest();
    });
});
async function docControl(doc, fileName) {
    await validateDoc(doc, fileName);
    checkRegisterButton();
    showOrHideMessage($('.Doc-error-message'), validations.Doc, "El documento parece ser invalido. Solo se aceptan archivos .jpg o .pdf");
}
function sendRegisterRequest() {
    var docData = new FormData();
    docData.append('Name', DocForm.Name);
    docData.append('Email', DocForm.Email);
    docData.append('Year', DocForm.Year);
    docData.append('Month', DocForm.Month);
    docData.append('Day', DocForm.Day);
    docData.append('EducationLevel', DocForm.EducationLevel);
    docData.append('EducationProgress', DocForm.EducationProgress);
    docData.append('FileName', DocForm.FileName);
    docData.append('Doc', DocForm.Doc)
    console.log(DocForm.FileName)
    $.ajax({
        url: '/Doc/Register',
        type: 'POST',
        data: docData,
        processData: false,
        contentType: false,
        success: function (response) {
            console.log('Documento enviado con éxito');
            console.log('Respuesta del servidor:', response);
        },
        error: function (xhr, status, error) {
            console.error('Error al enviar documento:', error);
        }
    });
}
