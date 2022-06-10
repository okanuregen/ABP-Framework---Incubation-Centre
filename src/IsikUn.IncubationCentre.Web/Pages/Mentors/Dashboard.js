$(function () {
    var l = abp.localization.getResource('IncubationCentre');

    var createProjectModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var createRequestModal = new abp.ModalManager(abp.appPath + 'Requests/CreateModal');

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createProjectModal.open();
    });

    $('#NewRequestButton').click(function (e) {
        e.preventDefault();
        createRequestModal.open();
    });

    createProjectModal.onResult(function () {
        abp.message.success(l("NewProjectCreateMessage"))
    });

    createRequestModal.onResult(function () {
        abp.notify.success(l("RequestSentSuccesfully"));
        setTimeout(() => {
            location.reload();
        }, 1200);

    });

});
