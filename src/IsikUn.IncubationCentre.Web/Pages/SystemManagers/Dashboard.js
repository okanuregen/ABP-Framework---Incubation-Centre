$(function () {

    var l = abp.localization.getResource('IncubationCentre');

    var service = isikUn.incubationCentre.applications.application;
    var createRequestModal = new abp.ModalManager(abp.appPath + 'Requests/CreateModal');

    var NewApplicationdataTable = $('#NewApplicationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        serverSide: false,
        paging: true,
        scrollY: '210px',
        order: [[1, "asc"]],
        searching: true,
        scrollX: true,
        ajax: abp.libs.datatables.createAjax(
            service.getList, { applicationStatus: "InReview" }),
        columnDefs: [
            {
                title: l('Actions'),
                rowAction: {
                    items:
                        [
                            {
                                text: l('Approve'),
                                visible: abp.auth.isGranted('IncubationCentre.SystemManagers'),
                                confirmMessage: function (data) {
                                    return l('YouAreApprovingAnAppplication', (data.record.senderName + " " + data.record.senderSurname));
                                },
                                action: function (data) {
                                    service.approveApplication(data.record.id)
                                        .then(function (data) {
                                            debugger;
                                            abp.notify.info(l('SuccessfullyApproved'));
                                            NewApplicationdataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Decline'),
                                visible: abp.auth.isGranted('IncubationCentre.SystemManagers'),
                                confirmMessage: function (data) {
                                    return l('YouAreApprovingAnAppplication', (data.record.senderName + " " + data.record.senderSurname));
                                },
                                action: function (data) {
                                    service.rejectApplication(data.record.id)
                                        .then(function (data) {
                                            debugger;
                                            abp.notify.info(l('SuccessfullyDeclined'));
                                            NewApplicationdataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('IncubationCentre.Application.Delete'),
                                confirmMessage: function (data) {
                                    return l('RequestDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            NewApplicationdataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('MembershipType'),
                data: "membershipType",
                render: function (data) {
                    return l("Enum:MembershipType_"+data)
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
                    return '<span data-long-name="true" data-long-name-size="10">'+data+'</span>';
                }
            }
        ]
    }));

    $('#NewApplicationTable')
        .on('init.dt', function () {
            $('[data-long-name="true"]').each(function (i, e) {
                var text = $(e).text().trim();
                var toolTipPlacemnt = $(e).attr("data-placement");
                var shortTextSize = $(e).attr("data-long-name-size");
                var stSize = parseInt(shortTextSize);
                stSize = stSize != NaN ? stSize : 10;

                var shortText = text;
                if (shortText.length > stSize) {
                    shortText = shortText.slice(0, stSize);
                    $(e).text(shortText + "...");
                }
                $(e).attr("data-toggle", "tooltip");
                $(e).attr("data-placement", ((toolTipPlacemnt != undefined && toolTipPlacemnt != "") ? toolTipPlacemnt : "top"));
                $(e).attr("title", text);
            });

            $('[data-toggle="tooltip"]').tooltip();
        })
        .dataTable();

    createRequestModal.onResult(function () {
        location.reload();
    });


    $('#NewRequestButton').click(function (e) {
        e.preventDefault();
        createRequestModal.open();
    });
});
