// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
	DynamicPagination();
});

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
	//alert("Sản phẩm đã được thêm vào giỏ hàng.");
}

function BuyNow() {
	$("#add-to-cart").attr("action", "/Customer/Order/BuyNow");
	$("#add-to-cart").submit();
}

function ChangeSortingProductCust(select) {
	var sort = select.value;
	//console.log(sort);

	var addRow = `<input type='hidden' name='sort' value=${sort} />`
	$("#cust-product-search-form").append(addRow);
	$("#cust-product-search-form").submit();
}

function DynamicPagination() {
	$(".dynamic-pagination").each(function () {
		var total = Number(this.dataset.total);
		var current = Number(this.dataset.current);
		var delta = Number(this.dataset.delta);
		var formId = this.dataset.formId;

		// Previous page
		if (current > 1) {
			// Can go to previous page
			$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${current - 1})"><i class="zmdi zmdi-chevron-left"></i></a></li>`);
		}
		else {
			// Cannot go to previous page
			$(this).append(`<li><a disabled><i class="zmdi zmdi-chevron-left"></i></a></li>`);
		}

		// Left side
		if (current - delta > 3) {
			// Truncate
			$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', 1)">1</a></li>`);
			$(this).append(`<li><a disabled>...</a></li>`);
			for (let i = current - delta; i < current; ++i) {
				$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${i})">${i}</a></li>`);
			}
		}
		else {
			// Not truncate
			for (let i = 1; i < current; ++i) {
				$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${i})">${i}</a></li>`);
			}
		}

		// Current
		$(this).append(`<li class="active"><a href="#" onclick="GoToPage('${formId}', ${current})">${current}</a></li>`);

		// Right side
		if (current + delta < total - 2) {
			// Truncate
			for (let i = current + 1; i <= current + delta; ++i) {
				$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${i})">${i}</a></li>`);
			}
			$(this).append(`<li><a disabled>...</a></li>`);
			$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${total})">${total}</a></li>`);
		}
		else {
			// Not truncate
			for (let i = current + 1; i <= total; ++i) {
				$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${i})">${i}</a></li>`);
			}
		}

		// Next page
		if (current < total) {
			// Can go to next page
			$(this).append(`<li><a href="#" onclick="GoToPage('${formId}', ${current + 1})"><i class="zmdi zmdi-chevron-right"></i></a></li>`);
		}
		else {
			// Cannot go to next page
			$(this).append(`<li><a disabled><i class="zmdi zmdi-chevron-right"></i></a></li>`);
		}
	});
}

function GoToPage(formId, page) {
	//alert.(`Submit to form #${formId}. Go to page ${page}`);
	var form = $("#" + formId);
	$(form).append(`<input hidden name="page" value="${page}" />`);
	$(form).submit();
}