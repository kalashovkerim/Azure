$("tr").click(function () {
    //this.remove();
    console.log(this.id);
    $(this).parent().children().removeClass('selected')
    $(this).addClass('selected');
});