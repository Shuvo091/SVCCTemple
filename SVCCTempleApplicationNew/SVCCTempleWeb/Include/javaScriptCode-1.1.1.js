//Sriramajayam
//javaScriptCode.1.1.1.js
$(function () {
    $(document).ajaxError(function (e, xhr) {
        try {
            switch (xhr.status) {
                case 401: //Unauthorized
                    window.open(xhr.responseJSON.UnauthorizedUrl + "?ReturnUrl=" + xhr.responseJSON.ReturnUrl, "_blank");
                    break;
                case 403: //Forbidden
                    document.getElementById("forbiddenUnauthorizedHRef").href = "/Home/Forbidden";
                    document.getElementById("forbiddenUnauthorizedHRef").click();
                    window.location.href = "";
                    break;
                default:
                    //document.getElementById("forbiddenUnauthorizedHRef").href = "/Home/Forbidden";
                    //document.getElementById("forbiddenUnauthorizedHRef").click();
                    //window.location.href = "";
                    break;
            }
        }
        catch (err) {
        }
    });
});
function validateInteger(event) {
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46 /*|| event.keyCode == 190*/) {

    } else {
        event.preventDefault();
    }
}
function validateDecimal(event, inputData) {
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {
        ;
    } else {
        event.preventDefault();
    }

    if (inputData.indexOf('.') !== -1 && event.keyCode == 190) {
        event.preventDefault();
    }
}
function ajaxGet(actionName, controllerName, ajaxUpdateTargetId) {
    console.log("controllerName=" + controllerName, "actionName=" + actionName, "queryString=" + queryString, "ajaxUpdateTargetId=" + ajaxUpdateTargetId, "methodName=" + "ajaxGet");
    var argumentsLength = arguments.length;
    if (argumentsLength % 2 === 0) {
        argumentsLength--;
    }
    var queryString = "", prefixChar = "?";
    for (var i = 3; i < argumentsLength; i = i + 2) {
        queryString += prefixChar + arguments[i] + "=" + arguments[i + 1];
        prefixChar = "&";
    }
    //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
    //dataType: "html",
    $.ajax({
        url: "/" + controllerName + "/" + actionName + queryString,
        type: "GET",
        success: function (responseData, textStatus, request) {
            console.log("SUCCESS!!!");
            console.log(textStatus);
            //console.log(request);
            //console.log(responseData);
            $("#" + ajaxUpdateTargetId).html(responseData);
        },
        error: function (xhr, exception) {
            console.log("ERROR???", xhr.status);
            //console.log(xhr);
            //console.log(xhr.responseText);
            console.log(exception);
            $("#" + ajaxUpdateTargetId).html(xhr.responseText);
            //throw "Error " + xhr.status + " exception=" + exception + " xhr.responseText=" + xhr.responseText;
            //alert("Error " + xhr.status);
        }
    });
}
function isInputNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode === 8)//0-9 or backspace
    //if ((window.event.keyCode >= 48 && window.event.keyCode <= 57) || window.event.keyCode == 8)//0-9 or backspace
    {
        return true;
    }
    else {
        return false;
    }
}
function replaceCharactersWithEmpty(inputText) {
    //When text data is pasted - replace with empty string
    //inputText = inputText.replace(/\D/g, '');
    //inputText = inputText.replace(/[^0-9.]/g, ""); - Replace except numbers and period
    //inputText = inputText.replace(/[^0-9]/g, ""); - Replace except numbers
    //inputText = inputText.replace(/[[a-z] + [^0-9\s.]+|.(?!\d)]/g, "");
    //Tried all of the above did not work - For now let me use the brute force method and replace this at a later date
    var inputChar, outputText = "";
    for (var i = 0; i < inputText.length; i++) {
        inputChar = inputText.charCodeAt(i);
        if (inputChar >= 91 && inputChar <= 100) {
            outputText += inputText.substr(i, 1);
        }
    }
    return outputText;
/*
$( "#textInput" ).bind( 'paste',function()
   {
       setTimeout(function()
       {
          //get the value of the input text
          var data= $( '#textInput' ).val() ;
          //replace the special characters to ''
          var dataFull = data.replace(/[^\w\s]/gi, '');
          //set the new value of the input text without special characters
          $( '#textInput' ).val(dataFull);
       });

    });
 */
}
function checkMaxLength(thisObject) {
    if (thisObject.value.length > thisObject.maxLength) {
        thisObject.value = thisObject.value.slice(0, thisObject.maxLength);
    }
}
//Not sure if below is needed.... Need to find the jquery code to add * for required
/*
function processAjaxOnSucces(responseObjectModel, modalFormName, showModal) {
    try {
        var responseObjectModelObject = JSON.parse(responseObjectModel.replace(/&quot;/g, '"'));
        if (responseObjectModelObject.IsSuccessStatusCode) {
            document.getElementById("responseTitle").innerHTML = responseObjectModelObject.ResponseTitle;
            document.getElementById("responseDescription").innerHTML = responseObjectModelObject.ResponseDescription;
            document.getElementById("responseMessages").innerHTML = "";
            for (var i = 0; i < responseObjectModelObject.ResponseMessages.length; i++) {
                document.getElementById("responseMessages").innerHTML += "<li>" + responseObjectModelObject.ResponseMessages[i].Value + "</li>";
            }
            if (showModal && modalFormName != undefined && modalFormName != null) {
                $("#" + modalFormName).modal({ backdrop: 'static', keyboard: false });
            }
        }
    }
    catch (err) {
        return;
    }
}
function ajaxPost(controllerName, actionName, ajaxContentElementId, ajaxUpdateTargetId) {
    var queryString = "";
    $.ajax({
        url: "/" + controllerName + "/" + actionName + queryString,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: $("#" + ajaxContentElementId).serialize(),
        dataType: "json",
        success: function (responseData, textStatus, request) {
            $("#" + ajaxUpdateTargetId).html(responseData);
        },
        error: function (xhr, exception) {
            console.log("Error " + xhr.status + " exception=" + exception + " xhr.responseText=" + xhr.responseText);
            //alert("Error " + xhr.status);
        }
    });
}
*/
window.reset = function (e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
}
function fileAttachment_onchange(files, imagePreviewDocumentElementId, fileNameDocumentElementId, fileSizeDocumentElementId, fileContentTypeDocumentElementId, fileSrcDataDocumentElementId, documentClientHeightElementId, documentClientHeightUnitElementId, documentClientWidthElementId, documentClientWidthUnitElementId) {
    var file = files[0];
    var objectUrl = URL.createObjectURL(file);
    img = new Image();
    img.onload = function () {
        document.getElementById(documentClientHeightElementId).value = img.height;
        document.getElementById(documentClientHeightUnitElementId).value = "px";
        document.getElementById(documentClientWidthElementId).value = img.width;
        document.getElementById(documentClientWidthUnitElementId).value = "px";
        URL.revokeObjectURL(objectUrl);
    };
    img.src = objectUrl;
    document.getElementById(imagePreviewDocumentElementId).onload = function () {
        URL.revokeObjectURL(objectUrl);
    };
    document.getElementById(imagePreviewDocumentElementId).src = objectUrl;
    var fileReader = new FileReader();
    fileReader.onload = function () {
        fileReader.result;
        //document.getElementById(fileSrcDataDocumentElementId).value = fileReader.result;
        $("#" + fileSrcDataDocumentElementId).val(fileReader.result);
    };
    fileReader.readAsDataURL(file);
    $("#" + fileNameDocumentElementId).val(file.name);
    $("#" + fileSizeDocumentElementId).val(file.size);
    $("#" + fileContentTypeDocumentElementId).val(file.type || "application/octet-stream");
    return;
    /*
    var _URL = window.URL || window.webkitURL;
    $("#file").change(function (e) {
    var file, img;
    if ((file = this.files[0])) {
        img = new Image();
        var objectUrl = _URL.createObjectURL(file);
        img.onload = function () {
            alert(this.width + " " + this.height);
            _URL.revokeObjectURL(objectUrl);
        };
        img.src = objectUrl;
    }
});
    */
}
function fileAttachment_onchange_save(files, imagePreviewDocumentElementId, fileNameDocumentElementId, fileSizeDocumentElementId, fileContentTypeDocumentElementId, fileSrcDataDocumentElementId, documentTypeIdObject, documentTypeId) {
    var file = files[0];

    document.getElementById(imagePreviewDocumentElementId).src = URL.createObjectURL(file);
    $("#" + fileNameDocumentElementId).val(file.name);
    $("#" + fileSizeDocumentElementId).val(file.size);
    $("#" + fileContentTypeDocumentElementId).val(file.type || "application/octet-stream");
    var fileReader = new FileReader();
    fileReader.onload = function () {
        fileReader.result;

        $("#" + fileSrcDataDocumentElementId).val(fileReader.result);
    };
    fileReader.readAsDataURL(file);
    if (documentTypeIdObject === null) {
        ;
    }
    else {
        documentTypeIdObject.value = documentTypeId;
    }
    //document.getElementById("closeButton").style.display = "block";
    return;
}
function getCurrentDateTime() {
    var currentDateTime = new Date();
    var temp = currentDateTime.getFullYear() + "-" + (currentDateTime.getMonth() + 1) + "-" + currentDateTime.getDate() + " " +
        currentDateTime.getHours() + ":" + currentDateTime.getMinutes() + ":" + currentDateTime.getSeconds();
    return temp;
}
function showHideForShow(inputElementId, typeAttributeValue) {
    $("#" + inputElementId).attr('type', typeAttributeValue);
    $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
}
function showHideForHide(inputElementId, typeAttributeValue) {
    $('#' + inputElementId).attr('type', typeAttributeValue);
    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
}
function objectifyForm(inp) {
    var jsonObject = {};
    for (var i = 0; i < inp.length; i++) {
        if (inp[i]['name'].substr(inp[i]['name'].length - 2) == "[]") {
            var tmp = inp[i]['name'].substr(0, inp[i]['name'].length - 2);
            if (Array.isArray(jsonObject[tmp])) {
                jsonObject[tmp].push(inp[i]['value']);
            } else {
                jsonObject[tmp] = [];
                jsonObject[tmp].push(inp[i]['value']);
            }
        } else {
            jsonObject[inp[i]['name']] = inp[i]['value'];
        }
    }
    return jsonObject;
}
function showImage(src, target) {
    var fr = new FileReader();
    // when image is loaded, set the src of the image where you want to display it
    fr.onload = function (e) { target.src = this.result; };
    src.addEventListener("change", function () {
        // fill fr with image data    
        fr.readAsDataURL(src.files[0]);
    });
}
