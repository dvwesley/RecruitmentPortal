//Datatables.Net sort plugin for editable text boxes
jQuery.extend(jQuery.fn.dataTableExt.oSort, {

    "editableTextBox-pre": function (s) {
        
        s = $(s).val();

        var a = s.replace('$', '');

        var b = "";

        if (a.length > 1) {
            b = a.replace(/\,/g, '');
        } else {
            b = -1;
        }

        return b;
    },

    "editableTextBox-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "editableTextBox-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});