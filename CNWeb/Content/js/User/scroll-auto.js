quadroDeAvisos = document.getElementById("quadroDeAvisos");
lineUp = document.getElementById("lineUp");
avisos = lineUp.getElementsByClassName("avisos");

var count = 0;
var myVar = setInterval(function () {
  atualiza();
}, 2000);

function atualiza() {
  lineUp.style.marginTop = 65 * count * -1 + "px";
  count++;
  if (count == 6) count = 0;
}
