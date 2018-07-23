using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist (Request.Form["newStylist"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }
    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpGet("/categories/{id}")]
    public ActionResult Details(int id)
    {
      Stylist item = Stylist.Find(id);
      return View(item);

    }
  }
}
