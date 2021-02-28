$(document).ready(function () {
    $(document).on('click', '#doActive', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        
        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                userId: id
            },
            url: '/Admin/User/DoActive/',
            success: function (data) {
                alert("Kullanıcı Aktif Edildi!");
                window.location.href = 'User';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });
    });

    $(document).on('click', '#doPassive', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        
        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                userId: id
            },
            url: '/Admin/User/DoPassive/',
            success: function (data) {
                alert("Kullanıcı Pasif Edildi!");
                window.location.href = 'User';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });
    });
});
