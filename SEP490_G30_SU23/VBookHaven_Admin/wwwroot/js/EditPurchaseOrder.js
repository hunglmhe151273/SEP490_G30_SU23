let suppliers = [{
    "supplierId": 1,
    "address": "abc địa chỉ chi tiết",
    "supplierName": "Tên nhà cung Cấp A",
    "phone": "0985457175",
    "status": true,
    "description": "Abc ghi chú",
    "email": "Storekeeper1@gmail.com",
    "provinceCode": 6,
    "province": "Tỉnh Bắc Kạn",
    "districtCode": 61,
    "district": "Huyện Ba Bể",
    "wardCode": 1891,
    "ward": "Xã Bành Trạch"
},
{
    "supplierId": 2,
    "address": "Địa chỉ chi tiết abc",
    "supplierName": "Nhà Cung Cấp A",
    "phone": "0985457175",
    "status": true,
    "description": "Ghi chú abc",
    "email": "Storekeeper1@gmail.com",
    "provinceCode": 1,
    "province": "Thành phố Hà Nội",
    "districtCode": 2,
    "district": "Quận Hoàn Kiếm",
    "wardCode": 40,
    "ward": "Phường Đồng Xuân"
},
{
    "supplierId": 3,
    "address": null,
    "supplierName": "Tên nhà cung cấp C",
    "phone": "0123456789",
    "status": true,
    "description": null,
    "email": "hunglmhe151273@fpt.edu.vn",
    "provinceCode": 1,
    "province": "Thành phố Hà Nội",
    "districtCode": 2,
    "district": "Quận Hoàn Kiếm",
    "wardCode": 40,
    "ward": "Phường Đồng Xuân"
},
{
    "supplierId": 4,
    "address": null,
    "supplierName": "Tên nhà cung cấp D",
    "phone": "0985457175",
    "status": true,
    "description": null,
    "email": "acc2hunglm@gmail.com",
    "provinceCode": 6,
    "province": "Tỉnh Bắc Kạn",
    "districtCode": 62,
    "district": "Huyện Ngân Sơn",
    "wardCode": 1945,
    "ward": "Xã Cốc Đán"
},
{
    "supplierId": 5,
    "address": null,
    "supplierName": "Tên nhà cung cấp E",
    "phone": "0985457175",
    "status": true,
    "description": null,
    "email": "acc2hunglm@gmail.com",
    "provinceCode": 6,
    "province": "Tỉnh Bắc Kạn",
    "districtCode": 62,
    "district": "Huyện Ngân Sơn",
    "wardCode": 1942,
    "ward": "Xã Bằng Vân"
},
{
    "supplierId": 6,
    "address": null,
    "supplierName": "Tên nhà cung cấp F",
    "phone": "0985457175",
    "status": true,
    "description": null,
    "email": "acc2hunglm@gmail.com",
    "provinceCode": 2,
    "province": "Tỉnh Hà Giang",
    "districtCode": 26,
    "district": "Huyện Đồng Văn",
    "wardCode": 712,
    "ward": "Thị trấn Phó Bảng"
},
{
    "supplierId": 7,
    "address": null,
    "supplierName": "Tên nhà cung cấp H",
    "phone": "0985457175",
    "status": true,
    "description": null,
    "email": null,
    "provinceCode": 2,
    "province": "Tỉnh Hà Giang",
    "districtCode": 26,
    "district": "Huyện Đồng Văn",
    "wardCode": 721,
    "ward": "Thị trấn Đồng Văn"
},
{
    "supplierId": 8,
    "address": null,
    "supplierName": "Tên nhà cung cấp G",
    "phone": "0912345678",
    "status": true,
    "description": null,
    "email": "hung181120011@gmail.com",
    "provinceCode": 1,
    "province": "Thành phố Hà Nội",
    "districtCode": 2,
    "district": "Quận Hoàn Kiếm",
    "wardCode": 43,
    "ward": "Phường Hàng Mã"
}
];
let products = [{
    "productId": 2,
    "name": "Sach test 2",
    "barcode": "PVN2",
    "unit": "Bộ",
    "unitInStock": 0,
    "purchasePrice": 20000,
    "retailPrice": 23000,
    "retailDiscount": 2,
    "wholesalePrice": 21000,
    "wholesaleDiscount": 2,
    "size": null,
    "weight": null,
    "description": null,
    "status": true,
    "isBook": true,
    "subCategoryId": 2
},
{
    "productId": 3,
    "name": "Sach test 3",
    "barcode": "PVN3",
    "unit": "Thùng",
    "unitInStock": 0,
    "purchasePrice": 25000,
    "retailPrice": 28000,
    "retailDiscount": 0.2,
    "wholesalePrice": 27000,
    "wholesaleDiscount": 0.2,
    "size": null,
    "weight": null,
    "description": null,
    "status": true,
    "isBook": true,
    "subCategoryId": 1
},
{
    "productId": 4,
    "name": "Sach test 4",
    "barcode": "PVN4",
    "unit": "Quyển",
    "unitInStock": 0,
    "purchasePrice": 30,
    "retailPrice": 35,
    "retailDiscount": 2,
    "wholesalePrice": 33,
    "wholesaleDiscount": 2,
    "size": null,
    "weight": null,
    "description": null,
    "status": true,
    "isBook": true,
    "subCategoryId": 1
},
{
    "productId": 5,
    "name": "Sach test 5",
    "barcode": "PVN5",
    "unit": "Bộ",
    "unitInStock": 0,
    "purchasePrice": 50,
    "retailPrice": 60,
    "retailDiscount": 5,
    "wholesalePrice": 55,
    "wholesaleDiscount": 5,
    "size": null,
    "weight": null,
    "description": null,
    "status": true,
    "isBook": true,
    "subCategoryId": 2
}
];
let productExists = [{
    "productId": 1,
    "name": "Sach test 1",
    "barcode": "PVN1",
    "unit": "Quyển",
    "unitInStock": 0,
    "purchasePrice": 15,
    "retailPrice": 17,
    "retailDiscount": 2,
    "wholesalePrice": 16,
    "wholesaleDiscount": 2,
    "size": null,
    "weight": null,
    "description": null,
    "status": true,
    "isBook": true,
    "subCategoryId": 1
}];


