$(function () {
    var l = abp.localization.getResource('IncubationCentre');

    var createRequestModal = new abp.ModalManager(abp.appPath + 'Requests/CreateModal');

    createRequestModal.onResult(function () {
        location.reload();
    });

    $('#NewRequestButton').click(function (e) {
        e.preventDefault();
        createRequestModal.open();

    });

    createRequestModal.onResult(function () {
        eventsTable.ajax.reload();
    });





    
});
