$('.ResourceName').click(function () {
    var resourceId = $(this).attr("data-val");
    recruitmentportalLibrary.recruitmentportalModals.recruitmentportalAjaxBootstrapModal("View Profile", "Details/" + resourceId, "Resource", '', false, true, "", "");
});

