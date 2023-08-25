$(document).ready(function () {
	ChangeUnitInCart();
});

function ChangeUnitInCart() {
    $("input.change-cart").on("change", function () {
        var quantityField = $(this);

        var id = $(quantityField).siblings(".product-id").first().text();
        var number = $(quantityField).val();
		//console.log(id, number);

        $.ajax({
            url: "/Customer/Order/UpdateCart",
            type: "POST",
            data: `number=${number}&id=${id}`,
            success: function (response) {
                // Change page cart number
                var unitPrice = $(quantityField).siblings(".product-unit-price").first().text();
                var discount = $(quantityField).siblings(".product-discount").first().text();
                var oldPrice = parseInt($(quantityField).parent().parent().siblings(".product-subtotal").first().text().replace(' VNĐ', '').replaceAll(',', ''));
                var oldTotalPrice = parseInt($("#page-cart-total-price").text().replace(' VNĐ', '').replaceAll(',', ''));

                console.log(unitPrice, oldPrice, oldTotalPrice);

                var newPrice = number * unitPrice * (1 - discount / 100);
                var newTotalPrice = oldTotalPrice + (newPrice - oldPrice);

                $(quantityField).parent().parent().siblings(".product-subtotal").first().text(newPrice.toLocaleString('en-US') + " VNĐ");
                $("#page-cart-total-price").text(newTotalPrice.toLocaleString('en-US') + " VNĐ");

                // Change header cart number
                var quantityIdName = "#cart-quantity-" + id;
                var priceIdName = "#cart-price-" + id;
                var totalIdName = "#cart-price-total";

                var headerCartQuantity = $(".total-cart-in").find(quantityIdName).first();
                var headerCartPrice = $(".total-cart-in").find(priceIdName).first();
                var headerCartTotal = $(".total-cart-in").find(totalIdName).first();

                $(headerCartQuantity).text(number.toLocaleString('en-US'));
                $(headerCartPrice).text(newPrice.toLocaleString('en-US') + " VNĐ");
                $(headerCartTotal).text(newTotalPrice.toLocaleString('en-US') + " VNĐ");
            },
            error: function (error) {
                console.error("Lỗi gọi UpdatCart API: " + error.responseText);
            },
        });
	});
}
