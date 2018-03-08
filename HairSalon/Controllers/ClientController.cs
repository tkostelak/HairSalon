using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    [HttpPost("/Client/Create")]
     public ActionResult CreateClient()
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

    [HttpGet("/client/{id}")]
    public ActionResult FindClient(int id)
    {
      Dictionary<string, object> clientData = new Dictionary<string, object>();
      Client newClient = Client.Find(id);
      clientData.Add("newClient", newClient);
      return View(clientData);
    }

    [HttpPost("/client/delete/{id}")]
    public ActionResult DeleteClient(int id)
    {
      Client.DeleteSpecificClient(id);
      return View("ClientDelete");
    }


    [HttpGet("/client/{id}/update")]
    public ActionResult UpdateClientForm(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }

    [HttpPost("/client/{id}/update")]
    public ActionResult UpdateClient(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.EditClient(Request.Form["updateClientName"]);
      return View ("UpdateClientConfirmation");
    }

    [HttpGet("client/add/new")]
    public ActionResult AddClient()
    {
      Dictionary<string, object> clientData = new Dictionary<string, object>();
      List<Stylist> stylistList = Stylist.GetAllStylists();
      clientData.Add("stylistList", stylistList);
      return View(clientData);
    }

    [HttpPost("/clients")]
    public ActionResult AddClientPost(int id)
    {
			string clientName = Request.Form["clientName"];
      int stylistId = int.Parse(Request.Form["addStylist"]);
      Client newClient = new Client(clientName, stylistId);
      newClient.SaveClient();
			List<Client> clientList = Client.GetAllClients();
      return View("Clients", clientList);
		}
  }
}
