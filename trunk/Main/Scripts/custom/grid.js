// define namespace
var grid = {};

// define methods
grid.render = function (name, width, height)
{
    // get the grid and set its css
    //var grid = $("#" + name).css("width", width).css("height", height)[0];
    var grid = $("#" + name)[0];

    // scan its structure for columns
    var rawColumns = $("#" + name + ">thead>tr>th");
    var columns = [];
    for (var i = 0; i < rawColumns.length; i++)
    {
        // get properties
        var id = rawColumns[i].id;
        var text = rawColumns[i].innerText;

        // add columns
        columns.push({ id: id, name: text, field: id });
    }

    // scan its structure for rows, i = 1 <== skip first header row
    var rows = [];
    for (var rowIndex = 1; rowIndex < grid.rows.length; rowIndex++)
    {
        // get cells
        var cells = grid.rows[rowIndex].cells;
        var obj = {};

        // for each cell
        for (var cellIndex = 0; cellIndex < cells.length; cellIndex++)
        {
            // get properties
            var value = cells[cellIndex].innerText;
            var id = columns[cellIndex].id;

            // add property
            obj[id] = value;
        }

        // add row
        rows.push(obj);
    }

    // define options
    var options =
        {
            enableCellNavigation: true,
            enableColumnReorder: false,
            forceFitColumns: true
        };

    // clear the grid
    grid.outerHTML = "<div id='" + name + "' />";
    $("#" + name).width(width).height(height);

    // render the grid
    var slickGrid = new Slick.Grid("#" + name, rows, columns, options);
};

