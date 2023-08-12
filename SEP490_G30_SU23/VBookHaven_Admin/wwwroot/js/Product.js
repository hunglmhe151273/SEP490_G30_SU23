const authorSelect = document.getElementById("AuthorIdList");

$(document).on('click', '#submitAuthor', function () {
    addAuthorByAPI();
});

function addAuthorByAPI() {
    var name = $('#authorName').val();
    var description = $('#authorDescription').val();
    var author = {
        "authorId": 0,
        "authorName": name,
        "description": description,
        "status": true
    }
    $.ajax({
        url: "https://localhost:7123/Admin/Product/AddAuthorAPI", // Replace with the correct API endpoint URL
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(author),
        success: function(response) {
            var authorOption = `
                <option value="${response.authorId}" selected>
                    ${response.authorName}
                </option>
            `
            // Thêm author mới vào select list
            $(authorSelect).append(authorOption);

            //reset form
            resetAddAuthorForm();
            round_success_noti("Thêm tác giả thành công");

        },
        error: function(error) {
            round_error_noti(error.responseText);
            resetAddAuthorForm();
            console.error("Lỗi gọi addAuthor API: " + error.responseText);
        },
    });
}

function resetAddAuthorForm(){
    // Clear input fields (set their values to empty strings)
    $("#authorName").val('');
    $("#authorDescription").val('');
}