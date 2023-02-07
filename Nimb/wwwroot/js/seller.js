$(document).ready(function () {
    var table = $('#table_id').dataTable();
});
function assignToEventsColumns() {    //future update
    var table = $('#table_id').dataTable({
        "ajax": {
            "url": "/Seller/GetAll"
        },
        "columns": [{
            "data": "firstName"
        }, {
            "data": "lastName"
        }, {
            "data": "patronymicName"
        }, {
            "data": "number"
        }, {
            "data": "emailAddress"
        }, {
            "width": "15%"
        }]
    })
}