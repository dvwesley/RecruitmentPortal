var consultantListTable;

$(document).ready(function () {
    $('#SuggestedConsultantListTable').dataTable({
        "searching": false,
        "lengthChange": false,
        "ordering": true,
        "pageLength": 10,
        "order": [[1, 'asc']],
        "language": {
            "paginate": {
                "previous": "&lt;",
                "next": "&gt;"
            }
        },
        "columnDefs": [
                {
                    "targets": 0, "orderable": false
                }
        ]
    });
});

function buildConsultantListURL() {
    return recruitmentportalLibrary.serverRootPath + "Resource/GetResourceList?name=" + $("#txtConsultantName").val();
}

$('#btnSearch').click(function () {
    if (consultantListTable) {
        var url = buildConsultantListURL();
        consultantListTable.ajax.url(url).load();
    }
    else {
        consultantListTable = $('#ConsultantListTable').DataTable({
            "searching": false,
            "lengthChange": false,
            "ordering": true,
            "paging": true,
            "pageLength": 5,
            "info": false,
            "order": [[1, 'asc']],
            "ajax": buildConsultantListURL(),
            "columns": [
                { "data": "", "orderable": false },
                { "data": "Name" },
                { "data": "Availability" },
                { "data": "ProjectEndDate" }
            ],
            "columnDefs": [
                {
                    "targets": 0, "render": function (data, type, full) {
                        return '<input class="resource-checkbox" type="checkbox" id="ConsultantList" name="ConsultantList" value="' + full["ResourceId"] + '" />';
                    }                    
                },
                {
                    "targets": 3, "render": function (data, type, full) {
                        return dtConvFromJSON(data);
                    }
                }
            ],
            "language": {
                "paginate": {
                    "previous": "&lt;",
                    "next": "&gt;"
                }
            }
        });
    }
    //$('#ConsultantListTable').show();
});

$('#btnSubmit').click(function () {    
    
    var selectResourceList = "";

    var osTable = $('#SuggestedConsultantListTable').dataTable();
    var osrowcollection = osTable.$(".resource-checkbox:checked", { "page": "all" });

    if (osrowcollection.length != 0) {        
        osrowcollection.each(function (index, elem) {
            selectResourceList += $(elem).val() + ",";
        });        
    }

    if (consultantListTable) {        
        var oTable = $('#ConsultantListTable').dataTable();
        var rowcollection = oTable.$(".resource-checkbox:checked", { "page": "all" });
        if (rowcollection.length == 0 && selectResourceList.length == 0) {
            alert('Select consultants to add to the requirement.');            
            return false;
        }
        else {
            rowcollection.each(function (index, elem) {
                selectResourceList += $(elem).val() + ",";
            });            
        }
    }
    else if(selectResourceList.length == 0) {
        alert('Select consultants to add to the requirement.');
        return false;
    }
    $('#ResourceIds').val(selectResourceList);
    return true;
});


function dtConvFromJSON(data) {
    if (data == null) return '';
    var r = /\/Date\(([0-9]+)\)\//gi
    var matches = data.match(r);
    if (matches == null) return '1/1/1950';
    var result = matches.toString().substring(6, 19);
    var epochMilliseconds = result.replace(
    /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
    '$1');
    var b = new Date(parseInt(epochMilliseconds));
    var c = new Date(b.toString());
    var curr_date = c.getDate();
    var curr_month = c.getMonth() + 1;
    var curr_year = c.getFullYear();
    /*
    var curr_h = c.getHours();
    var curr_m = c.getMinutes();
    var curr_s = c.getSeconds();
    var curr_offset = c.getTimezoneOffset() / 60
    */
    var d = curr_month.toString() + '/' + curr_date + '/' + curr_year;
    return d;
}