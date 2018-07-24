using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _clientId;
    private string _clientName;
    private int _stylist_Id;

    public Client(string ClientName, int Stylist_Id = 0, int ClientId = 0)
    {
      _clientName = ClientName;
      _clientId = ClientId;
      _stylist_Id = Stylist_Id;
    }
    public int GetClientId()
    {
      return _clientId;
    }
    public string GetClientName()
    {
      return _clientName;
    }
    public int GetStylist_Id()
    {
      return _stylist_Id;
    }
    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetClientId() == newClient.GetClientId());
        bool nameEquality = (this.GetClientName() == newClient.GetClientName());
        bool stylist_IdEquality = (this.GetStylist_Id() == newClient.GetStylist_Id());
        return(idEquality && nameEquality && stylist_IdEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetClientId().GetHashCode();
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
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
      cmd.CommandText = @"INSERT INTO clients (Name, Stylist_Id) VALUES (@name, @stylist_Id);";

      cmd.Parameters.Add(new MySqlParameter("@name", _clientName));
      cmd.Parameters.Add(new MySqlParameter("@stylist_Id", _stylist_Id));

      cmd.ExecuteNonQuery();
      _clientId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int Stylist_Id = rdr.GetInt32(2);
        Client newClient = new Client(ClientName, Stylist_Id, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      
      return allClients;
    }
    public static List<Client> GetClientsById(int id)
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_Id = @stylist_Id ;";

      cmd.Parameters.Add(new MySqlParameter("@stylist_Id", id));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int Stylist_Id = rdr.GetInt32(2);
        Client newClient = new Client(ClientName, Stylist_Id, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allClients;
    }
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ClientId = 0;
      string ClientName = "";
      int Stylist_Id = 0;

      while(rdr.Read())
      {
        ClientId = rdr.GetInt32(0);
        ClientName = rdr.GetString(1);
        Stylist_Id = rdr.GetInt32(2);
      }
      Client newClient = new Client(ClientName, Stylist_Id, ClientId);
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return newClient;
    }
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET Name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _clientId));
      cmd.Parameters.Add(new MySqlParameter("@newName", newName));


      cmd.ExecuteNonQuery();
      _clientName = newName;

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
     cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

     cmd.Parameters.Add(new MySqlParameter("@thisID", _clientId));

     cmd.ExecuteNonQuery();

     conn.Close();
     if(conn != null)
     {
       conn.Dispose();
     }
   }
  }
}
