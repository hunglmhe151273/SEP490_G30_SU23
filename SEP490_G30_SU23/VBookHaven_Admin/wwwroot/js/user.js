
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
                    console.log("image: " + data);
                    if (data !== null){
                    var fixedImageUrl = data.replace(/\\/g, "/");
                    console.log("fixedImageUrl: "+ fixedImageUrl);
                        return `<img src="${fixedImageUrl}" alt="" style="width: 70px; height: 70px;">`;
                    }
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
            {
                "data": "id",
                "render": function (data) {
                    console.log("id: " + data);
                    return `
                        <td class="text-center">
                            <button type="button" class="btn btn-sm btn-default" onclick="window.location.href='/admin/user/edit?userId=${data}'">
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
                round_success_noti(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}

$(document).ready(function () {
    $(".filter").on("input", function () {
        var search = $("#search").val().toLowerCase();
        var role = $("#role").val().toLowerCase();
        var status = $("#status").val();
        $("#myTable tr").filter(function () {
            $(this).toggle(
                status == "1" ? $(this).find('td:eq(5)').val().is(":checked") : $(this).find('td:eq(5)').val() &&
                    $(this).find('td:eq(4)').text().toLowerCase().indexOf(role) > -1 &&
                    ($(this).find('td:eq(0)').text().toLowerCase().indexOf(search) > -1 ||
                        $(this).find('td:eq(2)').text().toLowerCase().indexOf(search) > -1 ||
                        $(this).find('td:eq(3)').text().toLowerCase().indexOf(search) > -1)
            );
        })
    });
});
function goBack() {
    history.back();
}