using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/stylist")]
      public ActionResult Stylists()
      {
        List<Stylist> stylistList = Stylist.GetAllStylists();
        return View(stylistList);
      }

      [HttpPost("/stylist")]
      public ActionResult AddStylists()
      {
        string stylistName = Request.Form["stylistName"];
        string stylistNumber = Request.Form["stylistNumber"];
        int stylistTenure = int.Parse(Request.Form["stylistExperience"]);
        string stylistSpecialty = Request.Form["stylistSpecialty"];
        Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure, stylistSpecialty);
        newStylist.SaveStylist();
        List<Stylist> stylistList = Stylist.GetAllStylists();

        return View("Stylists", stylistList);
      }

      [HttpGet("/stylist/{id}")]
      public ActionResult FindStylist(int id)
      {
        Stylist newStylist = Stylist.Find(id);
        return View(newStylist);
      }

    }
  }