const supplierSelect = document.getElementById('supplierSelect');
const supplierInfoContainer = document.getElementById('supplierInfoContainer');
const supplierContainer = document.getElementById('supplierContainer');
const productList = document.getElementById('list-product');
const orderContainer = document.getElementById('orderContainer');


//display product list
function displayProductList() {
    products.forEach(product => {
        console.log(product);
    })
}


//for test
// populateSuppliersSelect();
// populateProductsSelect();

var purchaseId = getParameterFromUrl('purchaseId');
function getParameterFromUrl(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
}
// Function to fetch suppliers and products data using AJAX
if (purchaseId !== null) {
    console.log("purchaseId" + purchaseId);
    fetchDataFromAPIs(purchaseId);
}

function fetchDataFromAPIs(purchaseId) {
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/GetAllSuppliers',
        type: 'GET',
        dataType: 'json',
        success: function (suppliersData) {
            suppliers = suppliersData;
            populateSuppliersSelect();
        },
        error: function (error) {
            console.log('Error fetching suppliers data:', error);
        }
    });
    let urlGetAllOtherProductsInPurchaseId = `https://localhost:7123/Admin/PurchaseOrder/GetAllOtherProductsByPurchaseId?purchaseId=${purchaseId}`;
    $.ajax({
        url: urlGetAllOtherProductsInPurchaseId,
        type: 'GET',
        dataType: 'json',
        success: function (productsData) {
            products = productsData;
            populateProductsSelect();
        },
        error: function (error) {
            console.log('Error fetching products data:', error);
        }
    });

}
// Function to populate suppliers select list
function populateSuppliersSelect() {
    console.log('Nhà cung cấp' + suppliers);
    supplierSelect.innerHTML = `
    <option value="">Chọn nhà cung cấp...</option>
    ${suppliers
            .map(
                supplier => `<option value="${supplier.supplierId}">${supplier.supplierName}</option>`
            )
            .join('')
        }
`;
}

