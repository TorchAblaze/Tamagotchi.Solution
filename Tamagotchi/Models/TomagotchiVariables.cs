using System;
using System.Collections.Generic;

namespace Tamagotchi.Models
{
  public class IndividualTamagotchi
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public int FoodLevel { get; set; }
    public int Attention { get; set; }
    public int Rest { get; set; }
    public int Id { get; }
    private static List<IndividualTamagotchi> _creatures = new List<IndividualTamagotchi> { };
    public static List<IndividualTamagotchi> GetAll()
    {
      return _creatures;
    }
    public IndividualTamagotchi(string name)
    {
      Name = name;
      Age = 1;
      FoodLevel = 10;
      Attention = 10;
      Rest = 10;
      _creatures.Add(this);
      Id = _creatures.Count;
    }
    public static void ClearAll()
    {
      _creatures.Clear();
    }
    public static IndividualTamagotchi Find(int searchId)
    {
      return _creatures[searchId - 1];
    }
  }
}
//Method for feeding
//Method for Attention/playing
//Method for Sleeping