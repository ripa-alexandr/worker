var index = (function () {
    var sendAjaxRequest = function (data) {
        $.ajax({
            url: urls["EmployeeIndex"],
            data: $.param(data, true)
        }).done(function (response) {
            $(".table-section").html(response);
            updateHistory(data);
        });
    };

    var updateHistory = function(data) {
        var url = window.location.href.split("?")[0];
        var newUrl = url + "?" + $.param(data, true);
        history.pushState({ foo: "bar" }, newUrl, newUrl);
    };

    var getData = function () {
        var data = {
            StatusFilter: $("#Filter_StatusFilter").find(":selected").val(),
            CurrentPage: $("#Filter_CurrentPage").val(),
            PageSize: $("#Filter_PageSize").val()
        }

        return data;
    }

    var initHandlers = function () {
        $(".table-section").on("click", ".page a", function () {
            $("#Filter_CurrentPage").val($(this).data("value"));
            sendAjaxRequest(getData());
        });

        $(".table-section").on("click", ".page-size a", function () {
            $("#Filter_CurrentPage").val(1);
            $("#Filter_PageSize").val($(this).data("value"));
            sendAjaxRequest(getData());
        });

        $("#slideSwitch").click(function() {
            var filter = $(".filter");
            
            if (filter.is(":visible")) {
                filter.slideUp();
            } else {
                filter.slideDown();
            }
        });

        $(".filter input[type=button]").click(function () {
            var data = {
                StatusFilter: $("#Filter_StatusFilter").find(":selected").val(),
                PageSize: $("#Filter_PageSize").val()
            }

            sendAjaxRequest(data);
        });
    };

    return {
        init: initHandlers
    }
})();