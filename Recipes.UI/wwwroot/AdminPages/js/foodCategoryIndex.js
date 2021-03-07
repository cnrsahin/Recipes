$(document).ready(function () {
    $(document).on('click', '#delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        Swal.fire({
            title: 'Silmek icin onayla?',
            text: `Çöp kutusuna taşınacaktır.`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#295939',
            cancelButtonColor: '#fa1e0e',
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
                        Swal.fire(model.Message, '', 'success').then((result) => window.location.href = 'FoodCategory');
                    },
                    error: function (error) {
                        console.log(error);
                        Swal.fire('Kısıtlı Erişim!!!', 'Bu işlem için yetkiniz bulunmamaktadır.', 'error');
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

        //placeModal.on('click', '#btnSave', function (event) {
        //    event.preventDefault();
        //    const postForm = $('#categoryAdd');
        //    const url = postForm.attr('action');
        //    const dataSend = postForm.serialize();

        //    $.post(url, dataSend).done(function (data) {
        //        toastr.info(data, 'Yeni Kategori Sonucu', { timeOut: 3000 });
        //        placeModal.find(".modal").modal('hide');
        //    });
        //});

        placeModal.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#categoryAdd');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        toastr.info(data, 'Yeni Kategori Sonucu', { timeOut: 3000 });
                        placeModal.find(".modal").modal('hide');
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(data, 'Hata!');
                    }
                });
            });

        /* New Category */
    });

    $(function () {
        const placeModal = $('#placeModal');
        const url = '/Admin/FoodCategory/Update/';
        $(document).on('click',
            '#update', function (evet) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { categoryId: id }).done(function (data) {
                    placeModal.html(data);
                    placeModal.find(".modal").modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        placeModal.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();
                const form = $('#categoryUpdate');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        toastr.info(data, 'Kategori', { timeOut: 3000 });
                        placeModal.find(".modal").modal('hide');
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(data, 'Hata!');
                    }
                });
            });
    });
});