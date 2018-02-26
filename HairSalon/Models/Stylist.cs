using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistName;
    private string _stylistNumber;
    private int _stylistTenure;
    private string _stylistSpecialty;
    private int _clientId;
    private int _stylistId;

    public Stylist(string stylistName, string stylistNumber, int stylistTenure, string stylistSpecialty, int clientId = 0, int stylistId = 0)
    {
      _stylistName = stylistName;
      _clientId = clientId;
      _stylistNumber = stylistNumber;
      _stylistTenure = stylistTenure;
      _stylistSpecialty = stylistSpecialty;
      _stylistId = stylistId;
    }

    public int GetClientId()
    {
      return _clientId;
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

    public string GetStylistSpecialty()
    {
      return _stylistSpecialty;
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
        int clientId = rdr.GetInt32(2);
        string stylistNumber = rdr.GetString(3);
        int stylistTenure = rdr.GetInt32(4);
        string stylistSpecialty = rdr.GetString(5);
        Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistSpecialty, clientId, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allStylists;
      }

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
            bool clientEquality = this.GetClientId() == newStylist.GetClientId();
            return (idEquality && stylistEquality && clientEquality);
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
               int clientId = 0;
               string stylistNumber = "";
               int stylistTenure = 0;
               string stylistSpecialist = "";

               while (rdr.Read())
               {
                   stylistId = rdr.GetInt32(0);
                   stylistName = rdr.GetString(1);
                   clientId = rdr.GetInt32(2);
                   stylistNumber = rdr.GetString(3);
                   stylistTenure = rdr.GetInt32(4);
                   stylistSpecialist = rdr.GetString(5);
               }

               Stylist foundStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistSpecialist, clientId, stylistId);

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
          cmd.CommandText = @"Insert INTO stylists (name, client_id, number, tenure, specialty) VALUES (@stylistName, @client_id, @stylistNumber, @stylistTenure, @stylistSpecialty);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@stylistName";
          name.Value = this._stylistName;
          cmd.Parameters.Add(name);

          MySqlParameter clientId = new MySqlParameter();
          clientId.ParameterName = "@client_id";
          clientId.Value = this._clientId;
          cmd.Parameters.Add(clientId);

          MySqlParameter number = new MySqlParameter();
          number.ParameterName = "@stylistNumber";
          number.Value = this._stylistNumber;
          cmd.Parameters.Add(number);

          MySqlParameter tenure = new MySqlParameter();
          tenure.ParameterName = "@stylistTenure";
          tenure.Value = this._stylistTenure;
          cmd.Parameters.Add(tenure);

          MySqlParameter specialty = new MySqlParameter();
          specialty.ParameterName = "@stylistSpecialty";
          specialty.Value = this._stylistSpecialty;
          cmd.Parameters.Add(specialty);

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
