using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _stylistId;
    private string _stylistName;
    private string _stylistNumber;
    private int _stylistTenure;
    private string _stylistSpecialty;

    public Stylist(string stylistName, string stylistNumber, int stylistTenure, string stylistSpecialty, int stylistId = 0)
    {
      _stylistId = stylistId;
      _stylistName = stylistName;
      _stylistNumber = stylistNumber;
      _stylistTenure = stylistTenure;
      _stylistSpecialty = stylistSpecialty;
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
        string stylistNumber = rdr.GetString(2);
        int stylistTenure = rdr.GetInt32(3);
        string stylistSpecialty = rdr.GetString(4);
        Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistSpecialty, stylistId);
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
            return (idEquality && stylistEquality);
          }
        }

        public void SaveStylist()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"Insert INTO stylists (name, number, tenure, specialty) VALUES (@stylistName, @stylistNumber, @stylistTenure, @stylistSpecialty);";

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
