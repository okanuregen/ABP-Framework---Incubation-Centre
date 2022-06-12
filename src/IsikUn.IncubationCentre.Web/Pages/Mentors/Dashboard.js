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





    var createEventModal = new abp.ModalManager(abp.appPath + 'Events/CreateModal');
    var editEventModal = new abp.ModalManager(abp.appPath + 'Events/EditModal');

    var eventsTable = $('#EventsTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.events.event.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [ // TODO : create new event button goes here

                            {
                                text: l('Edit'),
                                visible: function (data) {
                                    debugger;
                                    return data.creatorPerson.identityUserId == abp.currentUser.id
                                },
                                action: function (data) {
                                    editEventModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: function (data) {
                                    debugger;
                                    return data.creatorPerson.identityUserId == abp.currentUser.id
                                },
                                confirmMessage: function (data) {
                                    return l('EntityDeletionConfirmationMessage', data.record.title);
                                },
                                action: function (data) {
                                    isikUn.incubationCentre.events.event.delete(data.record.id)
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
                title: l('ProjectName'),
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

    createEventModal.onResult(function () {
        eventsTable.ajax.reload();
    });

    editEventModal.onResult(function () {
        eventsTable.ajax.reload();
    });
});
