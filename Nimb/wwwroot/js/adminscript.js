var currentitemid;

$("tr").click(function () {
    if (this.id != "headtr" && !(this.classList.contains('selected'))) {
        $(this).parent().children().removeClass('selected')
        $(this).addClass('selected');
        currentitemid = this.id;
    }
    else if (this.id != "headtr" && this.classList.contains('selected')) {
        $(this).removeClass('selected');
        currentitemid = null;
    }
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

