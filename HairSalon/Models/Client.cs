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

    public Client(string clientName, int clientId)
    {
      _clientName = clientName;
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

    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }

    public int GetClientId()
    {
      return _clientId;
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

        Client newClient = new Client(clientName, clientId);
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
          Client newClient = new Client( clientName, clientId );

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
      cmd.CommandText = @"Insert INTO clients (name, id) VALUES (@clientName, @client_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@clientName";
      name.Value = this._clientName;
      cmd.Parameters.Add(name);

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@client_id";
      clientId.Value = this._clientId;
      cmd.Parameters.Add(clientId);

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

      public void DeleteClient()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId";

        MySqlParameter deleteId = new MySqlParameter();
        deleteId.ParameterName = "@thisId";
        deleteId.Value = this.GetClientId();
        cmd.Parameters.Add(deleteId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public List<Stylist> GetClientStylist()
      {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT stylists.* FROM clients
           JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
           JOIN stylists ON (stylists_clients.stylist_id = stylists.id)
           WHERE clients.id = @ClientId;";

       MySqlParameter clientIdParameter = new MySqlParameter();
       clientIdParameter.ParameterName = "@ClientId";
       clientIdParameter.Value = _clientId;
       cmd.Parameters.Add(clientIdParameter);

       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       List<Stylist> stylists = new List<Stylist>{};

       while(rdr.Read())
       {
         int stylistId = rdr.GetInt32(0);
         string stylistName = rdr.GetString(1);
         string stylistNumber = rdr.GetString(2);
         int stylistTenure = rdr.GetInt32(3);
         Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistId);
         stylists.Add(newStylist);
       }
       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
       return stylists;
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


           while (rdr.Read())
           {
               clientId = rdr.GetInt32(0);
               clientName = rdr.GetString(1);
           }

           Client foundClient = new Client(clientName, clientId);

              conn.Close();
              if (conn != null)
              {
                  conn.Dispose();
              }

             return foundClient;
        }
    }
}
