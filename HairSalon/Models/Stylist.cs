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
    private string _stylistTenure;
    private string _stylistSpecialty;

    public Stylist(string stylistName, string stylistNumber, string stylistTenure, string stylistSpecialty, int stylistId = 0)
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

    public string GetStylistTenure()
    {
      return _stylistTenure;
    }

    public string GetStylistSpecialty()
    {
      return _stylistSpecialty;
    }

    public static List<Stylist> GetAll()
    {
      return null;
      // List<Stylist> allStylists = new List<Stylist> {};
      // MySqlConnection conn = DB.Connection();
      // conn.Open();
      // MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"SELECT * FROM stylists ORDER BY stylist ASC;";
      // MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      // while(rdr.Read())
      // {
      //   int stylistId = rdr.GetInt32(0);
      //   string stylistName = rdr.GetString(1);
      //   string stylistNumber = rdr.GetString(2);
      //   string stylistTenure = rdr.GetString(3);
      //   string stylistSpecialty = rdr.GetString(4);
      //   Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistSpecialty, stylistId);
      //   allStylists.Add(newStylist);
      // }
      // conn.Close();
      // if (conn !=null)
      // {
      //   conn.Dispose();
      // }
      // return allStylists;
      }

      public static void DeleteAllStylists()
      {
        MySqlConnection conn = DB.Connection();
             conn.Open();

             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"DELETE FROM stylist;";

             cmd.ExecuteNonQuery();

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }
        }
    }
}
