var currentitemid;

$("tr").click(function () {
    $(this).parent().children().removeClass('selected')
    $(this).addClass('selected');
    currentitemid = this.id;
    
});
$(".Delete").click(function () {
    $('#' + currentitemid).remove();
    window.location.href = "Delete?Id=" + currentitemid;
});
$(".Edit").click(function () {
    window.location.href = "UserEdit?Id=" + currentitemid;
});