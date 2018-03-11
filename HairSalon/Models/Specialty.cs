using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _specialtyId;
    private string _specialtyName;

    public Specialty(string specialtyName, int specialtyId =0)
    {
      _specialtyId = specialtyId;
      _specialtyName = specialtyName;
    }

    //GETTERS AND SETTERS

    public int GetSpecialtyId()
    {
      return _specialtyId;
    }

    public string GetSpecialtyName()
    {
      return _specialtyName;
    }

    public static void DeleteAllSpecialties()
    {
      MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM specialty;";

           cmd.ExecuteNonQuery();

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
      }

    public static Specialty FindSpecialty(int id)
    {
  		MySqlConnection conn = DB.Connection();
  		conn.Open();

  		var cmd = conn.CreateCommand() as MySqlCommand;
  		cmd.CommandText = @"SELECT * FROM specialty WHERE id = @thisId;";

  		MySqlParameter thisId = new MySqlParameter();
  		thisId.ParameterName = "@thisId";
  		thisId.Value = id;
  		cmd.Parameters.Add(thisId);

  		var rdr = cmd.ExecuteReader() as MySqlDataReader;

  		int specialtyId = 0;
  		string specialtyName = "";

  		while (rdr.Read())
  		{
  		  specialtyId = rdr.GetInt32(0);
  		  specialtyName = rdr.GetString(1);
  		}
  		Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);
  		conn.Close();
  		if (conn != null)
  		{
  		  conn.Dispose();
  		}
  		return foundSpecialty;
    }
    public static List<Specialty> GetAllSpecialties()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialty ORDER BY specialty_name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);

        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allSpecialties;
      }

      public static Specialty Find(int id)
      {
        MySqlConnection conn = DB.Connection();
             conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialty WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
             thisId.ParameterName = "@thisId";
             thisId.Value = id;
             cmd.Parameters.Add(thisId);

             var rdr = cmd.ExecuteReader() as MySqlDataReader;

             int specialtyId = 0;
             string specialtyName = "";

             while (rdr.Read())
             {
                 specialtyId = rdr.GetInt32(0);
                 specialtyName = rdr.GetString(1);
             }

             Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);

              conn.Close();
              if (conn != null)
              {
                  conn.Dispose();
              }

             return foundSpecialty;

         }

      public void SaveSpecialty()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"Insert INTO specialty (specialty_name, id) VALUES (@specialtyName, @specialtyId);";

        MySqlParameter specialtyName = new MySqlParameter();
        specialtyName.ParameterName = "@specialtyName";
        specialtyName.Value = this._specialtyName;
        cmd.Parameters.Add(specialtyName);

        MySqlParameter specialtyId = new MySqlParameter();
        specialtyId.ParameterName = "@specialtyId";
        specialtyId.Value = this._specialtyId;
        cmd.Parameters.Add(specialtyId);

        cmd.ExecuteNonQuery();
        _specialtyId = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
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

         MySqlParameter stylistId = new MySqlParameter("@Stylistid", newStylist.GetStylistId());
         cmd.Parameters.Add(stylistId);

         MySqlParameter specialtyId = new MySqlParameter(@"SpecialtyId", _specialtyId);
         cmd.Parameters.Add(specialtyId);
         cmd.ExecuteNonQuery();
         conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
       }

       public List<Stylist> GetStylists () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT stylist_id FROM stylists_specialties WHERE specialty_id = @specialtyId;";

            MySqlParameter specialtyIdParameter = new MySqlParameter ();
            specialtyIdParameter.ParameterName = "@specialtyId";
            specialtyIdParameter.Value = _specialtyId;
            cmd.Parameters.Add (specialtyIdParameter);

            var rdr = cmd.ExecuteReader () as MySqlDataReader;

            List<int> stylistIdList = new List<int> { };
            while (rdr.Read ()) {
                int stylistId = rdr.GetInt32 (0);
                stylistIdList.Add (stylistId);
            }
            rdr.Dispose ();

            List<Stylist> stylists = new List<Stylist> { };
            foreach (int stylistId in stylistIdList) {
                var stylistQuery = conn.CreateCommand () as MySqlCommand;
                stylistQuery.CommandText = @"SELECT * FROM stylists WHERE id = @StylistId;";

                MySqlParameter stylistIdParameter = new MySqlParameter ();
                stylistIdParameter.ParameterName = "@StylistId";
                stylistIdParameter.Value = stylistId;
                stylistQuery.Parameters.Add (stylistIdParameter);

                var stylistQueryRdr = stylistQuery.ExecuteReader () as MySqlDataReader;
                while (stylistQueryRdr.Read ()) {
                    int thisStylistId = stylistQueryRdr.GetInt32 (0);
                    string stylistName = stylistQueryRdr.GetString (1);
                    string stylistNumber = stylistQueryRdr.GetString (2);
                    int stylistTenure = stylistQueryRdr.GetInt32 (3);

                    Stylist foundStylist = new Stylist (stylistName, stylistNumber, stylistTenure, thisStylistId);
                    stylists.Add (foundStylist);
                }
                stylistQueryRdr.Dispose ();
            }
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return stylists;
        }
    }
 }
