/*<summary>
	* saves the pitStop values in DB and shows the crested pit stops in UI.
</summary>
*/
function CreatePitStop() {
    var url = "/PitStop/AddPitStop";

    $("#loaderDiv").show();
    var formData = $("#addPitStop").serialize();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        success: function (data, status, StatusCode) {
                $("#loaderDiv").hide();
                $("#ModalAddPit").modal("hide");
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $("#1b").html(data);
        },
        
    });
}

/*<summary>
	* Fetch values fromDB and shows in EditPit Stop page.
</summary>
*/
function pitStopEdit() {

    var latlng = new google.maps.LatLng(1.281766, 103.818346);
    var mapOptions = {
        center: latlng,
        zoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("mapEdit"), mapOptions);

    var currentLatitude = $('#Latitude').val();
    var currentLongitude = $('#Longitude').val();
    var marker;
    var url = "/PitStop/GetPitStop";
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            currentLatitude = data[0].Latitude;
            currentLongitude = data[0].Longitude;
            var currentLocation = new google.maps.LatLng(currentLatitude, currentLongitude);
            placeMarker(currentLocation);
            google.maps.event.addListener(map, "click", function (event) {
                placeMarker(event.latLng);
                getAddress(event.latLng);
            });
        }

    });
    

    function getAddress(location) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ "latLng": location }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                
                $('#editAddress').val(results[0].formatted_address);
                $('#Latitude').val(results[0].geometry.location.lat);
                $('#Longitude').val(results[0].geometry.location.lng);
            }
        });
    }

    function placeMarker(location) {
        
        if (marker != null) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
            position: location,
            title: 'PitStop',
            icon: "/Content/Images/pitStop.png",
            map: map
        });
        map.panTo(location);
    }


}

/*<summary>
	* To create a pitStop, In UI it shows maps and creation details
</summary>
*/
function pitStopLocate() {
    var marker;
        var latlng = new google.maps.LatLng(1.281766, 103.818346);
    var mapOptions = {
        center: latlng,
        zoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("mapCreate"), mapOptions);
    
    google.maps.event.addListener(map, "click", function (event) {
            placeMarker(event.latLng, map);
            getAddress(event.latLng);
        });

        function getAddress(location) {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ "latLng": location }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    $('#Address').val(results[0].formatted_address);
                    $('#Latitude').val(results[0].geometry.location.lat);
                    $('#Longitude').val(results[0].geometry.location.lng);
                }
            });
        }

    function placeMarker(location, map) {
        
            if (marker != null) {
                marker.setMap(null);
            }
            marker = new google.maps.Marker({
                position: location,
                title: 'PitStop',
                icon: "/Content/Images/pitStop.png",
                map: map
            });
            map.panTo(location);
        }
    
}

/*<summary>
	* Shows edit dialog box
</summary>
*/
var EditPitStop = function (pitStopId) {
    var url = "/PitStop/EditPitStops?pitStopId=" + pitStopId;

    $("#ModalBodyEditPit").load(url, function () {
        $("#ModalEditPit").modal("show");
    });
}

var VewPitstop = function (pitStopId) {
    var url = "/PitStop/PitStopDetails?pitStopId=" + pitStopId;

    $("#ModalBodyviewPit").load(url, function () {
        $("#ModalviewPit").modal("show");
    });
};

/*<summary>
	* Saves updated values of PitStops during edit and saves in DB
</summary>
*/
var UpdatePitStop = function () {
    $("#loaderDiv").show();
    var formData = $("#editPitForm").serialize();
    $.ajax({
        type: "POST",
        url: "/PitStop/AddPitStop",
        data: formData,
        success: function (data) {
            $("#loaderDiv").hide();
            $("#ModalEditPit").modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#1b").html(data);
        }
    })
}

/*<summary>
	* Shows delete pitStop dialog
</summary>
*/
var deletePitStop = function (pitStipId) {
    $('#hiddenPitStopId').val(pitStipId);
    $("#deleteModel").modal('show');

};

/*<summary>
	* deletes the PitStop from UI and Updates in DB
</summary>
*/
var DeletePitConfirm = function () {
    $("#loaderDiv").show();
    var pitStopId = $('#hiddenPitStopId').val();
    $.ajax({
        type: "POST",
        url: '/PitStop/DeletePitStop',
        data: { PitStopId: pitStopId },
        success: function (result) {
            $("#loaderDiv").hide();
            $("#deleteModel").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#1b").html(result);
        }

    });
}