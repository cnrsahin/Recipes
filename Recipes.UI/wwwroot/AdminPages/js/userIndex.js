$(document).ready(function () {
    $(document).on('click', '#doActiveOrPassive', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                userId: id
            },
            url: '/Admin/User/DoActiveOrPassive/',
            success: function (data) {
                alert("Kullanıcı Statüsü Değiştirildi!");
                window.location.href = 'User';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });

    });
});
