$(document).ready(function () {
    $('#denyModal').on('show.bs.modal', function (event) {
        // data-anketa-id
        var id = event.relatedTarget.attributes[4].value;

        $('#modalForm_id').val(id);
    });
});