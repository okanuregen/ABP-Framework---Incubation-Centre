var l = null;
$(function () {
    l = abp.localization.getResource('IncubationCentre');

    var createModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');

    $('#ProjectsTable thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#example thead');

    var dataTable = $('#ProjectsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.projects.project.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Projects.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Detail'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Projects'),
                                    action: function (data) {
                                        location.href = "/Projects/Detail?=id=" + data.record.id;
                                    }
                                },
                                {
                                    text: l('Delete'),
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

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
