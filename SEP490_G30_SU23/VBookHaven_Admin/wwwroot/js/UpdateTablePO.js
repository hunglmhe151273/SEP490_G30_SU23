$('.single-select').select2({
    theme: 'bootstrap4',
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: $(this).data('placeholder'),
    allowClear: Boolean($(this).data('allow-clear')),
});

// Update invoice function
var updateInvoice = function () {
    var num, discount, price, sum;
    let totalPrice = 0,
        totalPay = 0,
        count = 0
    vat = 0;


    //default values
    $('#totalPrice').text('0');
    $('#totalPay').text('0');
    if (isNotNullOrEmpty($('#totalVat').val()))
        vat = $('#totalVat').val();

    console.log('vat là: ' + vat);

    $('#myTable tr').each(function () {
        $(this).find("td:first").text(count++)
    });
    $('#orderContainer tr').each(function () {
        $(this).find($('.sum')).text(0);
        num = $(this).find($(".num")).val();
        discount = $(this).find($(".discount")).val();
        price = $(this).find($(".price")).val();

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