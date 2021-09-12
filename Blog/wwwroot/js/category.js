﻿var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $("#tblCategorias").dataTable({
        "ajax": {
            "url": "/admin/categories/getAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "50%" },
            { "data": "orden", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class='text-center'>
<a href='/admin/categories/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width: 100px'><i class='fas fa-edit'> Editar</i></a>
&nbsp;
<a onclick=Delete("/admin/categories/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width: 100px'><i class='fas fa-trash'> Borrar</i></a>
</div>
                    `
                }, "width" : "30%"
            }
        ],
        "language": {
            "emptyTable" : "No hay registros"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Está seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.api().ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            }
        });
    });
}