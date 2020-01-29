$(document).ready(function () {
    $('#phoneNumber').click(function (e) {
        $(this).removeClass('btn-primary');
        $(this).addClass('btn-default');
        var phoneNumber = $(this).val();
        $(this).text(phoneNumber);
    });
});