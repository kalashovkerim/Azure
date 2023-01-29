$(document).ready(function () {
    const params = new URLSearchParams(window.location.search);

    var selectedBrand = "All";
    if (params.get('Name') != null) {
        selectedBrand = params.get('Name');
    }
    
    var selectedCategory = "All";
    if (params.get('Category') != null) {
        selectedCategory = params.get('Category');
    }

    $("#lblBrendSelectId").val(selectedBrand);
    $("#lblCategorySelectId").val(selectedCategory);

    $("#lblBrendSelectId").change(function () {
        selectedBrand = $("#lblBrendSelectId option:selected").text();
        window.location.href = "GetCategories?Name=" + selectedBrand + "&Category=" + "All";
        
    })
    $("#lblCategorySelectId").change(function () {
        selectedCategory = $("#lblCategorySelectId option:selected").text();
        window.location.href = "GetGoods" + "?Name=" + $("#lblBrendSelectId option:selected").text() + "&Category=" + selectedCategory;
    })
    $("#searchbtn").click(function () {
        
        if ($("#searchfield").val() != null && $("#searchfield").val() != " " && $("#searchfield").val() != "") {
            alert($("#searchfield").val());
            window.location.href = "SearchGood?request=" + $("#searchfield").val(); //оно не сробатывает так и должно быть
        }
    });
})