var cursor = {};

cursor.setWait = function()
{
    $("body").css("cursor", "wait");
};

cursor.setDefault = function ()
{
    $("body").css("cursor", null);
};