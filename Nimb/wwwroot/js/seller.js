var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {    
    var dataTable = $('#table_id').DataTable({
        rowReorder: {
            selector: 'td:nth-child(2)'
        },
        responsive: true,
        "ajax": {
            "url": "/Seller/GetAll"
        },
        "columns": [
            { "data": "firstName", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            { "data": "patronymicName", "width": "15%" },
            { "data": "number", "width": "15%" },
            { "data": "emailAddress", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href='/Seller/EditUser?id=${data}'
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a  onClick=Delete('/Seller/Delete/${data}')
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        $('#table_id').DataTable().ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}