$("#supplierSelect").change(() => {
    selectedValue = $("#supplierSelect").val();
    if (selectedValue !== '') {
        var selectedSupplier = suppliers.find(supplier => supplier.supplierId === parseInt(selectedValue));
        console.log('selectedSupplier' + selectedSupplier);
        showSupplierInfo(selectedSupplier);
        supplierContainer.style.display = 'none';
    } else {
        supplierSelect.value = '';
        supplierContainer.style.display = 'block';
    }
});

// Function to show the supplier info
function showSupplierInfo(supplier) {
    // Clear supplier info container

    var showSupplierInfoDiv = `
            <div class="supplier-info">
                <h6 class="text-primary fw-bold">${supplier.supplierName} <button type="button" onclick="closeButton()" class="btn-close"></button></h6>
                <label class="fw-bold">Số điện thoại: ${supplier.phone}</label></br>
            `;
    if (supplier.province !== null) {
        showSupplierInfoDiv += `<label class="fw-bold">Địa chỉ: ${supplier.province}</label>`;
    }
    showSupplierInfoDiv += `</div>`;

    supplierInfoContainer.innerHTML = showSupplierInfoDiv;
    // // Add event listener to the close button
    // const closeBtn = supplierInfoContainer.querySelector('.btn-close');
    // closeBtn.addEventListener('click', function() {
    //     supplierInfoContainer.innerHTML = '';//clear supplier Infomation
    //     $('#supplierSelect').val('').trigger('change'); //change value
    //     supplierContainer.style.display = 'block'; // Show the supplier container
    // });

}
function closeButton() {
    supplierInfoContainer.innerHTML = '';//clear supplier Infomation
    $('#supplierSelect').val('').trigger('change'); //change value
    supplierContainer.style.display = 'block'; // Show the supplier container
}

// Function to populate products select list
function populateProductsSelect() {
    productList.innerHTML = `
            ${products
            .map(
                product => `<a href="javascript:;" class="list-group-item item" onclick="getBookInfo(this)">
                                    <div class="row d-flex align-items-center">
                                        <div class="col-6 d-flex align-items-center">
                                            <div class="flex-shrink-0 cover">
                                            <div class="hidden-content productId">${product.productId}</div>
                                            <div class="hidden-content name">${product.name}</div>
                                            <div class="hidden-content barcode">${product.barcode}</div>
                                            <div class="hidden-content unit">${product.unit}</div>
                                            <div class="hidden-content purchasePrice">${product.purchasePrice}</div>
                                            <div class="hidden-content presentImage">${product.presentImage}</div>
                                            <div class="hidden-content unitInStock">${product.unitInStock}</div>
                                                <img class="productImg"  src="${product.presentImage}" class="productImg">
                                            </div>
                                            <div class="flex-grow-1 ms-3">
                                                <p class="m-0 search-info fs-6 productName">${product.name}</p>
                                                <small class="m-0 text-secondary">Mã SP: <span
                                                        class="search-info productBarcode">${product.barcode}</span></small>
                                                <p class="m-0">Đơn vị: <span class="text-info productUnit">${product.unit}</span></p>
                                            </div>
                                        </div>
                                        <div class="col-6 text-end">
                                            <p class="m-0"><strong>Giá nhập:</strong>${product.purchasePrice}
                                            </p>
                                            <p class="m-0 text-info fw-bold">Tồn: ${product.unitInStock}</p>
                                        </div>
                                    </div>
                                </a>`
            )
            .join('')
        }
        `;
}

