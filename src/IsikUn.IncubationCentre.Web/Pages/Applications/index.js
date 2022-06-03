$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.applications.application;
    var createModal = new abp.ModalManager(abp.appPath + 'Applications/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Applications/EditModal');

    var dataTable = $('#ApplicationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('IncubationCentre.Application.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Application.Delete'),
                                confirmMessage: function (data) {
                                    return l('ApplicationDeletionConfirmationMessage', data.record.id);
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
                title: l('ApplicationMembershipType'),
                data: "membershipType"
            },
            {
                title: l('ApplicationSenderMail'),
                data: "senderMail"
            },
            {
                title: l('ApplicationSenderPhoneNumber'),
                data: "senderPhoneNumber"
            },
            {
                title: l('ApplicationExplanation'),
                data: "explanation"
            },
            {
                title: l('ApplicationReceiver'),
                data: "receiver"
            },
            {
                title: l('ApplicationApplicationStatus'),
                data: "applicationStatus"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewApplicationButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
