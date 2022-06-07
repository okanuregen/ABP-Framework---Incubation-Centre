$(function () {
    var l = abp.localization.getResource('IncubationCentre');

    var createProjectModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createProjectModal.open();
    });

    createProjectModal.onResult(function () {
        abp.message.success(l("NewProjectCreateMessage"))
    });

});
