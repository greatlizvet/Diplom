$(document).ready(function () {
    var forms = $('#forms');
    var formfilter = $('#formfilter');
    var paginationBlock = $('#paging');

    formfilter.submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        window.sortFilterFormData.updatePropsFromSerializeQuery(data);
        data = window.sortFilterFormData.getSerializeQuery();
        $.ajax({
            url     : $(this).attr('action'),
            type    : $(this).attr('method'),
            data    : data,
            success : function (html) {
                data = $(html);

                ankets = data.find('#forms').children();
                paging = data.find('#paging').children();

                forms.children().remove();
                paginationBlock.children().remove();

                forms.append(ankets);
                paginationBlock.append(paging);
            }
        });
    });
    
});