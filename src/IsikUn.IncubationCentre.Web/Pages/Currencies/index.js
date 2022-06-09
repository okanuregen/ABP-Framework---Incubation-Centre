$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.currencies.currency;
    var createModal = new abp.ModalManager(abp.appPath + 'Currencies/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Currencies/EditModal');

    var dataTable = $('#CurrencyTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('IncubationCentre.Currencies.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Currencies.Delete'),
                                confirmMessage: function (data) {
                                    return l('EntityDeletionConfirmationMessage', data.record.title);
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
                title: l('Country'),
                data: "country"
            },
            {
                title: l('Title'),
                data: "title"
            },
            {
                title: l('Symbol'),
                data: "symbol"
            },
            {
                title: l('IsDefault'),
                data: "isDefault",
                render: function (data) {
                    if (data == null) return "-";
                    return data ? "<i class='fa fa-check text-success'></i>" : "<i class='fa fa-times text-danger'></i>"
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

    $('#NewCurrencyButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
