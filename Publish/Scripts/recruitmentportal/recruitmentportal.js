$(function () {   

    $('.theme-link').click(function () {
        var selectedtheme = $(this).attr('datatheme');
        //var themeurl = themes[$(this).attr('datatheme')];        
        $.ajax({
            cache: false,
            url: buildThemeChangeURL(selectedtheme),
            success: function (data) {
                var hreftext = recruitmentportalLibrary.serverRootPath + "Content/themes/" + data;
                $('link[title="bootstrap-theme"]').attr('href', hreftext);
                return false;
            }
        })
    });

    $('.input-group.date').datepicker({
        todayHighlight: true,
        autoclose: true
    });
});

function buildThemeChangeURL(selectedtheme) {
    return recruitmentportalLibrary.serverRootPath + "Home/LoadTheme?Theme=" + selectedtheme;
}

$("input[data-val-length-max]").each(function (index, element) {
    var length = parseInt($(this).attr("data-val-length-max"));
    $(this).prop("maxlength", length);
});

//Create a new recruitmentportal function library and attach it to the window for global application access
//Depends on jQuery
(function recruitmentportalLibrary(window) {

    var recruitmentportalLibrary = this;

    window.recruitmentportalLibrary = recruitmentportalLibrary;

    //Publicly accessible server root path, for use application-wide
    this.serverRootPath = "/";

    //Public function to validate any form built using Bootstrap and MVC Validators
    //Requires the following input parameters:
    //* = Required
    //*formToValidate - HTML form element or collection
    this.validateBootstrapForm = function (formToValidate) {

        //Check that the object passed into this function is not undefined
        if (formToValidate != undefined) {
            $(formToValidate).submit(new function () {

                //Get any bootstrap control groups that exist on page
                var formGroups = $(".form-group");

                //Check for MVC field validation errors & validation summary errors
                $.each(formGroups, function (index, value) {

                    var fieldErrors = $(formGroups[index]).find(".field-validation-error");

                    var validationSummary = formToValidate.find(".validation-summary-errors");

                    if (validationSummary) {
                        validationSummary.addClass("text-error");
                    }

                    //Add the bootstrap error CSS class whenever field validation errors exist on page
                    if (fieldErrors.length > 0) {
                        $(formGroups[index]).addClass("has-error");
                    }
                });
            });
        }
    }

    //Public function to set the root path of the application from the server
    //Requires the following input parameters:
    //* = Required
    //*newRootPath - URL root path
    this.setServerRootPath = function (newRootPath) {

        if (newRootPath != undefined) {
            serverRootPath = newRootPath;
        }
    }
})(window);