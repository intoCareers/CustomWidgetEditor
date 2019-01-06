$("textarea").each(function () {
  this.setAttribute("style", "height:" + (this.scrollHeight) + 'px;overflow-y:hidden;');
}).on("input", function () {
  this.style.height = "auto";
  this.style.height = (this.scrollHeight) + "px";
});
$(function () {
  var $input = $(".focus");
  var strLen = $input.val().length * 2;
  $input.focus();
  $input[0].setSelectionRange(strLen, strLen);
  $("#check-show-sites").click(function () {
    $("#checkboxes-sites").slideToggle(this.checked);
  });
});