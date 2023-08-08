let customers = [{
    "shipInfoId": 0,
    "phone": null,
    "shipAddress": null,
    "customerId": null,
    "status": null,
    "customerName": null
    }
];
let products = [{
        "productId": 0,
        "name": null,
        "barcode": null,
        "unit": null,
        "unitInStock": null,
        "purchasePrice": null,
        "retailPrice": null,
        "retailDiscount": null,
        "wholesalePrice": null,
        "wholesaleDiscount": null,
        "size": null,
        "weight": null,
        "description": null,
        "status": true,
        "isBook": true,
        "subCategoryId": null
    }
];
const customerSelect = document.getElementById('customerSelect');
const customerInfoContainer = document.getElementById('customerInfoContainer');
const customerContainer = document.getElementById('customerContainer');
const productList = document.getElementById('list-product');
const orderContainer = document.getElementById('orderContainer');

//Call API
fetchDataFromAPIs();

//for test
//populatecustomersSelect();
//populateProductsSelect();

// Function to fetch customers and products data using AJAX
function fetchDataFromAPIs() {
    $.ajax({
        url: 'https://localhost:7123/Admin/Order/GetAllShippingInfo',
        type: 'GET',
        dataType: 'json',
        success: function(customersData) {
            customers = customersData; // Update the customers array with fetched data
            //customers.forEach(customer => {
            //    console.log(customer);
            //});
            populatecustomersSelect();
        },
        error: function(error) {
            console.log('Error fetching customers data:', error);
        }
    });
    $.ajax({
        url: 'https://localhost:7123/Admin/Order/GetAllActiveProducts',
        type: 'GET',
        dataType: 'json',
        success: function(productsData) {
            products = productsData;
            //products.forEach(product => {
            //    console.log(product);
            //})
            populateProductsSelect();
        },
        error: function(error) {
            console.log('Error fetching products data:', error);
        }
    });

}
// Function to populate customers select list
function populatecustomersSelect() {
    customerSelect.innerHTML = `
    <option value="">Chọn khách hàng...</option>
    ${customers
        .map(
            customer => `
                <option value="${customer.shipInfoId}"
                    title="Địa chỉ: ${customer.shipAddress}">
                    ${customer.customerName} - ${customer.phone}
                </option>
            `
        )
        .join('')
    }
`;
}

$("#customerSelect").change(() => {
    selectedValue = $("#customerSelect").val();
    if (selectedValue !== '') {
        var selectedcustomer = customers.find(customer => customer.shipInfoId === parseInt(selectedValue));
        showcustomerInfo(selectedcustomer);
        customerContainer.style.display = 'none'; 
    } else {
        customerSelect.value = ''; 
        customerContainer.style.display = 'block';
    }
});

// Function to show the customer info
function showcustomerInfo(customer) {
    // Clear customer info container

        var showcustomerInfoDiv =  `
        <div class="customer-info">
            <h6 class="text-primary fw-bold">${customer.customerName} <button type="button" class="btn-close"></button></h6>
            <label class="fw-bold">Số điện thoại: ${customer.phone}</label></br>
        `;
    if (customer.shipAddress !== null) {
        showcustomerInfoDiv += `<label class="fw-bold">Địa chỉ: ${customer.shipAddress}</label>`;
        }
    showcustomerInfoDiv += `</div>`;

    // Add hidden input for model binding
    showcustomerInfoDiv += `
        <input hidden name="Order.CustomerName" value="${customer.customerName}" />
        <input hidden name="Order.Phone" value="${customer.phone}" />
        <input hidden name="Order.ShipAddress" value="${customer.shipAddress}" />
    `

    customerInfoContainer.innerHTML = showcustomerInfoDiv;

    // Add event listener to the close button
    const closeBtn = customerInfoContainer.querySelector('.btn-close');
    closeBtn.addEventListener('click', function() {
        customerInfoContainer.innerHTML = '';//clear customer Infomation
        $('#customerSelect').val('').trigger('change'); //change value
        console.log("customerSelect VALUE: " + customerSelect.value)
        customerContainer.style.display = 'block'; // Show the customer container
    });
}

