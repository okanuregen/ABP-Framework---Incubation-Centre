$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.events.event;
    var createModal = new abp.ModalManager(abp.appPath + 'Events/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Events/EditModal');

    var dataTable = $('#EventTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('IncubationCentre.Event.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Event.Delete'),
                                confirmMessage: function (data) {
                                    return l('EventDeletionConfirmationMessage', data.record.id);
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
                title: l('EventTitle'),
                data: "title"
            },
            {
                title: l('EventEventDate'),
                data: "eventDate"
            },
            {
                title: l('EventDescription'),
                data: "description"
            },
            {
                title: l('EventProjectId'),
                data: "projectId"
            },
            {
                title: l('EventProject'),
                data: "project"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewEventButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
