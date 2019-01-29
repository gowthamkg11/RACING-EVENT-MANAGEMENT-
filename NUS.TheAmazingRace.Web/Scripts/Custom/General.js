function ajaxPost(loaderDivId, formID, url, formData, successfn, failurefn) {
    $("#" + loaderDivId).show();
    var formData = $("#" + formID).serialize();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        success: successfn,
        error: failurefn
    });
}

function ajaxGet(loaderDivId, url, formData, successfn, failurefn) {
    $("#" + loaderDivId).show();
    $.ajax({
        type: "GET",
        url: url,
        data: formData,
        success: successfn,
        error: failurefn
    });
}

FailureFunction=function (error) {
    $("#FailureModalBody").html(error);
    $("#FailureModal").modal("show");
}