// Function to populate products select list
function populateProductsSelect() {
    productList.innerHTML = `
        ${products
            .map(
                product => `<a href="javascript:;" class="list-group-item item" onclick="getProductInfo(this)">
                                <div class="row d-flex align-items-center">
                                    <div class="col-6 d-flex align-items-center">
                                        <div class="flex-shrink-0 cover">
                                            <img src="${product.presentImage}" class="productImg">
                                        </div>
                                        <div class="flex-grow-1 ms-3">
                                            <p class="m-0 search-info fs-6 productName">${product.name}</p>
                                            <small class="m-0 text-secondary">Mã SP: <span
                                                    class="search-info productBarcode">${product.barcode}</span></small>
                                            <p class="m-0">Đơn vị: <span class="text-info productUnit">${product.unit}</span></p>
                                        </div>
                                    </div>
                                    <div class="col-6 text-end">
                                        <p class="m-0"><strong>Giá lẻ:</strong> ${product.retailPrice}
                                        </p>
                                        <p class="m-0"><strong>Giá sỉ:</strong> ${product.wholesalePrice}
                                        </p>

                                        <p class="productId" hidden>${product.productId}</p>
                                        <p class="retailPrice" hidden>${product.retailPrice}</p>
                                        <p class="retailDiscount" hidden>${product.retailDiscount}</p>
                                        <p class="wholesalePrice" hidden>${product.wholesalePrice}</p>
                                        <p class="wholesaleDiscount" hidden>${product.wholesaleDiscount}</p>

                                        <p class="m-0 text-info fw-bold">Tồn: ${product.unitInStock}</p>
                                    </div>
                                </div>
                            </a>`
            )
            .join('')
        }
    `;
}

// Hàm để lấy thông tin khi bấm vào thẻ <a>
function getProductInfo(linkElement) {
    //lấy được phần tử cần ẩn
    const theA = linkElement;
    // Lấy các phần tử con bên trong thẻ <a> được click
    const bookTitle = linkElement.querySelector('.productName').textContent;
    const unit = linkElement.querySelector('.productUnit').textContent;
    var imgSrc = linkElement.querySelector('.productImg').src;

    const productId = linkElement.querySelector('.productId').textContent;
    const retailPrice = linkElement.querySelector('.retailPrice').textContent;
    const retailDiscount = linkElement.querySelector('.retailDiscount').textContent;
    const wholesalePrice = linkElement.querySelector('.wholesalePrice').textContent;
    const wholesaleDiscount = linkElement.querySelector('.wholesaleDiscount').textContent;

    //Hiển thị sản phẩm ở order
    var isRetail = $("#price-type").val() == "Giá lẻ";
    var price = isRetail ? retailPrice : wholesalePrice;
    var discount = isRetail ? retailDiscount : wholesaleDiscount;

    const productHTML = `
        <td></td>
        <td><img src="${imgSrc}" style="aspect-ratio: 1/1"></td>
        <td>
            <div class="ellipsis">
            ${bookTitle}
            </div>
        </td>
        <td>${unit}</td>
        <td>
            <div class="input-group">
                <input name="QuantityList" type="number" value="1" min="1" step="1" class="form-control num">
            </div>
        </td>
        <td>
            <div class="input-group">
                <input name="PriceList" class="form-control price num" step="1000" min="0" value="${price}" type='number' />
            </div>
        </td>
        <td>
            <div class="input-group">
                <input name="DiscountList" class="form-control discount num" step="0.1" min="0" value="${discount}" type='number' />
                <span class="input-group-text">%</span>
            </div>
        </td>
        <td><span class="sum"></span> VNĐ</td>
        <td>
            <a type="button" class="btn btn-close btn-danger delete"></a>
        </td>
        <td hidden><input name="ProductIdList" hidden value="${productId}"></td>
    `;

    const productRow = document.createElement('tr');
        productRow.innerHTML = productHTML;

        const removeBtn = productRow.querySelector('.btn-danger');
        removeBtn.addEventListener('click', function() {
            // xóa product row
            productRow.remove();

            // thêm lại the a vao select product list
                // C1: Thêm lại thẻ a
            //productList.append(theA);
                // C2: Hiển thị lại thẻ a
            theA.style.display = 'block';
            updateOrder();
        });

        orderContainer.appendChild(productRow);

        // C1: Xóa thẻ a
        //theA.remove();
        // C2: Ẩn thẻ a
        theA.style.display = 'none';
        updateOrder();
}

//$(document).on('click', '#submitProduct', function () {
//    //call api to add
//    addProductByAPI();
//});

$(document).on('click', '#submitcustomer', function () {
    addcustomerByAPI();
});

