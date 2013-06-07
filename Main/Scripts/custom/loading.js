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

    $(this).ajaxError(function (event, jqxhr, settings, exception)
    {
        alert("Ajax to '" + settings.url + "' has error: " + exception);
    });

    // add div and hide it
    loading.control.appendTo(document.body).hide();
    //loading.control.appendTo(document.body);
})