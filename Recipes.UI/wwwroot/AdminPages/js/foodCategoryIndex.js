$(document).ready(function () {
    $(document).on('click', '#deleteOrUndoDelete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                foodCategoryId: id,
                isWantDelete: true
            },
            url: '/Admin/FoodCategory/DeleteOrUndoDelete/',
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
});