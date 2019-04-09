using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Todo
/// </summary>
public class Gift
{
    public int GiftId { get; set; }
    public string GiftName { get; set; }
    public string GiftPrice { get; set; }
    public string HasBeenBoughtBy { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
}