// define namespace
var debug = {};

// define methods
debug.showDialog = function (title, content)
{
    // create a div and add it to document
    var container = $("<div/>").addClass("debug-dialog");
    $(document.body).append(container);

    // create button
    $("<button/>").text("Close").click(function ()
    {
        container.remove();
    }).appendTo(container);

    // create content    
    //$("<span style='font-weight: bold;'>" + title + "</span>")
    $("<span/>").css("font-weight", "bold").text(title).appendTo(container);
    $("<pre/>").text(content).appendTo(container);
};