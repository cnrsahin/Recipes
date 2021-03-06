﻿$(document).ready(function () {
    $(document).on('click', '#UnDelete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                recipeId: id
            },
            url: '/Admin/Recipe/UnDelete/',
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