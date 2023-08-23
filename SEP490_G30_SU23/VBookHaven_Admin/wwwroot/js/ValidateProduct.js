﻿// Change style for invalid input
function invalidInput(inputId, feedbackId, message) {
    $(inputId).addClass("is-invalid");
    $(inputId).removeClass("is-valid");
    $(feedbackId).addClass("invalid-feedback");
    $(feedbackId).removeClass("valid-feedback");
    $(feedbackId).text(message);
}

// Change style for valid input
function validInput(inputId, feedbackId) {
    $(inputId).addClass("is-valid");
    $(inputId).removeClass("is-invalid");
    $(feedbackId).addClass("valid-feedback");
    $(feedbackId).removeClass("invalid-feedback");
    $(feedbackId).text("OK");
}

// Submit button enabled when all input valid
function validateSubmit(form) {
    var input = 0;
    var valid = 0;
    $(form).find('.validate').each(function () {
        input++;
    })
    $(form).find('.is-valid').each(function () {
        valid++;
    })
    if (input == valid) {
        /*console.log("Can submit");*/
        $(form).find('.submit').removeAttr('disabled');
    } else {
        /*console.log(input, valid);*/
        $(form).find('.submit').attr('disabled', 'disabled');
    }
}

/*-----------------------------------------------------------------------------------------------------------------*/

// Add product
$(document).on('change', '#Product_Barcode', function () {
    var barcodeInput = $(this);
    var barcode = $(this).val();
    var id = $("#Product_ProductId").val();
    $.ajax({
        url: "/Admin/Product/ValidateBarcodeAsyncAPI",
        type: "GET",
        contentType: "application/json",
        data: { barcode: barcode, id: id },
        success: function (response) {
            if (response != "") {
                invalidInput(barcodeInput, $("#Product_Barcode-feedback"), response);
            } else {
                validInput(barcodeInput, $("#Product_Barcode-feedback"));
            }
        },
        error: function (error) {
            console.error("Lỗi gọi validate barcode API: " + error.responseText);
        },
    });
});

$(document).on('change', '#Product_Name', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_Name-feedback"), "Tên sản phẩm là bắt buộc");
    } else {
        validInput(this, $("#Product_Name-feedback"));
	}
});

$(document).on('change', '#Product_Unit', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_Unit-feedback"), "Đơn vị sản phẩm là bắt buộc");
    } else {
        validInput(this, $("#Product_Unit-feedback"));
    }
});

$(document).on('change', '#Product_PurchasePrice', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_PurchasePrice-feedback"), "Giá nhập là bắt buộc");
    } else if ($(this).val() < 0) {
        invalidInput(this, $("#Product_PurchasePrice-feedback"), "Giá nhập phải không nhỏ hơn 0");
    } else {
        validInput(this, $("#Product_PurchasePrice-feedback"));
    }
});

$(document).on('change', '#Product_RetailPrice', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_RetailPrice-feedback"), "Giá lẻ là bắt buộc");
    } else if ($(this).val() < 0) {
        invalidInput(this, $("#Product_RetailPrice-feedback"), "Giá lẻ phải không nhỏ hơn 0");
    } else {
        validInput(this, $("#Product_RetailPrice-feedback"));
    }
});

$(document).on('change', '#Product_RetailDiscount', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_RetailDiscount-feedback"), "Chiết khấu lẻ là bắt buộc");
    } else if ($(this).val() < 0) {
        invalidInput(this, $("#Product_RetailDiscount-feedback"), "Chiết khấu lẻ phải không nhỏ hơn 0");
    } else if ($(this).val() > 100) {
        invalidInput(this, $("#Product_RetailDiscount-feedback"), "Chiết khấu lẻ phải không vượt quá 100");
    } else {
        validInput(this, $("#Product_RetailDiscount-feedback"));
    }
});

$(document).on('change', '#Product_WholesalePrice', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_WholesalePrice-feedback"), "Giá sỉ là bắt buộc");
    } else if ($(this).val() < 0) {
        invalidInput(this, $("#Product_WholesalePrice-feedback"), "Giá sỉ phải không nhỏ hơn 0");
    } else {
        validInput(this, $("#Product_WholesalePrice-feedback"));
    }
});

$(document).on('change', '#Product_WholesaleDiscount', function () {
    if ($(this).val() == "") {
        invalidInput(this, $("#Product_WholesaleDiscount-feedback"), "Chiết khấu sỉ là bắt buộc");
    } else if ($(this).val() < 0) {
        invalidInput(this, $("#Product_WholesaleDiscount-feedback"), "Chiết khấu sỉ phải không nhỏ hơn 0");
    } else if ($(this).val() > 100) {
        invalidInput(this, $("#Product_WholesaleDiscount-feedback"), "Chiết khấu sỉ phải không vượt quá 100");
    } else {
        validInput(this, $("#Product_WholesaleDiscount-feedback"));
    }
});

// Warning when selling price is lower than buying price
function warningPrice() {
    var purchasePrice = $("#Product_PurchasePrice").val();
    var retailPrice = $("#Product_RetailPrice").val() * (1 - $("#Product_RetailDiscount").val() / 100);
    var wholesalePrice = $("#Product_WholesalePrice").val() * (1 - $("#Product_WholesaleDiscount").val() / 100);

    if ($("#Product_PurchasePrice").hasClass("is-valid")
        && $("#Product_RetailPrice").hasClass("is-valid")
        && $("#Product_RetailDiscount").hasClass("is-valid")
        && $("#Product_WholesalePrice").hasClass("is-valid")
        && $("#Product_WholesaleDiscount").hasClass("is-valid")) {

        //console.log(purchasePrice, retailPrice, wholesalePrice);

        if (retailPrice <= purchasePrice && wholesalePrice <= purchasePrice) {
            $("#warningPrice").text("Giá lẻ và giá sỉ đang lớn hơn giá nhập");
        } else if (retailPrice <= purchasePrice) {
            $("#warningPrice").text("Giá lẻ đang lớn hơn giá nhập");
        } else if (wholesalePrice <= purchasePrice) {
            $("#warningPrice").text("Giá sỉ đang lớn hơn giá nhập");
        } else {
            $("#warningPrice").text("");
        }
    } else {
        $("#warningPrice").text("");
    }
}

$(document).ready(function () {
    warningPrice();
})

$(document).on('change', '#Product_PurchasePrice, #Product_RetailPrice, #Product_RetailDiscount, #Product_WholesalePrice, #Product_WholesaleDiscount', function () {
    warningPrice();
});

/*-----------------------------------------------------------------------------------------------------------------*/

// Add author
$(document).on('change', '#authorName', function () {
    console.log("validate author");
    if ($(this).val() == "") {
        invalidInput(this, $("#authorName-feedback"), "Tên tác giả là bắt buộc");
    } else {
        validInput(this, $("#authorName-feedback"));
    }
});

//-----------------------------------------------------------------------------------------------------------------

// Validate form
$(document).on('keyup click', '.addForm', function () {
    validateSubmit(this);
})

// Set submit button disable
$(document).ready(function () {
    $('.add.submit').attr('disabled', 'disabled');
})