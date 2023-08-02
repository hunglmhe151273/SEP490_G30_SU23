$('.single-select').select2({
    theme: 'bootstrap4',
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: $(this).data('placeholder'),
    allowClear: Boolean($(this).data('allow-clear')),
});

// prevent: "e", "=", ",", "-", "." 
$(document).on('keydown', 'input[type=number]', function (e) {
    if ([69, 187, 188, 189, 190].includes(e.keyCode)) {
        e.preventDefault();
    }
})

// Update invoice function
var updateInvoice = function () {
    var num, discount, price, sum, markup;
    let totalPrice = 0,
        totalPay = 0,
        count = 0
    vat = 0;


    //default values
    $('#totalPrice').text('0');
    $('#totalPay').text('0');

    // check valid VAT
    var vatValue = $('#totalVat').val();
    if (isNaN(vatValue) || vatValue >= 100 || !isNotNullOrEmpty(vatValue)) {
        $('#totalVat').val(0);
        vatValue = 0;
    }
    vat = vatValue;
    console.log('vat là: ' + vat);

    $('#myTable tr').each(function () {
        $(this).find("td:first").text(count++)
    });
    $('#orderContainer tr').each(function () {
        $(this).find($('.sum')).text(0);

        // check quantity validation
        var numValue = $(this).find($(".num")).val();
        if (isNaN(numValue) || !isNotNullOrEmpty(numValue)) {
            $(this).find($(".num")).val(1);
            numValue = 1;
        }
        num = numValue;

        // check discount validation
        var discountValue = $(this).find($(".discount")).val();
        if (isNaN(discountValue) || discountValue >= 100 || !isNotNullOrEmpty(discountValue)) {
            $(this).find($(".discount")).val(0);
            discountValue = 0;
        }
        discount = discountValue;

        priceValue = $(this).find($(".price")).val();
        if (isNaN(priceValue) || !isNotNullOrEmpty(priceValue)) {
            $(this).find($(".price")).val(0);
            priceValue = 0;
        }
        price = priceValue;

        if (isNotNullOrEmpty(num) &&
            isNotNullOrEmpty(discount) &&
            isNotNullOrEmpty(price)) {
            sum = (parseFloat(price) * (100 - parseFloat(discount)) / 100 * num);
            totalPrice += parseFloat(sum);
            totalPay = totalPrice * (100 + parseFloat(vat)) / 100;
            //totalPrice += parseFloat(parseFloat(price) * (100 - parseFloat(discount)) / 100 * num);
            //totalPay += parseFloat(parseFloat(price) * (100 - parseFloat(discount) + parseFloat(vat)) / 100 * num)
            $(this).find($('.sum')).text(sum.toLocaleString('en'));
            $('#totalPrice').text(totalPrice.toLocaleString('en'));
            $('#totalPay').text(totalPay.toLocaleString('en'));
        }
    });
    // NEW CODE HERE
    // validate paid and debt 
    var paid = $('#paid').val();
    console.log(paid);
    var debt = parseFloat(totalPay) - paid;
    console.log(debt);
    if (parseFloat(totalPay) >= paid) {
        $('#debt').text(debt.toLocaleString('en'));
    } else {
        $('#paid').val(totalPay)
        $('#debt').text('0');
    }
    //END NEW CODE
    totalPrice = 0;
    totalPay = 0;
}

$(document).ready(function () {
    updateInvoice();

    // Search product
    $('#search').on({
        'click': function () {
            $('#searchList').fadeIn('fast');
        },
        'blur': function () {
            $('#searchList').fadeOut('fast');
        },
        'keyup': function () {
            var searchText = $(this).val();
            $('#list-product > .item').each(function () {
                $(this).toggle(
                    $(this).find('.search-info').text().toLowerCase().indexOf(searchText.toLowerCase()) !== -1);
            });
        }
    });

    $('#myTable')
        .change(updateInvoice)
        .keyup(updateInvoice);

    $('#countTable')
        .change(updateInvoice)
        .keyup(updateInvoice);
});

function isNotNullOrEmpty(value) {
    return value !== null && value !== undefined && value !== '';
}