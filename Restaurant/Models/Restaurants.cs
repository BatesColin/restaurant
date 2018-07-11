using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Restaurant.Models
{
  public class Cuisine
  {
    private int _cuisineId;
    private string _cuisineName;
    private string _meal;
    private string _origin;
    private float _price;
    private string _cuisineDescription;

    public Cuisine(string CuisineName, string Meal, string Origin, float Price, string CuisineDescription, int CuisineId = 0)
    {
      _cuisineName = CuisineName;
      _meal = Meal;
      _origin = Origin;
      _price = Price;
      _cuisineDescription = CuisineDescription;
      _cuisineId = CuisineId;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public int GetCuisineName()
    {
      return _cuisineName;
    }
    public int GetMeal()
    {
      return _meal;
    }
    public int GetOrigin()
    {
      return _origin;
    }
    public int GetPrice()
    {
      return _price;
    }
    public int GetCuisineDescription()
    {
      return _cuisineDescription;
    }

  }
  public class Diner
  {
    private int _dinerId;
    private string _dinerName;
    private string _location;
    private string _popularity;
    private string _priceRange;
    private int _rating;
    private string _dinerDescription;

    public Diner(string DinerName, string Location, string Popularity, string PriceRange, int Rating, string DinerDescription, DinerId = 0)
    {
      _dinerName = DinerName;
      _location = Location;
      _popularity = Popularity;
      _priceRange = PriceRange;
      _rating = Rating;
      _dinerDescription = DinerDescription;
      _dinerId = DinerId;
    }
    public int GetDinerID()
    {
      return _dinerId;
    }
    public string GetDinerName()
    {
      return _dinerName;
    }
    public string GetDinerLocation()
    {
      return _location;
    }
    public string GetDinerPopularity()
    {
      return _popularity;
    }
    public string GetDinerPriceRange()
    {
      return _priceRange;
    }
    public int GetDinerRating()
    {
      return _rating;
    }
    public string GetDinerDesctipton()
    {
      return _dinerDescription;
    }
    
  }
}
