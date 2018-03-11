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
    public ActionResult ViewAllClients(int clientId)
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

    [HttpGet("/client/{clientId}")]
    public ActionResult FindClient(int clientId)
    {
      Dictionary<string, object> clientData = new Dictionary<string, object>();
      Client newClient = Client.Find(clientId);
      Console.WriteLine(clientId);
      List<Client> allClients = Client.GetAllClients();
      clientData.Add("newClient", newClient);
      clientData.Add("allClients", allClients);
      return View(clientData);
    }

    [HttpGet("/client/delete/{clientId}")]
    public ActionResult DeleteClient(int clientId)
    {
      Client newClient = Client.Find(clientId);
      Console.WriteLine(clientId);
      newClient.DeleteClient();
      return View ("ClientDelete");
    }

    [HttpGet("/client/{clientId}/update")]
    public ActionResult UpdateClientForm(int clientId)
    {
      Dictionary<string, object> clientData = new Dictionary<string, object>();
      List<Client> clientList = Client.GetAllClients();
      Client thisClient = Client.Find(clientId);
      clientData.Add("clientList", clientList);
      clientData.Add("thisClient", thisClient);
      return View(clientData);
    }

    [HttpPost("/client/{clientId}")]
    public ActionResult UpdateClientInfo(int clientId)
    {
			Client newClient = Client.Find(clientId);
      newClient.EditClient(Request.Form["updateClientName"]);
      return RedirectToAction("FindClient", new {clientId = clientId});
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
