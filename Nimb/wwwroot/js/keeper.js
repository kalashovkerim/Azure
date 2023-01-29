$(document).ready(function () {

    const params = new URLSearchParams(window.location.search);

    if (params.get('Id') != null) {
        selectedID = params.get('Id');
    }

    $("#suppselectid").val(selectedID);

    $(".confirmbtn").click(function () {
        if ($("#suppselectid").val() != null) {
            window.location.href = "AddProduct?Id=" + $("#suppselectid").val();
        }
    });
})