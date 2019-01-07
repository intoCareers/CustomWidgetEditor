$("textarea").each(function () {
  this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
}).on("input", function () {
  this.style.height = "auto";
  this.style.height = (this.scrollHeight) + "px";
});
$(function () {
  $.getJSON(globals,
    function (data) {
    });
  var $input = $(".focus");
  var strLen = $input.val().length * 2;
  $input.focus();
  $input[0].setSelectionRange(strLen, strLen);

  //to dynamically load sites from state selection drop down list
  var sites = $("#sites-dropdown");
  $("#states-dropdown").change(function() {
    $.getJSON(globals[0].addNew,
      { stateAbbr: $(this).val() },
      function (response) {
        console.log(response);
        sites.empty().append($("<option></option").val(0).text("(Optional) Select a site"));
        $.each(response,
          function(index, item) {
            sites.append($("<option></option").val(item.key).text(item.value));
          });
      });
  });
});