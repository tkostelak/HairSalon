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
    }
 }
