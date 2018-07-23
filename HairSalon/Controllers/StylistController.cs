using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }
    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
  }
}
