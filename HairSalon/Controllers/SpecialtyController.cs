using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
      [HttpGet("/specialty/add")]
      public ActionResult SpecialtyAddForm()
      {
        return View("SpecialtyForm");
      }

      [HttpGet("/specialty")]
      public ActionResult Specialties()
      {
        List<Specialty> specialtyList = Specialty.GetAllSpecialties();
        return View(specialtyList);
      }

      [HttpPost("/specialty")]
      public ActionResult CreateSpecialty()
      {
        string specialtyName = Request.Form["specialtyName"];

        Specialty newSpecialty = new Specialty( specialtyName );
        newSpecialty.SaveSpecialty();
        List<Specialty> specialtyList = Specialty.GetAllSpecialties();

        return View("Specialties", specialtyList);
      }
    }
  }
