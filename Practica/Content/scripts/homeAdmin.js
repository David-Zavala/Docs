$(document).ready(function () {
    $('body .dropdown-toggle').dropdown();
    $('#DocsTable').DataTable({
        
        'columnDefs': [
            {
                'searchable': false,
                'orderable': false,
                'targets': [0, 9],

            },
        ],
        layout: {
            top2Start: {
                pageLength: {
                    menu: [ 5, 10, 20, 25, 50, 100]
                }
            },
            top2End: {
                search: {
                    placeholder: 'Busqueda general'
                }
            },
            topStart: {
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        title: 'Registros - ' + GetActualDate(),
                        text: 'PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            modifier: {
                                page: 'current'
                            },
                            columns: [1,2,3,4,5,6,7,8]
                        }
                    },
                    {
                        extend: 'excelHtml5',
                        title: 'Registros - ' + GetActualDate(),
                        text: 'Excel',
                        exportOptions: {
                            modifier: {
                                page: 'current'
                            },
                            columns: [1,2,3,4,5,6,7,8]
                        }
                    }
                ]
            },
            topEnd: {
                buttons: [
                    {
                        text: 'Eliminar selecci&oacuten',
                        className: 'DeleteSelectionButton',
                        action: function (e, dt, node, config) {
                            $("#acceptDeletingModal").attr({ "option": "multiple" });
                            var ids = "";
                            $('.SelectItem').each(function (index, element) {
                                if ($(this).is(':checked')) ids += $(this).val() + "<br />";
                            });
                            $("#confirmDeletionContent").html(ids);
                            $("#confirmDeletion").removeClass("hidden");
                        }
                    }
                ]
            },
            bottomStart: 'info',
            bottomEnd: 'paging'
        },
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        initComplete: function () {
            $('#DocsTable tfoot tr').insertAfter($('#DocsTable thead'))
            this.api()
                .columns( [1,2,3,4,5,6,7,8] )
                .every(function () {
                    let column = this;
                    let title = column.footer().textContent;

                    let input = document.createElement('input');
                    input.classList.add('form-control','form-control-sm','searchForColumn');
                    input.placeholder = title;
                    column.footer().replaceChildren(input);

                    input.addEventListener('keyup', () => {
                        if (column.search() != this.value) {
                            column.search(input.value).draw();
                        }
                    });
                });
        }
    });
    var DeleteButtonActivator = 0;
    $('.SelectItem').on('change', function () {
        if (this.checked) {
            DeleteButtonActivator++;
            $('.DeleteSelectionButton').css('display','block');
        }
        else {
            DeleteButtonActivator--;
            if (DeleteButtonActivator == 0)
                $('.DeleteSelectionButton').css('display', 'none');
        }
    });
    $('.ButtonViewJPG').on('click', function () {
        var filePath = $(this).val();
        var fileId = $(this).attr('fileId');
        console.log(filePath);
        console.log(fileId);
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
    $("#AdminRole").on('change', function () {
        role = $(this).prop('checked');
        if (role) {
            $(".checkbox-label").addClass("border-green");
            $(".checkbox-label").text("Administrador");
        }
        else {
            $(".checkbox-label").removeClass("border-green");
            $(".checkbox-label").text("Usuario");
        }
        DocForm.AdminRole = role;
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
    $(".ButtonDelete").on('click', function () {
        fileId = $(this).val();
        console.log(fileId);
        $("#acceptDeletingModal").attr({ "option": "unique" });
        $("#acceptDeletingModal").attr({ "fileId": fileId });
        $("#confirmDeletionContent").html(fileId + "<br />");
        $("#confirmDeletion").removeClass("hidden");
    });
    $('.closeDeletingModal').on('click', function () {
        var modalItem = $(this).attr("fatherModal");
        $('#' + modalItem).addClass('hidden');
        $("#acceptDeletingModal").attr({ "option": "" });
        $("#acceptDeletingModal").attr({ "fileId": "" });
        $("#confirmDeletionContent").html("");
    });
    $('#rejectDeletingModal').on('click', function () {
        var modalItem = $(this).attr("fatherModal");
        $('#' + modalItem).addClass('hidden');
        $("#acceptDeletingModal").attr({ "option": "" });
        $("#acceptDeletingModal").attr({ "fileId": "" });
        $("#confirmDeletionContent").html("");
    });
    $('#acceptDeletingModal').on('click', function () {
        option = $(this).attr("option");
        if (option == "multiple") DeleteSelection();
        if (option == "unique") {
            fileId = $(this).attr("fileId");
            DeleteItem(fileId);
        }
        var modalItem = $(this).attr("fatherModal");
        $('#' + modalItem).addClass('hidden');
        $("#acceptDeletingModal").attr({ "option": "" });
        $("#acceptDeletingModal").attr({ "fileId": "" });
        $("#confirmDeletionContent").html("");
    });
    $('.ButtonDownload').on('click', function (e) {
        var file = $(this).val().substring(18);
        console.log(file);
        DownloadFile(file);
    });
});

function DeleteSelection() {
    $("#charging-icon").removeClass('hidden');

    var fileIds = [];
    $('.SelectItem').each(function (index, element) {
        if ($(this).is(':checked')) fileIds.push($(this).val());
    });

    $.ajax({
        url: '/Doc/MultipleDelete',
        type: 'POST',
        data: { fileIds: fileIds },
        success: function (ans) {
            if (ans.success == true) {
                location.reload();
                console.log("Eliminados exitosamente");
            }
            else
                console.log("Hubo un problema: " + ans.message)

            if (ans.errors.length > 0) {
                console.log("No se pudieron eliminar los siguientes: ");
                $.each(ans.errors, function (index, value) {
                    console.log(value);
                });
            }
        },
        error: function () {
            console.log("Algo salio mal en la llamada");
        }
    });

    $("#charging-icon").addClass('hidden');
}

function DeleteItem(fileId) {
    $("#charging-icon").removeClass('hidden');

    $.ajax({
        url: '/Doc/Delete',
        type: 'POST',
        data: { fileId: fileId },
        success: function (ans) {
            if (ans.success == true) {
                location.reload();
                console.log("Eliminado exitosamente");
            }
            else
                console.log("Hubo un problema: " + ans.message)
        },
        error: function () {
            console.log("Algo salio mal en la llamada");
        }
    });

    $("#charging-icon").addClass('hidden');
}

function GetActualDate() {
    var fechaActual = new Date();
    var yy = fechaActual.getFullYear();
    var mm = fechaActual.getMonth() + 1;
    var dd = fechaActual.getDate();
    var fechaFormateada = dd + " / " + mm + " / " + yy;
    return fechaFormateada;
}

function DownloadFile(fileName) {
    var url = "Data/SavedFiles/" + fileName;

    $.ajax({
        url: url,
        cache: false,
        xhr: function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 2) {
                    if (xhr.status == 200) {
                        xhr.responseType = "blob";
                    } else {
                        xhr.responseType = "text";
                    }
                }
            };
            return xhr;
        },
        success: function (data) {
            var blob = new Blob([data], { type: "application/octetstream" });

            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, fileName);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", fileName);
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);
            }
        }
    });
};