$(function () {
    var l = abp.localization.getResource('IncubationCentre');
    var createModal = new abp.ModalManager(abp.appPath + 'Milestones/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Milestones/EditModal');

    var dataTable = $('#MilestonesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.milestones.milestone.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Milestones.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Milestones.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'EntityDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        isikUn.incubationCentre.milestones.milestone
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Category'),
                    data: "category"
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewMilestoneButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
