$(function () {
    "use strict";
    $("#btn-switchstate").click(function () {
        $.ajax({
            type: "POST",
            url: "Message/SwitchState",
            dataType: "Json",
            data: { __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $('#logoutForm')).val() },
            success: function (resp) {
                if (resp.IsSuccess) {
                    ChangeState(resp.Result)
                } else {
                    alert(resp.Exception);
                }
            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    });
});

function ChangeState(state) {
    var currentState = $("#btn-switchstate");

    if (state === true) {
        currentState.html("My state: visible <i  class='glyphicon glyphicon-eye-open'></i>");
    } else {
        currentState.html("My state: invisible <i  class='glyphicon glyphicon-eye-close'></i>");
    }
}

function UpdateTodayLeft() {
    "use strict";
    $.ajax({
        type: "POST",
        //url: "Message/GetTodayLeft",
        url: "GetTodayLeft",
        dataType: "Json",
        data: { __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $('#logoutForm')).val() },
        success: function (resp) {
            if (resp.IsSuccess) {
                if (resp.Result !== "NoUser") {
                    $("#btn-todayleft span").html(resp.Result);
                }
            } else {
                alert(resp.Exception);
            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

$(document).ready(function () {
    var msgHub = $.connection.messagesHub;

    console.log(msgHub);

    msgHub.client.loggedin = function (order) {
        alert(order);
    };

    $.connection.hub.start().done(function () {
        msgHub.server.login();
    });
});