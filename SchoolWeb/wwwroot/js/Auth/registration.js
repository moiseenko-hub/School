$(document).ready(function () {
  $("#UserName").on("keyup", function () {
    const name = $("#UserName").val();
    const promise = $.get(`/api/auth/isUniq?name=${name}`);

    $("[type=submit]").attr("disabled", "disabled");
    $(".waiter").show();
	$(".error-placeholder").hide();

    promise.then(function (isUniq) {
      $(".waiter").hide();

      if (!isUniq) {
        $(".error-placeholder").show();
      } else {
        $(".error-placeholder").hide();
        $("[type=submit]").removeAttr("disabled");
      }
    });
  });
});
