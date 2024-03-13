var validations = { Email:0 , Password:0 };

function checkValidationMessages() {
    $('#validation-summary').find(':empty').hide();
    $('#validation-summary').not(':empty').show();

    $('.field-validation-error').find(':empty').hide();
    $('.field-validation-error').not(':empty').show();
}
$(document).ready(function () {

    $('#Email').blur(function () {
        var email = $(this).val();
        
        $.ajax({
            url: '/Account/CheckEmail',
            type: 'GET',
            data: { email: email },
            success: function (response) {
                console.log(response);
                if (response.isValid) {
                    $('#Email-error').text(null);
                    console.log("Valido :)");
                }
                else {
                    $('#Email-error').text(response.errorMessage);
                    console.log("Invalido :(");
                }
            }
        });

        checkValidationMessages();
    });
    $('#Password').blur(function () {
        var password = $(this).val();
        if (password != null | "")
            $('#Password-error').text(null);
        else
            $('#Password-error').text(response.errorMessage);
        //$.ajax({
        //    url: '/Account/CheckPassword',
        //    type: 'GET',
        //    data: { email: email },
        //    success: function (respone) {
        //        if (response.isValid) {
        //            $('#Email-error').text(null);
        //        }
        //        else {
        //            $('#Email-error').text(response.errorMessage);
        //        }
        //    }
        //});

        checkValidationMessages();
    });
});