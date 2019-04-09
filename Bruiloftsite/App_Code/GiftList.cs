using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Todo
/// </summary>
public class GiftList
{
    public int ListId { get; set; }
    public int OwnerId { get; set; }
    public string ListCode { get; set; }
    public string ListName { get; set; }
    public DateTime LastEdited { get; set; }
}