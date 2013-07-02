// define class
var Grid = function ()
{
};

// define public properties
Grid.prototype.DataSourceName = null;
Grid.prototype.GridName = null;
Grid.prototype.GridControl = null;
Grid.prototype.NewRowContentUrl = null;

// define private properties
Grid.prototype._currentNewRowId = 0;
Grid.prototype._cacheNewRowContent = null;

// define public static properties
Grid.Namespace = "grid";
Grid.CustomAttributeNamespace = "data-" + Grid.Namespace + "-";
Grid.CustomAttributeId = Grid.CustomAttributeNamespace + "id";
Grid.CustomAttributePropertyName = Grid.CustomAttributeNamespace + "propertyname";
Grid.IdPendingChanges = "PendingChanges";
Grid.IdNewRowDisplayContent = "NewRowDisplayContent";
Grid.IdNewRowContentTemplate = "NewRowContentTemplate";
Grid.IdContentPlaceHolder = "ContentPlaceHolder";
Grid.IdNewRowContentPanel = "NewRowContentPanel";
Grid.IdTable = "Table";


// ============================================================================
Grid.prototype.GenerateInputs = function (currentDiv, editType, id)
{
    /// <summary>Generates inputs from input tags that belongs to given DIV tag.</summary>
    /// <param name="currentDiv" type="DIV">Reference to the DIV tag that contains input tags.</param>
    /// <param name="editType" type="String">Type of the edit: new, edit, delete.</param>
    /// <param name="id" type="Number">ID of the row on client or server side (depends on the edit type).</param>

    // generate inputs
    var inputs = $(currentDiv).find(":input").filter(":not(:button)");

    // prepare result
    var result = [];

    // for each input
    for (var i = 0; i < inputs.length; i++)
    {
        // get the input
        var input = $(inputs[i]);

        // generate input tags:
        // key: new_1_Property1, value: <value>
        // key: edit_1_Property1, value: <value>
        result.push(this.CreateInputTag(editType, id, input.attr("id"), input.val()));
    }

    // return result
    return result;
};

// ============================================================================
Grid.prototype.CreateInputTag = function (editType, id, propertyName, value)
{
    /// <summary>Creates an input tag that contais combination of given parameters.</summary>
    /// <param name="editType" type="String">Type of the edit: new, edit, delete.</param>
    /// <param name="id" type="Integer">ID of the row on client or server side (depends on the edit type).</param>
    /// <param name="propertyName" type="String">Name of the binding property on the server side.</param>
    /// <param name="value" type="String">Value to bind to the property on the server side, available with new and edit Edit Type only.</param>
    /// <returns type="INPUT" />

    // prepare an input tag
    var tagName = this.DataSourceName + "_" + editType + "_" + id;

    // for each case
    switch (editType)
    {
        case "new":
        case "edit":
            tagName += "_" + propertyName;
            break;
        case "delete":
            break;
        default:
            throw "Unexpected edit type: " + editType;
    }

    // create tag
    return $("<input/>").attr("type", "hidden").attr("id", tagName).attr("name", tagName).val(value);
};

// ============================================================================
Grid.prototype.FindControl = function (name)
{
    /// <summary>Finds the control specifically belongs to this grid.</summary>
    /// <param name="name" type="String">Name of the object, does not include the prefix "[gridName]_".</param>
    /// <returns type="$" />

    // do not accept undefined parameter
    if (name == undefined)
    {
        throw "Invalid argument: undefined name.";
    }

    // find the grid first, if not done yet
    if (this.GridControl == null)
    {
        this.GridControl = $("#" + this.GridName);
    }

    // try to find the control inside the grid
    var result = this.GridControl.find("[" + Grid.CustomAttributeId + "='" + name + "']");

    // if not exactly matched
    if (result.length != 1)
    {
        console.log(result);
        throw "Find control '" + name + "' but found " + result.length + ' result(s). Expecting exactly 1 result.';
    }

    // return result
    return result;
};