// Hàm để lấy thông tin khi bấm vào selectlist - thẻ <a>
function getBookInfo(linkElement) {
    // Lấy các phần tử con bên trong thẻ <a> được click
    const productId = linkElement.querySelector('.hidden-content.productId').innerHTML;
    const name = linkElement.querySelector('.hidden-content.name').innerHTML;
    const barcode = linkElement.querySelector('.hidden-content.barcode').innerHTML;
    const unit = linkElement.querySelector('.hidden-content.unit').innerHTML;
    const purchasePrice = linkElement.querySelector('.hidden-content.purchasePrice').innerHTML;
    const presentImage = linkElement.querySelector('.hidden-content.presentImage').innerHTML;
    const unitInStock = linkElement.querySelector('.hidden-content.unitInStock').innerHTML;
    //1. Hiển thị sản phẩm ở order
    const productHTML = `
            <input name="ProductIDList" value="${productId}" hidden>
            <td></td>
            <td><img class="productImg" src="${presentImage}"></td>
            <td>
                <div class="ellipsis">
                ${name}
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
                    <input name="UnitPriceList" class="form-control price num" step="1000" min="0" value="0" type='number'>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input name="DiscountList" class="form-control discount num" step="0.1" min="0" value="0" type='number'>
                    <span class="input-group-text">%</span>
                </div>
            </td>
            <td><span class="sum"></span> VNĐ</td>
            <td>
            <a href="javascript:;" onclick="addSelect(this)" class="btn btn-close btn-danger delete">
                <div class="hidden-content productId">${productId}</div>
                <div class="hidden-content name"> ${name}</div>
                <div class="hidden-content barcode">${barcode}</div>
                <div class="hidden-content unit">${unit}</div>
                <div class="hidden-content purchasePrice">${purchasePrice}</div>
                <div class="hidden-content presentImage">${presentImage}</div>
                <div class="hidden-content unitInStock">${unitInStock}</div>
             </a>
            </td>
        `;
    const productRow = document.createElement('tr');
    productRow.innerHTML = productHTML;
    //const removeBtn = productRow.querySelector('.btn-danger.delete');
    //removeBtn.addEventListener('click', function () {
    //    //onclick="addSelect(this)" --> thêm product lại về selectlist
    //    // xóa product row
    //    //productRow.remove();
    //    updateInvoice();
    //});

    orderContainer.appendChild(productRow);

    //2. Cập nhật lại product list - xóa sp trong product list
    products = products.filter((product) => product.productId !== Number(productId));
    products.sort((a, b) => a.productId - b.productId);
    populateProductsSelect();
    updateInvoice();
}

$(document).on('click', '#submitProduct', function () {
    //call api to add
    addProductByAPI();
});

$(document).on('click', '#submitSupplier', function () {
    addSupplierByAPI();
});

