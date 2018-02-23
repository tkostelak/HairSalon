using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace Client.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }
