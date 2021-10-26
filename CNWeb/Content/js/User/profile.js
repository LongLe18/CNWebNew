$(function() {
    $("#datepicker").datepicker({
        autoclose: true,
        todayHighlight: true
    }).datepicker('update', new Date());
});
document.getElementById("select1").onclick = function() {
    document.getElementById("buy").style.display = "none";
    document.getElementById("profile-detail").style.display = "block"; // hide body div tag
    document.getElementById("chance-pw").style.display = "none";
    document.getElementById("select1").style.color = "#ee4d2d";
    document.getElementById("select2").style.color = "gray";
    document.getElementById("select4").style.color = "black";
};
document.getElementById("select4").onclick = function() {
    document.getElementById("profile-detail").style.display = "none";
    document.getElementById("buy").style.display = "block"; // hide body div tag
    document.getElementById("chance-pw").style.display = "none";
    document.getElementById("select4").style.color = "#ee4d2d";
    document.getElementById("select1").style.color = "gray";
    document.getElementById("select2").style.color = "gray";
};
document.getElementById("select2").onclick = function() {
    document.getElementById("buy").style.display = "none";
    document.getElementById("profile-detail").style.display = "none"; // hide body div tag
    document.getElementById("chance-pw").style.display = "block";
    document.getElementById("select2").style.color = "#ee4d2d";
    document.getElementById("select4").style.color = "black";
    document.getElementById("select1").style.color = "gray";
};