// ============================================================================
Grid.prototype.OnNewRowClicked = function ()
{
    /// <summary>Fired when the text "Click here to add new row" clicked.</summary>

    // get the content container
    var container = this.FindControl(Grid.IdNewRowContentPanel);

    // save the grid context
    var grid = this;

    // if the content is cached, take from that
    if (this._cacheNewRowContent != undefined)
    {
        container.html(this._cacheNewRowContent);
        return;
    }

    // get content from service
    $.get(this.NewRowContentUrl, function (data)
    {        
        // clone the New Row buttons and append to the downloaded content
        //content.append(grid.FindControl(Grid.IdNewRowButtonsTemplate).html());

        // clone the New Row template
        var content = $("<div/>").html(grid.FindControl(Grid.IdNewRowContentTemplate).html());

        // replace the placeholder with downloaded content and remove its name to avoid later duplicated search results (with the template)
        content.find("[" + Grid.CustomAttributeId + "='" + Grid.IdContentPlaceHolder + "']").html(data).attr(Grid.CustomAttributeId, "");

        // replace the content with special attribute to avoid collision with other inputs in the parent form
        content.children(":input").each(function ()
        {
            // get control
            var control = $(this);

            // replace its attribute
            control.attr(Grid.CustomAttributePropertyName, control.attr("name")).attr("name", null);
        });
        
        // save to cache
        grid._cacheNewRowContent = content.html();

        // show it        
        grid.FindControl(Grid.IdNewRowDisplayContent).hide();
        grid.FindControl(Grid.IdNewRowContentPanel).html(content).show();
    });
};

// ============================================================================
Grid.prototype.OnNewRowSaveButtonClicked = function ()
{
    /// <summary>Fired when the button Save in New Row Row panel is clicked.</summary>

    // save & reset
    this.OnNewRowSaveNewButtonClicked(false);

    // hide the panel
    this.OnNewRowCancelButtonClicked();
};


// ============================================================================
Grid.prototype.OnNewRowSaveNewButtonClicked = function (isResetPanel)
{
    /// <summary>Fired when the button Save in New Row Row panel is clicked.</summary>
    /// <param name="isResetPanel" type="Boolean">Indicates whether the input panel will be reset after saving.</param>

    // TODO: validate

    // allocate new id
    var id = this._currentNewRowId++;

    // generate inputs & find pending changes div to insert to
    var inputs = this.GenerateInputs(this.FindControl(Grid.IdNewRowContentPanel), "new", id);
    var pendingChangesPanel = grid.FindControl(Grid.IdPendingChanges); // append to pending changes div
    for (var i = 0; i < inputs.length; i++)
    {
        pendingChangesPanel.append(inputs[i]);
    }

    // clone last row
    var lastRow = this.FindControl(Grid.IdTable).find("tr:last");
    lastRow.after(lastRow[0].outerHTML);

    // reset panel
    if (isResetPanel)
    {
        var container = this.FindControl(Grid.IdNewRowContentPanel);
        container.html(this._cacheNewRowContent);
    }
    return;
};


// ============================================================================
Grid.prototype.OnNewRowCancelButtonClicked = function ()
{
    /// <summary>Fired when the button Cancel in New Row Row panel is clicked.</summary>
    /// <param name="sender" type="DIV">Reference to the BUTTON tag that fires the event.</param>

    // hiden current panel
    this.FindControl(Grid.IdNewRowContentPanel).hide();
    this.FindControl(Grid.IdNewRowDisplayContent).show();
};

//// ============================================================================
//Grid.prototype.SwitchRow = function (currentRow, isNextRow)
//{
//    /// <summary>Switches display from current to next row</summary>
//    /// <param name="currentRow" type="TR">Reference to the current row that will be hidden.</param>
//    /// <param name="isNextRow" type="Boolean">Indicates the next row to show is next or previous to current row.</param>    

//    // hide current row
//    $(currentRow).hide();

//    // get next row
//    var nextRow = isNextRow ? $(currentRow).next() : $(currentRow).prev();

//    // generate dynamic content
//    if (isNextRow)
//    {
//        // showing content in next row
//        var nextRowName = currentRow.id + Grid.IdContentRow;

//        // found or not matched
//        if (nextRow.length == 0 || nextRow[0].id != nextRowName)
//        {
//            // not found
//            // create new row

//            // get column count
//            var columnCount = $("tr").last().parents("table").find("th").length;

//            // generate HTML
//            nextRow = $("<tr/>").attr("id", nextRowName).append($("<td/>").attr("colspan", columnCount)).insertAfter(currentRow);
//        }
//    }

//    // show new row
//    return nextRow.show();
//};