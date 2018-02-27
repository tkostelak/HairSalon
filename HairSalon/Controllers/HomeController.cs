using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

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
        int stylistTenure = Int32.Parse(Request.Form["stylistExperience"]);
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


      [HttpPost("/Client/Create")]
       public ActionResult AddClient()
      {
         string clientName = Request.Form["clientName"];
         int stylistId = Int32.Parse(Request.Form["stylistId"]);
         Client newClient = new Client(clientName, stylistId);
         newClient.SaveClient();
         var Client = new Client(clientName, stylistId);
         List<Client> clientList = Client.GetClients();

         return View("ClientConfirmation", clientList);
       }
    }
  }
