// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function DeleteImage(element) {
	var grandparent = $(element).parent().parent();

	var id = $(grandparent).find(":first").text();

	var deleteRow = "<input name='DeleteImageIdList' value='" + id + "' />"
	$("#DeleteImageList").append(deleteRow);

	$(grandparent).remove();
	//console.log(id);
}

function AddToCart() {
	$("#add-to-cart").attr("action", "/Customer/Order/AddToCart");
	$("#add-to-cart").submit();
}