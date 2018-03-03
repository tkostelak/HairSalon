using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
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

    [HttpGet("/client/view/all")]
    public ActionResult ViewAllClients()
    {
      List<Client> clientList = Client.GetAllClients();
      return View("Clients", clientList);
    }

    [HttpPost("/client/delete")]
    public ActionResult DeleteAllClients()
    {
      Client.DeleteAllClients();
      return View("Clients");
    }
  }
}
