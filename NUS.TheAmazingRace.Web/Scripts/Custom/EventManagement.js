var ConfirmDelete = function (eventId) {
    $("#hiddenEventId").val(eventId)
    $("#myModal").modal('show');

}

var DeleteEvent = function () {
    $("#loaderDiv").show();
    var eventId = $("#hiddenEventId").val();
    $.ajax({
        type: "POST",
        url: '/Event/DeleteEvent',
        data: { EventId: eventId },
        success: function (result) {
            $("#loaderDiv").hide();
            $("#myModal").modal('hide');
            $("#EventCard_" + eventId).remove();
        }

    })
}
var EditEvent = function (eventId) {
    var url = "/Event/EditEvent?EventID=" + eventId;

    $("#ModalBodyEdit").load(url, function () {
        $("#ModalEdit").modal("show");
    })
}

var AddEvent = function () {
    var url = "/Event/CreateEvent";

    $("#ModalBodyAdd").load(url, function () {
        $("#ModalAdd").modal("show");
    })
}

var ShowEvent = function (eventId) {
    var url = "/Event/EventDetails?EventID=" + eventId;

    $("#ModalBodyShow").load(url, function () {
        $("#ModalShow").modal("show");
    })
}
