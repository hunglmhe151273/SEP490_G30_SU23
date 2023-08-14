$(document).ready(function () {
	ValidateUnitInCartInput();
});

function ValidateUnitInCartInput() {
	$("input.validate-number").on("change keyup", function () {
		var max = parseInt($(this).attr("max"));
		var min = parseInt($(this).attr("min"));

		if ($(this).val() > max)
			$(this).val(max);

		if ($(this).val() < min)
			$(this).val(min);
	});
}