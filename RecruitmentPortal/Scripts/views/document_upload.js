$("input:file").change(function () {    
    CheckExtension();
});

function CheckExtension() {
    var filePath = $("input:file").val();
    var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var allowedExtension = ",doc,docx,jpeg,jpg,gif,png,pdf,mp4,"
    if (allowedExtension.indexOf("," + ext + ",", 0) == -1) {
        alert('The extension ' + ext + ' files are not allowed.');
        $("#UploadFile1").val("");
        return false;
    }
    return true;
}

/*
$(document).ready(function () {
    $('#ResourceDocument').validate({
        ignore: [],
        rules: {
            FileUpload1: {
                required: true,
                accept: 'docx|doc|jpeg|jpg|gif|png|pdf'
            }
        }
    });
});
*/

$('#btnSubmit').click(function () {
    var filePath = $("input:file").val();    
    if (filePath.length <= 0) {
        alert('Please select a file to upload.')
        return false;
    }
    else {
        return CheckExtension();
    }
    return true;
});