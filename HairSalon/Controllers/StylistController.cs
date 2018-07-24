using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
        List<Stylist> allStylists = Stylist.GetAll();


        return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {

      return View(); 
    }

    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist (Request.Form["newStylist"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }
    [HttpGet("/stylists/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Stylist thisStylist = Stylist.Find(id);
        return View(thisStylist);
    }
    [HttpPost("/stylists/{id}/update")]
    public ActionResult Update(int id)
    {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Edit(Request.Form["updateStylist"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Delete();
        return RedirectToAction("Index");
    }
}
}
