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
        return View("SpecialtyDirectory", specialtyList);
      }

      [HttpPost("/specialty")]
      public ActionResult CreateSpecialty()
      {
        string specialtyName = Request.Form["specialtyName"];

        Specialty newSpecialty = new Specialty( specialtyName );
        newSpecialty.SaveSpecialty();
        List<Specialty> specialtyList = Specialty.GetAllSpecialties();

        return View("SpecialtyDirectory", specialtyList);
      }

      [HttpGet("/specialty/{id}")]
      public ActionResult FindSpecialty(int id)
      {
        Dictionary<string, object> specialtyData = new Dictionary<string, object>();
        List<Specialty> specialtyList = Specialty.GetAllSpecialties();
        Specialty newSpecialty = Specialty.Find(id);
        List<Stylist> stylistList = newSpecialty.GetStylists();
        List<Stylist> allStylists = Stylist.GetAllStylists();
        specialtyData.Add("allStylists", allStylists);
        specialtyData.Add("stylistList", stylistList);
        specialtyData.Add("specialtyList", specialtyList);
        specialtyData.Add("newSpecialty", newSpecialty);
        return View(specialtyData);
      }

      [HttpPost("/specialty/{specialtyId}/stylist/new")]
      public ActionResult AddStylistToSpecialty (int specialtyId)
      {
        Specialty specialty = Specialty.Find(specialtyId);
        Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylistId"]));
        specialty.AddStylist(stylist);
        return RedirectToAction("FindSpecialty", new {id = specialtyId});
      }
  }
}
