$("#lblBrendSelectId").change(function () {
    window.location.href = "GetPrice?name=" + $("#lblBrendSelectId").val();

})
$("#countField").change(function () {
    var count = parseInt($(this).val())
    $("#total").text("Total:" + 200 * count + "$");
})