using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Restaurant.Models;

namespace Restaurant.Tests
{
  [TestClass]
  public class DinerTests : IDisposable
  {
    public DinerTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
    }
    public void Dispose()
    {
      Cuisine.DeleteAll();
      Diner.DeleteAll();
    }
    [TestMethod]
    public void Save_Test()
    {
      //Arrange
      Diner testDiner = new Diner("McDonalds", "Seattle", "Popular", "Inexpensive", 3, "Questionable meat, good fries", 1);
      testDiner.Save();
      //Act
      List<Diner> emptyList = new List<Diner>{};
      List<Diner> testList = new List<Diner>{testDiner};
      List<Diner> result = Diner.GetAll();
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Diner testDiner = new Diner("Qdoba", "Redmond", "Semi-popular", "Average priced", 4, "Basically Chipotle, but better.", 4);
      testDiner.Save();
      //Act
      Diner result = Diner.Find(testDiner.GetDinerId());
      //Assert
      Assert.AreEqual(testDiner, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      Diner testDiner = new Diner("Brown Bag", "Kirkland", "The beeeesssttt", "dirt cheap", 5, "A little burnt", 6);
      testDiner.Save();
      Diner updateDiner = new Diner("Brown Bag", "Kirkland", "The beeessssttt", "dirt cheap", 5, "Ok a lot burnt", 6, testDiner.GetDinerId());
      //Act
      testDiner.Edit("Brown Bag", "Kirkland", "The beeessssttt", "dirt cheap", 5, "Ok a lot burnt", 6);

      Diner result = Diner.Find(testDiner.GetDinerId());
      //Assert

      Assert.AreEqual(updateDiner, result);

    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Diner testDiner = new Diner("Thai Ginger", "Factoria", "very popular", "medium priced", 4, "YUeah delicious. yummy.", 5);
      testDiner.Save();
      List<Diner> emptyList = new List<Diner>{};

      //Act
      Diner.Find(testDiner.GetDinerId()).Delete();
      List<Diner> result = Diner.GetAll();
      //Assert
      CollectionAssert.AreEqual(emptyList, result);
    }
  }
}
