var toastshow = false;
$(document).ready(function () {
    $(".confirm").click(function () { 
        toastshow = true;
    });
    if (toastshow) {
        console.log("Tostshow");
        $("#toastContainer").toast("show");
    }
});
