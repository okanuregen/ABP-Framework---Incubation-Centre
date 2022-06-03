$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.requests.request;
    var createModal = new abp.ModalManager(abp.appPath + 'Requests/Request/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Requests/Request/EditModal');

    var dataTable = $('#RequestTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('IncubationCentre.Request.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Request.Delete'),
                                confirmMessage: function (data) {
                                    return l('RequestDeletionConfirmationMessage', data.record.id);
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
                title: l('RequestSenderId'),
                data: "senderId"
            },
            {
                title: l('RequestSender'),
                data: "sender"
            },
            {
                title: l('RequestReceiverId'),
                data: "receiverId"
            },
            {
                title: l('RequestReceiver'),
                data: "receiver"
            },
            {
                title: l('RequestTitle'),
                data: "title"
            },
            {
                title: l('RequestExplanation'),
                data: "explanation"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewRequestButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
