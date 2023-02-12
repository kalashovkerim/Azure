$(document).ready(function () {
    loadDataTable();
});


<<<<<<< HEAD
function loadDataTable() {    
=======
//var table = $('#example').DataTable({
//    rowReorder: {
//        selector: 'td:nth-child(2)'
//    },
//    responsive: true
//}

function loadDataTable() {    //future update
>>>>>>> 13d460eda53c9389fc0596da8de7b61bfcf57dae
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