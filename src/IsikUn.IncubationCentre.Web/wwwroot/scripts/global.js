$(function () {
    $('[data-long-name="true"]').each(function (i, e) {
        var text = $(e).text();
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




















    $('[data-toggle="tooltip"]').tooltip();

});
