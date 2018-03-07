using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
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
      int result = Stylist.GetAllStylists().Count;
      Console.WriteLine("This is the number of Stylists in the result list: " + result);

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void SaveStylist_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jamie Stephens","509-200-2000", 3 , 0 );

      //Act
      testStylist.SaveStylist();
      Stylist savedStylist = Stylist.GetAllStylists()[0];
      int result = savedStylist.GetStylistId();
      int testId = testStylist.GetStylistId();
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsStylistInDataBase_Stylist()
    {
    //Arrange
    Stylist testStylist = new Stylist("Jamie Stephens","509-200-2000", 3 , 0 );
    testStylist.SaveStylist();

    //Act
    Stylist foundStylist = Stylist.Find(testStylist.GetStylistId());

    //Assert
    Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void DeleteAllStylists_DeletesAllStylistsInDatabase_true()
    {
    //Arrange
    Stylist testStylist = new Stylist("Jamie Stephens","509-200-2000", 3 , 0 );
    testStylist.SaveStylist();

    //Act
    Stylist.DeleteAllStylists();
    int result = Stylist.GetAllStylists().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }

    [TestMethod]
    public void Edit_UpdatesStylistsinDatabase_String()
    {
      //Arrange
      string firstStylistName = "Pickle Rick";
      Stylist testStylist = new Stylist(firstStylistName, "666-666-6666", 3 , 0);
      testStylist.SaveStylist();
      string secondStylistName = "Tiny Rick";

      //Act
      testStylist.EditStylist(secondStylistName);
      string result = Stylist.Find(testStylist.GetStylistId()).GetStylistName();

      //Assert
      Assert.AreEqual(secondStylistName, result);
    }

    // [TestMethod]
    // public void AddClientStylist_AddsClientToStylistDatabase_True()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist("Jamie Stephens","509-200-2000", 3 , 0);
    //   testStylist.SaveStylist();
    //
    //   Client testClient = new Client("Bob Macpherson", 3 , 3 );
    //   testClient.SaveClient();
    //
    //   Client testClient2 = new Client("Jilly Giles", 1, 1);
    //   testClient2.SaveClient();
    //
    //   //Act
    //   testStylist.AddClientStylist(testClient);
    //   testStylist.AddClientStylist(testClient2);
    //
    //   List<Client> result = testStylist.GetClients();
    //   List<Client> testList = new List<Client>{testClient, testClient2};
    //
    //   //Assert
    //   CollectionAssert.AreEqual(testList, result);
    // }
  }
}
