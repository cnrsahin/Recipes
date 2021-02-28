$(document).ready(function () {
    $(document).on('click', '#delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        Swal.fire({
            title: 'Silmek icin onayla?',
            text: 'Bu kategori silinecektir!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet',
            cancelButtonText: 'Hayır'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        foodCategoryId: id
                    },
                    url: '/Admin/FoodCategory/Delete/',
                    success: function (data) {
                        const model = jQuery.parseJSON(data);
                        Swal.fire(model.Message, 'testttt', 'success').then((result) => window.location.href = 'FoodCategory' );
                    },
                    error: function (error) {
                        console.log(error);
                        Swal.fire('Yetkiniz bulunmamaktadır!', '', 'error');
                    }
                });
            }
        });
    });

    $(function () {
        /*  Modal form starts*/
        const placeModal = $('#placeModal');
        const url = '/Admin/FoodCategory/Add/';
        $('#AddBtn').click(function () {
            $.get(url).done(function (data) {
                placeModal.html(data);
                placeModal.find(".modal").modal('show');
            });
        });
        /* Modal form ends. */

        /* New Category */
        placeModal.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const postForm = $('#categoryAdd');
            const url = postForm.attr('action');
            const dataSend = postForm.serialize();

            $.post(url, dataSend).done(function (data) {
                toastr.info(data, 'Yeni Kategori Sonucu', { timeOut: 3000 });
                placeModal.find(".modal").modal('hide');
            });
        });
        /* New Category */
    });
});