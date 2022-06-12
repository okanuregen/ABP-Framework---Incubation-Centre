$(function () {
    var l = abp.localization.getResource('IncubationCentre');

    var createProjectModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var createRequestModal = new abp.ModalManager(abp.appPath + 'Requests/CreateModal');
    var createEventModal = new abp.ModalManager(abp.appPath + 'Events/CreateModal');

    var dataEventsTable = $('#EventsTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.events.event.getList),
        columnDefs: [
            //{
            //    rowAction: {
            //        items:
            //            [
            //                {
            //                    text: l('Edit'),
            //                    visible: abp.auth.isGranted('IncubationCentre.Events.Edit'),
            //                    action: function (data) {
            //                        editModal.open({ id: data.record.id });
            //                    }
            //                },
            //                {
            //                    text: l('Delete'),
            //                    visible: abp.auth.isGranted('IncubationCentre.Events.Delete'),
            //                    confirmMessage: function (data) {
            //                        return l('EventDeletionConfirmationMessage', data.record.id);
            //                    },
            //                    action: function (data) {
            //                        service.delete(data.record.id)
            //                            .then(function () {
            //                                abp.notify.info(l('SuccessfullyDeleted'));
            //                                dataEventsTable.ajax.reload();
            //                            });
            //                    }
            //                }
            //            ]
            //    }
            //},
            {
                title: l('Title'),
                data: "title"
            },
            {
                title: l('Date'),
                data: "eventDate",
                render: function (data) {
                    if (data == null) return "-";
                    try {
                        var date = new Date(data).toLocaleDateString();
                        return date != "Invalid Date" ? date : "-";
                    } catch {
                        return "-";
                    }
                }
            },
            {
                title: l('Description'),
                data: "description"
            },
            {
                title: l('Project'),
                data: "project",
                render: function (data) {
                    if (data == null) return "-";
                    if (data.name == null) return "-";
                    return data.name;
                }
            },
        ]
    }));


    $('#NewEventButton').click(function (e) {
        e.preventDefault();
        createEventModal.open();
    });

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
        location.reload();
    });


});
