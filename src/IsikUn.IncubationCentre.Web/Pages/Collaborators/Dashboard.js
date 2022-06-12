$(function () {
    var l = abp.localization.getResource('IncubationCentre');

    var createProjectModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var createRequestModal = new abp.ModalManager(abp.appPath + 'Requests/CreateModal');

    $('#JoinProjectButton').click(function (e) {
        e.preventDefault();
        location.href = "/Projects";
    });

    $('#NewRequestButton').click(function (e) {
        e.preventDefault();
        createRequestModal.open();
    });

    createProjectModal.onResult(function () {
        abp.message.success(l("NewProjectCreateMessage"))
    });

    createRequestModal.onResult(function () {
        location.reload();
    });

});
