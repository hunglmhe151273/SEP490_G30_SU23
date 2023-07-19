$(document).ready(function () {
    $('.show-sub').click(function () {
        var subcategories = $('.category').has(this).nextUntil('.category');
        subcategories.toggle();
    });
});