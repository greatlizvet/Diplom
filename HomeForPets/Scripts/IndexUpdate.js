$(document).ready(function () {
    var forms = $('#forms');
    var formfilter = $('#formfilter');

    formfilter.submit(function (e) {
        e.preventDefault();
        $.ajax({
            url     : $(this).attr('action'),
            type    : $(this).attr('method'),
            data    : $(this).serialize(),
            success: function (html) {
                data = $(html);
                ankets = data.find('#forms').children();
                forms.children().remove();
                forms.append(ankets);
            }
        });
    });
    
});