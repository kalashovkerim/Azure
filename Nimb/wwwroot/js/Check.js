$("#lblBrendSelectId").change(function () {
    window.location.href = window.location.href + '?' + $("#lblBrendSelectId").val();
    $(".pprice").text("200");
})
$("#countField").change(function () {
    var count = parseInt($(this).val())
    $("#total").text("Total:" + 200 * count + "$");
})