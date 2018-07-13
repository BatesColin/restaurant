using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Restaurant.Models
{
  public class Diner
  {
    private int _dinerId;
    private string _dinerName;
    private string _location;
    private string _popularity;
    private string _priceRange;
    private int _rating;
    private string _dinerDescription;
    private int _cuisine_Id;

    public Diner(string DinerName, string Location, string Popularity, string PriceRange, int Rating, string DinerDescription, int Cuisine_Id, int DinerId = 0)
    {
      _dinerName = DinerName;
      _location = Location;
      _popularity = Popularity;
      _priceRange = PriceRange;
      _rating = Rating;
      _dinerDescription = DinerDescription;
      _cuisine_Id = Cuisine_Id;
      _dinerId = DinerId;
    }
    public int GetDinerId()
    {
      return _dinerId;
    }
    public string GetDinerName()
    {
      return _dinerName;
    }
    public string GetLocation()
    {
      return _location;
    }
    public string GetPopularity()
    {
      return _popularity;
    }
    public string GetPriceRange()
    {
      return _priceRange;
    }
    public int GetRating()
    {
      return _rating;
    }
    public string GetDescription()
    {
      return _dinerDescription;
    }
    public int GetCuisine_Id()
    {
      return _cuisine_Id;
    }
    public override bool Equals(System.Object otherDiner)
    {
      if (!(otherDiner is Diner))
      {
        return false;
      }
      else
      {
        Diner newDiner = (Diner) otherDiner;

        bool dinerNameEquality = (this.GetDinerName() == newDiner.GetDinerName());
        bool locationEquality = (this.GetLocation() == newDiner.GetLocation());
        bool popularityEquality = (this.GetPopularity() == newDiner.GetPopularity());
        bool priceRangeEquality = this.GetPriceRange() == newDiner.GetPriceRange();
        bool descriptionEquality = this.GetDescription() == newDiner.GetDescription();
        bool cuisine_IdEquality = this.GetCuisine_Id() == newDiner.GetCuisine_Id();
        bool idEquality = (this.GetDinerId() == newDiner.GetDinerId());
        //return (dinerNameEquality);
        return (idEquality && dinerNameEquality && locationEquality && popularityEquality && priceRangeEquality && descriptionEquality && cuisine_IdEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetDinerId().GetHashCode();
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (Name, Location, Popularity, PriceRange, Rating, Description, cuisine_id) VALUES (@name, @location, @popularity, @priceRange, @rating, @description, @cuisine_Id);";

      cmd.Parameters.Add(new MySqlParameter("@name", _dinerName));
      cmd.Parameters.Add(new MySqlParameter("@location", _location));
      cmd.Parameters.Add(new MySqlParameter("@popularity", _popularity));
      cmd.Parameters.Add(new MySqlParameter("@priceRange", _priceRange));
      cmd.Parameters.Add(new MySqlParameter("@rating", _rating));
      cmd.Parameters.Add(new MySqlParameter("@description", _dinerDescription));
      cmd.Parameters.Add(new MySqlParameter("@cuisine_Id", _cuisine_Id));

      cmd.ExecuteNonQuery();
      _dinerId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Diner> GetAll()
    {
      List<Diner> allDiner = new List<Diner> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int DinerId = rdr.GetInt32(0);
        string DinerName = rdr.GetString(1);
        string Location = rdr.GetString(2);
        string Popularity = rdr.GetString(3);
        string PriceRange = rdr.GetString(4);
        int Rating = rdr.GetInt32(5);
        string DinerDescription = rdr.GetString(6);
        int Cuisine_Id = rdr.GetInt32(7);
        Diner newDiner = new Diner(DinerName, Location, Popularity, PriceRange, Rating, DinerDescription, Cuisine_Id, DinerId);
        allDiner.Add(newDiner);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allDiner;
    }
    public static Diner Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int DinerId = 0;
      string DinerName = "";
      string Location = "";
      string Popularity = "";
      string PriceRange = "";
      int Rating = 0;
      string DinerDescription = "";
      int Cuisine_Id = 0;

      while(rdr.Read())
      {
        DinerId = rdr.GetInt32(0);
        DinerName = rdr.GetString(1);
        Location = rdr.GetString(2);
        Popularity = rdr.GetString(3);
        PriceRange = rdr.GetString(4);
        Rating = rdr.GetInt32(5);
        DinerDescription = rdr.GetString(6);
        Cuisine_Id = rdr.GetInt32(7);
      }
      Diner newDiner = new Diner(DinerName, Location, Popularity, PriceRange, Rating, DinerDescription, Cuisine_Id, DinerId);
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return newDiner;
    }
    public void Edit(string newDinerName, string newLocation, string newPopularity, string newPriceRange, int newRating, string newDinerDescription, int newCuisine_Id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET Name = @newDinerName, Location = @newLocation, Popularity = @newPopularity, PriceRange = @newPriceRange, Rating = @newRating, Description = @newDinerDescription, cuisine_id = @newCuisine_Id WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _dinerId));
      cmd.Parameters.Add(new MySqlParameter("@newDinerName", newDinerName));
      cmd.Parameters.Add(new MySqlParameter("@newLocation", newLocation));
      cmd.Parameters.Add(new MySqlParameter("@newPopularity", newPopularity));
      cmd.Parameters.Add(new MySqlParameter("@newPriceRange", newPriceRange));
      cmd.Parameters.Add(new MySqlParameter("@newRating", newRating));
      cmd.Parameters.Add(new MySqlParameter("@newDinerDescription", newDinerDescription));
      cmd.Parameters.Add(new MySqlParameter("@newCuisine_Id", newCuisine_Id));

      cmd.ExecuteNonQuery();
      _dinerName = newDinerName;
      _location = newLocation;
      _popularity = newPopularity;
      _priceRange = newPriceRange;
      _rating = newRating;
      _dinerDescription = newDinerDescription;
      _cuisine_Id = newCuisine_Id;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisID", _dinerId));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
