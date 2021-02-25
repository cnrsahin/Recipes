$(document).ready(function () {
    $(document).on('click', '#delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                foodCategoryId: id
            },
            url: '/Admin/FoodCategory/Delete/',
            success: function () {
                alert("Çöp kutusuna taşındı!");
                window.location.href = 'FoodCategory';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });
    });

    $(function () {
        /*  Modal Add form starts*/
        const placeModal = $('#placeModal');
        const url = '/Admin/FoodCategory/Add/';
        $('#AddBtn').click(function () {
            $.get(url).done(function (data) {
                placeModal.html(data);
                placeModal.find(".modal").modal('show');
            });
        });
        /* Modal Add form ends. */

        placeModal.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const postForm = $('#categoryAdd');
            const url = postForm.attr('action');
            const dataSend = postForm.serialize();

            $.post(url, dataSend).done(function (data) {
                toastr.info(data, 'Yeni Kategori Sonucu', {timeOut: 3000});
                placeModal.find(".modal").modal('hide');
            });
        });
    });
});