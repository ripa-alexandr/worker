var relativeUrls = {
    EmployeeIndex: "/Employee"
};

var urls = {};

var initApi = function () {
    var approot = $("body").data("approot") || "";

    for (var key in relativeUrls) {
        if (!relativeUrls.hasOwnProperty(key))
            continue;

        urls[key] = approot + relativeUrls[key];
    }
};

$(function() {
    initApi();
});
