$(function () {
    var l = abp.localization.getResource('IncubationCentre');


    var dataTable = $('#AllProjectsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.projects.project.getList, {
                FilterByInvesmentReady = true,
                InvesmentReady = true
            }),
            columnDefs: [
                { // TODO this the part that needs to be changed
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                { 
                                    text: l('Detail'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Projects.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Invest'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Projects.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'EntityDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        isikUn.incubationCentre.projects.project
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
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l("Enum:ProjectStatus_" + data);
                    }
                },
                {
                    title: l('CompletionDate'),
                    data: "completionDate",
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
                    title: l('Tags'),
                    data: "tags",
                    render: function (data) {
                        if (data == null) return "-";
                        var text = "";
                        data.split(",").forEach(function (tag) {
                            text += `<span class="badge badge-info me-2">${tag}</span>`
                        });
                        return text == "" ? "-" : text;
                    }
                },
                {
                    title: l('SharePerInvest'),
                    data: "sharePerInvest",
                    render: function (data) {
                        return data + " %";
                    }
                },
                {
                    title: l('InvesmentReady'),
                    data: "invesmentReady",
                    render: function (data) {
                        return data ? `<span class="badge badge-success p-2">${l('Yes')}</span>` : `<span class="badge badge-danger p-2">${l('No')}</span>`
                    }
                },
                {
                    title: l('OpenForInvesment'),
                    data: "openForInvesment",
                    render: function (data) {
                        return data ? `<span class="badge badge-success p-2">${l('Yes')}</span>` : `<span class="badge badge-danger p-2">${l('No')}</span>`
                    }
                }
            ]
        })
    );

    //$('#MyMentoringProjectsTable')
    //    .on('init.dt', function () {
    //        $('#MyMentoringProjectsTable').DataTable().draw();

    //    })
    //    .dataTable();


    var dataTable1 = $('#EventsTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('Details'),
                                visible: abp.auth.isGranted('IncubationCentre.Events.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
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
                data: "eventDate"
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