function addcustomerByAPI() {
    var name = $('#customer').val();
    var phone = $('#phone').val();
    var address = $('#address').val();
    var customerLength = customers.length;
    var customerDTO = {
        "shipInfoId": 0,
        "phone": phone,
        "shipAddress": address,
        "customerId": null,
        "status": true,
        "customerName": name
    }
    $.ajax({
        url: "https://localhost:7123/Admin/Order/AddNewShippingInfo", // Replace with the correct API endpoint URL
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(customerDTO),
        success: function(response) {
            //thêm customer vào select list
            customers[customerLength] = response;
        
            //sửa lại cho đúng id của nó -done
            var customerOption = `
                <option value="${response.shipInfoId}"
                    title="Địa chỉ: ${response.shipAddress}">
                    ${response.customerName} - ${response.phone}
                </option>
            `
            // Thêm select list customer mới -- giá trị là customer vừa tạo
            $("#customerSelect").append(customerOption);
            $('#customerSelect').val(response.shipInfoId).trigger('change'); //change value

            /* Da trigger change o tren roi, ko can giau select va hien thi customer moi lai nua */
            //customerContainer.style.display = 'none';
            //// hiển thị customer mới
            //showcustomerInfo(customers[customerLength]);

            //reset form
            resetAddAuthorForm();
            round_success_noti("Thêm khách hàng thành công");

        },
        error: function(error) {
            round_error_noti(error.responseText);
            resetAddAuthorForm();
            console.error("Lỗi gọi addcustomer API: " + error.responseText);
        },
    });
}
//function addProductByAPI() {
//    var productName = $('#productName').val();
//    var barCode = $('#barCode').val();
//    var price = $('#price').val();
//    var quantity = $('#quantity').val();
//    var unit = $('#unit').val();
//    var isBook = $('#isBook').val(); 

//    var bookDTO =  {
//            "productId": null,
//            "name": productName,
//            "barcode": barCode,
//            "unit": unit,
//            "purchasePrice": price,
//            "unitInStock": quantity,
//            "isBook": isBook,
//            "presentImage": null
//            }
//    $.ajax({
//        url: "https://localhost:7123/Admin/PurchaseOrder/AddProduct", // Replace with the correct API endpoint URL
//        type: "POST",
//        contentType: "application/json",
//        data: JSON.stringify(bookDTO),
//        success: function(response) {
//            var productItem = `
//            <a href="javascript:;" class="list-group-item item" onclick="getProductInfo(this)">
//                <div class="row d-flex align-items-center">
//                    <div class="col-6 d-flex align-items-center">
//                        <div class="flex-shrink-0 cover">
//                            <img src="${response.presentImage}" class="productImg" >
//                        </div>
//                        <div class="flex-grow-1 ms-3">
//                            <p class="m-0 search-info fs-6 productName">${response.name}</p>
//                            <small class="m-0 text-secondary">Mã SP: <span
//                                    class="search-info productBarcode">${response.barcode}</span></small>
//                            <p class="m-0">Đơn vị: <span class="text-info productUnit">${response.unit}</span></p>
//                        </div>
//                    </div>
//                    <div class="col-6 text-end">
//                        <p class="m-0"><strong>Giá nhập:</strong> ${response.purchasePrice}
//                        </p>
//                        <p class="m-0 text-info fw-bold">Tồn: ${response.unitInStock}</p>
//                    </div>
//                </div>
//            </a>`;
//            $('#list-product').append(productItem);
        
//            $('#list-product > a:last').trigger('click')

//            resetCreateProductForm();
//            round_success_noti("Thêm sản phẩm thành công");
//        },
//        error: function(error) {
//            round_error_noti(error.responseText);
//            resetCreateProductForm()
//            console.error("Lỗi gọi addProduct API: " + error.responseText);
//        },
//    });
//}

function resetAddAuthorForm(){
    // Clear input fields (set their values to empty strings)
    $("#customer").val('');
    $("#phone").val('');
    $("#address").val('');
}
//function resetCreateProductForm(){
//    // Clear input fields (set their values to empty strings)
//        $('#productName').val('');
//        $('#barCode').val('');
//        $('#price').val('');
//    $('#quantity').val('');
//    $('#unit').val('');
//    $('#isBook').val(true);
//}

/* Update Order function */
var updateOrder = function () {
    var num, discount, price, sum, markup;
    let totalPrice = 0,
        totalPay = 0,
        count = 0
    vat = 0
    paid = 0;


    //default values
    $('#totalPrice').text('0');
    $('#totalPay').text('0');
    var vatValue = $('#totalVat').val();
    if (isNaN(vatValue) || vatValue >= 100 || !isNotNullOrEmpty(vatValue)) {
        $('#totalVat').val(5);
        vatValue = 5;
    }
    vat = vatValue;

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

    paid = $('#paid').val();
    var debt = parseFloat(totalPay) - paid;
    if (parseFloat(totalPay) >= paid && !isNaN(paid)) {
        $('#debt').text(debt.toLocaleString('en'));
    } else {
        $('#paid').val(totalPay)
        $('#debt').text('0');
    }
    totalPrice = 0;
    totalPay = 0;
}

$(document).ready(function () {
    updateOrder();

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
        .change(updateOrder)
        .keyup(updateOrder);

    $('#countTable')
        .change(updateOrder)
        .keyup(updateOrder);
});

function isNotNullOrEmpty(value) {
    return value !== null && value !== undefined && value !== '';
}