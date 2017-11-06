$(document).ready(function () {

    $('#SkillSetListTable').hide();
    $('#SelectedSkillSetListTable').hide();
});

var skillSetListTable;

function buildSkillSetListURL() {
    return recruitmentportalLibrary.serverRootPath + "SkillSet/GetSkillSetList?name=" + $("#txtSkillSetName").val();
}

$('#btnSearchSkillSet').click(function () {
    if (skillSetListTable) {
        var url = buildSkillSetListURL();
        skillSetListTable.ajax.url(url).load();
    }
    else {
        skillSetListTable = $('#SkillSetListTable').DataTable({
            "searching": false,
            "lengthChange": false,
            "ordering": true,
            "paging": true,
            "pageLength": 5,
            "info": false,
            "order": [[1, 'asc']],
            "ajax": buildSkillSetListURL(),
            "columns": [
                { "data": "", "orderable": false },
                { "data": "Text" }
            ],
            "columnDefs": [
                {
                    "targets": 0, "render": function (data, type, full) {
                        return '<button type="button" class="addbutton btn btn-default btn-xs" title="Add this SkillSet" onClick="javascript:GetSelectedSkillSet(this)" value="' + full["Value"] + ',' + full["Text"] + '">+</button>';
                    }
                }
            ]
        });
    }
    $('#SkillSetListTable').show();
    $('#SelectedSkillSetListTable').show();
});

function GetSelectedSkillSet(obj) {
    var skillset = $(obj).val();
    if (skillset.length > 0) {
        var arr = skillset.split(",");
        var skillsetId = arr[0];
        var skillsetName = arr[1];
        var skillsets = $('#SelectedSkillSetCSV').val();        
        $('#SelectedSkillSetCSV').val(skillsets + "," + skillsetId + ",");        
        $('#SelectedSkillSetListTable').append('<tr><td><button type="button" class="removebutton btn btn-default btn-xs" title="Remove this SkillSet" value="' + arr[0] + '">X</button></td><td>' + arr[1] + '</td></tr>');
    }
}

$(document).on('click', 'button.removebutton', function () {
    var skillsetId = $(this).closest('tr').children('td:first').children('button').val();
    var skillsets = $('#SelectedSkillSetCSV').val();    
    skillsets = skillsets.replace("," + skillsetId + ",", "");    
    $('#SelectedSkillSetCSV').val(skillsets);    
    $(this).closest('tr').remove();
    return false;
});

$('#btnSubmit').click(function () {
    $('#Email').prop("disabled", false);
});