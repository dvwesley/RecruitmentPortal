$(document).ready(function () {
    $('#ListTable').dataTable({
        "searching": false,
        "lengthChange": false,
        "ordering": true,
        "pageLength": 10,
        "order": [[0, 'asc']],
        "language": {
            "paginate": {
                "previous": "&lt;",
                "next": "&gt;"
            }
        }
    });

    $('#BigListTable').dataTable({
        "searching": false,
        "lengthChange": false,
        "ordering": true,
        "pageLength": 5,
        "order": [[0, 'asc']],
        "language": {
            "paginate": {
                "previous": "&lt;",
                "next": "&gt;"
            }
        }
    });
});

