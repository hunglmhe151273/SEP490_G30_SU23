// Change style for invalid input
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
        $(form).find('.submit').removeAttr('disabled');
    } else {
        $(form).find('.submit').attr('disabled','disabled');
    }
}
//-----------------------------------------------------------------------------------------------------------------

//Add product
$(document).on('keyup', '#productName', function () {
    if ($(this).val().length >= 20 || $(this).val() == "") {
        invalidInput(this, $('#product-feedback'), "Tên sản phẩm không được vượt quá 20 kí tự");
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

$(document).on('keyup', '#quantity', function () {
    if (isNaN($(this).val()) || $(this).val() == 0) {
        invalidInput(this, $('#quantity-feedback'), "Giá trị phải là số tự nhiên lớn hơn 0");
    } else {
        validInput(this, $('#quantity-feedback'))
    }
});

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
    if (isNaN($(this).val())) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại chỉ được chứa chữ số");
    } else if ($(this).val() == "") {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại cấp là bắt buộc");
    } else if ($(this).val().length != 10) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại phải có đúng 10 kí tự");
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

// Add payment
$(document).on('keyup', '#paid', function () {
    var debt = $('#debt').text()
    var paid = $(this).val()
    if (parseFloat(paid) > debt) {
        $('#paid').val(debt);
        validInput(this, $('#paid-feedback'))
    } else if ($(this).val() == "") {
        invalidInput(this, $('#paid-feedback'), "Số tiền không để trống");
    } else if (isNaN($(this).val())) {
        invalidInput(this, $('#paid-feedback'), "Số tiền chỉ được chứa chữ số");
    } else if ($(this).val() < 1000) {
        invalidInput(this, $('#paid-feedback'), "Số tiền không hợp lệ");
    } else {
        validInput(this, $('#paid-feedback'))
    }
})

$(document).on('keyup change', '#paid-date', function () {
    if ($(this).val() == "") {
        invalidInput(this, $('#paid-date-feedback'), "Ngày không để trống");
    } else {
        validInput(this, $('#paid-date-feedback'))
    }
})
//-----------------------------------------------------------------------------------------------------------------

// Add address
$(document).on('keyup', '#name', function () {
    if ($(this).val().length >= 30) {
        invalidInput(this, $('#name-feedback'), "Tên không được vượt quá 30 kí tự");
    } else {
        validInput(this, $('#name-feedback'))
    }
});

$(document).on('keyup', '#cus-phone', function () {
    if (isNaN($(this).val())) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại chỉ được chứa chữ số");
    } else if ($(this).val() == "") {
        validInput(this, $('#phone-feedback'))
    } else if ($(this).val().length != 10) {
        invalidInput(this, $('#phone-feedback'), "Số điện thoại phải có đúng 10 kí tự");
    } else {
        validInput(this, $('#phone-feedback'))
    }
});

$(document).on('keyup', '#address', function () {
    if ($(this).val().length >= 100) {
        invalidInput(this, $('#address-feedback'), "Tên không được vượt quá 100 kí tự");
    } else if ($(this).val() == "") {
        invalidInput(this, $('#address-feedback'), "Địa chỉ không được rỗng");
    } else {
        validInput(this, $('#address-feedback'))
    }
});

//-----------------------------------------------------------------------------------------------------------------
// Reset form
$(document).on('click', '.reset', function () {
    resetForm(this);
});

// Validate form
$(document).on('keyup click', '.addForm', function () {
    validateSubmit(this);
})

// Set submit button disable
$(document).ready(function () {
    $('#submitProduct').attr('disabled','disabled');
    $('#submitSupplier').attr('disabled','disabled');
})