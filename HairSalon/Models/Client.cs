using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private string _clientName;
    private int _stylistId;
    private int _clientId;


    public Stylist(string stylistName, string stylistNumber, int stylistTenure, string stylistSpecialty, int clientId = 0, int stylistId = 0)
    {
      _stylistName = stylistName;
      _clientId = clientId;
      _stylistNumber = stylistNumber;
      _stylistTenure = stylistTenure;
      _stylistSpecialty = stylistSpecialty;
      _stylistId = stylistId;
    }
