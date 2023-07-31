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
    },
    {
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
const supplierSelect = document.getElementById('supplierSelect');
const supplierInfoContainer = document.getElementById('supplierInfoContainer');
const supplierContainer = document.getElementById('supplierContainer');
const productList = document.getElementById('list-product');
const orderContainer = document.getElementById('orderContainer');

console.log(supplierSelect);
////Call API
//fetchDataFromAPIs();

//for test
populateSuppliersSelect();
populateProductsSelect();
// Function to fetch suppliers and products data using AJAX
function fetchDataFromAPIs() {
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/GetAllSuppliers',
        type: 'GET',
        dataType: 'json',
        success: function(suppliersData) {
            suppliers = suppliersData; // Update the suppliers array with fetched data
            //       suppliers.forEach(supplier => {
            //     console.log(supplier);
            // });
            populateSuppliersSelect();
        },
        error: function(error) {
            console.log('Error fetching suppliers data:', error);
        }
    });
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/getallproducts',
        type: 'GET',
        dataType: 'json',
        success: function(productsData) {
            products = productsData;
            products.forEach(product => {
                console.log(product);
            })
            populateProductsSelect();
        },
        error: function(error) {
            console.log('Error fetching products data:', error);
        }
    });

}
// Function to populate suppliers select list
function populateSuppliersSelect() {
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
        console.log(suppliers)
        var selectedSupplier = suppliers.find(supplier => supplier.supplierId === parseInt(selectedValue));
        console.log(selectedSupplier);
        showSupplierInfo(selectedSupplier);
        supplierContainer.style.display = 'none'; 
    } else {
        clearSupplierInfo();
        supplierContainer.style.display = 'block';
    }
});

 // Function to clear the supplier info container
 function clearSupplierInfo() {
    supplierInfoContainer.innerHTML = '';
    supplierSelect.value = ''; // Set the select option to an empty string
    supplierContainer.style.display = 'block'; // Show the supplier container
}
    // Function to show the supplier info
    function showSupplierInfo(supplier) {
        // Clear supplier info container
        supplierInfoContainer.innerHTML = `
        <div class="supplier-info">
            <h5>Thông tin nhà cung cấp</h5>
            <h6 class="text-primary fw-bold">${supplier.supplierName} <button type="button" class="btn-close"></button></h6>
            <label class="fw-bold">${supplier.phone}</label>
        </div>
        `;

        // Add event listener to the close button
        const closeBtn = supplierInfoContainer.querySelector('.btn-close');
        closeBtn.addEventListener('click', function() {
            clearSupplierInfo();
            supplierContainer.style.display = 'block'; // Show the supplier container
            supplierSelect.value = ''; // Set the select option to an empty string
        });
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
                                                                        <img src="https://product.hstatic.net/1000230347/product/but_bi_thien_long_tl-027__1__1024x1024.jpg">
                                                                    </div>
                                                                    <div class="flex-grow-1 ms-3">
                                                                        <p class="m-0 search-info fs-6 productName">${product.name}</p>
                                                                        <small class="m-0 text-secondary">Mã SP: <span
																				class="search-info productBarcode">${product.barcode}</span></small>
                                                                        <p class="m-0">Đơn vị: <span class="text-info productUnit">${product.unit}</span></p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-6 text-end">
                                                                    <p class="m-0"><strong>Giá nhập:</strong> ${product.retailPrice}
                                                                    </p>
                                                                    <p class="m-0 text-info fw-bold">Tồn: 145</p>
                                                                </div>
                                                            </div>
                                                        </a>`
                )
                .join('')
            }
        `;
    }

    // Hàm để lấy thông tin khi bấm vào thẻ <a>
    function getBookInfo(linkElement) {
        //lấy được phần tử cần ẩn
        const theA = linkElement;
        // Lấy các phần tử con bên trong thẻ <a> được click
        const bookTitle = linkElement.querySelector('.productName').textContent;
        const productCode = linkElement.querySelector('.productBarcode').textContent;
        const unit = linkElement.querySelector('.productUnit').textContent;

        // Hiển thị thông tin lấy được
        console.log('Tên sách: ', bookTitle);
        console.log('Mã SP: ', productCode);
        console.log('Đơn vị: ', unit);
            const productHTML = `
            <td></td>
            <td> <img src="assets/images/avatars/avatar-1.png"></td>
            <td>
                <div class="ellipsis">
                ${bookTitle}
                </div>
            </td>
            <td>${unit}</td>
            <td>
                <div class="input-group">
                    <input type="number" value="5" min="1" class="form-control num">
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input class="form-control price" value="0" type='number' class="num">
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input class="form-control discount" step="0.5" min="0" value="0" type='number' class="num">
                    <span class="input-group-text">%</span>
                </div>
            </td>
            <td><span class="sum"></span> VNĐ</td>
            <td>
                <a type="button" class="btn btn-close btn-danger delete"></a>
            </td>
        `;
        const productRow = document.createElement('tr');
            productRow.innerHTML = productHTML;

            const removeBtn = productRow.querySelector('.btn-danger');
            removeBtn.addEventListener('click', function() {
                // xóa product row
                productRow.remove();

                // them product lại vào selectlist
                theA.style.display = 'block';
                updateInvoice();
            });

            orderContainer.appendChild(productRow);

            // Hide the selected product option
            theA.style.display = 'none';
            updateInvoice();
    }

    function addQuickProduct() {
        var product = $('#product').val();
        var barCode = $('#barCode').val();
        var price = $('#price').val();
        var quantity = $('#quantity').val();
        var unit = $('#unit').val();

        var productItem = `
        <a href="javascript:;" class="list-group-item item" onclick="getBookInfo(this)">
            <div class="row d-flex align-items-center">
                <div class="col-6 d-flex align-items-center">
                    <div class="flex-shrink-0 cover">
                        <img src="">
                    </div>
                    <div class="flex-grow-1 ms-3">
                        <p class="m-0 search-info fs-6 productName">${product}</p>
                        <small class="m-0 text-secondary">Mã SP: <span
                                class="search-info productBarcode">${barCode}</span></small>
                        <p class="m-0">Đơn vị: <span class="text-info productUnit">${unit}</span></p>
                    </div>
                </div>
                <div class="col-6 text-end">
                    <p class="m-0"><strong>Giá nhập:</strong> ${price}
                    </p>
                    <p class="m-0 text-info fw-bold">Tồn: ${quantity}</p>
                </div>
            </div>
        </a>`;
        $('#list-product').append(productItem);

        $('#list-product > a:last').trigger('click')
    };

    $(document).on('click', '#submitProduct', function () {
        addQuickProduct();
    });

    function addQuickSupplier() {
        var name = $('#supplier').val();
        var phone = $('#phone').val();
        var supplierLength = suppliers.length;

        supplier = {
            "supplierId": (supplierLength + 1),
            "address": null,
            "supplierName": name,
            "phone": phone,
            "status": true,
            "description": null,
            "email": null,
            "provinceCode": null,
            "province": null,
            "districtCode": null,
            "district": null,
            "wardCode": null,
            "ward": null
        }

        suppliers[supplierLength] = supplier;

        console.log(suppliers)

        var supplierOption = `<option value="${supplierLength + 1}">${name}</option>`

        $("#supplierSelect").append(supplierOption);
        
        supplierContainer.style.display = 'none'; 
        showSupplierInfo(suppliers[supplierLength]);
    }

    $(document).on('click', '#submitSupplier', function () {
        addQuickSupplier();
    });
