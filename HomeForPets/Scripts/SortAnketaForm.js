$(document).ready(function () {
    var forms = $('#forms');
    var sortAnketaForm = $('#sortAnketaForm');
    var sortSelect = sortAnketaForm.find('#Sort');

    sortSelect.change(function(e) {
        sortAnketaForm.submit();
    });

    sortAnketaForm.submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        window.sortFilterFormData.updatePropsFromSerializeQuery(data);
        data = window.sortFilterFormData.getSerializeQuery();
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: data,
            success: function (html) {
                data = $(html);
                ankets = data.find('#forms').children();
                forms.children().remove();
                forms.append(ankets);
            }
        });
    });

});