var ChangePic = function (Email) {
    var url = "/Manage/ChangeProfilePic?userId=" + Email;

    $("#ModalBodyChange").load(url, function () {
        $("#ModalChange").modal("show");
    });
};