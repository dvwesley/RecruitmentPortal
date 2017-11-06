$(document).ready(function () {
    $("#State").combobox();
    $("#ReferredConsultantId").combobox();
    $("#ReferrerId").combobox();
    $("#Tier1VendorId").combobox();
    $("#Tier2VendorId").combobox();

    $('#btnAddReferrer').click(function () {        
        recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("Create Referrer", "New", "Referrer", '', false, false);
    });
    
    $('#btnAddVendor').click(function () {
        recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("Create Vendor", "New", "Vendor", '', false, false);
    });

    $('#btnAddVendor1').click(function () {
        recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("Create Vendor", "New", "Vendor", '', false, false);
    });
});

$.validator.setDefaults({
    ignore: [],
    invalidHandler: function (event, validator) {
        $(".tab-content").find("div.tab-pane:hidden:has(div.has-error)").each(function (index, tab) {
            var id = $(tab).attr("id");
            $('a[href="#' + id + '"]').tab('show');
        });

    }
});

function LoadTabWithError() {
    if ($(".tab-content").find("div.tab-pane.active:has(div.has-error)").length == 0) {
        $(".tab-content").find("div.tab-pane:hidden:has(div.has-error)").each(function (index, tab) {
            var id = $(tab).attr("id");
            $('a[href="#' + id + '"]').tab('show');
        });
    }
}

$('#btnSubmit').click(function () {
    var IsTabValid = $("#ResourceForm").valid();
    if (!IsTabValid) {
        LoadTabWithError();
    }
    $('#Email').prop("disabled", false);
});