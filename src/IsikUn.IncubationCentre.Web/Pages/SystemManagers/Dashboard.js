var page = {
    defines: {
        MentorAssignmentTable : null
    }
}
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
                                            if (data == null) {
                                                abp.message.warn(l("UnsuccesfullCreatingAccount"))
                                            } else {
                                                abp.message.success(l('SuccessfullyCreatingAccount', data.identityUser.userName));
                                            }
                                            NewApplicationdataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Decline'),
                                visible: abp.auth.isGranted('IncubationCentre.SystemManagers'),
                                confirmMessage: function (data) {
                                    return l('YouAreRejectingAnAppplication', (data.record.senderName + " " + data.record.senderSurname));
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
                title: l('Role'),
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
                    return '<span data-long-name="true" data-long-name-size="105">'+data+'</span>';
                }
            }
        ]
    }));

    $('#NewApplicationTable')
        .on('init.dt', function () {
            debugger;
            $('#NewApplicationTable').DataTable().draw();
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


    var newProjectDataTable = $('#NewProjectsListTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false,
            paging: true,
            scrollY: '210px',
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(isikUn.incubationCentre.projects.project.getList, { status: 5, fiterByStatus: true }),
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
                                        return l('YouAreApprovingAProject', (data.record.name));
                                    },
                                    action: function (data) {
                                        isikUn.incubationCentre.projects.project.approveProject(data.record.id)
                                            .then(function (data) {
                                                abp.notify.info(l('SuccessfullyApproved'));
                                                newProjectDataTable.ajax.reload();
                                            });
                                    }
                                },
                                {
                                    text: l('Decline'),
                                    visible: abp.auth.isGranted('IncubationCentre.SystemManagers'),
                                    confirmMessage: function (data) {
                                        return l('YouAreRejectingAProject', (record.name));
                                    },
                                    action: function (data) {
                                        isikUn.incubationCentre.projects.project.rejectProject(data.record.id)
                                            .then(function (data) {
                                                abp.notify.info(l('SuccessfullyDeclined'));
                                                newProjectDataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Entreprenur'),
                    data: "entrepreneurs",
                    render: function (data) {
                        try {
                            return data[0].identityUser.name + " " + data[0].identityUser.surname + " (" + data[0].identityUser.userName + ")";
                        } catch {
                            return "-";
                        }
                    }
                },
                {
                    title: l('ProjectName'),
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

    var assignMentortModal = new abp.ModalManager(abp.appPath + 'Projects/AssignMentorModal');
    assignMentortModal.onOpen(function () {
        $('#AvailableMentorsTable').DataTable().draw();
    });
    var MentorAssignmentsListTable = $('#MentorAssignmentsListTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false,
            paging: true,
            scrollY: '210px',
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
                                    text: l('Assign'),
                                    visible: abp.auth.isGranted('IncubationCentre.SystemManagers'),
                                    action: function (data) {
                                        assignMentortModal.open({ projectId: data.record.id });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Entreprenur'),
                    data: "entrepreneurs",
                    render: function (data) {
                        try {
                            return data[0].identityUser.name + " " + data[0].identityUser.surname + " (" + data[0].identityUser.userName + ")";
                        } catch {
                            return "-";
                        }
                    }
                },
                {
                    title: l('Mentors'),
                    data: "mentors",
                    render: function (data) {
                        if (data == null) return 0;
                        return data.length;
                    }
                },
                {
                    title: l('ProjectName'),
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

    page.defines.MentorAssignmentTable = MentorAssignmentsListTable;
    $('.nav-tabs a').on('shown.bs.tab', function (event) {
        $('#MentorAssignmentsListTable').DataTable().draw();
    });
});
