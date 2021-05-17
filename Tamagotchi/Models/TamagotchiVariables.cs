using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Tamagotchi.Models
{
  public class IndividualTamagotchi
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public int FoodLevel { get; set; }
    public int Attention { get; set; }
    public int Rest { get; set; }
    public int Id { get; set; }
    
    public IndividualTamagotchi(string name)
    {
      Name = name;
      Age = 1;
      FoodLevel = 10;
      Attention = 10;
      Rest = 10;
    }

    public IndividualTamagotchi(string name, int id)
    {
      Name = name;
      Id = id;
      Age = 1;
      FoodLevel = 10;
      Attention = 10;
      Rest = 10;
    }

    public override bool Equals(System.Object otherIndividualTamagotchi)
    {
      if(!(otherIndividualTamagotchi is IndividualTamagotchi))
      {
        return false;
      }
      else
      {
        IndividualTamagotchi newTamagotchi = (IndividualTamagotchi) otherIndividualTamagotchi;
        bool nameEquality = (this.Name == newTamagotchi.Name);
        bool ageEquality = (this.Age == newTamagotchi.Age);
        bool foodLevelEquality = (this.FoodLevel == newTamagotchi.FoodLevel);
        bool attentionEquality = (this.Attention == newTamagotchi.Attention);
        bool restEquality = (this.Rest == newTamagotchi.Rest);
        bool idEquality = (this.Id == newTamagotchi.Id);
        return (nameEquality && ageEquality && foodLevelEquality && attentionEquality && restEquality && idEquality);
      }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tamagotchi;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<IndividualTamagotchi> GetAll()
    {
      List<IndividualTamagotchi> allTamagotchis = new List<IndividualTamagotchi>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tamagotchi;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int tamagotchiId = rdr.GetInt32(0);
        string tamagotchiName = rdr.GetString(1);
        IndividualTamagotchi newTamagotchi = new IndividualTamagotchi(tamagotchiName, tamagotchiId);
        newTamagotchi.Age = rdr.GetInt32(2);
        newTamagotchi.FoodLevel = rdr.GetInt32(3);
        newTamagotchi.Attention = rdr.GetInt32(4);
        newTamagotchi.Rest = rdr.GetInt32(5);
        allTamagotchis.Add(newTamagotchi);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTamagotchis;
    }
    public static IndividualTamagotchi Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tamagotchi WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int tamagotchiId = 0;
      string tamagotchiName = "";
      int tamagotchiAge = 5;
      int tamagotchiFoodLevel = 5;
      int tamagotchiAttention = 5;
      int tamagotchiRest = 5;
      while (rdr.Read())
      {
        tamagotchiId = rdr.GetInt32(0);
        tamagotchiName = rdr.GetString(1);
        tamagotchiAge = rdr.GetInt32(2);
        tamagotchiFoodLevel = rdr.GetInt32(3);
        tamagotchiAttention = rdr.GetInt32(4);
        tamagotchiRest = rdr.GetInt32(5);
      }
      IndividualTamagotchi foundTamagotchi =  new IndividualTamagotchi(tamagotchiName, tamagotchiId);
      foundTamagotchi.Age = tamagotchiAge;
      foundTamagotchi.FoodLevel = tamagotchiFoodLevel;
      foundTamagotchi.Attention = tamagotchiAttention;
      foundTamagotchi.Rest = tamagotchiRest;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundTamagotchi;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tamagotchi (name, age, food_level, attention, rest) VALUES (@TamagotchiName, @TamagotchiAge, @TamagotchiFoodLevel, @TamagotchiAttention, @TamagotchiRest);";
      // MySqlParameter name = new MySqlParameter();
      // name.ParameterName = "@TamagotchiName";
      // name.Value = this.Name;
      cmd.Parameters.AddWithValue("@TamagotchiName", this.Name);
      cmd.Parameters.AddWithValue("@TamagotchiAge", this.Age);
      cmd.Parameters.AddWithValue("@TamagotchiFoodLevel", this.FoodLevel);
      cmd.Parameters.AddWithValue("@TamagotchiAttention", this.Attention);
      cmd.Parameters.AddWithValue("@TamagotchiRest", this.Rest);
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
//Method for feeding
//Method for Attention/playing
//Method for Sleeping