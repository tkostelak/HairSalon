using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace Stylist.Tests
{
  [TestClass]
  public class TrackTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
