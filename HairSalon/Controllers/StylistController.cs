using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

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
      Dictionary<string, object> stylistData = new Dictionary<string, object>();
      List<Specialty> specialtyList = Specialty.GetAllSpecialties();
      Stylist newStylist = Stylist.Find(id);
      List<Client> allClients = Client.GetAllClients();
      List<Client> clientList = newStylist.GetClients();
      List<Specialty> specialtyStylistList = newStylist.GetStylistSpecialties();
      stylistData.Add("specialtyStylistList", specialtyStylistList);
      stylistData.Add("allClients", allClients);
      stylistData.Add("clientList", clientList);
      stylistData.Add("specialtyList", specialtyList);
      stylistData.Add("newStylist", newStylist);
      return View(stylistData);
    }

    [HttpPost("/stylist/specialty/add/{id}")]
    public ActionResult AddStylistSpecialty(int id)
    {
      Stylist newStylist = Stylist.Find(id);
      Specialty newSpecialty = Specialty.FindSpecialty(int.Parse(Request.Form["addSpecialty"]));
      newStylist.AddSpecialty(newSpecialty);
      return RedirectToAction("FindStylist", new { id = id});
    }

    [HttpPost("/stylist/client/add/{id}")]
    public ActionResult AddClientToStylist(int id)
    {
      Stylist newStylist = Stylist.Find(id);
      Client newClient = Client.Find(int.Parse(Request.Form["addClient"]));
      newStylist.AddClientStylist(newClient);
      return RedirectToAction("FindStylist", new { id = id});
    }

    [HttpPost("/stylist/delete/{id}")]
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
      return RedirectToAction("Stylists" );
    }
  }
}
