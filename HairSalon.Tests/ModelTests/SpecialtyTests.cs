using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tyler_kostelak_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAllStylists();
      Client.DeleteAllClients();
      Specialty.DeleteAllSpecialties();
    }

    [TestMethod]
    public void SaveSpecialty_AssignsIdToObject_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Men's Haircuts", 0);

      //Act
      testSpecialty.SaveSpecialty();
      Specialty savedSpecialty = Specialty.GetAllSpecialties()[0];
      int result = savedSpecialty.GetSpecialtyId();
      int testId = testSpecialty.GetSpecialtyId();
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}
