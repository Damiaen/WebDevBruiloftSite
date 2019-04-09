using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebMatrix.Data;

/// <summary>
/// Summary description for List
/// </summary>
public class GiftProvider
{
    public static List<Gift> GetById(string listId)
    {
        List<Gift> result = new List<Gift>();
        Database db = Database.Open("Database");
        var rows = db.Query("SELECT * FROM PresentListGifts WHERE PresentListId = " + listId +"ORDER BY Priority DESC");
        foreach (var row in rows)
        {
            result.Add(new Gift() {
                GiftId = row.GiftId,
                GiftName = row.GiftName,
                GiftPrice = row.GiftPrice,
                HasBeenBoughtBy = row.HasBeenBoughtBy,
                Location = row.Location,
                Description = row.Description,
                Priority = row.Priority
            });
        }
        db.Close();
        return result;
    }

    public static List<Gift> GetSingleById(string listId)
    {
        List<Gift> result = new List<Gift>();
        Database db = Database.Open("Database");
        var dbCommand = "SELECT * FROM PresentListGifts WHERE GiftId = @0";
        var row = db.QuerySingle(dbCommand, listId);
            result.Add(new Gift()
            {
                GiftId = row.GiftId,
                GiftName = row.GiftName,
                GiftPrice = row.GiftPrice,
                HasBeenBoughtBy = row.HasBeenBoughtBy,
                Location = row.Location,
                Description = row.Description,
                Priority = row.Priority
            });
        db.Close();
        return result;
    }

    public static bool deleteGiftById(string Id)
    {
        Database db = Database.Open("Database");
        var deleteCommand = "DELETE FROM PresentListGifts WHERE GiftId = @0";
        db.Execute(deleteCommand, Id);
        return true;
    }

    public static void updateGift(string giftId, string name, string price, string description, string store, string priority)
    {
        Database db = Database.Open("Database");
        var updateCommand = "UPDATE PresentListGifts SET GiftName=@0, GiftPrice=@1, Description=@2, Location=@3, Priority=@4 WHERE GiftId=@5";
        db.Execute(updateCommand, name, price, description, store, priority, giftId);
    }

    public static void buyGiftById(string buyerName, string selectedGiftId)
    {
        Database db = Database.Open("Database");
        var updateCommand = "UPDATE PresentListGifts SET HasBeenBoughtBy=@0 WHERE GiftId=@1";
        db.Execute(updateCommand, buyerName, selectedGiftId);
    }

    public static void addNewGift(string listId, string name, string price, string description, string store, string priority)
    {
        Database db = Database.Open("Database");
        var insertCommand = "INSERT INTO PresentListGifts (PresentListId, GiftName, GiftPrice, Description, Location, Priority) VALUES(@0, @1, @2, @3, @4, @5)";
        db.Execute(insertCommand, listId, name, price, description, store, priority);
    }
}