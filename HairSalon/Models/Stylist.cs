using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _stylistId;
    private string _stylistName;

    public Stylist(string StylistName, int StylistId = 0)
    {
      _stylistName = StylistName;
      _stylistId = StylistId;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetStylistName()
    {
      return _stylistName;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetStylistId() == newStylist.GetStylistId());
        bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
        return(idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetStylistId().GetHashCode();
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (Name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _stylistName));

      cmd.ExecuteNonQuery();
      _stylistId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int StylistId = rdr.GetInt32(0);
        string StylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(StylistName, StylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allStylists;
          }
          public static Stylist Find(int id)
         {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

           MySqlParameter searchId = new MySqlParameter();
           searchId.ParameterName = "@searchId";
           searchId.Value = id;
           cmd.Parameters.Add(searchId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           int StylistId = 0;
           string StylistName = "";

           while(rdr.Read())
           {
             StylistId = rdr.GetInt32(0);
             StylistName = rdr.GetString(1);
           }
           Stylist newStylist = new Stylist(StylistName, StylistId);
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

           return newStylist; 
    }
  }
}
