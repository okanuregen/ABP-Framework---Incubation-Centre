$(function () {
    var l = abp.localization.getResource('IncubationCentre');
    var createModal = new abp.ModalManager(abp.appPath + 'Collaborators/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Collaborators/EditModal');

    var dataTable = $('#CollaboratorsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.collaborators.collaborator.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Detail'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Collaborators'),
                                    action: function (data) {
                                        location.href = "/Collaborators/Detail?id=" + data.record.id;
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('IncubationCentre.Collaborators.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'EntityDeletionConfirmationMessage',
                                            data.record.identityUser.userName
                                        );
                                    },
                                    action: function (data) {
                                        isikUn.incubationCentre.collaborators.collaborator
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
                    data: "identityUser.name"
                },
                {
                    title: l('Surname'),
                    data: "identityUser.surname"
                },
                {
                    title: l('Email'),
                    data: "identityUser.email"
                },
                {
                    title: l('Skills'),
                    data: "skills",
                    render: function (data) {
                        var text = "";
                        data.map(x => x.name).forEach(function (skill) {
                            text += `<span class="badge badge-info me-2">${skill}</span>`
                        });
                        return text == "" ? "-" : text;
                    }
                },
                {
                    title: l('isActive'),
                    data: "isActivated",
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

    $('#NewCollaboratorButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
