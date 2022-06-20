$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.applications.application;
    var createModal = new abp.ModalManager(abp.appPath + 'Applications/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Applications/EditModal');

    var NewApplicationdataTable = $('#ApplicationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        serverSide: false,
        paging: true,
        scrollY: '440px',
        order: [[1, "asc"]],
        searching: true,
        scrollX: false,
        ajax: abp.libs.datatables.createAjax(
            service.getList),
        columnDefs: [
            {
                title: l('Status'),
                data: "applicationStatus",
                render: function (data) {
                    if (data == 0) {
                        return "<div class='text-center'><i class='fa fa-clock text-info'></i></div>"
                    }
                    if (data == 1) {
                        return "<div class='text-center'><i class='fa fa-check text-success'></i></div>"
                    }
                    if (data == 2) {
                        return "<div class='text-center'><i class='fa fa-times text-danger'></i></div>"
                    }
                    return "-";
                }
            },
            {
                title: l('Role'),
                data: "membershipType",
                render: function (data) {
                    return l("Enum:MembershipType_" + data)
                }
            },
            {
                title: l('Name'),
                data: "senderName"
            },
            {
                title: l('Surname'),
                data: "senderSurname"
            },
            {
                title: l('CreationTime'),
                data: "creationTime",
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
                title: l('Mail'),
                data: "senderMail"
            },
            {
                title: l('Explanation'),
                data: "explanation",
                render: function (data) {
                    return '<span data-long-name="true" data-long-name-size="105">' + data + '</span>';
                }
            }
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
