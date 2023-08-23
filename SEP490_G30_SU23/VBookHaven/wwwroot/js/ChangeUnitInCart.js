$(document).ready(function () {
	ChangeUnitInCart();
});

function ChangeUnitInCart() {
    $("input.change-cart").on("change", function () {
        var id = $(this).siblings(".product-id").first().text();
        var number = $(this).val();
		console.log(id, number);

        $.ajax({
            url: "/Customer/Order/UpdateCart",
            type: "POST",
            data: `number=${number}&id=${id}`,
            success: function (response) {
                var idName = "#cart-quantity-" + id;
                var headerCartQuantity = $(".total-cart-pro").find(idName).first();
                $(headerCartQuantity).text(number);
            },
            error: function (error) {
                console.error("Lỗi gọi UpdatCart API: " + error.responseText);
            },
        });
	});
}
