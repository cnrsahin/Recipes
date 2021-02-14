$(document).ready(function () {
    $(document).on('click', '#deleteOrUndoDelete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                commentId: id,
                isWantDelete: true
            },
            url: '/Admin/Comment/DeleteOrUndoDelete/',
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