// define class
var Grid = function ()
{
};

// define public properties
Grid.prototype.DataSourceName = null;
Grid.prototype.GridName = null;
Grid.prototype.NewRowContentUrl = null;

// define private properties
Grid.prototype._currentNewRowId = 0;
Grid.prototype._cacheNewRowContent = null;

// define public static properties
Grid.Namespace = "grid";
Grid.CustomAttributeNamespace = "data-" + Grid.Namespace + "-";
Grid.CustomAttributePropertyName = Grid.CustomAttributeNamespace + "propertyname";
Grid.IdContentRow = "_ContentRow";
Grid.IdPendingChanges = "PendingChanges";
Grid.IdNewRowButtons = "NewRowButtons";

// ============================================================================
Grid.prototype.SwitchRow = function (currentRow, isNextRow)
{
    /// <summary>Switches display from current to next row</summary>
    /// <param name="currentRow" type="TR">Reference to the current row that will be hidden.</param>
    /// <param name="isNextRow" type="Boolean">Indicates the next row to show is next or previous to current row.</param>    

    // hide current row
    $(currentRow).hide();

    // get next row
    var nextRow = isNextRow ? $(currentRow).next() : $(currentRow).prev();

    // generate dynamic content
    if (isNextRow)
    {
        // showing content in next row
        var nextRowName = currentRow.id + Grid.IdContentRow;

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
    }

    // show new row
    return nextRow.show();
};

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
        result.push(this.CreateInputTag(editType, id, input.attr(Grid.CustomAttributePropertyName), input.val()));
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

    // try to find the control or the grid itself
    var result = name != null ? $("#" + this.GridName + "_" + name) : $("#" + this.GridName);

    // if not exactly matched
    if (result.length != 1)
    {
        console.log(result);
        throw "Find control '" + name + "' but found " + result.length + 'result(s). Expecting exactly 1 result.';
    }

    // return result
    return result;
};



// ============================================================================
Grid.prototype.OnNewRowClicked = function (currentRow)
{
    // switch row
    var nextRow = this.SwitchRow(currentRow, true);
    var container = $(nextRow).find("td");
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
        // fill content to the row
        var content = container.html(data);

        // replace the content with special attribute to avoid collision with other inputs in the parent form
        content.children(":input").each(function ()
        {
            // get control
            var control = $(this);

            // replace its attribute
            control.attr(Grid.CustomAttributePropertyName, control.attr("name")).attr("name", null);
        });

        // clone the New Row buttons and append to the downloaded content
        content.append(grid.FindControl(Grid.IdNewRowButtons).html());

        // save to cache
        grid._cacheNewRowContent = content.html();
    });
};

// ============================================================================
Grid.prototype.OnNewRowSaveButtonClicked = function (sender)
{
    /// <summary>Fired when the button Save in New Row Row panel is clicked.</summary>
    /// <param name="sender" type="DIV">Reference to the BUTTON tag that fires the event.</param>

    // save & reset
    this.OnNewRowSaveNewButtonClicked(sender, false);

    // hide the panel
    this.OnNewRowCancelButtonClicked(sender);
};


// ============================================================================
Grid.prototype.OnNewRowSaveNewButtonClicked = function (sender, isResetPanel)
{
    /// <summary>Fired when the button Save in New Row Row panel is clicked.</summary>
    /// <param name="sender" type="DIV">Reference to the BUTTON tag that fires the event.</param>
    /// <param name="isResetPanel" type="Boolean">Indicates whether the input panel will be reset after saving.</param>

    // TODO: validate

    // allocate new id
    var id = this._currentNewRowId++;

    // generate inputs
    var inputs = this.GenerateInputs($(sender).parents("tr")[0], "new", id);

    // append to pending changes div
    for (var i = 0; i < inputs.length; i++)
    {
        this.FindControl(Grid.IdPendingChanges).append(inputs[i]);
    }

    // reset panel
    if (isResetPanel)
    {
        var container = $(sender).parents("td:first");
        container.html(this._cacheNewRowContent);
    }
    return;
};


// ============================================================================
Grid.prototype.OnNewRowCancelButtonClicked = function (sender)
{
    /// <summary>Fired when the button Cancel in New Row Row panel is clicked.</summary>
    /// <param name="sender" type="DIV">Reference to the BUTTON tag that fires the event.</param>

    // hiden current panel
    this.SwitchRow($(sender).parents("tr")[0], false);
};