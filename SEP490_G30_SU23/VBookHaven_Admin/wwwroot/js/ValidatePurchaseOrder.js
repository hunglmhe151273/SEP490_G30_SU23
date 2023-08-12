// Change style for invalid input
function invalidInput(inputId, feedbackId, message) {
    $(inputId).addClass("is-invalid");
    $(inputId).removeClass("is-valid");
    $(feedbackId).addClass("invalid-feedback");
    $(feedbackId).removeClass("valid-feedback");
    $(feedbackId).text(message);
}

// Change style for valid input
function validInput(inputId, feedbackId, message) {
    $(inputId).addClass("is-valid");
    $(inputId).removeClass("is-invalid");
    $(feedbackId).addClass("valid-feedback");
    $(feedbackId).removeClass("invalid-feedback");
    $(feedbackId).text(message);
}

// Reset all input
function resetForm(button) {
    $(button).parent().parent().find('.feedback').each(function () {
        $(this).text('');
        $(this).removeClass("valid-feedback");
        $(this).removeClass("invalid-feedback");
    });
    $(button).parent().parent().find('.important').each(function () {
        $(this).removeClass("is-valid");
        $(this).removeClass("is-invalid");
    })
}
//-----------------------------------------------------------------------------------------------------------------

//Add product
$(document).on('keyup', '#productName', function () {
    if ($(this).val().length >= 100 || $(this).val() == "") {
        invalidInput(this, $('#product-feedback'), "Tên sản phẩm không được vượt quá 100 kí tự");
    } else {
        validInput(this, $('#product-feedback'))
    }
});

$(document).on('keyup', '#barCode', function () {
    if ($(this).val().length >= 100) {
        invalidInput(this, $('#barCode-feedback'), "Tên sản phẩm không được vượt quá 100 kí tự");
    } else {
        validInput(this, $('#barCode-feedback'))
    }
});

$(document).on('keyup', '#price', function () {
    if (isNaN($(this).val()) || $(this).val() == 0) {
        invalidInput(this, $('#price-feedback'), "Giá trị phải là số tự nhiên lớn hơn 0");
    } else {
        validInput(this, $('#price-feedback'))
    }
});

// $(document).on('keyup', '#quantity', function() {
//     if (isNaN($(this).val()) || $(this).val() == 0) {
//         invalidInput(this, $('#quantity-feedback'), "Giá trị phải là số tự nhiên lớn hơn 0");
//     } else {
//         validInput(this, $('#quantity-feedback'))
//     }
// });

$(document).on('keyup', '#unit', function () {
    if ($(this).val().length >= 100 || $(this).val() == "") {
        invalidInput(this, $('#unit-feedback'), "Đơn vị không được vượt quá 100 kí tự");
    } else {
        validInput(this, $('#unit-feedback'))
    }
});
//-----------------------------------------------------------------------------------------------------------------

// Add supplier
$(document).on('keyup', '#supplier', function () {
    if ($(this).val().length >= 100) {
        invalidInput(this, $('#supplier-feedback'), "Tên nhà cung cấp không được vượt quá 100 kí tự");
    } else if ($(this).val() == "") {
        invalidInput(this, $('#supplier-feedback'), "Tên nhà cung cấp là bắt buộc");
    } else {
        validInput(this, $('#supplier-feedback'))
    }
});

$(document).on('keyup', '#phone', function () {
    if ($(this).val().length != 10) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại phải có đúng 10 kí tự");
    } else if ($(this).val() == "") {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại cấp là bắt buộc");
    } else if (isNaN($(this).val())) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại chỉ được chứa chữ số");
    } else {
        validInput(this, $('#phone-feedback'))
    }
});

$(document).on('keyup', '#description', function () {
    if ($(this).val().length >= 1000) {
        invalidInput(this, $('#description-feedback'), "Ghi chú không được vượt quá 1000 kí tự");
    } else {
        validInput(this, $('#description-feedback'))
    }
});
//-----------------------------------------------------------------------------------------------------------------

// Reset form
$(document).on('click', '.reset', function () {
    resetForm(this);
});

// Submit button enabled when all input valid
$(document).on('keyup', '.addForm', function () {
    var input = 0;
    var valid = 0;
    $(this).find('.validate').each(function () {
        input++;
    })
    $(this).find('.is-valid').each(function () {
        valid++;
    })
    if (input == valid) {
        $(this).find('.submit').removeAttr('disabled');
    } else {
        $(this).find('.submit').attr('disabled', 'disabled');
    }
})

// Set submit button disable
$('#submitProduct').attr('disabled', 'disabled');
$('#submitSupplier').attr('disabled', 'disabled');
//-----------------------------------------Validate Barcode----------------------
// Add product
$(document).on('keyup', '#barCode', function () {
    var barcodeInput = $(this);
    var barcode = $(this).val();
    var id = $("#Product_ProductId").val();
    $.ajax({
        url: "https://localhost:7123/Admin/Product/ValidateBarcodeAsyncAPI",
        type: "GET",
        contentType: "application/json",
        data: { barcode: barcode, id: id },
        success: function (response) {
            if (response != "") {
                invalidInput(barcodeInput, $("#barCode-feedback"), response);
            } else {
                validInput(barcodeInput, $("#barCode-feedback"), '');
            }
        },
        error: function (error) {
            console.error("Lỗi gọi validate barcode API: " + error.responseText);
        },
    });
});