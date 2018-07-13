using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using System.Collections.Generic;
using System;

namespace Restaurant.Controllers
{
  public class DinerController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
        // Diner newDiner = new Diner(Request.Query["new-item"]);
        // newDiner.Save();
        // List<Diner> result = new List<Diner>();
        // return View(result);
        List<Diner> allDiners = Diner.GetAll();
        return View(allDiners);
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/items")]
    public ActionResult Create()
    {
      Diner newDiner = new Diner (Request.Form["newDiner"]);
      newDiner.Save();
      List<Diner> allDiners = Diner.GetAll();
      return View("Index", allDiners);
    }
    [HttpGet("/items/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Diner thisDiner = Diner.Find(id);
        return View(thisDiner);
    }
    [HttpPost("/items/{id}/update")]
    public ActionResult Update(int id)
    {
        Diner thisDiner = Diner.Find(id);
        thisDiner.Edit(Request.Form["newname"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/items/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Diner thisDiner = Diner.Find(id);
        thisDiner.Delete();
        return RedirectToAction("Index");
    }
    // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Diner.ClearAll();
    //   return View();
    // }
    // [HttpGet("/items/{id}")]
    // public ActionResult Details(int id)
    // {
    //     Diner item = Diner.Find(id);
    //     return View(item);
    // }
  }
}
