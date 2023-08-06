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



const supplierSelect = document.getElementById('supplierSelect');
const supplierInfoContainer = document.getElementById('supplierInfoContainer');
const supplierContainer = document.getElementById('supplierContainer');
const productList = document.getElementById('list-product');
const orderContainer = document.getElementById('orderContainer');

//Call API
fetchDataFromAPIs(purchaseId);

//for test
// populateSuppliersSelect();
// populateProductsSelect();
var purchaseId = getParameterFromUrl('purchaseId');

function getParameterFromUrl(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
}
// Function to fetch suppliers and products data using AJAX

fetchDataFromAPIs(purchaseId);

function fetchDataFromAPIs(purchaseId) {
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
    let urlGetAllOtherProductsInPurchaseId = `https://localhost:7123/Admin/PurchaseOrder/GetAllOtherProductsByPurchaseId?purchaseId=${purchaseId}`;
    $.ajax({
        url: urlGetAllOtherProductsInPurchaseId,
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
        console.log('selectedSupplier'+selectedSupplier);
        showSupplierInfo(selectedSupplier);
        supplierContainer.style.display = 'none'; 
    } else {
        supplierSelect.value = ''; 
        supplierContainer.style.display = 'block';
    }
});
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
    // Function to show the supplier info
    function showSupplierInfo(supplier) {
        // Clear supplier info container

            var showSupplierInfoDiv =  `
            <div class="supplier-info">
                <h6 class="text-primary fw-bold">${supplier.supplierName} <button type="button" class="btn-close"></button></h6>
                <label class="fw-bold">Số điện thoại: ${supplier.phone}</label></br>
            `;
            if (supplier.province !== null) {
                showSupplierInfoDiv += `<label class="fw-bold">Địa chỉ: ${supplier.province}</label>`;
            }
            showSupplierInfoDiv += `</div>`;

        supplierInfoContainer.innerHTML = showSupplierInfoDiv;

        // Add event listener to the close button
        const closeBtn = supplierInfoContainer.querySelector('.btn-close');
        closeBtn.addEventListener('click', function() {
            supplierInfoContainer.innerHTML = '';//clear supplier Infomation
            $('#supplierSelect').val('').trigger('change'); //change value
            console.log("supplierSelect VALUE: " + supplierSelect.value)
            supplierContainer.style.display = 'block'; // Show the supplier container
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
                                            <div class="hidden-content productId">${product.productId}</div>
                                            <div class="hidden-content name">${product.name}</div>
                                            <div class="hidden-content barcode">${product.barcode}</div>
                                            <div class="hidden-content unit">${product.unit}</div>
                                            <div class="hidden-content purchasePrice">${product.purchasePrice}</div>
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
                                            <p class="m-0"><strong>Giá nhập:</strong>${product.purchasePrice}
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
        // Lấy các phần tử con bên trong thẻ <a> được click
    const productId = linkElement.querySelector('.hidden-content.productId').innerHTML;
    const name = linkElement.querySelector('.hidden-content.name').innerHTML;
    const barcode = linkElement.querySelector('.hidden-content.barcode').innerHTML;
    const unit = linkElement.querySelector('.hidden-content.unit').innerHTML;
    const purchasePrice = linkElement.querySelector('.hidden-content.purchasePrice').innerHTML;

        //Hiển thị sản phẩm ở order
            const productHTML = `
            <td>${productId}</td>
            <td></td>
            <td>
                <div class="ellipsis">
                ${name}
                </div>
            </td>
            <td>${unit}</td>
            <td>
                <div class="input-group">
                    <input type="number" value="1" min="1" step="1" class="form-control num">
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input class="form-control price" step="1000" min="0" value="0" type='number' class="num">
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input class="form-control discount" step="0.1" min="0" value="0" type='number' class="num">
                    <span class="input-group-text">%</span>
                </div>
            </td>
            <td><span class="sum"></span> VNĐ</td>
            <td>
            <a href="javascript:;" type="button" onclick="addSelect(this)" class="btn btn-close btn-danger delete">
                <div class="hidden-content productId">${productId}</div>
                <div class="hidden-content name"> ${name}</div>
                <div class="hidden-content barcode">${barcode}</div>
                <div class="hidden-content unit">${unit}</div>
                <div class="hidden-content purchasePrice">${purchasePrice}</div>
             </a>
            </td>
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
                //theA.style.display = 'block';
                updateInvoice();

            });

            orderContainer.appendChild(productRow);

            //cập nhật lại product list
            console.log('before'+products);
            console.log('productID: '+productId);
            products = products.filter((product) => product.productId !== Number(productId));
            console.log('after'+products);
            products.sort((a, b) => a.productId - b.productId);
            populateProductsSelect();
            // // C1: Xóa thẻ a
            // theA.remove();
            // // C2: Ẩn thẻ a
            // //theA.style.display = 'none';
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
            success: function(response) {
                //thêm supplier vào select list
                suppliers[supplierLength] = response;
                console.log("supplier cua them:"+suppliers[supplierLength])
        
                //sửa lại cho đúng id của nó -done
                var supplierOption = `<option value="${response.supplierId}">${response.supplierName}</option>`
                // Thêm select list supplier mới -- giá trị là supplier vừa tạo
                $("#supplierSelect").append(supplierOption);
                $('#supplierSelect').val(response.supplierId).trigger('change'); //change value
                console.log( '#supplierSelect: '+$('#supplierSelect').val());
                supplierContainer.style.display = 'none';
                // hiển thị supplier mới
                showSupplierInfo(suppliers[supplierLength]);
                //reset form
                resetCreateSupplierForm();
                round_success_noti("Thêm nhà cung cấp thành công");

            },
            error: function(error) {
                round_error_noti(error.responseText);
                resetCreateSupplierForm();
                console.error("Lỗi gọi addSupplier API: " + error.responseText);
            },
        });
    }
    function addProductByAPI() {
        let productName = $('#productName').val();
        let barCode = $('#barCode').val();
        let price = $('#price').val();
        let quantity = $('#quantity').val();
        let unit = $('#unit').val();
        let isBook = $('#isBook').val(); 

        let bookDTO =  {
                "productId": null,
                "name": productName,
                "barcode": barCode,
                "unit": unit,
                "purchasePrice": price,
                "unitInStock": quantity,
                "isBook": isBook,
                "presentImage": null
                }
        $.ajax({
            url: "https://localhost:7123/Admin/PurchaseOrder/AddProduct", // Replace with the correct API endpoint URL
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(bookDTO),
            success: function(response) {
                var productItem = `
                <a href="javascript:;" class="list-group-item item" onclick="getBookInfo(this)">
                    <div class="row d-flex align-items-center">
                        <div class="col-6 d-flex align-items-center">
                            <div class="flex-shrink-0 cover">
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
                $('#list-product').append(productItem);
        
                $('#list-product > a:last').trigger('click')

                resetCreateProductForm();
                round_success_noti("Thêm sản phẩm thành công");
            },
            error: function(error) {
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
    // Create a new product object
    const newProduct = {
        productId: Number(productId),
        name,
        barcode,
        unit,
        purchasePrice,
        // Add other properties with their corresponding values if needed
  };
  console.log(newProduct);
  console.log('curproduct'+products);
  // Push the new product object to the productExists array
     products.push(newProduct);
     // Sorting products by productId in ascending order
    products.sort((a, b) => a.productId - b.productId);
        populateProductsSelect();
        parentTr.remove();
    }

    function resetCreateSupplierForm(){
        // Clear input fields (set their values to empty strings)
        $("#supplier").val('');
        $("#phone").val('');
        $("#description").val('');
    }
    function resetCreateProductForm(){
        // Clear input fields (set their values to empty strings)
         $('#productName').val('');
         $('#barCode').val('');
         $('#price').val('');
        $('#quantity').val('');
        $('#unit').val('');
        $('#isBook').val(true); 
    }