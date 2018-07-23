using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Index()
    {
        List<Client> allClients = Client.GetAll();


        return View(allClients);    }

    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
      // return new EmptyResult(); //Test will fail
      return View(); //Test will pass
    }
    [HttpPost("/categories")]
    public ActionResult Create()
    {
      Client newClient = new Client (Request.Form["newClient"]);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }
    [HttpGet("/categories/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Client thisClient = Client.Find(id);
        return View(thisClient);
    }
    [HttpPost("/categories/{id}/update")]
    public ActionResult Update(int id)
    {
        Client thisClient = Client.Find(id);
        thisClient.Edit(Request.Form["newname"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/categories/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Client thisClient = Client.Find(id);
        thisClient.Delete();
        return RedirectToAction("Index");
    }
    [HttpPost("/categories/delete")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return View();
    }
  }
}
