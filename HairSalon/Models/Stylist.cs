using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;
using System.Linq;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistName;
    private string _stylistNumber;
    private int _stylistTenure;
    private int _stylistId;
    public List<Client> clientList = new List<Client>();
    public Stylist(string stylistName, string stylistNumber, int stylistTenure, int stylistId = 0)
    {
      _stylistName = stylistName;
      _stylistNumber = stylistNumber;
      _stylistTenure = stylistTenure;
      _stylistId = stylistId;
    }


    public int GetStylistId()
    {
      return _stylistId;
    }

    public string GetStylistName()
    {
      return _stylistName;
    }

    public string GetStylistNumber()
    {
      return _stylistNumber;
    }

    public int GetStylistTenure()
    {
      return _stylistTenure;
    }

    //METHODS

    public void EditStylist(string newStylistName)
    {
      MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"UPDATE stylists SET name = @newStylistName WHERE id = @thisId;";

       MySqlParameter stylistName = new MySqlParameter();
       stylistName.ParameterName = "@newStylistName";
       stylistName.Value = newStylistName;
       cmd.Parameters.Add(stylistName);

       MySqlParameter stylistId = new MySqlParameter();
       stylistId.ParameterName = "@thisId";
       stylistId.Value = this._stylistId;
       cmd.Parameters.Add(stylistId);

       cmd.ExecuteNonQuery();
       _stylistName = newStylistName;

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
    }

    public void AddClientStylist (Client newClient)
    {
      MySqlConnection conn = DB.Connection ();
      conn.Open ();
      var cmd = conn.CreateCommand () as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@StylistId, @clientId);";

      MySqlParameter stylistId = new MySqlParameter ();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = _stylistId;
      cmd.Parameters.Add (stylistId);

      MySqlParameter clientId = new MySqlParameter ();
      clientId.ParameterName = "@clientId";
      clientId.Value = newClient.GetClientId();
      cmd.Parameters.Add (clientId);

      cmd.ExecuteNonQuery ();
      conn.Close ();
      if (conn != null)
      {
         conn.Dispose ();
      }
    }

    public List<Specialty> GetStylistSpecialties()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialty.* FROM stylists
         JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
         JOIN specialty ON (stylists_specialties.specialty_id = specialty.id)
         WHERE stylists.id = @SpecialtyId;";

      MySqlParameter specialtyIdParameter = new MySqlParameter("@SpecialtyId", _stylistId);
      cmd.Parameters.Add(specialtyIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Specialty> specialtyList = new List<Specialty>{};
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        specialtyList.Add(newSpecialty);
      }
      conn.Close();
       if(conn != null)
       {
         conn.Dispose();
       }
       return specialtyList;
     }

    public void AddSpecialty (Specialty newSpecialty)
    {
      MySqlConnection conn = DB.Connection ();
      conn.Open ();
      var cmd = conn.CreateCommand () as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter stylistId = new MySqlParameter ();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = _stylistId;
      cmd.Parameters.Add (stylistId);

      MySqlParameter specialtyId = new MySqlParameter ();
      specialtyId.ParameterName = "@SpecialtyId";
      specialtyId.Value = newSpecialty.GetSpecialtyId ();
      cmd.Parameters.Add (specialtyId);

      cmd.ExecuteNonQuery ();
      conn.Close ();
      if (conn != null)
      {
         conn.Dispose ();
      }
    }

    public List<Client> GetClients()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT clients.* FROM stylists
         JOIN stylists_clients ON (stylists.id = stylists_clients.stylist_id)
         JOIN clients ON (stylists_clients.client_id = clients.id)
         WHERE stylists.id = @StylistId;";

     MySqlParameter stylistId = new MySqlParameter();
     stylistId.ParameterName = "@StylistId";
     stylistId.Value = _stylistId;
     cmd.Parameters.Add(stylistId);

     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
     List<Client> clients = new List<Client>{};

     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string clientName = rdr.GetString(1);
       Client newClient = new Client(clientName, clientId);
       clients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
         conn.Dispose();
     }
     return clients;
    }
    public static List<Stylist> GetAllStylists()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists ORDER BY name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistNumber = rdr.GetString(2);
        int stylistTenure = rdr.GetInt32(3);
        Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allStylists;
      }

      public static void DeleteSpecificStylist(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE from stylists WHERE id = @thisId;";

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

      //This method will delete all stylists from the database, use with caution!
      public static void DeleteAllStylists()
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

        public override bool Equals(System.Object otherStylist)
        {
          if (!(otherStylist is Stylist))
          {
            return false;
          }
          else
          {
            Stylist newStylist = (Stylist) otherStylist;
            bool idEquality = (this.GetStylistId() == newStylist.GetStylistId());
            bool stylistEquality = (this.GetStylistName() == newStylist.GetStylistName());
            // bool clientEquality = this.GetClientId() == newStylist.GetClientId();
            return (idEquality && stylistEquality);
          }
        }

        public override int GetHashCode()
        {
          return this.GetStylistName().GetHashCode();
        }

        public static Stylist Find(int id)
        {
          MySqlConnection conn = DB.Connection();
               conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

          MySqlParameter thisId = new MySqlParameter();
               thisId.ParameterName = "@thisId";
               thisId.Value = id;
               cmd.Parameters.Add(thisId);

               var rdr = cmd.ExecuteReader() as MySqlDataReader;

               int stylistId = 0;
               string stylistName = "";
               string stylistNumber = "";
               int stylistTenure = 0;

               while (rdr.Read())
               {
                   stylistId = rdr.GetInt32(0);
                   stylistName = rdr.GetString(1);
                   stylistNumber = rdr.GetString(2);
                   stylistTenure = rdr.GetInt32(3);
               }

               Stylist foundStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistId);

                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }

               return foundStylist;

           }

        public void SaveStylist()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"Insert INTO stylists (name, number, tenure) VALUES (@stylistName, @stylistNumber, @stylistTenure);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@stylistName";
          name.Value = this._stylistName;
          cmd.Parameters.Add(name);

          MySqlParameter number = new MySqlParameter();
          number.ParameterName = "@stylistNumber";
          number.Value = this._stylistNumber;
          cmd.Parameters.Add(number);

          MySqlParameter tenure = new MySqlParameter();
          tenure.ParameterName = "@stylistTenure";
          tenure.Value = this._stylistTenure;
          cmd.Parameters.Add(tenure);

          cmd.ExecuteNonQuery();
          _stylistId = (int) cmd.LastInsertedId;

          conn.Close();
          if (conn != null)
            {
              conn.Dispose();
            }
        }
    }
}
