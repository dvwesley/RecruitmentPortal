$(document).ready(function () {

    $('#SkillSetListTable').hide();
    $('#SelectedSkillSetListTable').hide();

    $('#Tier1ClientId').combobox();
    $('#Tier2ClientId').combobox();

    $('#btnAddClient').click(function () {
        recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("Create Client", "New", "Client", '', false, false);
    });

    $('#btnAddClient1').click(function () {
        recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("Create Client", "New", "Client", '', false, false);
    });

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
        var mandatoryskillsets = $('#MandatorySkillSetCSV').val();
        $('#SelectedSkillSetCSV').val(skillsets + "," + skillsetId + ",");
        $('#MandatorySkillSetCSV').val(mandatoryskillsets + "," + skillsetId + ",");
        $('#SelectedSkillSetListTable').append('<tr><td><button type="button" class="removebutton btn btn-default btn-xs" title="Remove this SkillSet" value="' + arr[0] + '">X</button></td><td>' + arr[1] + '</td><td><input type="checkbox" checked="true" onClick="javascript:GetMandatorySkillSet(this)" value="' + arr[0] + ',' + arr[1] + '" /></td></tr>');
    }
}

function GetMandatorySkillSet(obj) {
    var skillset = $(obj).val();
    alert(skillset);
    var arr = skillset.split(",");
    var skillsetId = arr[0];
    var skillsetName = arr[1];
    if ($(obj).is(":checked")) {
        var mandatoryskillsets = $('#MandatorySkillSetCSV').val();
        $('#MandatorySkillSetCSV').val(mandatoryskillsets + "," + skillsetId + ",");
    }
    else {
        var mandatoryskillsets = $('#MandatorySkillSetCSV').val();
        mandatoryskillsets = mandatoryskillsets.replace("," + skillsetId + ",", "");
        $('#MandatorySkillSetCSV').val(mandatoryskillsets);
    }
    alert($('#MandatorySkillSetCSV').val());
}

$(document).on('click', 'button.removebutton', function () {
    var skillsetId = $(this).closest('tr').children('td:first').children('button').val();
    var skillsets = $('#SelectedSkillSetCSV').val();
    var mandatoryskillsets = $('#MandatorySkillSetCSV').val();
    skillsets = skillsets.replace("," + skillsetId + ",", "");
    mandatoryskillsets = mandatoryskillsets.replace("," + skillsetId + ",", "");
    $('#SelectedSkillSetCSV').val(skillsets);
    $('#MandatorySkillSetCSV').val(mandatoryskillsets);
    $(this).closest('tr').remove();
    return false;
});