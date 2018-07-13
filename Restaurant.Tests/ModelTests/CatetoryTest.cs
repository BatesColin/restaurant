using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Restaurant.Models;

namespace Restaurant.Tests
{
  [TestClass]
  public class CuisineTests : IDisposable
  {
    public CuisineTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
    }
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
    [TestMethod]
    public void Save_Test()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Chinese");
      testCuisine.Save();
      //Act
      List<Cuisine> emptyList = new List<Cuisine>{};
      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      List<Cuisine> result = Cuisine.GetAll();
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Mexican");
      testCuisine.Save();
      //Act
      Cuisine result = Cuisine.Find(testCuisine.GetCuisineId());
      //Assert
      Assert.AreEqual(testCuisine, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      string  testName = "American";
      Cuisine testCuisine = new Cuisine(testName, 1);
      testCuisine.Save();
      string updateName = "MURICAN";
      //Act
      testCuisine.Edit(updateName);
      string result = Cuisine.Find(testCuisine.GetCuisineId()).GetCuisineName();
      //Assert
      Assert.AreEqual(updateName, result);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("America");
      testCuisine.Save();
      List<Cuisine> emptyList = new List<Cuisine>{};

      //Act
      Cuisine.Find(testCuisine.GetCuisineId()).Delete();
      List<Cuisine> result = Cuisine.GetAll();
      //Assert
      CollectionAssert.AreEqual(emptyList, result);
    }
  }
}
