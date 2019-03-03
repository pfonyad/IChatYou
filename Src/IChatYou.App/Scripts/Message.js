function SetRowClickEvent() {
    $("#myTable").on("click", "tbody tr", function () {
        $("#modal-msgread-text").html($(this).children(".tdmsgtext").html());
        $("#modal-msgread-title").html("From: " + $(this).children(".tdmsgsender").html());
        $("#modal-msgread").modal('toggle');
    });
}

function SetUsersRowClickEvent() {
    $("#myTable").on("click", "tbody tr", function () {
        $("#modal-sendmsg-title").html("To: " + $(this).children(".tduser").html());
        $("#modal-sendmsg-text").data("target", $(this).children(".tduser").html());
        $('#modal-sendmsg').modal('toggle');
    });
}

function SetSendMEssageEvent() {
    $("#modal-sendmsg-send").click(function () {
        SendMessage($("#modal-sendmsg-text").data("target"), $("#modal-sendmsg-text").val());
        $('#modal-sendmsg').modal('hide');
        $("#modal-sendmsg-text").val("");
    });
}

function SendMessage(target, message) {
    "use strict";
    $.ajax({
        type: "POST",
        url: "Message/SendMessage",
        dataType: "Json",
        data: { targetUserName: target, message: message },
        success: function (resp) {
            if (resp.IsSuccess) {
                alert(resp.Result);
            } else {
                alert(resp.Exception);
            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}