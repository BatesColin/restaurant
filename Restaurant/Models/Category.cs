using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Restaurant.Models
{
  public class Cuisine
  {
    private int _cuisineId;
    private string _cuisineName;

    public Cuisine(string CuisineName, int CuisineId = 0)
    {
      _cuisineName = CuisineName;
      _cuisineId = CuisineId;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public string GetCuisineName()
    {
      return _cuisineName;
    }
    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetCuisineId() == newCuisine.GetCuisineId());
        bool idCuisineName = (this.GetCuisineName() == newCuisine.GetCuisineName());
        return (idEquality && idCuisineName);
      }
    }
    public override int GetHashCode()
    {
      return this.GetCuisineId().GetHashCode();
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM categories;";
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
      cmd.CommandText = @"INSERT INTO categories (Name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _cuisineName));

      cmd.ExecuteNonQuery();
      _cuisineId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisine = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CuisineId = rdr.GetInt32(0);
        string CuisineName = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
        allCuisine.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allCuisine;
    }
    public static Cuisine Find(int id)
    {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";

    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = id;
    cmd.Parameters.Add(searchId);

    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int CuisineId = 0;
    string CuisineName = "";

    while(rdr.Read())
    {
      CuisineId = rdr.GetInt32(0);
      CuisineName = rdr.GetString(1);
    }
    Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
    conn.Close();
    if (conn != null)
    {
        conn.Dispose();
    }
    return newCuisine;
    }
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE categories SET Name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _cuisineId));
      cmd.Parameters.Add(new MySqlParameter("@newName", newName));

      cmd.ExecuteNonQuery();
      _cuisineName = newName;

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
      cmd.CommandText = @"DELETE FROM categories WHERE id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisID", _cuisineId));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
