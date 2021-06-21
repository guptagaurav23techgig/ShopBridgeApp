// Get Cookie by name
function getCookie(name) {    
    var cookieArr = document.cookie.split(";");
    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");        
        if (name == cookiePair[0].trim()) {           
            return decodeURIComponent(cookiePair[1]);
        }
    }    
    return null;
}


function setCookie(key, value, expiry) {
    var expires = new Date();
    expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));    
    document.cookie = key + "=" + value + ";" + expires + ";path=/";
}


function getCookies(key) {
    var name = key + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
    return keyValue ? keyValue[2] : null;
}


$('.numericOnly').keypress(function (e) {

    var charCode = (e.which) ? e.which : event.keyCode

    if (String.fromCharCode(charCode).match(/[^0-9]/g))

        return false;

});     


function myIP() {
    if (window.XMLHttpRequest) xmlhttp = new XMLHttpRequest();
    else xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

    xmlhttp.open("GET", " http://api.hostip.info/get_html.php ", false);
    xmlhttp.send();
    //console.log(xmlhttp.responseText);

    hostipInfo = xmlhttp.responseText.split("\n");

    for (i = 0; hostipInfo.length >= i; i++) {
        ipAddress = hostipInfo[i].split(":");
        if (ipAddress[0] == "IP") return ipAddress[1];
    }

    return false;
}

//var startingAppUrl = '';

// for live
//startingAppUrl = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];;

// For Local
//startingAppUrl = window.location.origin;

//function GetList(url, type, pageId, obj, dataType) {
//    var ArrDtls = [];
//    var token = getCookie("token");
//    $.ajax({
//        type: type,
//        url: url,
//        contentType: "application/json",
//        beforeSend: function (xhr) {
//            xhr.setRequestHeader('Authorization', 'Bearer ' + token);
//            xhr.setRequestHeader('PageId', pageId);
//        },
//        data: JSON.stringify(obj),
//        //crossDomain: true, 
//        dataType: dataType,
//        //async: false,  
//        success: function (result, textStatus, xhr) {
//            ArrDtls = result
//            console.log(xhr.status);
//            console.log(textStatus);
//        },
//        error: function (xhr, status) {
//            console.log(xhr.status);
//            console.log(status);
//            ArrDtls = { type: "fail", message: xhr.status == "401" ? "Unauthorized" : "Error in requesting" };
//        }
//    });
//    return ArrDtls;
//}

function RquestMVCToAPI(url, data) {    
    //debugger;
    var AppBaseUrl = getCookie("appBaseUrl");
    var resultData = [];
    var valData = {};
    valData["url"] = url;
    valData["data"] = JSON.stringify(data);
    $.ajax({
        url: AppBaseUrl + '/APIClient/RquestToAPI',
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(valData),
        async: false,
        success: function (result, textStatus, xhr) {
            
            if (result != null) {
                resultData = result;
            }
            else {
                resultData = { Type: "fail", Message: "Error in requesting" };
            }
            console.log(xhr.status);
            console.log(textStatus);
        },
        error: function (xhr, status) {
            console.log(xhr.status);
            console.log(status);
            
            resultData = { Type: "fail", Message: xhr.status == "401" ? "Unauthorized" : "Error in requesting" };
        }
    });

    return resultData;
}

function GetRquestMVCToAPI(URL, QueryString, IsGetAll) {    
    var AppBaseUrl = getCookie("appBaseUrl");
    var resultData = [];
    $.ajax({
        url: AppBaseUrl + '/APIClient/GetRquestToAPI',
        type: "GET",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: { url: URL, queryString: QueryString, isGetAll: IsGetAll },
        async: false,
        success: function (response, textStatus, xhr) {
            console.log(response);
            console.log(xhr.status);
            console.log(textStatus); 
            
            resultData = response;
        },
        error: function (xhr, status) {
            console.log(xhr.status);
            console.log(status);
            
            resultData = { Type: "fail", Message: xhr.status == "401" ? "Unauthorized" : "Error in requesting" };
        }
    });

    return resultData;
}