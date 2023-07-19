
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/GetAllStaff' },
        "columns": [
            {
                "data": "staff.image",
                "render": function (data) {
                    console.log("image: "+ data);
                    var fixedImageUrl = data.replace(/\\/g, "/");
                    console.log("fixedImageUrl: "+ fixedImageUrl);
                    return `<img src="${fixedImageUrl}" alt="" style="max-width: 100px; max-height: 100px;">`;//
                },
                "width": "5%" },
            { "data": "staff.fullName", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "staff.phone", "width": "15%" },
            { "data": "staff.address", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                         <td>
                                        <div class="form-check form-switch">
                                            <input onclick=LockUnlock('${data.id}') class="form-check-input text-center fs-5 m-0" type="checkbox" >
                                        </div>
                                    </td>
                    `
                    }
                    else {
                        return `
                        <td>
                                        <div class="form-check form-switch">
                                            <input onclick=LockUnlock('${data.id}') class="form-check-input text-center fs-5 m-0" type="checkbox" checked>
                                        </div>
                                    </td>
                    `
                    }

                },
                "width": "20%"
            },
            // Additional column with the desired content
            {
                "render": function () {
                    return `
                        <td class="text-center">
                            <button type="button" class="btn btn-sm btn-default">
                                <i class="lni lni-eye"></i>
                            </button>
                        </td>
                    `;
                },
                "width": "20%"
            }
        ]
    });
}
function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}