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
                $(button.parents("tr")).remove();
              }
            });
          }
        });
    });
  $("#current-modal").on("show.bs.modal",
    function (event) {
      var button = $(event.relatedTarget);
      var url = globals[0].statesDropDown + "/" + button.data("id") + "?stateAbbr=" + button.data("stateabbr");
      var modal = $(this);
      modal.find(".modal-body").hide().load(url).fadeIn("slow");
    });
});

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
  var vars = [], hash;
  var hashes = window.location.href.slice(window.location.href.indexOf("?") + 1).split("&");
  for (var i = 0; i < hashes.length; i++) {
    hash = hashes[i].split("=");
    vars.push(hash[0]);
    vars[hash[0]] = hash[1];
  }
  return vars;
}