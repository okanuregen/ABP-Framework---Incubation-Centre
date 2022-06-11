var l = null;
var page = {
    defines: {
        currentTab: "ProjectsTab",
    }
}
$(function () {
    l = abp.localization.getResource('IncubationCentre');
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var userId = params.id;

    var editMyselfModal = new abp.ModalManager(abp.appPath + 'Investors/EditModal');


    $('#editMyInfo').click(function (e) {
        e.preventDefault();
        editMyselfModal.open({ id: userId })
    });
    editMyselfModal.onResult(function () {
        location.reload();
    });


    $('[data-role="setTab"]').click(function (e) {
        var tab = $(this).attr("data-target");
        if (page.defines.currentTab == tab) return;

        $('[data-role="setTab"]').removeClass("active");
        $(this).addClass("active");

        $('[data-role="tab"]').hide();
        $("#" + tab).show();

        page.defines.currentTab = tab;

    });
    
});
