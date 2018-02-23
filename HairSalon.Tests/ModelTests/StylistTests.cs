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
    public void Dispose()
    {
      Stylist.DeleteAllStylists();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tyler_kostelak_test;";
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
      Stylist testStylist = new Stylist("Jamie Stephens","509-200-2000", "3" ,"Men's HairCuts", 0);

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
  }
}
