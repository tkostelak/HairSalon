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
    public void GetAll_DataBaseAtFirst_0()
    {
      
      //Arrange, Act
      int result = Client.GetClients().Count;
      Console.WriteLine("This is the number of Clients in the result list: " + result);

      //Assert
      Assert.AreEqual(0, result);
    }







  }
}
