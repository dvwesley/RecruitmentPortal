$(document).ready(function () {     
    $("#StHours").rules('add', {
        required: true,
        messages: {
            required: "Hours is required."
        }
    });
    $("#StMinutes").rules('add', {
        required: true,
        messages: {
            required: "Minutes is required."
        }
    });
    $("#EndHours").rules('add', {
        required: true,
        messages: {
            required: "Hours is required."
        }
    });
    $("#EndMinutes").rules('add', {
        required: true,
        messages: {
            required: "Minutes is required."
        }
    });

    $('#InterviewerId').combobox();
});
