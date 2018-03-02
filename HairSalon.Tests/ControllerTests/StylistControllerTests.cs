using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
      [TestMethod]
      public void Stylists_ReturnsCorrectView_True()
      {
        //Arrange
        StylistController controller = new StylistController();
        IActionResult actionResult = controller.Stylists();
        ViewResult stylistsView = controller.Stylists() as ViewResult;

        //Act
        var result = stylistsView.ViewData.Model;

        //Assert
        Assert.IsInstanceOfType(result, typeof(List<Stylist>));
      }
   }
}
