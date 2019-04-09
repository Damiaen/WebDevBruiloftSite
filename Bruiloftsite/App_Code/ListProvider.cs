using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for List
/// </summary>
public class ListProvider
{
    public static bool CheckIfListValid(string listId, string uniqueToken)
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        string provider = "System.Data.SqlClient";
        Database db = Database.OpenConnectionString(connectionString, provider);
        var checkIfValid = db.QuerySingle("SELECT * FROM PresentLists WHERE ListCode = " + uniqueToken + "AND ListId = " + listId);
        return checkIfValid != null ? true : false;
    }

    public static string GetListByToken(string uniqueToken)
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        string provider = "System.Data.SqlClient";
        Database db = Database.OpenConnectionString(connectionString, provider);
        var checkIfValid = db.QuerySingle("SELECT * FROM PresentLists WHERE ListCode = " + uniqueToken);
        if (checkIfValid != null) {
            return Convert.ToString(checkIfValid.ListId);
        }   else
        {
            return "error";
        }
    }

    public static List<GiftList> GetListByOwnerId(int Id)
    {
        List<GiftList> result = new List<GiftList>();
        Database db = Database.Open("Database");
        var dbCommand = "SELECT * FROM PresentLists WHERE OwnerId=@0";
        var rows = db.Query(dbCommand, Id);
        foreach (var row in rows)
        {
            result.Add(new GiftList()
            {
                ListId = row.ListId,
                OwnerId = row.OwnerId,
                ListCode = row.ListCode,
                ListName = row.ListName,
                LastEdited = row.LastEdited,
            });
        }
        db.Close();
        return result;
    }

    public static bool CheckIfOwner(string listId, string uniqueToken, int currentUserId)
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        string provider = "System.Data.SqlClient";
        Database db = Database.OpenConnectionString(connectionString, provider);
        var checkIfValid = db.QuerySingle("SELECT * FROM PresentLists WHERE ListCode = " + uniqueToken + "AND ListId = " + listId +" AND OwnerId = "+currentUserId);
        return checkIfValid != null ? true : false;
    }

    public static void deleteListById(string Id)
    {
        Database db = Database.Open("Database");
        var deleteCommand = "DELETE FROM PresentLists WHERE ListId = @0";
        db.Execute(deleteCommand, Id);

    }

    public static void addNewList(int userId, string listCode, string name, string lastEdited)
    {
        Database db = Database.Open("Database");
        var insertCommand = "INSERT INTO PresentLists (OwnerId, ListCode, ListName, LastEdited) VALUES(@0, @1, @2, @3)";
        db.Execute(insertCommand, userId, listCode, name, lastEdited);
    }

    //public static Gift Insert(string todoName)
    //{
    //    Database db = Database.Open("Todo");

    //    int count = Convert.ToInt32(db.QueryValue("SELECT COUNT(1) FROM Todo WHERE name = @0", todoName));
    //    if (count > 0)
    //    {
    //        return null;
    //    }

    //    int result = db.Execute("INSERT INTO Todo (Name) VALUES (@0)", todoName);
    //    if (result == 1)
    //    {
    //        int todoId = Convert.ToInt32(db.GetLastInsertId());

    //        var record = db.QuerySingle("SELECT TodoId, Name FROM Todo WHERE TodoId = @0", todoId);
    //        db.Close();
    //        return new Gift() { TodoId = record.TodoId, Name = record.Name };
    //    }
    //    throw new Exception("INSERT Failed!");
    //}

    //public static bool Delete(int todoId)
    //{
    //    Database db = Database.Open("Todo");
    //    int result = db.Execute("DELETE FROM Todo WHERE TodoId = @0", todoId);
    //    db.Close();
    //    return result == 1;
    //}
}