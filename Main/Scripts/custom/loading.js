var loading = {};

// config
loading.divHtml = "<div class='loading'>Loading...</div>";

// generate control & event handlers
loading.control = $(loading.divHtml);

$(document).ready(function ()
{
    $(this).ajaxStart(function ()
    {
        loading.control.show();
    });

    $(this).ajaxStop(function ()
    {
        loading.control.hide();
    });

    $(this).ajaxError(function (event, jqxhr, settings)
    {
        // get encoded status text
        //var rawStatusText = jqxhr.statusText.replace(/~~NL~~/g, "\r");

        //alert("Ajax call has been failed. \r\rDetails: \rURL: " + settings.url + "\rStatus Code: " + jqxhr.status + "\rStatus Text: " + rawStatusText);
        //alert("Ajax call has been failed. \r\rDetails: \rURL: " + settings.url + "\rStatus Code: " + jqxhr.status + "\rResponse: " + jqxhr.responseText);
        //var message = "Ajax call has been failed. \r\rDetails: \rURL: " + settings.url + "\rStatus Code: " + jqxhr.status + "\rResponse: " + jqxhr.responseText;
        //document.clear();
        //document.write("<div class='error-dialog'><button onclick=''>Close</button><pre>" + message + "</pre>");
        debug.showDialog("URL: \"" + settings.url + "\", Status Code: " + jqxhr.status + ", Response: " + jqxhr.statusText, jqxhr.responseText);
    });

    // add div and hide it
    loading.control.appendTo(document.body).hide();
    //loading.control.appendTo(document.body);
})