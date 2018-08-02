using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _specialtyId;
    private string _specialtyName;

    public Specialty(string SpecialtyName, int SpecialtyId = 0)
    {
      _specialtyName = SpecialtyName;
      _specialtyId = SpecialtyId;
    }
    public int GetSpecialtyId()
    {
      return _specialtyId;
    }
    public string GetSpecialtyName()
    {
      return _specialtyName;
    }
    public override bool Equals(System.Object otherSpecialty)
    {
      if(!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetSpecialtyId() == newSpecialty.GetSpecialtyId());
        bool nameEquality = (this.GetSpecialtyName() == newSpecialty.GetSpecialtyName());
        return(idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetSpecialtyId().GetHashCode();
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties; DELETE FROM stylists; DELETE FROM stylists_specialties;";
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
      cmd.CommandText = @"INSERT INTO specialties (Name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _specialtyName));

      cmd.ExecuteNonQuery();
      _specialtyId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int SpecialtyId = rdr.GetInt32(0);
        string SpecialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      // return new List<Specialty>{}; //Test will fail
      return allSpecialtys; //Test will pass
    }
    public static Specialty Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int SpecialtyId = 0;
     string SpecialtyName = "";

     while(rdr.Read())
     {
       SpecialtyId = rdr.GetInt32(0);
       SpecialtyName = rdr.GetString(1);
     }
     Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
     conn.Close();
     if (conn != null)
     {
         conn.Dispose();
     }
     // return new Specialty("", 0); //Test will fail
     return newSpecialty; //Test will pass
    }
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET Name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _specialtyId));
      cmd.Parameters.Add(new MySqlParameter("@newName", newName));

      cmd.ExecuteNonQuery();
      _specialtyName = newName;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId; DELETE FROM stylists_specialties WHERE specialty_id = @thisId";

      cmd.Parameters.Add(new MySqlParameter("@thisId", _specialtyId));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", _specialtyId));
      cmd.Parameters.Add(new MySqlParameter("@StylistId", newStylist.GetStylistId()));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialties
      JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
      JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
      WHERE specialties.id = @SpecialtyId;";

      cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", _specialtyId));

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistTitle = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistTitle, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
      // return new List<Stylist>{};
    }
  }
}
