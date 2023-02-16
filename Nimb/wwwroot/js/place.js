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
                        <a href='/Seller/Check?id=${data}'
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Place an Order</a>
					</div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}
