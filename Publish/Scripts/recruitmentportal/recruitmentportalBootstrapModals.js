(function recruitmentportalModals(window) {

    //Default modal title
    var defaultModalWindowTitle = "System Message";

    //Default return path
    var returnPath = location.href;

    //Multi-Part Form Submission flag
    var multiPartSubmit = false;

    //Default Save and Cancel text
    var saveButtonText = "Save";
    var cancelButtonText = "Cancel";

    //Make a reference to this closure
    var recruitmentportalModals = this;

    //Extend the recruitmentportalLibrary class
    window.recruitmentportalLibrary.recruitmentportalModals = recruitmentportalModals;

    //Create a public variable to hold the ID of each form currently being displayed
    this.recruitmentportalFormId = "";

    //alert("Here2");


    this.recruitmentportalStaticBootstrapModal = function (modalTitle, modalContent, appendModal) {

        //Find all pre-existing modals, and hide them
        $('.modal').modal('hide');

        //Use the word "Close" instead of the default "Cancel for static 1 button modals
        cancelButtonText = "Close";

        //Build the Bootstrap Modal Window
        var newModal = new modalPresentation(modalTitle, modalContent);

        //Trigger the Bootstrap Modal Window
        newModal.modal({ keyboard: true, backdrop: "static" });

        if (appendModal == true) {

            newModal.on('hidden.bs.modal', function (e) {

                newModal.remove();

                $.each($('.modal'), function (index, value) {

                    var modalWindow = $($('.modal')[index]);

                    if (modalWindow != newModal) {
                        modalWindow.modal('show');
                    }
                });

            });
        }
    }

    //Function to create an AJAX Modal window for an MVC View/Form
    //Accepts:
    //modalTitle - String
    //modalAction - String
    //modalController - String
    //modalRouteValues - String
    //appendModal - Boolean
    //wideView - Boolean
    //saveText - String
    //cancelText - String
    //returnPath - String
    //multiPartForm - Boolean
    this.recruitmentportalAjaxBootstrapModal = function (modalTitle,
                                            modalAction,
                                            modalController,
                                            modalRouteValues,
                                            appendModal,
                                            wideView,
                                            saveText,
                                            cancelText,
                                            newReturnPath,
                                            multiPartForm) {

        //Find any modals present and remove them
        $('.modal').modal('hide');

        //Only remove previous modals when append is false
        if (appendModal === false || appendModal == undefined) {
            $('.modal').remove();
        }

        //Default wideView to false when false or undefined
        if (wideView == false || wideView == undefined) {
            wideView = false;
        }

        //Set the returnPath if it exists
        if (newReturnPath != undefined && newReturnPath != "" && returnPath != "") {
            returnPath = newReturnPath;
        }
        else {
            //Revert to default when return path is not provided
            returnPath = location.href;
        }

        //Set the save and cancel button text when requested
        if (saveText != "" && saveText != undefined) {
            saveButtonText = saveText;
        }
        else {
            //Revert to default when save text is not provided
            saveButtonText = "Save";
        }

        if (cancelText != "" && cancelText != undefined) {
            cancelButtonText = cancelText;
        }
        else {
            //Revert to default when cancel text is not provided
            cancelButtonText = "Cancel";
        }

        //Set the multipart flag
        if (multiPartForm == true && multiPartForm != undefined) {
            multiPartSubmit = true;
        }

        //Set the modal content to a progress spinner for ajax forms
        var modalContent = "<div class='appSpinner'></div>";

        //Build the Bootstrap Modal Window
        var newModal = new modalPresentation(modalTitle, modalContent, true, wideView);

        //Trigger the Bootstrap Modal Window
        newModal.modal({ keyboard: true, backdrop: "static" });

        //Destroy the appended modal and re-display the parent modal when required
        if (appendModal == true) {

            newModal.on('hidden.bs.modal', function (e) {

                newModal.remove();

                $.each($('.modal'), function (index, value) {

                    var modalWindow = $($('.modal')[index]);

                    if (modalWindow != newModal) {
                        modalWindow.modal('show');
                    }
                });

            });
        }

        newModal.on('shown.bs.modal', function (e) {

            //Only execute an ajax get if the controller and action are defined
            if (modalController != undefined && modalAction != undefined && $(".modal-body").find(".appSpinner").length > 0) {
                //Get the view from the server for modal display
                $.ajax({
                    type: "GET",
                    url: recruitmentportalLibrary.serverRootPath + modalController + '/' + modalAction,
                    dataType: "html",
                    data: modalRouteValues,
                    cache: false,
                    success: function (response, status, jqXhr) {

                        //Find the form from the response, must be the first element
                        var form = $(response);

                        //Check for the server error handling page
                        //Do not append the form if it is present, append the error box instead
                        var errorCheck = $(form).find("#errorBox");

                        if (errorCheck.length > 0) {
                            //Clean up any matching forms that exist on the page before adding the new one
                            $(".recruitmentportalModalForm").remove();
                            $(".modal-body").empty();

                            //Append the Error Box with its message
                            $(".modal-body").append(errorCheck);

                            //Expand the Error Box
                            errorCheck.find(".col-md-5").switchClass("col-md-5", "col-md-12");

                            //Remove the save button
                            $('#ajaxModalSaveButton').remove();

                            //Change the cancel text to close, accordingly
                            $('#modalCloseButton').val("Close");

                        }
                        else {
                            //Set a class for all modal forms
                            $(form).attr("class", "recruitmentportalModalForm");

                            //Check for a form ID, set it randomly-unique if none exists
                            if ($(form).attr("id") == "" || $(form).attr("id") == undefined) {

                                var modalFormDate = new Date();

                                recruitmentportalFormId = "form" + modalFormDate.getDate() + modalFormDate.getDay() + modalFormDate.getMinutes() + modalFormDate.getMilliseconds()

                                $(form).attr("id", recruitmentportalFormId);
                            }

                            //Clean up any matching forms that exist on the page before adding the new one
                            $(".recruitmentportalModalForm").remove();

                            $(".modal-body").empty();

                            //Append the response
                            $(".modal-body").append($(form));

                            //Create the save button, and bind it to the form
                            var saveButton = $('#ajaxModalSaveButton');
                            saveButton.removeAttr('disabled');

                            saveButton.bind('click', null, function () { ajaxFormSubmission(modalAction, modalController); });
                        }
                    }
                });

            }
        });

    }

    var modalPresentation = function (newModalTitle, newModalContent, useSaveButton, wideView) {

        //Default Title when not present
        if (newModalTitle == undefined) {
            newModalTitle = defaultModalWindowTitle;
        }

        var saveButton = $("<button id='ajaxModalSaveButton' data-loading-text='<i class=\"saveIcon\"></i><div class=\"saveButtonLoadingTextbox\">" + saveButtonText + "</div>' type='button' class='btn btn-sm btn-primary' disabled='true'>" + saveButtonText + "</button>");

        var modalClass = "modal fade";

        var modalDialogClass = "modal-dialog";

        var modalBodyClass = "modal-body";

        //Append the wide modal view class at presentation time when requested
        if (wideView == true) {
            modalClass = "wideBootstrapModal " + modalClass;

            modalDialogClass = "modal-dialog-wide " + modalDialogClass;

            modalBodyClass = "model-body-wideheight " + modalBodyClass;
        }

        var presentationContent = $("<div class='" + modalClass + "' id='recruitmentportalModal' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>" +
                                "<div class='" + modalDialogClass + "'>" +
                                "<div class='modal-content'>" +
                                "<div class='modal-header'>" +
                                    //Modal close button - X in the corner of the header
                                    "<button type='button' class='close' data-dismiss='modal'>×</button>" +
                                    "<h3 id='modalTitle'>" + newModalTitle + "</h3>" +
                                "</div>" +
                                "<div class='" + modalBodyClass + "'>" +
                                    newModalContent +
                                "</div>" +
                                "<div class='modal-footer'>" +
                                    "<input id='modalCloseButton' data-dismiss='modal' type='button' value='" + cancelButtonText + "' class='btn btn-sm btn-default'>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                            "</div>");

        //Only use the save button in presentation when flagged for use
        if (useSaveButton == true && useSaveButton != undefined) {
            $(presentationContent).find(".modal-footer").prepend(saveButton);
        }

        return presentationContent;
    }
    
    var ajaxFormSubmission = function(modalAction, modalController)
    {        
        var newFormId = "#" + recruitmentportalFormId;
        var formData;

        if (multiPartSubmit == false) {
            formData = $(newFormId).serialize();
     
            $.ajax({
                type: "POST",
                url: recruitmentportalLibrary.serverRootPath + modalController + '/' + modalAction,
                dataType: "html",
                data: formData,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    ajaxErrorCallback(jqXHR, textStatus, errorThrown);
                },
                success: function (response) {
                    ajaxSuccessCallback(response);
                }
            });
        }
        else {
            formData = new FormData($(newFormId)[0]);

            $.ajax({
                url: recruitmentportalLibrary.serverRootPath + modalController + '/' + modalAction,
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                error: function (xhr, textStatus, errorThrown) {
                    ajaxErrorCallback(xhr, textStatus, errorThrown);
                },
                success: function (response) {
                    ajaxSuccessCallback(response);
                }
            });
        }

        //Prevent double-clicking while posts are occurring
        var saveButton = $('#ajaxModalSaveButton');

        //Set the bootstrap save button state to loading
        saveButton.button('loading');
    }

    var ajaxErrorCallback = function (xhr, textStatus, errorThrown) {
        alert(xhr.responseText);
        var internalError = $("<section class='container' id='errorBox'><div class='row'><div class='col-md-12 shaded_box'><p class='text-danger text-centered'>An error occurred in processing the last request. This may be due to an issue on the web server or an unavailable resource. Please try again later.</p></div></div></section>");
        var notFound = $("<section class='container' id='errorBox'><div class='row'><div class='col-md-12 shaded_box'><p class='text-danger text-centered'>The page or file requested could not be found or is unavailable.</p></div></div></section>");

        $("#modalTitle").empty();
        $(".modal-body").empty();

        if (errorThrown == "not found") {
            $("#modalTitle").append("Page Not Found");
            $(".modal-body").append(notFound);
        }
        else {
            $("#modalTitle").append("Server Error");
            $(".modal-body").append(internalError);
        }
    }

    var ajaxSuccessCallback = function (response) {

        var newFormId = "#" + recruitmentportalFormId;

        //Enable the Save button in case of error.
        var saveButton = $('#ajaxModalSaveButton');

        //Reset the bootstrap save button state
        saveButton.button('reset');

        //Find the form from the response, must be the first element
        var form = $(response);

        //Check to see whether validation errors occurred
        //Only close the modal when success without errors
        //Check for the server error handling page
        //Do not append the form if it is present, append the error box instead
        var errorCheck = $(form).find("#errorBox");

        if (errorCheck.length > 0) {
            //Clean up any matching forms that exist on the page before adding the new one
            $(".recruitmentportalModalForm").remove();
            $(".modal-body").empty();

            //Append the Error Box with its message
            $(".modal-body").append(errorCheck);

            //Expand the Error Box
            errorCheck.find(".span5").switchClass("span5", "span12");

            //Remove the save button
            $('#ajaxModalSaveButton').remove();

            //Change the cancel text to close, accordingly
            $('#modalCloseButton').val("Close");

        }
        else if ($(form).find(".field-validation-error").length > 0) {

            //Set a class for all modal forms
            $(form).attr("class", "recruitmentportalModalForm");
            $(form).attr("id", recruitmentportalFormId);

            $(".modal-body").empty();

            //Append the response
            $(".modal-body").append($(form));

            //Apply Bootstrap validation
            recruitmentportalLibrary.validateBootstrapForm($(form));
        }
        else {
            $('.modal').modal('hide');

            $('.modal').on('hidden.bs.modal', function (e) {

                //Reload the current page when no return path modification has been requested
                //Else direct the browser the requested return path passed into the modal class
                if (window.location.href == returnPath) {
                    window.location.reload();
                } else {
                    window.location.href = returnPath;
                }

            })
        }
    }

})(window);