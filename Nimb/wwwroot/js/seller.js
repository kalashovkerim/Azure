$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {    
    var table = $('#table_id').dataTable({
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
            { "data": "emailAddress", "width": "15%" }
        ]
    });
}