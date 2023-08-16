/**
 * Initializes the event handlers for the modal buttons when the document is ready.
 * This function sets up the click event handlers for buttons with specific IDs to show or hide the modal.
 *
 * @function
 * @name initializeModalEventHandlers
 * @memberof jQuery
 * @returns {void}
 */
$(document).ready(function () {
    /**
     * Event handler for the "Open Modal (Mobile)" button click event.
     * Shows the modal with the ID "myModal" when clicked.
     *
     * @function
     * @name onOpenModalMobileClick
     * @memberof jQuery
     * @returns {void}
     */
    $('#openModalBtnMobile').on('click', function () {
        $('#myModal').modal('show');
    });

    /**
     * Event handler for the "Open Modal (Desktop)" button click event.
     * Shows the modal with the ID "myModal" when clicked.
     *
     * @function
     * @name onOpenModalDesktopClick
     * @memberof jQuery
     * @returns {void}
     */
    $('#openModalBtnDesktop').on('click', function () {
        $('#myModal').modal('show');
    });

    /**
     * Event handler for the "Close Modal" button click event.
     * Hides the modal with the ID "myModal" when clicked.
     *
     * @function
     * @name onCloseModalClick
     * @memberof jQuery
     * @returns {void}
     */
    $('#closeModalBtn').on('click', function () {
        $('#myModal').modal('hide');
    });
});