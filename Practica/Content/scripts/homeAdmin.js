$(document).ready(function () {
    $('#DocsTable').DataTable({
        layout: {
            top2Start: {
                pageLength: {
                    menu: [ 10, 25, 50, 100]
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
                        text: 'PDF',
                        exportOptions: {
                            modifier: {
                                page: 'current'
                            },
                            columns: [0,1,2,3,4,5,6,7]
                        }
                    },
                    {
                        extend: 'excelHtml5',
                        text: 'Excel',
                        exportOptions: {
                            modifier: {
                                page: 'current'
                            },
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
                        }
                    }
                ]
            },
            topEnd: null,
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
                .columns()
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