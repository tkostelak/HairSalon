using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private int _clientId;
    private string _clientName;
    private int _stylistId;

    public Client(string clientName, int stylistId, int clientId =0)
    {
      _clientName = clientName;
      _stylistId = stylistId;
      _clientId = clientId;
    }

    //GETTERS AND SETTERS

    public void SetClientId(int clientId)
    {
      _clientId = clientId;
    }

    public string GetClientName()
    {
      return _clientName;
    }

    public int GetClientId()
    {
      return _clientId;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public override int GetHashCode()
    {
      return this.GetClientId().GetHashCode();
    }

    //METHODS

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetClientId() == newClient.GetClientId());
        bool clientEquality = (this.GetClientName() == newClient.GetClientName());
        return (idEquality && clientEquality);
      }
    }

    public static List<Client> GetAllClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients ORDER BY name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, stylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allClients;
      }

      public void EditClient(string newClientName)
      {
        MySqlConnection conn = DB.Connection();
         conn.Open();
         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"UPDATE clients SET name = @newClientName WHERE id = @thisId;";

         MySqlParameter clientName = new MySqlParameter();
         clientName.ParameterName = "@newClientName";
         clientName.Value = newClientName;
         cmd.Parameters.Add(clientName);

         MySqlParameter clientId = new MySqlParameter();
         clientId.ParameterName = "@thisId";
         clientId.Value = this._clientId;
         cmd.Parameters.Add(clientId);

         cmd.ExecuteNonQuery();
         _clientName = newClientName;

         conn.Close();
         if (conn != null)
         {
             conn.Dispose();
         }
      }

    public List<Client> GetClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          int stylistID = rdr.GetInt32(2);
          Client newClient = new Client(clientName, stylistID);

          allClients.Add(newClient);
        }

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      return allClients;
    }

    public void SaveClient()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"Insert INTO clients (name, id, stylist_id) VALUES (@clientName, @client_id, @thisstylist_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@clientName";
      name.Value = this._clientName;
      cmd.Parameters.Add(name);

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@client_id";
      clientId.Value = this._clientId;
      cmd.Parameters.Add(clientId);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@thisstylist_id";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _clientId = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        {
          conn.Dispose();
        }
      }


      public static void DeleteAllClients()
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

      public static void DeleteSpecificClient(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE from clients WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
      }

      public static Client Find(int id)
      {
        MySqlConnection conn = DB.Connection();
             conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
          thisId.ParameterName = "@thisId";
           thisId.Value = id;
           cmd.Parameters.Add(thisId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;

           int clientId = 0;
           string clientName = "";
           int stylistId = 0;

           while (rdr.Read())
           {
               clientId = rdr.GetInt32(0);
               clientName = rdr.GetString(1);
               stylistId = rdr.GetInt32(2);
           }

           Client foundClient = new Client(clientName, stylistId);

              conn.Close();
              if (conn != null)
              {
                  conn.Dispose();
              }

             return foundClient;
        }
    }
}
