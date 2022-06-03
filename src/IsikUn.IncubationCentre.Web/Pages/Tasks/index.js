$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.tasks.task;
    var createModal = new abp.ModalManager(abp.appPath + 'Tasks/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Tasks/EditModal');

    var dataTable = $('#TaskTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('IncubationCentre.Task.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Task.Delete'),
                                confirmMessage: function (data) {
                                    return l('TaskDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('TaskAssignedToId'),
                data: "assignedToId"
            },
            {
                title: l('TaskAssignedTo'),
                data: "assignedTo"
            },
            {
                title: l('TaskisDone'),
                data: "isDone"
            },
            {
                title: l('TaskTitle'),
                data: "title"
            },
            {
                title: l('TaskDescription'),
                data: "description"
            },
            {
                title: l('TaskCompletionDate'),
                data: "completionDate"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewTaskButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
