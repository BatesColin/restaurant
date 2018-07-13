using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using System.Collections.Generic;
using System;

namespace Restaurant.Controllers
{
  public class CuisineController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Index()
    {
        // Cuisine newCuisine = new Cuisine(Request.Query["new-item"]);
        // newCuisine.Save();
        // List<Cuisine> result = new List<Cuisine>();
        // return View(result);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View(allCuisines);
    }

    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/categories")]
    public ActionResult Create()
    {
      Cuisine newCuisine = new Cuisine (Request.Form["newCuisine"]);
      newCuisine.Save();
      List<Cuisine> allCuisines = Cuisine.GetAll();
      return View("Index", allCuisines);
    }
    [HttpGet("/categories/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Cuisine thisCuisine = Cuisine.Find(id);
        return View(thisCuisine);
    }
    [HttpPost("/categories/{id}/update")]
    public ActionResult Update(int id)
    {
        Cuisine thisCuisine = Cuisine.Find(id);
        thisCuisine.Edit(Request.Form["newname"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/categories/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Cuisine thisCuisine = Cuisine.Find(id);
        thisCuisine.Delete();
        return RedirectToAction("Index");
    }
    // [HttpPost("/categories/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Cuisine.ClearAll();
    //   return View();
    // }
    // [HttpGet("/categories/{id}")]
    // public ActionResult Details(int id)
    // {
    //     Cuisine item = Cuisine.Find(id);
    //     return View(item);
    // }
  }
}
