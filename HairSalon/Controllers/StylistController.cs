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
    [HttpGet("/stylist/add")]
    public ActionResult AddStylistForm()
    {
      return View("AddStylist");
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

    [HttpPost("/stylist/{id}/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist.DeleteSpecificStylist(id);
      return View("StylistDelete");
    }

    [HttpPost("/stylist/delete")]
    public ActionResult DeleteAllStylists()
    {
      Stylist.DeleteAllStylists();
      return View("Stylists");
    }

    [HttpGet("/stylist/{id}/update")]
    public ActionResult UpdateStylistForm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpPost("/stylist/{id}/update")]
    public ActionResult UpdateStylist(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.EditStylist(Request.Form["updateStylistName"]);
      return View("StylistUpdateConfirmation");
    }
  }
}
