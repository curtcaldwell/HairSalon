using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {

    [HttpGet("/specialties")]
    public ActionResult Index()
    {
        List<Specialty> allSpecialties = Specialty.GetAll();
        // return new EmptyResult(); //Test 1 will fail
        // return View(0); //Test 2 will fail
        return View(allSpecialties);  //Test will pass
    }

    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      // return new EmptyResult(); //Test will fail
      return View(); //Test will pass
    }

    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      Specialty newSpecialty = new Specialty (Request.Form["newSpecialty"]);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View("Index", allSpecialties);
    }
    [HttpGet("/specialties/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Specialty thisSpecialty = Specialty.Find(id);
        return View(thisSpecialty);
    }
    [HttpPost("/specialties/{id}/update")]
    public ActionResult Update(int id)
    {
        Specialty thisSpecialty = Specialty.Find(id);
        thisSpecialty.Edit(Request.Form["updateSpecialty"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Specialty thisSpecialty = Specialty.Find(id);
        thisSpecialty.Delete();
        return RedirectToAction("Index");
    }
    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult AddSpecialty(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylistid"]));
      specialty.AddStylist(stylist);
      return RedirectToAction("Details",  new { id = specialtyId });
    }
    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> selectedStylists = selectedSpecialty.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("allStylists", allStylists);
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("selectedStylists", selectedStylists);
      // return new EmptyResult(); //Test 1 will fail
      // return View(0); //Test 2 will fail
      return View(model); //Test will pass
    }
  }
}