function addSupplierByAPI() {
    let name = $('#supplier').val();
    let phone = $('#phone').val();
    let description = $('#description').val();
    let supplierLength = suppliers.length;
    let supplierDTO = {
        "supplierId": null,
        "address": null,
        "supplierName": name,
        "phone": phone,
        "status": true,
        "description": description,
        "email": null,
        "provinceCode": null,
        "province": null,
        "districtCode": null,
        "district": null,
        "wardCode": null,
        "ward": null
    }
    $.ajax({
        url: "https://localhost:7123/Admin/Suppliers/AddSupplier", // Replace with the correct API endpoint URL
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(supplierDTO),
        success: function (response) {
            //thêm supplier vào select list
            suppliers[supplierLength] = response;
            console.log("supplier cua them:" + suppliers[supplierLength])

            //sửa lại cho đúng id của nó -done
            var supplierOption = `<option value="${response.supplierId}">${response.supplierName}</option>`
            // Thêm select list supplier mới -- giá trị là supplier vừa tạo
            $("#supplierSelect").append(supplierOption);
            $('#supplierSelect').val(response.supplierId).trigger('change'); //change value
            console.log('#supplierSelect: ' + $('#supplierSelect').val());
            supplierContainer.style.display = 'none';
            // hiển thị supplier mới
            showSupplierInfo(suppliers[supplierLength]);
            //reset form
            resetCreateSupplierForm();
            round_success_noti("Thêm nhà cung cấp thành công");

        },
        error: function (error) {
            round_error_noti(error.responseText);
            resetCreateSupplierForm();
            console.error("Lỗi gọi addSupplier API: " + error.responseText);
        },
    });
}
function addProductByAPI() {
    var productName = $('#productName').val();
    var barCode = $('#barCode').val();
    var price = $('#price').val();
    var unit = $('#unit').val();
    // lấy giá trị radio button
    const radioButtons = document.getElementsByName('IsBook');
    const selectedValue = radioButtons[0].checked.toString();
    var isBook = JSON.parse(selectedValue);
    var bookDTO = {
        "productId": null,
        "name": productName,
        "barcode": barCode,
        "unit": unit,
        "purchasePrice": price,
        "isBook": isBook,
        "presentImage": null
    }
    console.log('bookDTO' + JSON.stringify(bookDTO));
    $.ajax({
        url: "https://localhost:7123/Admin/PurchaseOrder/AddProduct", // Replace with the correct API endpoint URL
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(bookDTO),
        success: function (response) {
            var productItem = `
                <a href="javascript:;" class="list-group-item item" onclick="getBookInfo(this)">
                    <div class="row d-flex align-items-center">
                        <div class="col-6 d-flex align-items-center">
                            <div class="flex-shrink-0 cover">
                            <div class="hidden-content productId">${response.productId}</div>
                            <div class="hidden-content name">${response.name}</div>
                            <div class="hidden-content barcode">${response.barcode}</div>
                            <div class="hidden-content unit">${response.unit}</div>
                            <div class="hidden-content purchasePrice">${response.purchasePrice}</div>
                            <div class="hidden-content presentImage">${response.presentImage}</div>
                            <div class="hidden-content unitInStock">${response.unitInStock}</div>
                                <img src="${response.presentImage}" class="productImg" >
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <p class="m-0 search-info fs-6 productName">${response.name}</p>
                                <small class="m-0 text-secondary">Mã SP: <span
                                        class="search-info productBarcode">${response.barcode}</span></small>
                                <p class="m-0">Đơn vị: <span class="text-info productUnit">${response.unit}</span></p>
                            </div>
                        </div>
                        <div class="col-6 text-end">
                            <p class="m-0"><strong>Giá nhập:</strong> ${response.purchasePrice}
                            </p>
                            <p class="m-0 text-info fw-bold">Tồn: ${response.unitInStock}</p>
                        </div>
                    </div>
                </a>`;
            //thêm response vào list
            products.push(response);
            $('#list-product').append(productItem);
            $('#list-product > a:last').trigger('click')

            resetCreateProductForm();
            round_success_noti("Thêm sản phẩm thành công");
        },
        error: function (error) {
            round_error_noti(error.responseText);
            resetCreateProductForm()
            console.error("Lỗi gọi addProduct API: " + error.responseText);
        },
    });
}

//new
function addSelect(linkElement) {
    // Get the parent <td> element of the <a> element
    const parentTd = linkElement.parentNode;
    // Get the parent <tr> element of the <td> element
    const parentTr = parentTd.parentNode;
    // Use querySelector to access the hidden elements inside the linkElement
    const productId = linkElement.querySelector('.hidden-content.productId').innerHTML;
    const name = linkElement.querySelector('.hidden-content.name').innerHTML;
    const barcode = linkElement.querySelector('.hidden-content.barcode').innerHTML;
    const unit = linkElement.querySelector('.hidden-content.unit').innerHTML;
    const purchasePrice = linkElement.querySelector('.hidden-content.purchasePrice').innerHTML;
    const unitInStock = linkElement.querySelector('.hidden-content.unitInStock').innerHTML;
    const presentImage = linkElement.querySelector('.hidden-content.presentImage').innerHTML;
    // Create a new product object
    const newProduct = {
        productId: Number(productId),
        name,
        barcode,
        unit,
        purchasePrice,
        unitInStock,
        presentImage,
    };
    // Push the new product object to the productExists array
    products.push(newProduct);
    // Sorting products by productId in ascending order
    products.sort((a, b) => a.productId - b.productId);
    populateProductsSelect();
    //Delete row in table
    parentTr.remove();
    updateInvoice();
}

function resetCreateSupplierForm() {
    // Clear input fields (set their values to empty strings)
    $("#supplier").val('');
    $("#phone").val('');
    $("#description").val('');
}
function resetCreateProductForm() {
    // Clear input fields (set their values to empty strings)
    $('#productName').val('');
    $('#barCode').val('');
    $('#price').val('');
    $('#quantity').val('');
    $('#unit').val('');
    $('#isBook').val(true);
}