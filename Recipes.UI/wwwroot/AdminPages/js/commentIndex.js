$(document).ready(function () {
    $(document).on('click', '#delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                commentId: id
            },
            url: '/Admin/Comment/Delete/',
            success: function () {
                alert("Çöp kutusuna taşındı!");
                window.location.href = 'Comment';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });
    });
});