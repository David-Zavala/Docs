$(document).ready(function () {
    $('.ButtonViewJPG').on('click', function () {
        var filePath = $(this).val().substring(1);
        $("#selected-image-object").attr({ "src": filePath });
        $("#selected-image").removeClass('hidden');
        console.log(filePath)
    });
});