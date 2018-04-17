var ConfirmDelete = function (eventId) {
    $("#hiddenEventId").val(eventId);
    $("#myModal").modal('show');

};

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

    });
};
var EditEvent = function (eventId) {
    var url = "/Event/EditEvent?EventID=" + eventId;

    $("#ModalBodyEdit").load(url, function () {
        $("#ModalEdit").modal("show");
    });
};

var AddEvent = function () {
    var url = "/Event/CreateEvent";

    $("#ModalBodyAdd").load(url, function () {
        $("#ModalAdd").modal("show");
    });
};

var ShowEvent = function (eventId) {
    var url = "/Event/EventDetails?EventID=" + eventId;

    $("#ModalBodyShow").load(url, function () {
        $("#ModalShow").modal("show");
    });
};

var ShowPitstop = function (eventId) {
    $("#loaderDiv").show();
    alert("Before" + eventId);
    
    var url = "/PitStop/GetPitStopData";
    $.ajax({
        type: "GET",
        url: url,
        data: { EventId: eventId },
        success: function (data) {
            $("#PitstopLoad").html(data)
            window.location.href= "/Event/Index?EventID=" + eventId;
           
        }

    });
};

var AddPitStop = function () {
    var url = "/PitStop/CreatePitStop";

    $("#ModalBodyAddPit").load(url, function () {
        $("#ModalAddPit").modal("show");
    });
};