$(function() {
  $.getJSON(globals,
    function(data) {
    });
  $("#widgets").on("click",
    ".js-delete",
    function() {
      var button = $(this);
      var stateAbbr = getUrlVars()["stateAbbr"];
      var data = JSON.stringify({ id: button.data("id"), stateAbbr: stateAbbr });
      bootbox.confirm("Are you sure you want to delete this form entry?",
        function(result) {
          if (result) {
            $.ajax({
              url: globals[0].deleteForm,
              method: "DELETE",
              contentType: "application/json",
              data: data,
              success: function() {
                $("#widgets").row(button.parents("tr")).remove().draw();
              }
            });
          }
        });
    });
});
// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
  var vars = [], hash;
  var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
  for (var i = 0; i < hashes.length; i++) {
    hash = hashes[i].split("=");
    vars.push(hash[0]);
    vars[hash[0]] = hash[1];
  }
  return vars;
}