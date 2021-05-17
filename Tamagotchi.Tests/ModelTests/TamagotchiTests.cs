using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Tamagotchi.Models;

namespace Tamagotchi.TestTools
{
  [TestClass]
  public class IndividualTamagotchiTests : IDisposable
  {
    public void Dispose()
    {
      IndividualTamagotchi.ClearAll();
    }

    public IndividualTamagotchiTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=tomagotchi_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_IndividualTamagotchiList()
    {
      List<IndividualTamagotchi> newList = new List<IndividualTamagotchi> { };
      List<IndividualTamagotchi> result = IndividualTamagotchi.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsTamagotchis_IndividualTamagotchiList()
    {
      string tamagotchiName1 = "CutiePie";
      string tamagotchiName2 = "Pip";
      IndividualTamagotchi newTamagotchi1 = new IndividualTamagotchi(tamagotchiName1);
      newTamagotchi1.Save();
      IndividualTamagotchi newTamagotchi2 = new IndividualTamagotchi(tamagotchiName2);
      newTamagotchi2.Save();
      List<IndividualTamagotchi> newList = new List<IndividualTamagotchi>{newTamagotchi1, newTamagotchi2};

      List<IndividualTamagotchi> result = IndividualTamagotchi.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTueIfNamesAreTheSame_IndividualTamagotchi()
    {
      IndividualTamagotchi firstTamagotchi = new IndividualTamagotchi("CutiePie");
      IndividualTamagotchi secondTamagotchi = new IndividualTamagotchi("CutiePie");
      Assert.AreEqual(firstTamagotchi, secondTamagotchi);
    }

    [TestMethod]
    public void Save_SavesToDatabase_IndividualTamagotchiList()
    {
      IndividualTamagotchi newTamagotchi = new IndividualTamagotchi("CutiePie");

      newTamagotchi.Save();
      List<IndividualTamagotchi> result = IndividualTamagotchi.GetAll();
      List<IndividualTamagotchi> testList = new List<IndividualTamagotchi>{newTamagotchi};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_ReturnsCorrectTamagotchiFromDatabase_IndividualTamagotchi()
    {
      IndividualTamagotchi newTamagotchi = new IndividualTamagotchi("CutiePie");
      newTamagotchi.Save();
      IndividualTamagotchi newTamagotchi2 = new IndividualTamagotchi("Pip");
      newTamagotchi2.Save();
      IndividualTamagotchi foundTamagotchi = IndividualTamagotchi.Find(newTamagotchi2.Id);
      Assert.AreEqual(newTamagotchi2, foundTamagotchi);
    }
  }
}