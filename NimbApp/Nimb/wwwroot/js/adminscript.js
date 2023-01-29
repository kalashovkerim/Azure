var currentitemid;

$("tr").click(function () {
    $(this).parent().children().removeClass('selected')
    $(this).addClass('selected');
    currentitemid = this.id;
    console.log(currentitemid);
});
$(".Delete").click(function () {
    $('#' + currentitemid).remove();
    if (currentitemid == null) {
        alert("Choose user!");
    }
    else {
        window.location.href = "Delete?id=" + currentitemid;
    }
});
$(".Edit").click(function () {
    if (currentitemid == null) {
        alert("Choose user!");
    }
    else {
        window.location.href = "UserEdit?id=" + currentitemid;
    }
});
$(".confirm").click(function () {
    window.location.href = "UserEditConfirm?id=" + currentitemid;
});