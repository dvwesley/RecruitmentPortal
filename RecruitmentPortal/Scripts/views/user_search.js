$('#btnSearchUser').click(function () {
    var email = $('#Email').val();
    if (email.length == 0) {
        alert('Please enter the email to search.');
        return;
    }
    $.ajax({
        cache: false,
        url: buildSearchUserURL(),
        dataType: "json",
        type: "GET",
        success: function (data) {
            if (data.FirstName) { $('#FirstName').val(data.FirstName); }
            if (data.MiddleInitial) { $('#MiddleInitial').val(data.MiddleInitial); }
            if (data.LastName) { $('#LastName').val(data.LastName); }
            if (data.Address1) { $('#Address1').val(data.Address1); }
            if (data.Address2) { $('#Address2').val(data.Address2); }
            if (data.City) { $('#City').val(data.City); }
            if (data.State) { $('#State').val(data.State); }
            if (data.Zipcode) { $('#Zipcode').val(data.Zipcode); }
            if (data.PhoneNo) { $('#PhoneNo').val(data.PhoneNo); }
        },
        error: function (data) {
            $('#FirstName').val(data.FirstName);
            $('#MiddleInitial').val(data.MiddleInitial);
            $('#LastName').val(data.LastName);
            $('#Address1').val(data.Address1);
            $('#Address2').val(data.Address2);
            $('#City').val(data.City);
            $('#State').val(data.State);
            $('#Zipcode').val(data.Zipcode);
            $('#PhoneNo').val(data.PhoneNo);
        }
    });

});

function buildSearchUserURL() {
    return recruitmentportalLibrary.serverRootPath + "Recruiter/GetUserDetails?email=" + $("#Email").val();
}

$(document).ajaxStart(function () {
    $('body').addClass('wait');
}).ajaxComplete(function () {
    $('body').removeClass('wait');
});


