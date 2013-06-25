// define class
var Grid = function ()
{
};

// define properties
Grid.prototype.NewRowContentUrl = null;

// ============================================================================
Grid.prototype.OnNewRowClicked = function (currentRow)
{
    // switch row
    var nextRow = this.SwitchRow(currentRow, null);

    // get content from service
    $.get(this.NewRowContentUrl, function (data) {
        // fill content to the row
        $(nextRow).find("td").html(data); 
    });    
};

// ============================================================================
Grid.prototype.SwitchRow = function (currentRow, isNextRow)
{
    // hide current row
    $(currentRow).hide();

    // get next row
    var nextRow = isNextRow ? $(currentRow).next() : $(currentRow).prev();
    var nextRowName = currentRow.id + "_ContentRow";

    // found or not matched
    if (nextRow.length == 0 || nextRow[0].id != nextRowName)
    {
        // not found
        // create new row

        // get column count
        var columnCount = $("tr").last().parents("table").find("th").length;

        // generate HTML
        nextRow = $("<tr/>").attr("id", nextRowName).append($("<td/>").attr("colspan", columnCount)).insertAfter(currentRow);
    }

    // show new row
    return nextRow.show();       
};


//// ============================================================================
//grid.render = function (name, width, height)
//{
//    // get table name
//    var tableName = "#" + name + "_table";

//    // get the grid and set its css
//    //var grid = $("#" + name).css("width", width).css("height", height)[0];
//    var grid = $(tableName)[0];

//    // scan its structure for columns
//    var rawColumns = $(tableName + ">thead>tr>th");
//    var columns = [];
//    for (var i = 0; i < rawColumns.length; i++)
//    {
//        // get properties
//        var id = rawColumns[i].id;
//        var text = rawColumns[i].innerText;

//        // add columns
//        columns.push({ id: id, name: text, field: id });
//    }

//    // scan its structure for rows, i = 1 <== skip first header row
//    var rows = [];
//    for (var rowIndex = 1; rowIndex < grid.rows.length; rowIndex++)
//    {
//        // get cells
//        var cells = grid.rows[rowIndex].cells;
//        var obj = {};

//        // for each cell
//        for (var cellIndex = 0; cellIndex < cells.length; cellIndex++)
//        {
//            // get properties
//            var value = cells[cellIndex].innerText;
//            var id = columns[cellIndex].id;

//            // add property
//            obj[id] = value;
//        }

//        // add row
//        rows.push(obj);
//    }

//    // define options
//    var options =
//        {
//            enableCellNavigation: true,
//            enableColumnReorder: false,
//            forceFitColumns: true
//        };

//    // clear the grid
//    grid.outerHTML = "<div id='" + name + "_table" + "' />";
//    $(tableName).width(width).height(height);

//    // render the grid
//    var slickGrid = new Slick.Grid(tableName, rows, columns, options);

//    // add grid to managed list
//    grid.controls.push(new { table: grid });
//};
