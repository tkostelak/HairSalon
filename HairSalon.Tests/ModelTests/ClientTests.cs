using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tyler_kostelak_test;";
    }
    public void Dispose()
    {
      Stylist.DeleteAllStylists();
      Client.DeleteAllClients();
    }

    [TestMethod]
    public void GetClients_DataBaseAtFirst_0()
    {
      //Arrange
      string clientName = "James Taylor";
      int stylistId = 1;
      Client newClient = new Client(clientName, stylistId);

      //Act
      int result = newClient.GetClients().Count;
      Console.WriteLine("This is the number of Clients in the result list: " + result);

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void DeleteAllClients_DeletesAllClientsInDatabase_true()
    {
    //Arrange
    Client testClient = new Client("Bob Macpherson", 3 , 3 );
    testClient.SaveClient();

    //Act
    Client.DeleteAllClients();
    int result = Client.GetAllClients().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
  }
}
