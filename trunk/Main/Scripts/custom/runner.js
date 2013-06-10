// define namespace
var runner = {};

// define methods
runner.run = function (url, data)
{
    // post url first
    $.post(url, data, function ()
    {
        // create a div and add it to document
        var container = $("<div/>").addClass("runner-dialog");
        $(document.body).append(container);

        // create button
        $("<button/>").text("Close").click(function ()
        {
            container.remove();
        }).appendTo(container);

        // try to get next chunk of messages
        runner.pollMessages(url, container);
    });
};

runner.pollMessages = function (url, container)
{
    // try to get data after 1s
    setTimeout(function ()
    {
        $.get(url, function (data, textStatus, jqXhr)
        {
            // add data, if any
            if (data.length > 0)
            {
                // data received, add to log window
                $("<pre/>").text(data).appendTo(container);
            }

            // if still running?
            if (data.length > 0 || jqXhr.status == 100)  //continue
            {
                // try to get next chunk
                runner.pollMessages(url, container);
                return;
            }

            // no more message
            $("<pre/>").text("Completed.").appendTo(container);
        });
    }, 1000);
};