$(document).ready(function () {
    $(document).on('click', '#deleteOrUndoDelete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                commentId: id,
                isWantDelete: false
            },
            url: '/Admin/Comment/DeleteOrUndoDelete/',
            success: function (data) {
                alert("Çöp kutusundan geri alındı!");
                window.location.href = 'GetRemoved';
            },
            error: function (error) {
                console.log(error);
                alert("Bu işlem için yetkiniz bulunmamaktadır!");
            }
        });
    });
});