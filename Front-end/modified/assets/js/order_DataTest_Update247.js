let customers = [{
        "customerId": 1,
        "address": "abc địa chỉ chi tiết",
        "customerName": "Tên nhà cung Cấp A",
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
        "customerId": 2,
        "address": "Địa chỉ chi tiết abc",
        "customerName": "Nhà Cung Cấp A",
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
        "customerId": 3,
        "address": null,
        "customerName": "Tên nhà cung cấp C",
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
        "customerId": 4,
        "address": null,
        "customerName": "Tên nhà cung cấp D",
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
        "customerId": 5,
        "address": null,
        "customerName": "Tên nhà cung cấp E",
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
        "customerId": 6,
        "address": null,
        "customerName": "Tên nhà cung cấp F",
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
        "customerId": 7,
        "address": null,
        "customerName": "Tên nhà cung cấp H",
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
        "customerId": 8,
        "address": null,
        "customerName": "Tên nhà cung cấp G",
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
const customerSelect = document.getElementById('customerSelect');
const customerInfoContainer = document.getElementById('customerInfoContainer');
const customerContainer = document.getElementById('customerContainer');
const productList = document.getElementById('list-product');
const orderContainer = document.getElementById('orderContainer');

//Call API
//fetchDataFromAPIs();

//for test
populatecustomersSelect();
populateProductsSelect();

// Function to fetch customers and products data using AJAX
function fetchDataFromAPIs() {
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/GetAllcustomers',
        type: 'GET',
        dataType: 'json',
        success: function(customersData) {
            customers = customersData; // Update the customers array with fetched data
            //       customers.forEach(customer => {
            //     console.log(customer);
            // });
            populatecustomersSelect();
        },
        error: function(error) {
            console.log('Error fetching customers data:', error);
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
// Function to populate customers select list
function populatecustomersSelect() {
    console.log('Nhà cung cấp' + customers);
    customerSelect.innerHTML = `
    <option value="">Chọn nhà cung cấp...</option>
    ${customers
        .map(
            customer => `<option value="${customer.customerId}">${customer.customerName}</option>`
        )
        .join('')
    }
`;
}

$("#customerSelect").change(() => {
    selectedValue = $("#customerSelect").val();
    if (selectedValue !== '') {
        var selectedcustomer = customers.find(customer => customer.customerId === parseInt(selectedValue));
        console.log('selectedcustomer'+selectedcustomer);
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
            if (customer.province !== null) {
                showcustomerInfoDiv += `<label class="fw-bold">Địa chỉ: ${customer.province}</label>`;
            }
            showcustomerInfoDiv += `</div>`;

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
                    product => `<a href="javascript:;" class="list-group-item item" onclick="getBookInfo(this)">
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
                                            <p class="m-0"><strong>Giá nhập:</strong> ${product.purchasePrice}
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
        const unit = linkElement.querySelector('.productUnit').textContent;
        var imgSrc = linkElement.querySelector('.productImg').src;

        console.log(bookTitle + ' ' + imgSrc);
        //Hiển thị sản phẩm ở order
            const productHTML = `
            <td></td>
            <td> <img src="${imgSrc}"></td>
            <td>
                <div class="ellipsis">
                ${bookTitle}
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
                <a type="button" class="btn btn-close btn-danger delete"></a>
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
                productList.append(theA);
                 // C2: Hiển thị lại thẻ a
                //theA.style.display = 'block';
                updateInvoice();
            });

            orderContainer.appendChild(productRow);

            // C1: Xóa thẻ a
            theA.remove();
            // C2: Ẩn thẻ a
            //theA.style.display = 'none';
            updateInvoice();
    }

    $(document).on('click', '#submitProduct', function () {
        //call api to add
        addProductByAPI();
    });

    $(document).on('click', '#submitcustomer', function () {
        addcustomerByAPI();
    });

    function addcustomerByAPI() {
        var name = $('#customer').val();
        var phone = $('#phone').val();
        var description = $('#description').val();
        var customerLength = customers.length;
        var customerDTO = {
            "customerId": null,
            "address": null,
            "customerName": name,
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
            url: "https://localhost:7123/Admin/customers/Addcustomer", // Replace with the correct API endpoint URL
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(customerDTO),
            success: function(response) {
                //thêm customer vào select list
                customers[customerLength] = response;
                console.log("customer cua them:"+customers[customerLength])
        
                //sửa lại cho đúng id của nó -done
                var customerOption = `<option value="${response.customerId}">${response.customerName}</option>`
                // Thêm select list customer mới -- giá trị là customer vừa tạo
                $("#customerSelect").append(customerOption);
                $('#customerSelect').val(response.customerId).trigger('change'); //change value
                console.log( '#customerSelect: '+$('#customerSelect').val());
                customerContainer.style.display = 'none';
                // hiển thị customer mới
                showcustomerInfo(customers[customerLength]);
                //reset form
                resetCreatecustomerForm();
                round_success_noti("Thêm nhà cung cấp thành công");

            },
            error: function(error) {
                round_error_noti(error.responseText);
                resetCreatecustomerForm();
                console.error("Lỗi gọi addcustomer API: " + error.responseText);
            },
        });
    }
    function addProductByAPI() {
        var productName = $('#productName').val();
        var barCode = $('#barCode').val();
        var price = $('#price').val();
        var quantity = $('#quantity').val();
        var unit = $('#unit').val();
        var isBook = $('#isBook').val(); 

        var bookDTO =  {
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


    function resetCreatecustomerForm(){
        // Clear input fields (set their values to empty strings)
        $("#customer").val('');
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