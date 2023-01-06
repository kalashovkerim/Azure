$(".confirm").click(function () {
    console.log("Karim");
  
    const urlParams = new URLSearchParams(window.location.search);
    var myid = urlParams.get('Id');
    window.location.href = "UserEditConfirm?Id=" + myid;
}); 