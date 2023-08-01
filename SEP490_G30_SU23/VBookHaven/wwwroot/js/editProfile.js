var readURL = function (input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('.profile-pic').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$(".file-upload ").on('change', function () {
    readURL(this);
});
$(".upload-button ").on('click', function () {
    $(".file-upload ").click();
});

function redirectToPage() {
    // Replace "target-page.html " with the URL of the page you want to redirect to
    window.location.href = "Changepassword";
}