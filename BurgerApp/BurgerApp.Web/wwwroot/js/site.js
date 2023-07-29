//Show BurgerAd
$(document).ready(function () {
    $('#openModalBtnMobile').on('click', function () {
        $('#myModal').modal('show');
    });

    $('#openModalBtnDesktop').on('click', function () {
        $('#myModal').modal('show');
    });

    $('#closeModalBtn').on('click', function () {
        $('#myModal').modal('hide');
    });
});