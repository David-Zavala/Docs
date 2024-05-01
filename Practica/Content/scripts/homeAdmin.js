$(document).ready(function () {
    $('.ButtonViewJPG').on('click', function () {
        var filePath = $(this).val();
        var fileId = $(this).attr('fileId');
        $("#selected-image-object").attr({ "src": filePath });
        $(".ButtonDownloadInModal").attr({ "value": filePath });
        $(".ButtonDeleteInModal").attr({ "value": fileId });
        $("#selected-image").removeClass('hidden');
    });
    $('#closeImageModal').on('click', function () {
        $("#selected-image").addClass('hidden');
        $("#selected-image-object").attr({ "src": "" });
        $(".ButtonDownloadInModal").attr({ "value": "" });
        $(".ButtonDeleteInModal").attr({ "value": "" });
    });
    $("#selected-image").on('click', function (e) {
        if (e.target === this) {
            $("#selected-image").addClass('hidden');
            $("#selected-image-object").attr({ "src": "" });
            $(".ButtonDownloadInModal").attr({ "value": "" });
            $(".ButtonDeleteInModal").attr({ "value": "" });
        }
    });
    //$(".ButtonDownload").on('click', function () {
    //    filePath = $(this).val();
    //    console.log(filePath);
    //    $.ajax({
    //        url: '/Doc/DownloadImage',
    //        type: 'GET',
    //        data: { filePath: filePath },
    //        success: function (file) {
    //            console.log("Imágen descargada exitosamente");
    //        },
    //        error: function () {
    //            console.log("Imágen no se pudo descargar");
    //        }
    //    });
    //});
    $(".ButtonDelete").on('click', function () {
        fileId = $(this).val();
        console.log(fileId);
        $.ajax({
            url: '/Doc/Delete',
            type: 'POST',
            data: { fileId: fileId },
            success: function (ans) {
                
                console.log("Eliminado exitosamente");
                
            },
            error: function () {
                console.log("Algo salio mal en la llamada");
            }
        });
    });
    $("#SearchFilter-Button").on('click', function () {
        filter = $('#SearchFilter').val();
        $.ajax({
            url: 'Home/HomeAdmin'
        });
        console.log(filter);
    });
    $("#SearchForm").on('submit', function () {
        filter = $('#SearchFilter').val();
        console.log(filter);
    });
});