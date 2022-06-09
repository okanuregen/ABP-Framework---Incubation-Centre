var l = null;
$(function () {
    l = abp.localization.getResource('IncubationCentre');

    $('#MenuItem_AccountDetail [data-text="MyProfile"]').text(l("MyProfile"));
    var myProfileLink = $('#MenuItem_AccountDetail').attr("href").split("/");
    if (myProfileLink.length == 3) {
        var role = myProfileLink[1][0].toLowerCase() + myProfileLink[1].substring(1);
        var identityUserId = $('#MenuItem_AccountDetail').attr("data-currentUser");
        isikUn.incubationCentre[role][role.slice(0, -1)].getList({ identityUserId: identityUserId }).then(function (user) {
            if (user.items.length > 0) {
                $('#MenuItem_AccountDetail').attr("href", $('#MenuItem_AccountDetail').attr("href") + "?id=" + user.items[0].id)
            } else {
                $('#MenuItem_AccountDetail').attr("href", "/");
            }
        })
    }

    $('[data-long-name="true"]').each(function (i, e) {
        var text = $(e).text().trim();
        var toolTipPlacemnt = $(e).attr("data-placement");
        var shortTextSize = $(e).attr("data-long-name-size");
        var stSize = parseInt(shortTextSize);
        stSize = stSize != NaN ? stSize : 10;

        var shortText = text;
        if (shortText.length > stSize) {
            shortText = shortText.slice(0,stSize);
            $(e).text(shortText + "...");
        }
        $(e).attr("data-toggle", "tooltip");
        $(e).attr("data-placement", ((toolTipPlacemnt != undefined && toolTipPlacemnt != "") ? toolTipPlacemnt : "top"));
        $(e).attr("title", text);
    });




    if (window.location.pathname == '/Account/Manage') {
        debugger;
        if (window.location.search.includes('redirectType=ForceChangePassword')) {
                var l = l || abp.localization.getResource("IncubationCentre");
                abp.message.warn(l("MustChangePassword"));
        }
    }




    $('[data-toggle="tooltip"]').tooltip();

});
