using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=Curt_Caldwell_Tests;";
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
    [TestMethod]
    public void Save_Test()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mark");
      testStylist.Save();
      //Act
      List<Stylist> testList = new List<Stylist>{testStylist};
      List<Stylist> result = Stylist.GetAll();
      //Assert
      CollectionAssert.AreEqual(testList, result);
}
}
}
