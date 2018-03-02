using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
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
      int stylistTenure = Int32.Parse(Request.Form["stylistExperience"]);
      Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistTenure);
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
