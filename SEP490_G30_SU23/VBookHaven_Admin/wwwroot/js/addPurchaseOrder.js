        // Get references to the select elements and the containers
    const supplierSelect = document.getElementById('supplierSelect');
    const supplierInfoContainer = document.getElementById('supplierInfoContainer');
    const supplierContainer = document.getElementById('supplierContainer');
    const productSelect = document.getElementById('productSelect');
    const orderContainer = document.getElementById('orderContainer');
    const orderForm = document.getElementById('orderForm');
// Temporary data
let suppliers = [];

let products = [];
    // Populate suppliers select list
function populateSuppliersSelect() {
    supplierSelect.innerHTML = `
    <option value="">Select...</option>
    ${suppliers
            .map(
                supplier => `<option value="${supplier.supplierId}">${supplier.supplierName}</option>`
            )
            .join('')
        }
    `;
}
  

    // Populate products select list
function populateProductsSelect() {
    productSelect.innerHTML = `
    <option value="">Select...</option>
    ${products
            .map(
                product => `<option value="${product.productId}">${product.name}</option>`
            )
            .join('')
        }
    `;
}
   

    // Add event listener for the select element
    supplierSelect.addEventListener('change', function () {
            const selectedValue = this.value;

    if (selectedValue === 'new') {
        createNewSupplierForm();
    supplierContainer.style.display = 'none'; // Hide the supplier container
    } else if (selectedValue !== '') {
        const selectedSupplier = suppliers.find(supplier => supplier.supplierId === parseInt(selectedValue));
    showSupplierInfo(selectedSupplier);
    supplierContainer.style.display = 'none'; // Hide the supplier container
            } else {
        clearSupplierInfo();
    supplierContainer.style.display = 'block'; // Show the supplier container
            }
        });

    // Function to show the supplier info
    function showSupplierInfo(supplier) {
        // Clear supplier info container
        supplierInfoContainer.innerHTML = `
                <div class="supplier-info">
                    <label>Name:</label>
                    <span>${supplier.supplierName}</span><br>
                    <label>Phone:</label>
                    <span>${supplier.phone}</span><br>
                    <span class="close-btn">Close</span>
                </div>
            `;

    // Add event listener to the close button
    const closeBtn = supplierInfoContainer.querySelector('.close-btn');
    closeBtn.addEventListener('click', function () {
        clearSupplierInfo();
    supplierContainer.style.display = 'block'; // Show the supplier container
    supplierSelect.value = ''; // Set the select option to an empty string
            });
        }

    // Function to clear the supplier info container
    function clearSupplierInfo() {
        supplierInfoContainer.innerHTML = '';
    supplierSelect.value = ''; // Set the select option to an empty string
    supplierContainer.style.display = 'block'; // Show the supplier container
        }

    // Function to add a product to the order
    function addProductToOrder(productId) {
    const selectedOption = productSelect.querySelector(`option[value="${productId}"]`);
    const productName = selectedOption.textContent;

    const productHTML = `
    <div class="product-info">
     <label>ProductID:</label>
     <input name="ProductIDList" value="${productId}">
    <label>Product Name:</label>
    <span>${productName}</span><br>
    <label>Quantity:</label>
    <input name="QuantityList" class="form-control"><br>
    <label>UnitPrice:</label>
    <input name="UnitPriceList" class="form-control"><br>
    <button class="btn btn-danger">Remove</button>
                    </div>
                        `;

                        const productRow = document.createElement('div');
                        //productRow.classList.add('form-control');
                        productRow.innerHTML = productHTML;

                        const removeBtn = productRow.querySelector('.btn-danger');
                        removeBtn.addEventListener('click', function () {
                            // Remove the product row from the order
                            productRow.remove();

                        // Add the product back to the product select list
                        const newOption = document.createElement('option');
                        newOption.value = productId;
                        newOption.text = productName;
                        newOption.setAttribute('data-image', productImage);
                        newOption.setAttribute('data-quantity', productQuantity);
                        productSelect.add(newOption);
            });

                        orderContainer.appendChild(productRow);

                        // Remove the selected product option
                        selectedOption.remove();
        }

                        // Event listener for the select product element
                        productSelect.addEventListener('change', function () {
            const selectedValue = this.value;

                        if (selectedValue !== '') {
                            addProductToOrder(selectedValue);
                        this.value = ''; // Reset the product select to an empty string
            }
        });

                        // Event listener for the order form submission
                        orderForm.addEventListener('submit', function (event) {
                            console.log('submit form');
                       // event.preventDefault();
            // Process the order form submission
            // ...
                        });


fetchDataFromAPIs();
// Lấy product list và supplier list từ api
function fetchDataFromAPIs() {
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/GetAllSuppliers',
        type: 'GET',
        dataType: 'json',
        success: function (suppliersData) {
            console.log(suppliersData);
            suppliers = suppliersData; // Update the suppliers array with fetched data
                   suppliers.forEach(supplier => {
                 console.log(supplier);
             });
            populateSuppliersSelect();
        },
        error: function (error) {
            console.log('Error fetching suppliers data:', error);
        }
    });
    $.ajax({
        url: 'https://localhost:7123/Admin/PurchaseOrder/getallproducts',
        type: 'GET',
        dataType: 'json',
        success: function (productsData) {
            console.log(productsData);
            products = productsData;
            products.forEach(product => {
                console.log(product);
            })
            populateProductsSelect();
        },
        error: function (error) {
            console.log('Error fetching products data:', error);
        }
    });
}
