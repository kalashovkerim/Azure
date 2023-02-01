var toastshow = false;
$(document).ready(function () {
    $(".confirm").click(function () { //toast ne otkrivaetsa
        toastshow = true;
    });
    if (toastshow) {
        console.log("Tostshow");
        $("#toastContainer").toast("show");
    }
});
